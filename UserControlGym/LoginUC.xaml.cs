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
    public partial class LoginUC : UserControl
    {
        
        public LoginUC()
        {
            InitializeComponent();
        }

        private void txtPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; 
            }
        }
    }
}
