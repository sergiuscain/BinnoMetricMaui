using BinnoMetricMaui.Service;
using BinnoMetricMaui.View;
using BinnoMetricMaui.ViewModel;
using LiveChartsCore.SkiaSharpView.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BinnoMetricMaui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .UseLiveCharts()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //Вкладки
        builder.Services.AddSingleton<ProductPage>();
        builder.Services.AddSingleton<ProductViewModel>();
        builder.Services.AddSingleton<EmployeePage>();
        builder.Services.AddSingleton<EmployeeViewModel>();
        builder.Services.AddSingleton<DowntimePage>();
        builder.Services.AddSingleton<DowntimeViewModel>();
        builder.Services.AddSingleton<DowntimeTypePage>();
        builder.Services.AddSingleton<DowntimeTypeViewModel>();
        builder.Services.AddSingleton<EquipmentLine>();
        builder.Services.AddSingleton<EquipmentLineViewModel>();
        builder.Services.AddSingleton<ProductionRecordPage>();
        builder.Services.AddSingleton<ProductionRecordViewModel>();
        builder.Services.AddSingleton<ProductionRecordCardPage>();
        builder.Services.AddSingleton<ProductionRecordCardViewModel>();
        builder.Services.AddSingleton<ProductionRecordsChartsPage>();
        builder.Services.AddSingleton<ProductionRecordsChartsViewModel>();

        //Сервисы
        builder.Services.AddSingleton<EmployeeService>();
        builder.Services.AddSingleton<AnalyticsService>();
        builder.Services.AddSingleton<ProductService>();
        builder.Services.AddSingleton<ProductionRecordService>();
        builder.Services.AddSingleton<HttpClient>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
