using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class CreditNoteModel
    {
       public CreditNote creditNote { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<CreditNoteDetails> creditNoteDetails { get; set; }

    }
}
