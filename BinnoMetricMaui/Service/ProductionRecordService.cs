using BinnoMetricMaui.Model;
using System.Net.Http.Json;
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

    public async Task<List<ProductionRecord>> GetProductionRecordsAsync(ProductionRecordFilter filter)
    {
        string url = "https://localhost:7259/api/ProductionRecords/GetProductionRecords";

        try
        {
            var products = await _productService.GetProductsAsync();
            var employees = await _employeeService.GetEmployeesAsync();

            var response = await _httpClient.PostAsJsonAsync(url, filter);
            response.EnsureSuccessStatusCode();

            var productionRecords = await response.Content.ReadFromJsonAsync<List<ProductionRecord>>();

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
    public async Task<ProductionRecord> GetProductionRecordAsync(int id)
    {
        string url = $"https://localhost:7259/api/ProductionRecords/GetProductionRecord?id={id}";
        try
        {
            var response = await _httpClient.GetStringAsync(url);
            ProductionRecord record = JsonSerializer.Deserialize<ProductionRecord>(response);

            var product = await _productService.GetProductAsync(record.ProductId);
            var seniorOperator = (await _employeeService.GetEmployeeAsync(record.SeniorOperatorId));
            var operatorD = (await _employeeService.GetEmployeeAsync(record.OperatorDId));
            var operatorNK = (await _employeeService.GetEmployeeAsync(record.OperatorNKLId));
            var packer = (await _employeeService.GetEmployeeAsync(record.PackerId));

            record.ProductName = product != null ? product.Name : "null";
            record.SeniorOperatorName = seniorOperator != null? seniorOperator.FullName : "null";
            record.OperatorDName = operatorD != null ? operatorD.FullName : "null";
            record.OperatorNKName = operatorNK != null ? operatorNK.FullName : "null";
            record.PackerName = packer != null ? packer.FullName : "null";
            return record;
        }
        catch
        {
            return null;
        }
    }

    public async Task<int> GetPageCountAsync(ProductionRecordFilter filter)
    {
        string url = "https://localhost:7259/api/ProductionRecords/GetPageCount";

        var response = await _httpClient.PostAsJsonAsync(url, filter);
        response.EnsureSuccessStatusCode();

        var pageCount = await response.Content.ReadFromJsonAsync<int>();
        return pageCount;
    }

    public async Task<bool> AddProductionRecordAsync(ProductionRecord newProductionRecord)
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

    public async Task<bool> DeleteProductionRecordAsync(int id)
    {
        string url = $"https://localhost:7259/api/ProductionRecords/DeleteProductionRecord?id={id}";
        try
        {
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<bool>(json);
                return result;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}