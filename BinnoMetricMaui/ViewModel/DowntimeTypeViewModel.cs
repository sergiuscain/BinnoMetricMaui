using CommunityToolkit.Mvvm.ComponentModel;
namespace BinnoMetricMaui.ViewModel;
public partial class DowntimeTypeViewModel : ObservableObject
{
    [ObservableProperty]
    private string message = "mvvm Типы простоев";
}
