using System;
using System.Collections.Generic;
using System.Text;

namespace BinnoMetricMaui.Model
{
    internal class ProductionRecord
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int ProductId { get; set; }
        public int EquipmentLineId { get; set; }
        public string SeriesNumber { get; set; }
        public int? ActualQuantity { get; set; }
        public string Comments { get; set; }

        // ID сотрудников
        public int? SeniorOperatorId { get; set; }
        public int? OperatorDId { get; set; }
        public int? OperatorNKLId { get; set; }
        public int? PackerId { get; set; }
    }
}
