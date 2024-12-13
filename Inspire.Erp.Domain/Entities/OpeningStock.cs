using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OpeningStock
    {
        public int? OpeningStockStockId { get; set; }
        public string OpeningStockPurchaseId { get; set; }
        public int? OpeningStockSno { get; set; }
        public string OpeningStockBatchCode { get; set; }
        public int? OpeningStockMaterialId { get; set; }
        public double? OpeningStockQty { get; set; }
        public int? OpeningStockCurrencyId { get; set; }
        public double? OpeningStockCRate { get; set; }
        public double? OpeningStockUnitRate { get; set; }
        public double? OpeningStockAmount { get; set; }
        public double? OpeningStockFcAmount { get; set; }
        public string OpeningStockRemakrs { get; set; }
        public int? OpeningStockFsno { get; set; }
        public string OpeningStockPosted { get; set; }
        public int? OpeningStockUnitId { get; set; }
        public int? OpeningStockLocationId { get; set; }
        public int? OpeningStockJobId { get; set; }
        public bool? OpeningStockIsEdit { get; set; }
        public DateTime? OpeningStockExpDate { get; set; }
        public bool? OpeningStockDelStatus { get; set; }
    }
}
