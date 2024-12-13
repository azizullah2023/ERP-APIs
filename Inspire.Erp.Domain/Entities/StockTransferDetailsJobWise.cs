using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockTransferDetailsJobWise
    {
        public int? StockTransferDetailsJobWiseId { get; set; }
        public int? StockTransferDetailsJobWiseSno { get; set; }
        public int? StockTransferDetailsJobWiseMaterialId { get; set; }
        public int? StockTransferDetailsJobWiseUnitId { get; set; }
        public double? StockTransferDetailsJobWiseQty { get; set; }
        public double? StockTransferDetailsJobWiseRate { get; set; }
        public string StockTransferDetailsJobWiseRemarks { get; set; }
        public bool? StockTransferDetailsJobWiseDelStatus { get; set; }
    }
}
