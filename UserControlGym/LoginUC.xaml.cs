using IT008_UIT.ViewModel;
using System.Windows.Controls;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl
    {
        public LoginViewModel Viewmodel { get; set; }
        public LoginUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new LoginViewModel();
        }
    }
}
