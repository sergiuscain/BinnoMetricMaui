using CommunityToolkit.Mvvm.ComponentModel;
namespace BinnoMetricMaui.ViewModel;
public partial class DowntimeViewModel : ObservableObject
{
    [ObservableProperty]
    private string message = "mvvm Простои";
}
