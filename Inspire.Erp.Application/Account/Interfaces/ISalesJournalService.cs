using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface ISalesJournalService
    {
        //public SalesJournal InsertSalesJournal(SalesJournal salesJournal,
        //  List<SalesJournalDetails> salesJournalDetails);

        public SalesJournalModel InsertSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails);
        public SalesJournalModel UpdateSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails);
        public int DeleteSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public SalesJournalModel GetSavedSalesJournalDetails(string pvno);
        public IEnumerable<SalesJournal> GetSalesJournal();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);


        public VouchersNumbers GetVouchersNumbers(string pvno);

    }
}
