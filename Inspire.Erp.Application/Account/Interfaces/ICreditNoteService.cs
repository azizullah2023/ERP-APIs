using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface ICreditNoteService
    {
        public CreditNote InsertCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails);
        public CreditNote UpdateCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails);
        public int DeleteCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public CreditNote GetSavedCreditNoteDetails(string pvno);
        public IEnumerable<CreditNote> GetCreditNote();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
    }
}
