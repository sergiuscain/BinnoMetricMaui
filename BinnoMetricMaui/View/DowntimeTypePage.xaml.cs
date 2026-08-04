using BinnoMetricMaui.ViewModel;

namespace BinnoMetricMaui.View;

public partial class DowntimeTypePage : ContentPage
{
	public DowntimeTypePage(DowntimeTypeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}