using BinnoMetricMaui.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BinnoMetricMaui.ViewModel;

public partial class ProductionRecordCardViewModel : ObservableObject
{
    [ObservableProperty]
    private ProductionRecord card;

    public ProductionRecordCardViewModel(ProductionRecord card)
    {
        Card = card;
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}