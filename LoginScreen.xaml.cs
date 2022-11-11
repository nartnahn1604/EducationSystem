using IT008_UIT.ViewModel;
using System.Windows;

namespace IT008_UIT
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginViewModel Viewmodel { get; set; }
        public LoginScreen()
        {
            InitializeComponent();
            //this.DataContext = Viewmodel = new LoginViewModel(LoginUC.txtEmail.Text, LoginUC.txtPassword.Password);

        }
    }
}
