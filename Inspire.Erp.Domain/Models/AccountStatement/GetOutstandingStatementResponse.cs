using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
  public  class GetOutstandingStatementResponse
    {
        public long AccountsTransactionsTransSno { get; set; }
        public string AccountsTransactionsAccNo { get; set; }
        public string AccountsTransactionsAccName { get; set; }
        public DateTime AccountsTransactionsTransDate { get; set; }
        public string AccountsTransactionsTransDateString { get; set; }
        public string AccountsTransactionsParticulars { get; set; }
        public decimal AccountsTransactionsDebit { get; set; }
        public decimal AccountsTransactionsCredit { get; set; }
        public decimal? AccountsTransactionsFcDebit { get; set; }
        public decimal? AccountsTransactionsFcCredit { get; set; }
        public string AccountsTransactionsVoucherType { get; set; }
        public string AccountsTransactionsVoucherNo { get; set; }
        public string AccountsTransactionsDescription { get; set; }
        public long AccountsTransactionsUserId { get; set; }
        public string AccountsTransactionsStatus { get; set; }
        public DateTime AccountsTransactionsTstamp { get; set; }
        public string RefNo { get; set; }
        public decimal AccountsTransactionsFsno { get; set; }
        public decimal? AccountsTransactionsAllocDebit { get; set; }
        public decimal? AccountsTransactionsAllocCredit { get; set; }
        public decimal? AccountsTransactionsAllocBalance { get; set; }
        public decimal? AccountsTransactionsFcAllocDebit { get; set; }
        public decimal? AccountsTransactionsFcAllocCredit { get; set; }
        public decimal? AccountsTransactionsFcAllocBalance { get; set; }
        public int? AccountsTransactionsLocation { get; set; }
        public long? AccountsTransactionsJobNo { get; set; }
        public long? AccountsTransactionsCostCenterId { get; set; }
        public DateTime? AccountsTransactionsApprovalDt { get; set; }
        public int? AccountsTransactionsDepartment { get; set; }
        public decimal? AccountsTransactionsFcRate { get; set; }
        public int? AccountsTransactionsCompanyId { get; set; }
        public int? AccountsTransactionsCurrencyId { get; set; }
        public double? AccountsTransactionsDrGram { get; set; }
        public double? AccountsTransactionsCrGram { get; set; }
        public string AccountsTransactionsCheqNo { get; set; }
        public string AccountsTransactionsLpoNo { get; set; }
        public DateTime? AccountsTransactionsCheqDate { get; set; }
        public string AccountsTransactionsOpposEntryDesc { get; set; }
        public double? AccountsTransactionsAllocUpdateBal { get; set; }
        public long? AccountsTransactionsDeptId { get; set; }
        public string AccountsTransactionsVatno { get; set; }
        public decimal? AccountsTransactionsVatableAmount { get; set; }
        public bool? AccountstransactionsDelStatus { get; set; }
        public string AccountstransactionsTotalDays { get; set; }
        public string AccountstransactionsRunningBalance { get; set; }
    }
    public class OutStandingStatementResponse
    {
        public List<GetOutstandingStatementResponse> Details { get; set; }
        public string TotalRunningBalance { get; set; }
        public string Currency { get; set; }
        public string TotalRunningBalanceInWords { get; set; }
        public string ZeroToThirty { get; set; }
        public string ThirtyOneToSixty { get; set; }
        public string SixtyOneToNinety { get; set; }
        public string NinetyOneToOneEighty { get; set; }
        public string OneEightyToThreeSixty { get; set; }
        public string AboveThreeSixty { get; set; }
        public string AccountName { get; set; }
        public string AccNo { get; set; }
    }

    public class OutStandingReportGridResponse
    {
        public List<OutStandingReportGrid> Details { get; set; }
        public string TotalRunningBalance { get; set; }
        public string Currency { get; set; }
        public string TotalRunningBalanceInWords { get; set; }
        public string ZeroToThirty { get; set; }
        public string ThirtyOneToSixty { get; set; }
        public string SixtyOneToNinety { get; set; }
        public string NinetyOneToOneEighty { get; set; }
        public string OneEightyToThreeSixty { get; set; }
        public string AboveThreeSixty { get; set; }
        public string AccountName { get; set; }
        public string AccNo { get; set; }
    }




    public class OutStandingReportGrid
    {
        public string AccountsTransactionsVoucherNo { get; set; }
        public string AccountsTransactionsVoucherType { get; set; }
        public string AccountsTransactionsAccNo { get; set; }
        public string AccountsTransactionsTransDate { get; set; }
        public string AccountsTransactionsDescription { get; set; }
        public string RefNo { get; set; }
        public decimal? AccountsTransactionsDebit { get; set; }
        public decimal? AccountsTransactionsCredit { get; set; }
        public decimal? settled { get; set; }
        public decimal? Balance { get; set; }
        public decimal? RunningBal { get; set; }
        public int Days { get; set; }
    }

}
