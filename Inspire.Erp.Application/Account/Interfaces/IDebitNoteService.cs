using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Entities;

namespace Inspire.Erp.Application.StoreWareHouse.Interface
{
    public interface IDebitNoteService
    {
        public DebitNote InsertDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails);
        public DebitNote UpdateDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails);
        public int DeleteDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public DebitNote GetSavedDebitNoteDetails(string pvno);
        public IEnumerable<DebitNote> GetDebitNote();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);


    }
}