using BinnoMetricMaui.ViewModel;
namespace BinnoMetricMaui.View;
public partial class ProductPage : ContentPage
{
	public ProductPage(ProductViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}