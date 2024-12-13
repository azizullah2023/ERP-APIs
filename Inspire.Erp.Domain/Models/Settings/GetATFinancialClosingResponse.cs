using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Settings
{
   public class GetATFinancialClosingResponse
    {
        public string MasterAccountsTableHead { get; set; }
        public string MasterAccountsTableMainHead { get; set; }
        public string MasterAccountsTableSubHead { get; set; }
        public string MasterAccountsTableRelativeNo { get; set; }
        public long AccountsTransactionsTransSno { get; set; }
        public string AccountsTransactionsAccNo { get; set; }
        public string AccountsTransactionsAccName { get; set; }
        public DateTime AccountsTransactionsTransDate { get; set; }
        public string AccountsTransactionsTransDateString { get; set; }
        public string AccountsTransactionsParticulars { get; set; }
        public decimal AccountsTransactionsDebit { get; set; }
        public decimal AccountsTransactionsDebitSum { get; set; }
        public decimal AccountsTransactionsCreditSum { get; set; }
        public decimal AccountsTransactionsCredit { get; set; }
        public string AccountsTransactionsVoucherType { get; set; }
        public string AccountsTransactionsVoucherNo { get; set; }
        public string AccountsTransactionsDescription { get; set; }
        public DateTime AccountsTransactionsTstamp { get; set; }
        public string RefNo { get; set; }
        public decimal? AccountsTransactionsAllocDebit { get; set; }
        public decimal? AccountsTransactionsAllocCredit { get; set; }
        public decimal? AccountsTransactionsAllocBalance { get; set; }
    }
    public class FinancialClosingResponse
    {
        public string head { get; set; }
        public string accName { get; set; }
        public string accNo { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string amount { get; set; }
    }
}
