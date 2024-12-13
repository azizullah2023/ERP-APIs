using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class ReceiptVoucherModel
    {
        public ReceiptVoucherMaster receiptVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<ReceiptVoucherDetails> receiptVoucherDetails { get; set; }

    }
}
