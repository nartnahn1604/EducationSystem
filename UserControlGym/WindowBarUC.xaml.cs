using IT008_UIT.ViewModel;
using System.Windows.Controls;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for CloseWindowButton.xaml
    /// </summary>
    public partial class WindowBarUC : UserControl
    {
        public WindowBarViewModel Viewmodel { get; set; }

        public WindowBarUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new WindowBarViewModel();
        }
    }
}
