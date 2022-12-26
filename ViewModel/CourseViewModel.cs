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

namespace IT008_UIT.ViewModel
{
    public class CourseViewModel : BaseViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private Course _selectedCourse { get; set; }
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }
        private GymDbContext Context { get; set; }

        private ObservableCollection<Course> _courseContext { get; set; }

        public ObservableCollection<Course> CourseContext
        {
            get => _courseContext;
            set { _courseContext = value; OnPropertyChanged(); }
        }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Course cou in Context.Courses.ToList())
                    {
                        _courseContext.Add(cou);
                    }
                }
            });
        }
        private async Task Delete(Course delete)
        {
            using (Context = new GymDbContext())
            {
                Context.Remove<Course>(delete);
                Context.SaveChanges();
                _courseContext.Remove(delete);
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
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as AddCourseUC;
            if (view != null)
            {
                var viewmodel = view.DataContext as AddCourseViewModel;
                if (viewmodel != null)
                {
                    await viewmodel.AddNewCourseAsync();
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
                        _courseContext = viewmodel.Sync();

                    }
                    

                    //lets run a fake operation for 3 seconds then close this baby.
                    Task.Delay(TimeSpan.FromSeconds(3))
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                            TaskScheduler.FromCurrentSynchronizationContext());

                }

            }



            

        }


        public override async void AddData()
        {
            if (IsLoading == false)
            {
                var view = new AddCourseUC
                {
                    DataContext = new AddCourseViewModel(_courseContext)
                };
                var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);
                
            }
        }


        public CourseViewModel() 
        {
            _courseContext = new ObservableCollection<Course>();
            Debug.WriteLine("In HopDongViewmodel...");
            BindingOperations.EnableCollectionSynchronization(CourseContext, _lockMutex);
            LoadData();
        }    
    }
}
