using CommunityToolkit.Mvvm.ComponentModel;
namespace BinnoMetricMaui.ViewModel;
public partial class EquipmentLineViewModel : ObservableObject
{
    [ObservableProperty]
    private string message = "mvvm линии";
}