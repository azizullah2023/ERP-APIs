using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class ContraVoucherModel
    {
       public ContraVoucher contraVoucher { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }
       public List<ContraVoucherDetails> contraVoucherDetails { get; set; }

    }
}
