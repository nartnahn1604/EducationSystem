using IT008_UIT.ViewModel;
using System.Windows.Controls;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for DynamicChartUC.xaml
    /// </summary>
    public partial class DynamicChartUC : UserControl
    {
        public DynamicChartViewModel Viewmodel { get; set; }
        public DynamicChartUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new DynamicChartViewModel();
        }
    }
}
