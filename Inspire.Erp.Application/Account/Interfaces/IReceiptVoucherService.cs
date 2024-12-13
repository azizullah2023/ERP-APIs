using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IReceiptVoucherService
    {
        public ReceiptVoucherMaster InsertReceiptVoucher(ReceiptVoucherMaster receiptVoucher, List<AccountsTransactions> accountsTransactions, List<ReceiptVoucherDetails> receiptVoucherDetails, List<AllocationDetails> alloDetails);
        public ReceiptVoucherMaster UpdateReceiptVoucher(ReceiptVoucherMaster receiptVoucher, List<AccountsTransactions> accountsTransactions, List<ReceiptVoucherDetails> receiptVoucherDetails, List<AllocationDetails> alloDetails);
        public int DeleteReceiptVoucher(ReceiptVoucherMaster receiptVoucher, List<AccountsTransactions> accountsTransactions, List<ReceiptVoucherDetails> receiptVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public ReceiptVoucherMaster GetSavedReceiptDetails(string Rvno);
        public IEnumerable<ReceiptVoucherMaster> GetReceiptVouchers();
        
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string Rvno);


    }
}
