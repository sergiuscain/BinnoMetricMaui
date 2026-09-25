using BinnoMetricMaui.ViewModel;
namespace BinnoMetricMaui.View;

public partial class ProductionRecordsChartsPage : ContentPage
{
	public ProductionRecordsChartsPage(ProductionRecordsChartsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}