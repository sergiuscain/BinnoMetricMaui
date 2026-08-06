using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BinnoMetricMaui.Model
{
    public class EmployeeStat
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("shiftsCount")]
        public int ShiftsCount { get; set; }
        [JsonPropertyName("totalProductsCount")]
        public int? TotalProductsCount { get; set; }
        [JsonPropertyName("averageProductsCount")]
        public int? AverageProductsCount { get; set; }
    }
}
