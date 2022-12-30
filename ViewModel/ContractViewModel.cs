

using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Drawing;

namespace IT008_UIT.ViewModel
{
    public class ContractViewModel : BaseViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private Contract _selectedContract { get; set; }
        public Contract SelectedContract
        {
            get => _selectedContract;
            set { _selectedContract = value; OnPropertyChanged(); }
        }
        private GymDbContext Context { get; set; }

        private ObservableCollection<Contract> _contractContext { get; set; }

        public ObservableCollection<Contract> ContractContext
        {
            get => _contractContext;
            set { _contractContext = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Ptcontract> _ptcontractContext { get; set; }

        public ObservableCollection<Ptcontract> PtContractContext
        {
            get => _ptcontractContext;
            set { _ptcontractContext = value; OnPropertyChanged(); }
        }

        public ICommand DeleteCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Contract con in Context.Contracts.ToList())
                    {
                        Customer customer_name = Context.Customers.Where(s => s.CustomerId == con.CustomerId).FirstOrDefault();
                        Course course_name = Context.Courses.Where(s => s.CourseId == con.CourseId).FirstOrDefault();
                        con.Customer = customer_name;
                        con.Course = course_name;
                        _contractContext.Add(con);
                    }
                    foreach(Ptcontract con in Context.Ptcontracts.ToList())
                    {
                        Customer customer_name = Context.Customers.Where(s => s.CustomerId == con.CustomerId).FirstOrDefault();
                        Ptcourse course_name = Context.Ptcourses.Where(s => s.PtcourseId == con.PtcourseId).FirstOrDefault();
                        con.Customer = customer_name;
                        con.Ptcourse = course_name;
                        _ptcontractContext.Add(con);
                    }
                    
                }
            });
        }
        public async Task LoadData()
        {
            IsLoading = true;
            _ = LoadDataAsync();
            await Task.Delay(3000);
            IsLoading = false;
        }
        private Task SearchDataAsync(String content)
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    //var Contract = Context.Contracts.Where(s => s.Name == content
                    //    || s.IdentityNumber == content).ToList();
                    //_contractContext.Clear();
                    //foreach (Contract cus in Contract)
                    //{
                    //    _contractContext.Add(cus);
                    //}

                    //var stringProperties = typeof(Contract).GetProperties().Where(prop =>
                    //prop.PropertyType == content.GetType());
                    //List<Contract> List = Context.Contracts.Where(Contract => stringProperties.Any(prop =>
                    //((prop.GetValue(Contract, null) == null) ? "" :
                    //prop.GetValue(Contract, null).ToString().ToLower()) == content)).ToList();


                    //_contractContext.Clear();
                    //if (List != null)
                    //{
                    //    Debug.WriteLine("!= null");
                    //    foreach (Contract cus in List)
                    //    {
                    //        _contractContext.Add(cus);
                    //    }
                    //}
                }
            });
        }
        public override void SearchData(String content)
        {
            _ = SearchDataAsync(content);
            Debug.WriteLine($"In search... for {content}");
        }

        
        private async Task Add(Contract replace)
        {
            using (Context = new GymDbContext())
            {
                Context.Add<Contract>(replace);
                Context.SaveChanges();
                _contractContext.Add(replace);
            }
        }
        private  void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
           => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddContractUC;
            if (view != null) 
            {
                var viewmodel = view.DataContext as AddContractViewModel;
                if(viewmodel != null)
                {
                   
                    await viewmodel.AddNewContractAsync();
                    if(viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    eventArgs.Cancel();

                    
                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _contractContext = viewmodel.NormalSync();
                    }

                    //lets run a fake operation for 3 seconds then close this baby.
                    Task.Delay(TimeSpan.FromSeconds(3))
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                            TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            //OK, lets cancel the close...
            
        }


        

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");

        private async Task<List<string>> LoadCourseName()
        {
            List<string> courseList = new List<string>();
            foreach (Course cor in Context.Courses.ToList())
            {
                if (cor != null)
                {
                    courseList.Add(cor.Name);
                }
            }
            return courseList;
        }

        private async Task<List<string>> LoadPtCourse()
        {
            List<string> ptCourseList = new List<string>();
            foreach (Ptcourse cor in Context.Ptcourses.ToList())
            {
                if (cor != null)
                {
                    ptCourseList.Add(cor.Name);
                }
            }
            return ptCourseList;
        }
        private async Task<List<string>> LoadPt()
        {
            List<string> ptCourseList = new List<string>();
            foreach (Pt pt in Context.Pts.ToList())
            {
                if (pt != null)
                {
                    ptCourseList.Add(pt.Name);
                }
            }
            return ptCourseList;
        }

        public override async void AddData()
        {
            if(IsLoading == false)
            {
                using (Context = new GymDbContext())
                {
                    Task<List<string>> getCoursename = LoadCourseName();
                    Task<List<string>> getPtCoursename = LoadPtCourse();
                    Task<List<string>> getPtname = LoadPt();
                    List<string> courseList = await getCoursename;
                    List<string> ptcourseList = await getPtCoursename;
                    List<string> ptList = await getPtname;

                    
                    var view = new AddContractUC
                    {
                        DataContext = new AddContractViewModel(_contractContext, _ptcontractContext, courseList, ptcourseList, ptList)
                    };
                    var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);
                }
            }
        }

        private async Task Update(Contract replace)
        {
            using (Context = new GymDbContext())
            {
                try
                {
                    Contract current = Context.Contracts.Where(t => t.ContractId.Equals(replace.ContractId)).FirstOrDefault();
                    if (current != null)
                    {
                        
                        //if (!(current.Name == replace.Name &&
                        //    current.IdentityNumber == replace.IdentityNumber &&
                        //    current.Gender == replace.Gender &&
                        //    current.Phone == replace.Phone &&
                        //    current.Email == replace.Email &&
                        //    current.Birthday == replace.Birthday &&
                        //    current.Address == replace.Address &&
                        //    current.Active == replace.Active))
                        //{
                        //    current.Name = replace.Name;
                        //    current.IdentityNumber = replace.IdentityNumber;
                        //    current.Gender = replace.Gender;
                        //    current.Phone = replace.Phone;
                        //    current.Email = replace.Email;
                        //    current.Birthday = replace.Birthday;
                        //    current.Address = replace.Address;
                        //    current.Active = replace.Active;
                        //    Context.SaveChanges();
                        //    //_contractContext.Remove(current);
                        //    //_contractContext.Add(replace);
                        //    for (int i = 0; i < _contractContext.Count; i++)
                        //    {
                        //        if (_contractContext[i].ContractId == replace.ContractId)
                        //            _contractContext[i] = replace;
                        //    }
                        //    //_contractContext.Clear();
                        //    //await LoadData(Flag);
                        //}
                    }
                    else await Add(replace);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private async Task Delete(Contract delete)
        {
            using (Context = new GymDbContext())
            {
                Context.Remove<Contract>(delete);
                Context.SaveChanges();
                _contractContext.Remove(delete);
            }
        }
        public ContractViewModel()
        {
            _contractContext = new ObservableCollection<Contract>();
            _ptcontractContext = new ObservableCollection<Ptcontract>();
            Debug.WriteLine("In HopDongViewmodel...");
            BindingOperations.EnableCollectionSynchronization(ContractContext, _lockMutex);
            BindingOperations.EnableCollectionSynchronization(PtContractContext, _lockMutex);
            LoadData();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Contract replace = SelectedContract;
                try
                {
                    Update(replace);
                }
                catch (Exception)
                {
                    throw;
                }
            });
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                
            }
            );

        }

    }
}
