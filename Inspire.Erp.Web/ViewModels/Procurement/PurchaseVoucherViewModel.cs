//using Inspire.Erp.Web.ViewModels.Procurement;
using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public partial class PurchaseVoucherViewModel
    {
        public long PurchaseVoucherPurId { get; set; }
        public string PurchaseVoucherVoucherNo { get; set; }
        public int? PurchaseVoucherPurchaseIdNum { get; set; }
        public string PurchaseVoucherPurchaseRef { get; set; }
        public string PurchaseVoucherPurchaseType { get; set; }
        public string PurchaseVoucherGrNo { get; set; }
        public DateTime? PurchaseVoucherGrDate { get; set; }
        public int? PurchaseVoucherSpId { get; set; }
        public string PurchaseVoucherSpAccNo { get; set; }
        public decimal? PurchaseVoucherSpAmount { get; set; }
        public decimal? PurchaseVoucherFcSpAmount { get; set; }
        public DateTime? PurchaseVoucherPurchaseDate { get; set; }
        public string PurchaseVoucherLpoNo { get; set; }
        public DateTime? PurchaseVoucherLpoDate { get; set; }
        public string PurchaseVoucherQuatationNo { get; set; }
        public DateTime? PurchaseVoucherQuatationDate { get; set; }
        public decimal? PurchaseVoucherActualAmount { get; set; }
        public decimal? PurchaseVoucherNetAmount { get; set; }
        public decimal? PurchaseVoucherTransportCost { get; set; }
        public decimal? PurchaseVoucherHandlingCharges { get; set; }
        public decimal? PurchaseVoucherFcActualAmount { get; set; }
        public decimal? PurchaseVoucherFcNetAmount { get; set; }
        public string PurchaseVoucherDescription { get; set; }
        public double? PurchaseVoucherDrAccNo { get; set; }
        public decimal? PurchaseVoucherDrAmount { get; set; }
        public decimal? PurchaseVoucherFcDrAmount { get; set; }
        public string PurchaseVoucherDisYn { get; set; }
        public string PurchaseVoucherDisAcNo { get; set; }
        public decimal? PurchaseVoucherDisAmount { get; set; }
        public decimal? PurchaseVoucherFcDisAmount { get; set; }
        public string PurchaseVoucherStatus { get; set; }
        public int? PurchaseVoucherFsno { get; set; }
        public int? PurchaseVoucherUserId { get; set; }
        public decimal? PurchaseVoucherFcRate { get; set; }
        public int? PurchaseVoucherLocationId { get; set; }
        public string PurchaseVoucherSupplyInvoiceNo { get; set; }
        public decimal? PurchaseVoucherDiscountPercentage { get; set; }
        public int? PurchaseVoucherPoNo { get; set; }
        public int? PurchaseVoucherCurrencyId { get; set; }
        public string CurrencyMasterCurrencyName { get; set; }
        public int? PurchaseVoucherCompanyId { get; set; }
        public int? PurchaseVoucherJobId { get; set; }
        public string PurchaseVoucherBatchCode { get; set; }
        public string PurchaseVoucherCashSupplierName { get; set; }
        public string PurchaseVoucherDayBookNo { get; set; }
        public decimal? PurchaseVoucherVatAmount { get; set; }
        public decimal? PurchaseVoucherVatPercentage { get; set; }
        public string PurchaseVoucherVatRoundSign { get; set; }
        public decimal? PurchaseVoucherVatRoundAmount { get; set; }
        public string PurchaseVoucherVatNo { get; set; }
        public bool? PurchaseVoucherExcludeVat { get; set; }
        public int? PurchaseVoucherIssuedId { get; set; }
        public bool? PurchaseVoucherJobDirectPurchase { get; set; }
        public string PurchaseVoucherGrnoTmp { get; set; }
        public int? PurchaseVoucherPartyId { get; set; }
        public string PurchaseVoucherPartyName { get; set; }
        public string PurchaseVoucherPartyAddress { get; set; }
        public string PurchaseVoucherPartyVatNo { get; set; }
        public decimal? PurchaseVoucherTotalGrossAmt { get; set; }
        public decimal? PurchaseVoucherTotalItemDisAmount { get; set; }
        public decimal? PurchaseVoucherTotalDiscountAmt { get; set; }
        public bool? PurchaseVoucherDelStatus { get; set; }

        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<PurchaseVoucherDetailsViewModel> PurchaseVoucherDetails { get; set; }
    }

    public partial class PurchaseVoucherListView
    {
        public long PurchaseVoucherPurId { get; set; }
        public string PurchaseVoucherVoucherNo { get; set; }
        public string PurchaseVoucherPurchaseType { get; set; }
        public string PurchaseVoucherGrNo { get; set; }
        public DateTime? PurchaseVoucherPurchaseDate { get; set; }
        public decimal? PurchaseVoucherNetAmount { get; set; }
        public int? PurchaseVoucherUserId { get; set; }
        public int? PurchaseVoucherLocationId { get; set; }
        public int? PurchaseVoucherPoNo { get; set; }
        public int? PurchaseVoucherCurrencyId { get; set; }        
        public int? PurchaseVoucherJobId { get; set; }
        public string PurchaseVoucherCashSupplierName { get; set; }
        public decimal? PurchaseVoucherVatAmount { get; set; }
        public string PurchaseVoucherVatRoundSign { get; set; }
        public decimal? PurchaseVoucherVatRoundAmount { get; set; }
        public string PurchaseVoucherVatNo { get; set; }
        public int? PurchaseVoucherPartyId { get; set; }
        public string PurchaseVoucherPartyName { get; set; }
        public decimal? PurchaseVoucherTotalGrossAmt { get; set; }
        public decimal? PurchaseVoucherTotalItemDisAmount { get; set; }
        public decimal? PurchaseVoucherTotalDiscountAmt { get; set; }

    }
}
