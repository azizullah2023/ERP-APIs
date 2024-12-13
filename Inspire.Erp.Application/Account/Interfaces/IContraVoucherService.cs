using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IContraVoucherService
    {
        public ContraVoucher InsertContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails);
        public ContraVoucher UpdateContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails);
        public int DeleteContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails);
        public IEnumerable<AccountsTransactions> GetAllTransaction();
        public ContraVoucher GetSavedContraVoucherDetails(string pvno);
        public IEnumerable<ContraVoucher> GetContraVoucher();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);
        public VouchersNumbers GetVouchersNumbers(string pvno);
    }
}
