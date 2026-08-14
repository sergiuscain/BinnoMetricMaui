using CommunityToolkit.Mvvm.ComponentModel;
namespace BinnoMetricMaui.ViewModel;
public partial class ProductionRecordViewModel : ObservableObject
{
    [ObservableProperty]
    private string message = "mvvm записи производства";
}
