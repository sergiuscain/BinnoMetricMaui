using BinnoMetricMaui.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace BinnoMetricMaui.ViewModel;

public partial class ProductionRecordsChartsViewModel : ObservableObject
{
    public ISeries[] Series { get; set; }
    public Axis[] XAxes { get; set; }

    public ProductionRecordsChartsViewModel(IEnumerable<ProductionRecord> records)
    {
        var points = records
            .GroupBy(r => r.StartTime.Date)
            .OrderBy(g => g.Key)
            .Select(g => new DateTimePoint(g.Key, g.Sum(r => r.ActualQuantity)))
            .ToArray();

        Series = new ISeries[]
        {
            new LineSeries<DateTimePoint>
            {
                Values = points,
                Name = "Выпуск за день",
                GeometrySize = 8,
            }
        };

        XAxes = new[]
        {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd.MM.yyyy"))
        };
    }
}