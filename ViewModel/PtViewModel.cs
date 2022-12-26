using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using IT008_UIT.Utils;
using LiveChartsCore.Geo;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IT008_UIT.ViewModel
{
    public class PtViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }
        private Pt _selectedPt { get; set; }
        public Pt SelectedPt
        {
            get => _selectedPt;
            set { _selectedPt = value; OnPropertyChanged(); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private ObservableCollection<Pt> _PtContext { get; set; }

        public ObservableCollection<Pt> PtContext
        {
            get => _PtContext;
            set { _PtContext = value; OnPropertyChanged(); }
        }
        public ICommand BookCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }
        public ICommand ViewCommand { get; set; }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Pt Pt in Context.Pts.Include(s => s.Ptcontracts).ToList())
                    {
                        _PtContext.Add(Pt);
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
        private Task SearchDataAsync(String content)
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    var Pt = Context.Pts.Where(s => s.Name.Contains(content)
                        || s.IdentityNumber.Contains(content) || s.Gender.Contains(content) ||
                        s.Phone.Contains(content) || s.Email.Contains(content) ||
                        s.Address == content || s.Birthday.ToString().Contains(content)).ToList();

                    _PtContext.Clear();

                    foreach (Pt cus in Pt)
                    {
                        Debug.WriteLine(cus.Birthday.ToString());
                        _PtContext.Add(cus);
                    }

                    //var stringProperties = typeof(Pt).GetProperties().Where(prop =>
                    //    prop.PropertyType == content.GetType());
                    //var Pt = Context.Pts.Where(Pt => 
                    //    stringProperties.Any(prop => 
                    //        prop.GetValue(Pt, null) == content)).ToList();
                    //_PtContext.Clear();
                    //foreach (Pt cus in Pt)
                    //{
                    //    _PtContext.Add(cus);
                    //}
                    //List<Pt> List = Context.Pts.Where(Pt => stringProperties.Any(prop =>
                    //((prop.GetValue(Pt, null) == null) ? "" :
                    //prop.GetValue(Pt, null).ToString().ToLower()) == content)).ToList();


                    //_PtContext.Clear();
                    //if (List != null)
                    //{
                    //    Debug.WriteLine("!= null");
                    //    foreach (Pt cus in List)
                    //    {
                    //        _PtContext.Add(cus);
                    //    }
                    //}
                }
            });
        }
       

        private async Task Delete(Pt delete)
        {
            using (Context = new GymDbContext())
            {
                Context.Remove<Pt>(delete);
                Context.SaveChanges();
                _PtContext.Remove(delete);
            }
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
          => Debug.WriteLine("You could intercePt the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercePt the closed event here (2).");

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercePt the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            Debug.WriteLine("sender: " + sender + "\nevent:" + eventArgs.Session.Content);
            
            
            var view = eventArgs.Session.Content as BookingUC;
            if (view != null)
            {
                var viewmodel = view.DataContext as BookingViewModel;
                if (viewmodel != null)
                {
                    viewmodel.AddNewBooking();
                }

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


        public PtViewModel()
        {
            _PtContext = new ObservableCollection<Pt>();
            Debug.WriteLine("In Viewmodel...");
            BindingOperations.EnableCollectionSynchronization(PtContext, _lockMutex);
            LoadData();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Pt replace = SelectedPt;
                try
                {
                    //Update(replace);
                }
                catch (Exception)
                {
                    throw;
                }
            });
            ViewCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, async (p) =>
            {
                Pt pt = SelectedPt;
                var view = new ViewPtDetailInfoUC
                {
                    DataContext = new ViewPtDetailInfoViewModel(pt)
                };
                var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);

            }
            );
            BookCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, async (p) =>
            {
                Pt pt = SelectedPt;
                var view = new BookingUC
                {
                    DataContext = new BookingViewModel(pt)
                };
                var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);

            }
           );

        }


    }
}

