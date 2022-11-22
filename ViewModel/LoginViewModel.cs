using IT008_UIT.PasswordSecure;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLoggedIn { get; set; }
        private string _UserEmail;
        public string UserEmail { get => _UserEmail; set { _UserEmail = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand DangNhapCommand { get; set; }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand PasswordVisibleCommand { get; set; }


        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }


        private void Login(object parameter)
        {
            Debug.WriteLine("email: " + UserEmail);
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                Password = ConvertToUnsecureString(secureString);
                Debug.WriteLine("password: "+Password);
            }
            
        }
        public LoginViewModel()
        {

            IsLoggedIn = false;
            DangNhapCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                Login(p);
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    MainWindow homescreen = new MainWindow();
                    homescreen.Show();
                    w.Close();
                    IsLoggedIn = true;
                }
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
