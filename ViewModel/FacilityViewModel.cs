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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class FacilityViewModel : BaseViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private Facility _selectedFacility { get; set; }
        public Facility SelectedFacility
        {
            get => _selectedFacility;
            set { _selectedFacility = value; OnPropertyChanged(); }
        }
        private GymDbContext Context { get; set; }

        private ObservableCollection<Facility> _facilityContext { get; set; }

        public ObservableCollection<Facility> FacilityContext
        {
            get => _facilityContext;
            set { _facilityContext = value; OnPropertyChanged(); }
        }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Facility fac in Context.Facilities.ToList())
                    {
                        TypesOfFacility type = Context.TypesOfFacilities.Where(s => s.TypeId == fac.TypeId).FirstOrDefault();
                        fac.Type.Name = type.Name;
                        _facilityContext.Add(fac);
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
        private async Task DeleteFacility(Facility delete)
        {
            using (Context = new GymDbContext())
            {
                Context.Remove<Facility>(delete);
                Context.SaveChanges();
                _facilityContext.Remove(delete);
            }
        }
        public override async void AddData()
        {
            if (IsLoading == false)
            {
                using (Context = new GymDbContext())
                {
                    List<string> TypeList = new List<string>();
                    foreach (TypesOfFacility type in Context.TypesOfFacilities.ToList())
                    {
                        if (type != null)
                        {
                            TypeList.Add(type.Name);
                        }
                    }
                    var view = new AddFacilityUC
                    {
                        DataContext = new AddFacilityViewModel(_facilityContext,TypeList)
                    };
                    var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);
                }
            }
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
           => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddFacilityUC;

            if (view != null)
            {
                var viewmodel = view.DataContext as AddFacilityViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewFacilityAsync();
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    //OK, lets cancel the close...
                    eventArgs.Cancel();

                    
                    if (viewmodel.Flag != false)
                    {
                        //...now, lets update the "session" with some new content!
                        eventArgs.Session.UpdateContent(new SampleProgressDialog());
                        //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
                        _facilityContext = viewmodel.Sync();
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

                    Facility fac = SelectedFacility;
                    try
                    {
                        await DeleteFacility(fac);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    //lets run a fake operation for 3 seconds then close this baby.
                    Task.Delay(TimeSpan.FromSeconds(0))
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                            TaskScheduler.FromCurrentSynchronizationContext());
                }

                
            }
        }

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");


        public ICommand GridChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public FacilityViewModel()
        {
            _facilityContext = new ObservableCollection<Facility>();
            Debug.WriteLine("In HopDongViewmodel...");
            BindingOperations.EnableCollectionSynchronization(FacilityContext, _lockMutex);
            LoadData();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Facility replace = SelectedFacility;
                try
                {
                    //Update(replace);
                }
                catch (Exception)
                {
                    throw;
                }
            });
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                var view = new SampleConfirmDialog();
                var result = DialogHost.Show(view, "RootDialog", ExtendedClosingEventHandler);
            }
            );
        }
    }
}
