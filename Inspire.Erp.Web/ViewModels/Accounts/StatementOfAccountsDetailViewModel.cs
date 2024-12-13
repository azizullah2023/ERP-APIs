using System;

namespace Inspire.Erp.Web.ViewModels
{
    public class StatementOfAccountsDetailViewModel
    {
        public string AccountsTransactions_AccNo { get; set; }
        public DateTime AccountsTransactions_TransDate { get; set; }
        public string AccountsTransactions_Particulars { get; set; }
        public double AccountsTransactions_Debit { get; set; }
        public double AccountsTransactions_Credit { get; set; }
        public string AccountsTransactions_VoucherType { get; set; }
        public string AccountsTransactions_VoucherNo { get; set; }
        public string AccountsTransactions_Description { get; set; }
        public string AccountsTransactions_FSNo { get; set; }
        public string CostCenter { get; set; }
    }
}
