using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.MIS
{
   public class GetAccountTransactionProfitLossResponse
    {
        public string MasterAccountsTableHead { get; set; }
        public string MasterAccountsTableMainHead { get; set; }
        public string MasterAccountsTableSubHead { get; set; }
        public long AccountsTransactionsTransSno { get; set; }
        public string AccountsTransactionsAccNo { get; set; }
        public string AccountsTransactionsAccName { get; set; }
        public string MARelativeNo { get; set; }
        public DateTime AccountsTransactionsTransDate { get; set; }
        public string AccountsTransactionsTransDateString { get; set; }
        public string AccountsTransactionsParticulars { get; set; }
        public decimal AccountsTransactionsDebit { get; set; }
        public decimal AccountsTransactionsDebitSum { get; set; }
        public decimal AccountsTransactionsCreditSum { get; set; }
        public decimal AccountsTransactionsCredit { get; set; }
        public string AccountsTransactionsVoucherType { get; set; }
        public string AccountsTransactionsVoucherNo { get; set; }
        public string AccountsTransactionsDescription { get; set; }
        public DateTime AccountsTransactionsTstamp { get; set; }
        public string RefNo { get; set; }
        public decimal? AccountsTransactionsAllocDebit { get; set; }
        public decimal? AccountsTransactionsAllocCredit { get; set; }
        public decimal? AccountsTransactionsAllocBalance { get; set; }
    }
    public class ProfitLossPrintSimpleResponse
    {
        public string AccName { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
    }
    public class WrapperProfitLossPrintSimpleResponse
    {
        public int? StationMasterCode { get; set; }
        public string StationMasterStationName { get; set; }
        public string StationMasterAddress { get; set; }
        public string StationMasterCity { get; set; }
        public string StationMasterPostOffice { get; set; }
        public string StationMasterTele1 { get; set; }
        public string StationMasterTele2 { get; set; }
        public string StationMasterFax { get; set; }
        public string StationMasterEmail { get; set; }
        public string StationMasterWebSite { get; set; }
        public string StationMasterCountry { get; set; }
        public string StationMasterLogoPath { get; set; }
        public string StationMasterSignPath { get; set; }
        public string StationMasterVatNo { get; set; }
        public string NetLoss { get; set; }
        public string TotalIncome { get; set; }
        public string TotalExpense { get; set; }
        public List<ProfitLossPrintSimpleResponse> Incomes { get; set; }
        public List<ProfitLossPrintSimpleResponse> Expenses { get; set; }
    }
    public class ProfitLossPrintMonthWiseResponse
    {
        public string AccName { get; set; }
        public string AccNo { get; set; }
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string Aug { get; set; }
        public string Sept { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }
        public string Total { get; set; }

    }
    public class WrapperProfitLossPrintMonthWiseResponse
    {
        public int? StationMasterCode { get; set; }
        public string StationMasterStationName { get; set; }
        public string StationMasterAddress { get; set; }
        public string StationMasterCity { get; set; }
        public string StationMasterPostOffice { get; set; }
        public string StationMasterTele1 { get; set; }
        public string StationMasterTele2 { get; set; }
        public string StationMasterFax { get; set; }
        public string StationMasterEmail { get; set; }
        public string StationMasterWebSite { get; set; }
        public string StationMasterCountry { get; set; }
        public string StationMasterLogoPath { get; set; }
        public string StationMasterSignPath { get; set; }
        public string StationMasterVatNo { get; set; }
        public string NetLoss { get; set; }
        public string TotalIncome { get; set; }
        public string TotalExpense { get; set; }
        public List<ProfitLossPrintMonthWiseResponse> Incomes { get; set; }
        public List<ProfitLossPrintMonthWiseResponse> Expenses { get; set; }
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string Aug { get; set; }
        public string Sept { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Dec { get; set; }
        public string Total { get; set; }

    }
    public class WrapperProfitLossPrintSummaryResponse
    {
        public int? StationMasterCode { get; set; }
        public string StationMasterStationName { get; set; }
        public string StationMasterAddress { get; set; }
        public string StationMasterCity { get; set; }
        public string StationMasterPostOffice { get; set; }
        public string StationMasterTele1 { get; set; }
        public string StationMasterTele2 { get; set; }
        public string StationMasterFax { get; set; }
        public string StationMasterEmail { get; set; }
        public string StationMasterWebSite { get; set; }
        public string StationMasterCountry { get; set; }
        public string StationMasterLogoPath { get; set; }
        public string StationMasterSignPath { get; set; }
        public string StationMasterVatNo { get; set; }
        public string NetLoss { get; set; }
        public string TotalIncome { get; set; }
        public string TotalExpense { get; set; }
        public List<ProfitLossPrintSummaryResponse> Incomes { get; set; }
        public List<ProfitLossPrintSummaryResponse> Expenses { get; set; }
    }
    public class ProfitLossPrintSummaryResponse
    {
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string RunningBalance { get; set; }
        public string AccName { get; set; }
    }
}
