using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class PtcourseViewModel : BaseViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private Ptcourse _selectedPtcourse { get; set; }
        public Ptcourse SelectedPtcourse
        {
            get => _selectedPtcourse;
            set { _selectedPtcourse = value; OnPropertyChanged(); }
        }
        private GymDbContext Context { get; set; }

        private ObservableCollection<Ptcourse> _ptcourseContext { get; set; }

        public ObservableCollection<Ptcourse> PtcourseContext
        {
            get => _ptcourseContext;
            set { _ptcourseContext = value; OnPropertyChanged(); }
        }

        public ICommand DeleteCommand { get;  set; }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Ptcourse cou in Context.Ptcourses.ToList())
                    {
                        _ptcourseContext.Add(cou);
                    }
                }
            });
        }
        private bool DeleteFlag;
        private async Task DeletePtCourse(Ptcourse delete)
        {
            using (Context = new GymDbContext())
            {
                var v = Context.Ptcontracts.Where(s => s.PtcourseId == delete.PtcourseId).FirstOrDefault();
                if (v == null)
                {
                    DeleteFlag = true;
                    Context.Remove<Ptcourse>(delete);
                    Context.SaveChanges();
                    _ptcourseContext.Remove(delete);
                }
                else
                {
                    DeleteFlag = false;
                }
            }
        }
        public async Task LoadData()
        {
            IsLoading = true;
            _ = LoadDataAsync();
            await Task.Delay(3000);
            IsLoading = false;
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
           => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddPtcourseUC;
            if (view != null)
            {

                var viewmodel = view.DataContext as AddPtcourseViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewPtcourseAsync();
                    bool flag = viewmodel.Flag;
                    if (flag  == false)
                    {
                        eventArgs.Cancel();
                        await ShowErrorDialog();
                    }

                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
                    else if (flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        _ptcourseContext = viewmodel.Sync();
                        //lets run a fake operation for 3 seconds then close this baby.
                        Task.Delay(TimeSpan.FromSeconds(3))
                            .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                                TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
            }

            else
            {
                var view1 = eventArgs.Session.Content as SampleConfirmDialog;
                if (view1 != null)
                {
                    eventArgs.Cancel();
                   
                    Ptcourse pt = SelectedPtcourse;
                    try
                    {
                        await DeletePtCourse(pt);
                    } 
                    catch (Exception ex) 
                    {
                        Debug.WriteLine(ex);
                    }
                    if (DeleteFlag == false)
                    {
                        eventArgs.Session.UpdateContent(new SampleErrorDialog());
                    }
                    else
                    {
                        Task.Delay(TimeSpan.FromSeconds(0))
                           .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                               TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
            }  
        }


        public override async void AddData()
        {
            if (IsLoading == false)
            {
                var view = new AddPtcourseUC
                {
                    DataContext = new AddPtcourseViewModel(_ptcourseContext)
                };
                var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);

            }
        }


        public PtcourseViewModel()
        {
            _ptcourseContext = new ObservableCollection<Ptcourse>();
            BindingOperations.EnableCollectionSynchronization(PtcourseContext, _lockMutex);
            LoadData();
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                var view = new SampleConfirmDialog();
                var result = DialogHost.Show(view, "RootDialog", ExtendedClosingEventHandler);
            }
           );
        }
        
    }

}
