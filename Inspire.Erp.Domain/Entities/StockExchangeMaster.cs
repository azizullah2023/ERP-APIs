using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockExchangeMaster
    {
        public string StockExchangeMasterSeNo { get; set; }
        public int? StockExchangeMasterSeNoNum { get; set; }
        public DateTime? StockExchangeMasterDate { get; set; }
        public string StockExchangeMasterDiffAccNo { get; set; }
        public int? StockExchangeMasterLocationId { get; set; }
        public double? StockExchangeMasterStockinAmt { get; set; }
        public double? StockExchangeMasterStockOutAmt { get; set; }
        public double? StockExchangeMasterDiffAmt { get; set; }
        public string StockExchangeMasterNarration { get; set; }
        public int? StockExchangeMasterFsno { get; set; }
        public int? StockExchangeMasterUnitId { get; set; }
        public string StockExchangeMasterAccNo { get; set; }
        public string StockExchangeMasterPartyName { get; set; }
        public bool? StockExchangeMasterDelStatus { get; set; }
    }
}
