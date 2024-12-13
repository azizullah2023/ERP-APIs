using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Store.Interfaces
{
    public interface IIssueReturnService
    {

        
             //public IEnumerable<ReportIssueReturn> IssueReturn_GetReportIssueReturn();
      
        //public IssueReturn InsertIssueReturn(IssueReturn issueReturn,
        //  List<IssueReturnDetails> issueReturnDetails);

        public IssueReturnModel InsertIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions, List<IssueReturnDetails> issueReturnDetails
             , List<StockRegister> stockRegister
            );
        public IssueReturnModel UpdateIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions, List<IssueReturnDetails> issueReturnDetails
            , List<StockRegister> stockRegister
            );
        public int DeleteIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions, List<IssueReturnDetails> issueReturnDetails
            , List<StockRegister> stockRegister
            );
        public IEnumerable<AccountsTransactions> GetAllTransaction();
  

        
        public IssueReturnModel GetSavedIssueReturnDetails(string pvno);
        public IEnumerable<IssueReturn> GetIssueReturn();
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate);

        public VouchersNumbers GetVouchersNumbers(string pvno);

    }
}
