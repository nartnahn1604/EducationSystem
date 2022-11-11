using IT008_UIT.ViewModel;
using System.Windows.Controls;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for MenuBarUC.xaml
    /// </summary>
    public partial class MenuBarUC : UserControl
    {
        public MenuBarViewModel ViewModel { get; set; }
        public MenuBarUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new MenuBarViewModel();
        }

        
    }
}
