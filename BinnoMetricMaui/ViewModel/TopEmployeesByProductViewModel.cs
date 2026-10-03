using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BinnoMetricMaui.ViewModel;

public partial class TopEmployeesByProductViewModel : ObservableObject
{
    private readonly AnalyticsService _analyticsService;

    public TopEmployeesByProductViewModel(int productId, AnalyticsService analyticsService)
    {
        ProductId = productId;
        _analyticsService = analyticsService;
        _ = LoadTopEmployeeByProduct();
    }
    [ObservableProperty]
    private ObservableCollection<EmployeeStat> employeeStat = new ObservableCollection<EmployeeStat>();
    [ObservableProperty]
    private string productNameText = "";

    [ObservableProperty]
    private bool startTimeEnabled = false;
    [ObservableProperty]
    private DateTime startTimeValue = DateTime.Today.AddMonths(-1);

    [ObservableProperty]
    private bool endTimeEnabled = false;
    [ObservableProperty]
    private DateTime endTimeValue = DateTime.Today;

    [ObservableProperty]
    private int minRecord = 0;
    [ObservableProperty]
    private int productId;

    private async Task LoadTopEmployeeByProduct()
    {
        DateTime? startTime = null;
        DateTime? endTime = null;
        int? minRecord = null;
        if (startTimeEnabled)
            startTime = StartTimeValue;
        if (endTimeEnabled)
            endTime = EndTimeValue;
        if (MinRecord > 0)
            minRecord = MinRecord;
        else
            minRecord = null;
        var top = await _analyticsService.GetTopEmployeesByProductAsync(ProductId, startTime, endTime, minRecord);
        ProductNameText = $"Продукт: {top.ProductName}";
        try
        {
            var employeeStatList = top.EmployeesStat;

            if (employeeStatList != null)
            {
                EmployeeStat.Clear();
                foreach (var employeeStat in employeeStatList)
                {
                    EmployeeStat.Add(employeeStat);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    [RelayCommand]
    public async Task ApplyFilter()
    {
        await LoadTopEmployeeByProduct();
    }
}