using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class StockLedgerResponse
    {
        public string ItemId { get; set; }
        public string PartNo { get; set; }
        public string RelativeNo { get; set; }
        public string ItemName { get; set; }
        public decimal StockRate { get; set; }
        public decimal OpenQuantity { get; set; }
        public decimal OpenValue { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal ReceivedValue { get; set; }
        public decimal SaledQuantity { get; set; }
        public decimal SaledValue { get; set; }
        public decimal IssueQuantity { get; set; }
        public decimal IssueValue { get; set; }
        public decimal ClosingQuantity { get; set; }
        public decimal ClosingValue { get; set; }

    }
}
