using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockStatement
    {
        public int? StockStatementId { get; set; }
        public int? StockStatementMaterialId { get; set; }
        public DateTime? StockStatementDateAdjust { get; set; }
        public string StockStatementMaterialName { get; set; }
        public double? StockStatementQuantity { get; set; }
        public double? StockStatementAvgPrice { get; set; }
        public double? StockStatementStockValue { get; set; }
        public string StockStatementDepCode { get; set; }
        public bool? StockStatementDelStatus { get; set; }
    }
}
