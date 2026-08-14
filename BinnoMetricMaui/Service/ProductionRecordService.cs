using BinnoMetricMaui.Model;
using System.Text;
using System.Text.Json;

namespace BinnoMetricMaui.Service;
public class ProductionRecordService
{
    private readonly HttpClient _httpClient;
    private readonly EmployeeService _employeeService;
    private readonly ProductService _productService;
    public ProductionRecordService(HttpClient httpClient, EmployeeService employeeService, ProductService productService)
    {
        _httpClient = httpClient;
        _employeeService = employeeService;
        _productService = productService;
    }

    public async Task<List<ProductionRecord>> GetProductionRecordAsync()
    {
        string url = "https://localhost:7259/api/ProductionRecords/GetProductionRecords";

        try
        {
            var products = await _productService.GetProductsAsync();
            var employees = await _employeeService.GetEmployeeAsync();
            var response = await _httpClient.GetStringAsync(url);
            var productionRecords = JsonSerializer.Deserialize<List<ProductionRecord>>(response);
            var productionRecordsWithName = productionRecords.Select(record =>
            {
                record.SeniorOperatorName = employees.FirstOrDefault(e => e.Id == record.SeniorOperatorId)?.FullName;
                record.OperatorDName = employees.FirstOrDefault(e => e.Id == record.OperatorDId)?.FullName;
                record.OperatorNKName = employees.FirstOrDefault(e => e.Id == record.OperatorNKLId)?.FullName;
                record.PackerName = employees.FirstOrDefault(e => e.Id == record.PackerId)?.FullName;
                record.ProductName = products.FirstOrDefault(p => p.Id == record.ProductId)?.Name;
                return record;
            }).ToList();
            return productionRecordsWithName;
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
