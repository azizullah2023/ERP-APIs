using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class PurchaseJournalModel
    {
       public PurchaseJournal purchaseJournal { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<PurchaseJournalDetails> purchaseJournalDetails { get; set; }

    }
}
