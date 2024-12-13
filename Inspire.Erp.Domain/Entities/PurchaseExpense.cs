using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseExpense
    {
        public int? PurchaseExpensePtcid { get; set; }
        public int? PurchaseExpenseSno { get; set; }
        public string PurchaseExpenseDrCr { get; set; }
        public string PurchaseExpensePurId { get; set; }
        public double? PurchaseExpenseTransAmount { get; set; }
        public double? PurchaseExpenseFcTransAmount { get; set; }
        public string PurchaseExpenseTransAccNo { get; set; }
        public string PurchaseExpenseTransRemarks { get; set; }
        public bool? PurchaseExpenseIsEdit { get; set; }
        public bool? PurchaseExpenseDelStatus { get; set; }
    }
}
