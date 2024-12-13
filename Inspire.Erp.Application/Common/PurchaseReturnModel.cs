using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class PurchaseReturnModel
    {
        public PurchaseReturn purchaseReturn { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseReturnDetails> purchaseReturnDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
        
    }
}
