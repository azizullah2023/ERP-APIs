using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
   public interface IBankReceiptVoucherService
    {
        public BankReceiptVoucher InsertBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher,List<AccountsTransactions> accountsTransactions,List<BankReceiptVoucherDetails> bankReceiptVoucherDetails, List<Domain.Models.AllocationDetails> allocationDetails);
        public BankReceiptVoucher UpdateBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher, List<AccountsTransactions> accountsTransactions, List<BankReceiptVoucherDetails> bankReceiptVoucherDetails, List<Domain.Models.AllocationDetails> allocationDetails);
        public int DeleteBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher, List<AccountsTransactions> accountsTransactions, List<BankReceiptVoucherDetails> bankReceiptVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public BankReceiptVoucher GetSavedBankReceiptDetails(string pvno);
        public IEnumerable<BankReceiptVoucher> GetBankReceiptVouchers();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public void InsertPDCDetails(List<PdcDetails> pdcDetails);
    }
}
