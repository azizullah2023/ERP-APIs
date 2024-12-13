using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class FinalStock
    {
        public int? FinalStockId { get; set; }
        public double? FinalStockQty { get; set; }
        public int? FinalStockLocationId { get; set; }
        public int? FinalStockItemId { get; set; }
        public bool? FinalStockDelStatus { get; set; }
    }
}
