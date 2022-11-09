using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;

namespace IT008_UIT.ViewModel
{

    public class PieChartViewModel : BaseViewModel
    {
        public PieChartViewModel()
        {
            // you could convert any IEnumerable to a pie series collection
            var data = new double[] { 2, 4, 1, 4, 5 };

            // Series = data.AsLiveChartsPieSeries(); this could be enough in some cases 
            // but you can customize the series properties using the following overload: 

            Series = data.AsLiveChartsPieSeries((value, series) =>
            {
                // here you can configure the series assigned to each value.
                series.Name = $"Series for value {value}";
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                series.DataLabelsFormatter = p => $"{p.PrimaryValue} / {p.StackedValue!.Total} ({p.StackedValue.Share:P2})";
            });

            // this is an equivalent and more verbose syntax. 
            // Series = new ISeries[]
            // {
            //     new PieSeries<double> { Values = new double[] { 2 }, Name = "Slice 1" },
            //     new PieSeries<double> { Values = new double[] { 4 }, Name = "Slice 2" },
            //     new PieSeries<double> { Values = new double[] { 1 }, Name = "Slice 3" },
            //     new PieSeries<double> { Values = new double[] { 4 }, Name = "Slice 4" },
            //     new PieSeries<double> { Values = new double[] { 3 }, Name = "Slice 5" }
            // };
        }
        public IEnumerable<ISeries> Series { get; set; }
    }
}
