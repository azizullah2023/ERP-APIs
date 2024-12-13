using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStockLedger1
    {
        public int TempStockLedgerIdentitypKey { get; set; }
        public string TempStockLedgerColumn1 { get; set; }
        public string TempStockLedgerColumn2 { get; set; }
        public string TempStockLedgerColumn3 { get; set; }
        public int? TempStockLedgerItemId { get; set; }
        public double? TempStockLedgerColumnValue { get; set; }
        public int? TempStockLedgerColNo1 { get; set; }
        public int? TempStockLedgerColNo2 { get; set; }
        public int? TempStockLedgerColNo3 { get; set; }
        public int? TempStockLedgerUnit { get; set; }
        public int? TempStockLedgerGroupId { get; set; }
        public string TempStockLedgerGroupName { get; set; }
        public bool? TempStockLedgerDelStatus { get; set; }
    }
}
