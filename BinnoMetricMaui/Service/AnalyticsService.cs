using BinnoMetricMaui.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BinnoMetricMaui.Service
{
    public class AnalyticsService
    {
        private readonly HttpClient _httpClient;
        public AnalyticsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TopEmployeesForCurrentProduct> GetTopEmployeesByProductAsync(int productId)
        {
            string url = $"https://localhost:7259/api/Analytics/GetTopEmployee?productId={productId}";

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
}
