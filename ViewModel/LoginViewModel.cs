using IT008_UIT.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string _password { get; set; }
        //public string Password
        //{
        //    get
        //    {
        //        return _password;
        //    }
        //    set
        //    {
        //        _password = value;
        //        OnPropertyChanged(nameof(Password));
        //    }
        //}
        public ICommand DangNhapCommand { get; set; }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand PasswordVisibleCommand { get; set; }
        public LoginViewModel()
        {
            DangNhapCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, async (p) =>
            {
                //Debug.WriteLine(this.Email + " " + this._password);
                var isAdmin = await FirebaseHelper.loginWithEmailAndPasswordAsync(_email, _password);
                //Debug.WriteLine(isAdmin);
                if (isAdmin)
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
                else
                    MessageBox.Show("Login Failed! Check your email and password!");
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
