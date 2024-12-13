using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockpos
    {
        public int? TempStockposItemId { get; set; }
        public string TempStockposItemName { get; set; }
        public string TempStockposDefaultUnit { get; set; }
        public int? TempStockposConversionType { get; set; }
        public double? TempStockposStockItemId { get; set; }
        public double? TempStockposStockBatch { get; set; }
        public double? TempStockposStockVariation { get; set; }
        public double? TempStockposNegativeBatchCount { get; set; }
        public double? TempStockposTotalBatchCount { get; set; }
        public bool? TempStockposDelStatus { get; set; }
    }
}
