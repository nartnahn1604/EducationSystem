using IT008_UIT.PasswordSecure;
using IT008_UIT.ViewModel;
using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Controls;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl, IHavePassword
    {
        public LoginUC()
        {
            InitializeComponent();
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }

        private void txtPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext)._password = ((PasswordBox)sender).Password; }
        }
    }
}
