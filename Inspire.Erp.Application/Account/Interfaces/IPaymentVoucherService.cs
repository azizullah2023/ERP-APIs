using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IPaymentVoucherService
    {
        public PaymentVoucher InsertPaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions, List<PaymentVoucherDetails> paymentVoucherDetails, List<AllocationDetails> paymentAllocation);
        public PaymentVoucher UpdatePaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions, List<PaymentVoucherDetails> paymentVoucherDetails, List<AllocationDetails> paymentAllocation);
        public int DeletePaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions, List<PaymentVoucherDetails> paymentVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public PaymentVoucher GetSavedPaymentDetails(string pvno);
        public IEnumerable<PaymentVoucher> GetPaymentVouchers();
        public VouchersNumbers GenerateVoucherNo();
        public IQueryable GetUserTracking(string voucherNo);
        public VouchersNumbers GetVouchersNumbers(string pvno);
        public ApiResponse<List<AllocationDetails>> GetAllocationDetails(string accountNo, string voucherNo);
    }
}
