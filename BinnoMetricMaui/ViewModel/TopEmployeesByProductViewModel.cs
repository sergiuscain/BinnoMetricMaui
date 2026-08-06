using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BinnoMetricMaui.ViewModel
{
    public partial class TopEmployeesByProductViewModel : ObservableObject
    {
        private readonly AnalyticsService _analyticsService;

        public TopEmployeesByProductViewModel(int productId, AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
            _ = LoadTopEmployeeByProduct(productId);
        }
        [ObservableProperty]
        private ObservableCollection<EmployeeStat> employeeStat = new ObservableCollection<EmployeeStat>();
        [ObservableProperty]
        private string productName = "";

        private async Task LoadTopEmployeeByProduct(int  productId)
        {
            var top = await _analyticsService.GetTopEmployeesByProductAsync(productId);
            try
            {
                var employeeStatList =  top.EmployeesStat;

                EmployeeStat.Clear();
                foreach (var employeeStat in employeeStatList)
                {
                    EmployeeStat.Add(employeeStat);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
