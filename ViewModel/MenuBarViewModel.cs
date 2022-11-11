using IT008_UIT.UserControlGym;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class MenuBarViewModel : BaseViewModel
    {
        public ICommand HomeCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MenuBarViewModel()
        {
            HomeCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new HomeViewModel();
                Debug.WriteLine("Home");
            }
            );
            CustomerCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new CustomerViewModel();
                Debug.WriteLine("Customer");
            }
            );
            CurrentView = new HomeViewModel();

        }
    }
}
