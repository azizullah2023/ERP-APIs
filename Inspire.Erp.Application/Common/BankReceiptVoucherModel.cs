using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class BankReceiptVoucherModel
    {
       public BankReceiptVoucher bankReceiptVoucher { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<BankReceiptVoucherDetails> bankReceiptVoucherDetails { get; set; }

    }
}
