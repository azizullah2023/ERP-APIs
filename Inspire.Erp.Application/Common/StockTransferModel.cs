using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class StockTransferModel
    {
        public StockTransfer stockTransfer { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<StockTransferDetails> stockTransferDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
