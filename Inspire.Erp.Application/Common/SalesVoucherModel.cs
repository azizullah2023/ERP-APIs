using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class SalesVoucherModel
    {
        public SalesVoucher salesVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<SalesVoucherDetails> salesVoucherDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
