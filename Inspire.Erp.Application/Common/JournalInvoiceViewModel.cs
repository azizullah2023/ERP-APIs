using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    class JournalInvoiceViewModel
    {
        public JournalInvoiceMaster journalInvoice { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<JournalInvoiceDetails> journalInvoiceDetails { get; set; }

    }
}



