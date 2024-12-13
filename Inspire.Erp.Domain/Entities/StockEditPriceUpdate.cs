using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockEditPriceUpdate
    {
        public int? StockEditPriceUpdateId { get; set; }
        public string StockEditPriceUpdateVno { get; set; }
        public int? StockEditPriceUpdateMaterialId { get; set; }
        public double? StockEditPriceUpdateRate { get; set; }
        public double? StockEditPriceUpdateQty { get; set; }
        public bool? StockEditPriceUpdateDelStatus { get; set; }
    }
}
