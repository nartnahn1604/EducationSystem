using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class DynamicChartViewModel : BaseViewModel
    {
        #region commands
        public ICommand ToggleSeries0Command { get; set; }
        public ICommand ToggleSeries1Command { get; set; }
        public ICommand ToggleSeries2Command { get; set; }
        #endregion

        public DynamicChartViewModel()
        {
            ToggleSeries0Command = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                Series[0].IsVisible = !Series[0].IsVisible;
            }
            );

            ToggleSeries1Command = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                Series[1].IsVisible = !Series[1].IsVisible;
            }
            );

            ToggleSeries2Command = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                Series[2].IsVisible = !Series[2].IsVisible;
            }
            );


        }
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<double>
            {
                Values = new ObservableCollection<double> { 2, 5, 4, 3 },
                IsVisible = true
            },
            new ColumnSeries<double>
            {
                Values = new ObservableCollection<double> { 6, 3, 2, 8 },
                IsVisible = true
            },
            new ColumnSeries<double>
            {
                Values = new ObservableCollection<double> { 4, 2, 8, 7 },
                IsVisible = true
            }
        };


    }
}
