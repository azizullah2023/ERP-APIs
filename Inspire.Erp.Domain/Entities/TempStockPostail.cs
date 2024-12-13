using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockPostail
    {
        public int? TempStockPostailItemId { get; set; }
        public string TempStockPostailBatchCode { get; set; }
        public DateTime? TempStockPostailExpiryDate { get; set; }
        public double? TempStockPostailStock { get; set; }
        public bool? TempStockPostailDelStatus { get; set; }
    }
}
