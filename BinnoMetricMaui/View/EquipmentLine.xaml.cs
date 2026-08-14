using BinnoMetricMaui.ViewModel;
namespace BinnoMetricMaui.View;
public partial class EquipmentLine : ContentPage
{
	public EquipmentLine(EquipmentLineViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}