using IT008_UIT.ViewModel;
using System.Windows;

namespace IT008_UIT
{
    /// <summary>
    /// Interaction logic for DatLaiMatKhauScreen.xaml
    /// </summary>
    public partial class DoiMatKhauScreen : Window
    {
        public DoiMatKhauViewModel Viewmodel { get; set; }
        public DoiMatKhauScreen()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new DoiMatKhauViewModel();
        }
    }
}
