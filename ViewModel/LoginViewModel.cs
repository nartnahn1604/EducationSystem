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
            DangNhapCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                MainWindow homescreen = new MainWindow();
                homescreen.Show();
                p.Close();
            }
            );

            QuenMatKhauCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                QuenMatKhauScreen quenmatkhauscreen = new QuenMatKhauScreen();
                quenmatkhauscreen.Show();
                p.Close();
            }
            );

            PasswordVisibleCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }
    }
}
