using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class OpeningStockVoucherModel
    {
        public OpeningStockVoucher openingstockVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<OpeningStockVoucherDetails> openingstockVoucherDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
