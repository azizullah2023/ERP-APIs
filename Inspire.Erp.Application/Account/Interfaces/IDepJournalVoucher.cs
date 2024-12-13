using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IDepJournalVoucher
    {
        JournalVoucher UpdateDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);

        int DeleteDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);
        JournalVoucher InsertDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails);
        IEnumerable<AccountsTransactions> GetAllTransaction();
        IEnumerable<JournalVoucher> GetDepJournalVoucher();
        JournalVoucher GetSavedDepJournalVoucherDetails(string pvno);

        VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        VouchersNumbers GetVouchersNumbers(string pvno);

        IQueryable GetJVTRacking(string JournalVoucher_VNO);
    }
}
