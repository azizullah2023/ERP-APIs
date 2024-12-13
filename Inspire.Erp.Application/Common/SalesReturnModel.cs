using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class SalesReturnModel
    {
        public SalesReturn salesReturn { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<SalesReturnDetails> salesReturnDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }        
    }
}
