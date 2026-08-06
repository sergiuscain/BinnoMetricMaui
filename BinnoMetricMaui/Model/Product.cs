using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BinnoMetricMaui.Model
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
