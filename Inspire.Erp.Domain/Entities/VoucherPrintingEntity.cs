using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class VoucherPrintingEntity
    {
        public string AccountsTransactions_TransSno { get; set; }
        public DateTime AccountsTransactions_TransDate { get; set; }
        public string AccountsTransactions_CheqNo { get; set; }
        public string Vouchers_Numbers_V_NO_NU { get; set; }
        public string AccountsTransactions_VoucherType { get; set; }
        public decimal AccountsTransactions_VoucherNo { get; set; }
        public string MA_AccName { get; set; }
        public string AccountsTransactions_Particulars { get; set; }
        public decimal AccountsTransactions_Debit { get; set; }
        public decimal AccountsTransactions_Credit { get; set; }
    }
}
