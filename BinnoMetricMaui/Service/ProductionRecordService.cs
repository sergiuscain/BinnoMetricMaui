using BinnoMetricMaui.Model;
using System.Text;
using System.Text.Json;

namespace BinnoMetricMaui.Service;
public class ProductionRecordService
{
    private readonly HttpClient _httpClient;
    public ProductionRecordService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductionRecord>> GetProductionRecordAsync()
    {
        string url = "https://localhost:7259/api/ProductionRecords/GetProductionRecords";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var productionRecords = JsonSerializer.Deserialize<List<ProductionRecord>>(response);
            return productionRecords;
        }
        catch
        {
            return new List<ProductionRecord>();
        }
    }

    internal async Task<bool> AddProductionRecordAsync(ProductionRecord newProductionRecord)
    {
        string url = "https://localhost:7259/api/ProductionRecords/AddProductionRecord";

        try
        {
            var json = JsonSerializer.Serialize(newProductionRecord);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    internal async Task<bool> DeleteProductionRecordAsync(int id)
    {
        string url = $"https://localhost:7259/api/ProductionRecords/DeleteProductionRecord?id={id}";
        try
        {
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Читаем содержимое ответа
                var json = await response.Content.ReadAsStringAsync();

                // Парсим JSON в bool
                var result = JsonSerializer.Deserialize<bool>(json);

                return result; // true или false
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

}
