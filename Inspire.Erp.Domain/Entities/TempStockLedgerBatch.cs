using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockLedgerBatch
    {
        public int? TempStockLedgerBatchItemId { get; set; }
        public string TempStockLedgerBatchItemName { get; set; }
        public double? TempStockLedgerBatchOpenQty { get; set; }
        public double? TempStockLedgerBatchOpenValue { get; set; }
        public double? TempStockLedgerBatchRecvdQty { get; set; }
        public double? TempStockLedgerBatchRecvdValue { get; set; }
        public double? TempStockLedgerBatchIssuedQty { get; set; }
        public double? TempStockLedgerBatchIssuedValue { get; set; }
        public double? TempStockLedgerBatchClosingQty { get; set; }
        public double? TempStockLedgerBatchClosingValue { get; set; }
        public string TempStockLedgerBatchRelativeName { get; set; }
        public bool? TempStockLedgerBatchDelStatus { get; set; }
    }
}
