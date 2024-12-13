using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ReportStockBaseUnitViewModel
    {
        public long? ItemGroupNo { get; set; }
        public string ItemGroupName { get; set; }
        public decimal? StockRegisterMatId { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public int? StockRegisterLocationID { get; set; }
        public string LocationMasterLocationName { get; set; }
        public decimal? StockRegisterUnitId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public decimal? Stock { get; set; }
        public decimal? StockRegisterRate { get; set; }
        public decimal? StockRegisterAmount { get; set; }

    }
}
