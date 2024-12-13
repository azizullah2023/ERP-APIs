using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class JournalVoucherModel
    {
       public JournalVoucher journalVoucher { get; set; }
       public List<AccountsTransactions> accountsTransactions { get; set; }

        public List<JournalVoucherDetails> journalVoucherDetails { get; set; }

    }
}
