using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class StatementOfAccountDetailResponse
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

    public class StatementOfAccountFCSummResponse
    {
        public string AccNo { get; set; }
        public string AccountName { get; set; }
        public double TotalDebit { get; set; }
        public double TotalCredit { get; set; }
        public double DebitBalance { get; set; }
        public double CreditBalance { get; set; }
    }

    public class StatementOfAccountFCDetailResponse
    {
        public string AccNo { get; set; }
        public string AccountName { get; set; }
        public DateTime TransDate { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string Particulars { get; set; }
        public string RefNo { get; set; }
        public double RunningBalance { get; set; }
    }
}
