using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public partial class ProgressiveInoiceModel
    {
        public ProgressiveInvoice progressiveInvoice { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

    }
}
