using IT008_UIT.UserControlGym;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IT008_UIT.Utils;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        // mọi thứ xử lý sẽ nằm trong này
        public ICommand HomeCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand StaffCommand { get; set; }
        public ICommand FacilityCommand { get; set; }
        public ICommand StatisticCommand { get; set; }
        public ICommand ContractCommand { get; set; }
        public ICommand CourseCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            HomeCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {

                CurrentView = new HomeViewModel();
            }
            );
            CustomerCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new CustomerViewModel();
            }
            );
            StaffCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new StaffViewModel();
            }
            );
            FacilityCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new FacilityViewModel();
            }
            );
            CourseCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new CourseViewModel();
            }
            );
            ContractCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new ContractViewModel();
            }
            );
            StatisticCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new StatisticViewModel();
            }
            );
            ExitCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p != null)
                {
                    LoginWindow login = new LoginWindow();
                    login.Show();
                    p.Close();
                }
            }
            );
            CurrentView = new HomeViewModel();

        }
    }
}
