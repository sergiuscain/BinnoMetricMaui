using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BinnoMetricMaui.Model
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName ("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;
    }
}
