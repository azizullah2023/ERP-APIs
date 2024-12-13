using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OpStock
    {
        public int OpStockSlNo { get; set; }
        public string OpStockMaterialCode { get; set; }
        public string OpStockMaterialName { get; set; }
        public double? OpStockWprice { get; set; }
        public double? OpStockRprice { get; set; }
        public DateTime? OpStockExpDate { get; set; }
        public string OpStockBatch { get; set; }
        public double? OpStockCost { get; set; }
        public double? OpStockPurchasePrice { get; set; }
        public double? OpStockStock { get; set; }
        public int? OpStockItemId { get; set; }
        public int? OpStockUnitId { get; set; }
        public bool? OpStockDelStatus { get; set; }
    }
}
