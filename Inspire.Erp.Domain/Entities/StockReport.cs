using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockReport
    {
        public int? StockReportItemId { get; set; }
        public string StockReportPartNo { get; set; }
        public string StockReportItemName { get; set; }
        public string StockReportBrand { get; set; }
        public string StockReportUnit { get; set; }
        public double? StockReportStock { get; set; }
        public double? StockReportAvgRate { get; set; }
        public double? StockReportStockValue { get; set; }
        public string StockReportRelativeNo { get; set; }
        public bool? StockReportDelStatus { get; set; }
    }
}
