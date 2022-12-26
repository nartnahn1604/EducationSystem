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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }
        private Staff _selectedStaff { get; set; }
        public Staff SelectedStaff
        {
            get => _selectedStaff;
            set { _selectedStaff = value; OnPropertyChanged(); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private ObservableCollection<Staff> _staffContext { get; set; }

        public ObservableCollection<Staff> StaffContext
        {
            get => _staffContext;
            set { _staffContext = value; OnPropertyChanged(); }
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
                    foreach (Staff sta in Context.Staffs.ToList())
                    {
                        Role role = Context.Roles.Where(s => s.RoleId == sta.RoleId).FirstOrDefault();
                        sta.Role = role;
                        _staffContext.Add(sta);
                    }
                }
            });
        }
        public async Task LoadData()
        {
            IsLoading = true;
            _ = LoadDataAsync();
            await Task.Delay(3500);
            IsLoading = false;
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {

        }

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddStaffUC;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddStaffViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewStaffAsync();
                    if (viewmodel.Flag == false)
                    {
                        await ShowErrorDialog();
                    }
                    //OK, lets cancel the close...
                    eventArgs.Cancel();

                    //...now, lets update the "session" with some new content!
                    eventArgs.Session.UpdateContent(new SampleProgressDialog());
                    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

                    if (viewmodel.Flag != false)
                    {
                        _staffContext = viewmodel.Sync();
                    }
                    

                    //lets run a fake operation for 3 seconds then close this baby.
                    Task.Delay(TimeSpan.FromSeconds(3))
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                            TaskScheduler.FromCurrentSynchronizationContext());

                }

            }



            
        }

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");

        public override async void AddData()
        {
            if (IsLoading == false)
            {
                using (Context = new GymDbContext())
                {
                    List<string> RoleList = new List<string>();
                    foreach (Role role in Context.Roles.ToList())
                    {
                        if (role != null)
                        {
                            RoleList.Add(role.Name);
                        }
                    }
                    var view = new AddStaffUC
                    {
                        DataContext = new AddStaffViewModel(_staffContext,RoleList)
                    };
                    var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);
                }
            }
        }

        public StaffViewModel()
        {
            _staffContext = new ObservableCollection<Staff>();
            BindingOperations.EnableCollectionSynchronization(StaffContext, _lockMutex);
            LoadData();
        }


    }
}
