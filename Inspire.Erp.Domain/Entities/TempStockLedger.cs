using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockLedger
    {
        public int? TempStockLedgerItemId { get; set; }
        public string TempStockLedgerItemName { get; set; }
        public double? TempStockLedgerOpenQty { get; set; }
        public double? TempStockLedgerOpenValue { get; set; }
        public double? TempStockLedgerRecvdQty { get; set; }
        public double? TempStockLedgerRecvdValue { get; set; }
        public double? TempStockLedgerIssuedQty { get; set; }
        public double? TempStockLedgerIssuedValue { get; set; }
        public double? TempStockLedgerClosingQty { get; set; }
        public double? TempStockLedgerClosingValue { get; set; }
        public string TempStockLedgerRelativeName { get; set; }
        public bool? TempStockLedgerDelStatus { get; set; }
    }
}
