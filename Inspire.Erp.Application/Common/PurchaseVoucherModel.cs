using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class PurchaseVoucherModel
    {
        public PurchaseVoucher purchaseVoucher { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseVoucherDetails> purchaseVoucherDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
