using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReportStockBaseUnit
    {
        public long? ItemGroupNo { get; set; }
        public string ItemGroupName { get; set; }
        public long? StockRegisterMaterialId { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public int? StockRegisterLocationID { get; set; }
        public string LocationMasterLocationName { get; set; }
        public int? StockRegisterUnitId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public decimal? Stock { get; set; }
        public decimal? StockRegisterRate { get; set; }
        public decimal? StockRegisterAmount { get; set; }
        public long? RelativeNo { get; set; }
    }
}
