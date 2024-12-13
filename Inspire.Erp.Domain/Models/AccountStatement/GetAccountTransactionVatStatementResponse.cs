﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
   public class GetAccountTransactionVatStatementResponse
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
    }
    public class GetVatStatementSummaryResponse
    {
        public List<GetAccountTransactionVatStatementSummaryResponse> Details { get; set; }
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
        public string Total { get; set; }
    }
    public class GetAccountTransactionVatStatementSummaryResponse
    {
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string RunningBalance { get; set; }
    }
    public class GetVatStatementDetailResponse
    {
        public List<GetAccountTransactionVatStatementDetailResponse> Details { get; set; }
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
        public string Total { get; set; }
    }

    public class GetAccountTransactionVatStatementDetailResponse
    {
        public string VoucherType { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string RunningBalance { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string RefNo { get; set; }
        public string VATNo { get; set; }
        public string VatAmount { get; set; }
    }
}
