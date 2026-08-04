using BinnoMetricMaui.ViewModel;

namespace BinnoMetricMaui.View;

public partial class EmployeePage : ContentPage
{
	public EmployeePage(EmployeeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}