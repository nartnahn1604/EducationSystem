using IT008_UIT.UserControlGym;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public ICommand KhachHangCommand { get; set; }
        public ICommand NhanVienCommand { get; set; }
        public ICommand VatTuCommand { get; set; }
        public ICommand ThongKeCommand { get; set; }
        public ICommand HopDongCommand { get; set; }
        public ICommand GoiTapCommand { get; set; }
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
            KhachHangCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new KhachHangViewModel();
            }
            );
            NhanVienCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new NhanVienViewModel();
            }
            );
            VatTuCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new VatTuViewModel();
            }
            );
            GoiTapCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new GoiTapViewModel();
            }
            );
            HopDongCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new HopDongViewModel();
            }
            );
            ThongKeCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CurrentView = new ThongKeViewModel();
            }
            );
            ExitCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p != null)
                {
                    LoginScreen login = new LoginScreen();
                    login.Show();
                    p.Close();
                }
            }
            );
            CurrentView = new HomeViewModel();

        }
    }
}
