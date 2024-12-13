using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class BankLedger
    {
        public string? AccountsTransactions_VoucherNo { get; set; }
        public decimal? AccountsTransactions_Debit { get; set; }
        public decimal? AccountsTransactions_Credit { get; set; }
        public long? AccountsTransactions_TransSno { get; set; }
        public int? checkNo { get; set; }
        public string? AccountsTransactions_Description { get; set; }
        public DateTime? AccountsTransactions_TransDate { get; set; }
    }
}
