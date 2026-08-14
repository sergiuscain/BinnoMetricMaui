using System.Text.Json.Serialization;
namespace BinnoMetricMaui.Model;
public class TopEmployeesForCurrentProduct
{
    [JsonPropertyName("productName")]
    public string ProductName { get; set; }
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    [JsonPropertyName("employeesStat")]
    public List<EmployeeStat> EmployeesStat { get; set; }
}