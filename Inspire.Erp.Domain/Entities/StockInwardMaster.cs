using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockInwardMaster
    {
        public string StockInwardMasterSiNo { get; set; }
        public int? StockInwardMasterSiNoNum { get; set; }
        public DateTime? StockInwardMasterSiDate { get; set; }
        public string StockInwardMasterVoucherType { get; set; }
        public int? StockInwardMasterCustNo { get; set; }
        public string StockInwardMasterCustName { get; set; }
        public int? StockInwardMasterLocationId { get; set; }
        public double? StockInwardMasterNetAmount { get; set; }
        public double? StockInwardMasterFcNetAmount { get; set; }
        public string StockInwardMasterNarration { get; set; }
        public int? StockInwardMasterFsno { get; set; }
        public int? StockInwardMasterUserId { get; set; }
        public double? StockInwardMasterFcRate { get; set; }
        public int? StockInwardMasterCurrencyId { get; set; }
        public bool? StockInwardMasterDelStatus { get; set; }
    }
}
