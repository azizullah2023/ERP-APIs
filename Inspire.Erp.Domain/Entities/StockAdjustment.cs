using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockAdjustment
    {
        public int? StockAdjustmentMaterialId { get; set; }
        public DateTime? StockAdjustmentAdjDate { get; set; }
        public double? StockAdjustmentManuelQty { get; set; }
        public double? StockAdjustmentAdjQty { get; set; }
        public string StockAdjustmentRemarks { get; set; }
        public int? StockAdjustmentSano { get; set; }
        public string StockAdjustmentStatus { get; set; }
        public DateTime? StockAdjustmentSdate { get; set; }
        public int? StockAdjustmentLocationId { get; set; }
        public int? StockAdjustmentFsno { get; set; }
        public bool? StockAdjustmentDelStatus { get; set; }
    }
}
