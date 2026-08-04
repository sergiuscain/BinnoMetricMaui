using BinnoMetricMaui.ViewModel;

namespace BinnoMetricMaui.View;

public partial class ProductionRecordPage : ContentPage
{
	public ProductionRecordPage(ProductionRecordViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}