using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class SalesJournalModel
    {
       public SalesJournal salesJournal { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<SalesJournalDetails> salesJournalDetails { get; set; }

    }
}
