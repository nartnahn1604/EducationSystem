using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand DangNhapCommand { get; set; }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand PasswordVisibleCommand { get; set; }
        public LoginViewModel()
        {
            DangNhapCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    MainWindow homescreen = new MainWindow();
                    homescreen.Show();
                    w.Close();
                }
            }
            );

            QuenMatKhauCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );

            PasswordVisibleCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
