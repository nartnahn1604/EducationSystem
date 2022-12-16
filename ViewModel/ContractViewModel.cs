

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

        private ObservableCollection<Contract> _ContractContext { get; set; }

        public ObservableCollection<Contract> ContractContext
        {
            get => _ContractContext;
            set { _ContractContext = value; OnPropertyChanged(); }
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
                    foreach (Contract cus in Context.Contracts.ToList())
                    {
                        Customer customer_name = Context.Customers.Where(s => s.CustomerId == cus.CustomerId).FirstOrDefault();
                        Course course_name = Context.Courses.Where(s => s.CourseId == cus.CourseId).FirstOrDefault();
                        cus.Customer.Name = customer_name.Name;
                        cus.Course.Name = course_name.Name;
                        _ContractContext.Add(cus);
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
                    //_ContractContext.Clear();
                    //foreach (Contract cus in Contract)
                    //{
                    //    _ContractContext.Add(cus);
                    //}

                    //var stringProperties = typeof(Contract).GetProperties().Where(prop =>
                    //prop.PropertyType == content.GetType());
                    //List<Contract> List = Context.Contracts.Where(Contract => stringProperties.Any(prop =>
                    //((prop.GetValue(Contract, null) == null) ? "" :
                    //prop.GetValue(Contract, null).ToString().ToLower()) == content)).ToList();


                    //_ContractContext.Clear();
                    //if (List != null)
                    //{
                    //    Debug.WriteLine("!= null");
                    //    foreach (Contract cus in List)
                    //    {
                    //        _ContractContext.Add(cus);
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

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
           => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private async Task Add(Contract replace)
        {
            using (Context = new GymDbContext())
            {
                Context.Add<Contract>(replace);
                Context.SaveChanges();
                _ContractContext.Add(replace);
            }
        }
        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;





            var view = eventArgs.Session.Content as AddContractUC;
            if (view != null) 
            {
                var viewmodel = view.DataContext as AddContractViewModel;
                var contract = viewmodel.GetData();
               
            }



            //OK, lets cancel the close...
            eventArgs.Cancel();
            
            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");

        public override async void AddData()
        {
            var view = new AddContractUC
            {
                DataContext = new AddContractViewModel()
            };
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);
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
                        //    //_ContractContext.Remove(current);
                        //    //_ContractContext.Add(replace);
                        //    for (int i = 0; i < _ContractContext.Count; i++)
                        //    {
                        //        if (_ContractContext[i].ContractId == replace.ContractId)
                        //            _ContractContext[i] = replace;
                        //    }
                        //    //_ContractContext.Clear();
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
                _ContractContext.Remove(delete);
            }
        }
        public ContractViewModel()
        {
            _ContractContext = new ObservableCollection<Contract>();
            Debug.WriteLine("In HopDongViewmodel...");
            BindingOperations.EnableCollectionSynchronization(ContractContext, _lockMutex);
            LoadData();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Contract replace = SelectedContract;
                using (Context = new GymDbContext())
                {
                    replace.Customer = Context.Customers.Where(s => s.CustomerId == replace.CustomerId).FirstOrDefault();
                }
                Debug.WriteLine("Course: " + replace.Customer.Name.ToString());
                //try
                //{
                //    Update(replace);
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            });
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                
            }
            );

        }

    }
}
