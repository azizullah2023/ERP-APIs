using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class BankPaymentVoucherModel
    {
       public BankPaymentVoucher bankPaymentVoucher { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<BankPaymentVoucherDetails> bankPaymentsVoucherDetails { get; set; }

    }
}
