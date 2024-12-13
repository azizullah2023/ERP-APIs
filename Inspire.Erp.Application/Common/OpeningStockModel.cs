using System;
using System.Collections.Generic;
using Inspire.Erp.Domain.Entities;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class OpeningStockModel
    {
        public OpeningStock openingStock { get; set; }
        public List<AccountsTransactions> AccountsTransactions { get; set; }

        public List<StockRegister> StockRegister { get; set; }

    }
}
