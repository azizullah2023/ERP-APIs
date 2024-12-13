using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IBankPaymentVoucherService
    {
        public BankPaymentVoucher InsertBankPaymentVoucher(BankPaymentVoucher bankPaymentVoucher, List<AccountsTransactions> accountsTransactions, List<BankPaymentVoucherDetails> bankPaymentVoucherDetails,List<AllocationDetails> allocationDetails);
        public BankPaymentVoucher UpdateBankPaymentVoucher(BankPaymentVoucher bankPaymentVoucher, List<AccountsTransactions> accountsTransactions, List<BankPaymentVoucherDetails> bankPaymentVoucherDetails, List<AllocationDetails> allocationDetails);
        public int DeleteBankPaymentVoucher(BankPaymentVoucher bankPaymentVoucher, List<AccountsTransactions> accountsTransactions, List<BankPaymentVoucherDetails> bankPaymentVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public BankPaymentVoucher GetSavedBankPaymentDetails(string pvno);
        public IEnumerable<BankPaymentVoucher> GetBankPaymentVouchers();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public void InsertPDCDetails(List<PdcDetails> pdcDetails);
    }
}
