using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockInwardDetails
    {
        public string StockInwardDetailsSiNo { get; set; }
        public int? StockInwardDetailsSno { get; set; }
        public int? StockInwardDetailsMaterialId { get; set; }
        public int? StockInwardDetailsUnitId { get; set; }
        public double? StockInwardDetailsRate { get; set; }
        public double? StockInwardDetailsQty { get; set; }
        public double? StockInwardDetailsNetAmount { get; set; }
        public double? StockInwardDetailsFcAmount { get; set; }
        public int? StockInwardDetailsFsno { get; set; }
        public string StockInwardDetailsBatch { get; set; }
        public DateTime? StockInwardDetailsExpDate { get; set; }
        public bool? StockInwardDetailsDelStatus { get; set; }
    }
}
