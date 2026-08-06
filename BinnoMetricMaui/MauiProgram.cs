using BinnoMetricMaui.Service;
using BinnoMetricMaui.View;
using BinnoMetricMaui.ViewModel;
using Microsoft.Extensions.Logging;

namespace BinnoMetricMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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

            //Сервисы
            builder.Services.AddSingleton<EmployeeService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<HttpClient>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
