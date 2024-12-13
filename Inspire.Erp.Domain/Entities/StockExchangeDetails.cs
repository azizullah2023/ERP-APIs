using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockExchangeDetails
    {
        public string StockExchangeDetailsSeNo { get; set; }
        public int? StockExchangeDetailsSno { get; set; }
        public int? StockExchangeDetailsMaterialId { get; set; }
        public int? StockExchangeDetailsUnitId { get; set; }
        public double? StockExchangeDetailsRate { get; set; }
        public double? StockExchangeDetailsQty { get; set; }
        public double? StockExchangeDetailsNetAmount { get; set; }
        public int? StockExchangeDetailsFsno { get; set; }
        public string StockExchangeDetailsBatch { get; set; }
        public DateTime? StockExchangeDetailsExpDate { get; set; }
        public string StockExchangeDetailsTransType { get; set; }
        public bool? StockExchangeDetailsDelStatus { get; set; }
    }
}
