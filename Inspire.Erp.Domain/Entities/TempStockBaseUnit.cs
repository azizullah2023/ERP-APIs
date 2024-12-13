using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockBaseUnit
    {
        public int? TempStockBaseUnitSno { get; set; }
        public int? TempStockBaseUnitMaterialId { get; set; }
        public double? TempStockBaseUnitOpening { get; set; }
        public string TempStockBaseUnitUnit { get; set; }
        public double? TempStockBaseUnitStock { get; set; }
        public double? TempStockBaseUnitClosing { get; set; }
        public double? TempStockBaseUnitAvgRate { get; set; }
        public double? TempStockBaseUnitAmount { get; set; }
        public bool? TempStockBaseUnitDelStatus { get; set; }
    }
}
