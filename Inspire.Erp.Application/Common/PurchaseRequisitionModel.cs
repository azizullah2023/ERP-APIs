using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{

    public partial class PurchaseRequisitionModel

    {
        public PurchaseRequisition purchaseRequisition { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseRequisitionDetails>purchaseRequisitionDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
