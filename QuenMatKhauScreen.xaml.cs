using IT008_UIT.ViewModel;
using System.Windows;

namespace IT008_UIT
{
    /// <summary>
    /// Interaction logic for QuenMatKhauScreen.xaml
    /// </summary>
    public partial class QuenMatKhauScreen : Window
    {
        public QuenMatKhauViewModel Viewmodel { get; set; }
        public QuenMatKhauScreen()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new QuenMatKhauViewModel();
        }
    }
}
