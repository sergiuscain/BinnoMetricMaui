using BinnoMetricMaui.ViewModel;

namespace BinnoMetricMaui.View;

public partial class ProductionRecordCardPage : ContentPage
{
    public ProductionRecordCardPage(ProductionRecordCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}