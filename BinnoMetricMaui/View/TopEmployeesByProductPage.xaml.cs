using BinnoMetricMaui.ViewModel;
namespace BinnoMetricMaui.View;
public partial class TopEmployeesByProductPage : ContentPage
{
	public TopEmployeesByProductPage(TopEmployeesByProductViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}