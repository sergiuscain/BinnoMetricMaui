using BinnoMetricMaui.ViewModel;

namespace BinnoMetricMaui.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}