using BinnoMetricMaui.Model;
using System.Text.Json;

namespace BinnoMetricMaui.Service;

public class AnalyticsService
{
    private readonly HttpClient _httpClient;
    public AnalyticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<TopEmployeesForCurrentProduct> GetTopEmployeesByProductAsync(int productId, DateTime? startDate, DateTime? endDate, int? minRecord)
    {
        string url = $"https://localhost:7259/api/Analytics/GetTopEmployee?productId={productId}&minRecord={minRecord}&startDate={startDate}&endDate={endDate}";
        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var topEmployeesByProduct = JsonSerializer.Deserialize<TopEmployeesForCurrentProduct>(response);
            return topEmployeesByProduct;
        }
        catch
        {
            return new TopEmployeesForCurrentProduct();
        }
    }
}
