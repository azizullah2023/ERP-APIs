using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{

    public partial class StockRequisitionModel

    {
        public StockRequisition StockRequisition { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<StockRequisitionDetails> StockRequisitionDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
