using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class DebitNoteModel
    {
       public DebitNote debitNote { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<DebitNoteDetails> debitNoteDetails { get; set; }

    }
}
