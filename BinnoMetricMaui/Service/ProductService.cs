using BinnoMetricMaui.Model;
using System.Text;
using System.Text.Json;
namespace BinnoMetricMaui.Service;

public class ProductService
{ 
    private readonly HttpClient _httpClient;
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        string url = "https://localhost:7259/api/Products/GetProducts";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var products = JsonSerializer.Deserialize<List<Product>>(response);
            return products;
        }
        catch
        {
            return new List<Product>();
        }
    }
    public async Task<Product> GetProductAsync(int id)
    {
        string url = $"https://localhost:7259/api/Products/GetProduct?id={id}";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var product = JsonSerializer.Deserialize<Product>(response);
            return product;
        }
        catch
        {
            return null;
        }
    }

    internal async Task<bool> AddProductAsync(Product newProduct)
    {
        string url = "https://localhost:7259/api/Products/AddProduct";

        try
        {
            var json = JsonSerializer.Serialize(newProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    internal async Task<bool> DeleteProductAsync(int id)
    {
        string url = $"https://localhost:7259/api/Products/DeleteProduct?id={id}";
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
