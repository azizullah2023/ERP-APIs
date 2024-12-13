using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class PurchaseQuotationModel
    {
        public PurchaseQuotation purchaseQuotation { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseQuotationDetails> purchaseQuotationDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
