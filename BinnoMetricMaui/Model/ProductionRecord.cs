using System.Text.Json.Serialization;

namespace BinnoMetricMaui.Model;
public class ProductionRecord
{
    // ID записей
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    [JsonPropertyName("equipmentLineId")]
    public int EquipmentLineId { get; set; }

    // ID сотрудников
    [JsonPropertyName("seniorOperatorId")]
    public int? SeniorOperatorId { get; set; }
    [JsonPropertyName("operatorDId")]
    public int? OperatorDId { get; set; }
    [JsonPropertyName("operatorNKLId")]
    public int? OperatorNKLId { get; set; }
    [JsonPropertyName("packerId")]
    public int? PackerId { get; set; }

    // Фио сотрудников
    public string SeniorOperatorName { get; set; }
    public string OperatorDName { get; set; }
    public string OperatorNKName { get; set; }
    public string PackerName { get; set; }

    // Остальные данные
    [JsonPropertyName("seriesNumber")]
    public string SeriesNumber { get; set; }
    [JsonPropertyName("actualQuantity")]
    public int? ActualQuantity { get; set; }
    [JsonPropertyName("comments")]
    public string Comments { get; set; }
    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }
    public string ProductName { get; set; }
}
