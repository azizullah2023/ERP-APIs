using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Store.Interfaces
{
    public interface IIssueVoucherService
    {

        
             //public IEnumerable<ReportIssueVoucher> IssueVoucher_GetReportIssueVoucher();
    

        //public IssueVoucher InsertIssueVoucher(IssueVoucher issueVoucher,
        //  List<IssueVoucherDetails> issueVoucherDetails);

        public IssueVoucherModel InsertIssueVoucher(IssueVoucher issueVoucher, List<AccountsTransactions> accountsTransactions, List<IssueVoucherDetails> issueVoucherDetails
             , List<StockRegister> stockRegister
            );
        public IssueVoucherModel UpdateIssueVoucher(IssueVoucher issueVoucher, List<AccountsTransactions> accountsTransactions, List<IssueVoucherDetails> issueVoucherDetails
             , List<StockRegister> stockRegister
            );
        public int DeleteIssueVoucher(IssueVoucher issueVoucher, List<AccountsTransactions> accountsTransactions, List<IssueVoucherDetails> issueVoucherDetails
             , List<StockRegister> stockRegister
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
  

        
        public IssueVoucherModel GetSavedIssueVoucherDetails(string pvno);
        public IEnumerable<IssueVoucher> GetIssueVoucher();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        public VouchersNumbers GetVouchersNumbers(string pvno);

    }
}
