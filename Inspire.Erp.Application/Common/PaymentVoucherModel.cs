using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class PaymentVoucherModel
    {
        public PaymentVoucher paymentVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PaymentVoucherDetails> paymentsVoucherDetails { get; set; }

    }
}
