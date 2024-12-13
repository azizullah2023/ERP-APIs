using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class CustomerPurchaseOrderModel
    {
        public CustomerPurchaseOrder customerPurchaseOrder { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<CustomerPurchaseOrderDetails> customerPurchaseOrderDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
