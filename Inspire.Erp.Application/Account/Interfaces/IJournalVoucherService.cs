using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IJournalVoucherService
    {
        public JournalVoucher InsertJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);
        public JournalVoucher UpdateJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);
        public int DeleteJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public JournalVoucher GetSavedJournalVoucherDetails(string pvno);
        public IEnumerable<JournalVoucher> GetJournalVoucher();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
        public IQueryable GetJVTRacking(string JournalVoucher_VNO);
    }
}
