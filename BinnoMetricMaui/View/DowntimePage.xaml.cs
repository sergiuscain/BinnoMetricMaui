using BinnoMetricMaui.ViewModel;
namespace BinnoMetricMaui.View;
public partial class DowntimePage : ContentPage
{
	public DowntimePage(DowntimeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}