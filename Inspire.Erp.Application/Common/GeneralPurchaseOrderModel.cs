﻿using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class GeneralPurchaseOrderModel
    {
        public GeneralPurchaseOrder GeneralpurchaseOrder { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<GeneralPurchaseOrderDetails> GeneralpurchaseOrderDetails { get; set; }
        public List<StockRegister> stockRegister { get; set; }
    }
}
