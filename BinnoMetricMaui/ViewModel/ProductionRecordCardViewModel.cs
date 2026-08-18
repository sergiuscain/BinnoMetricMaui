using BinnoMetricMaui.Model;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BinnoMetricMaui.ViewModel;

public partial class ProductionRecordCardViewModel : ObservableObject
{
    private readonly ProductionRecord _card;
    public ProductionRecordCardViewModel(ProductionRecord card)
    {
        _card = card;
    }
}
