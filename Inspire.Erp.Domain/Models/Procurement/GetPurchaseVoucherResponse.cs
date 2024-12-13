using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Procurement
{
   public class GetPurchaseVoucherResponse
    {
        public long PurchaseVoucherPurId { get; set; }
        // public string PurchaseVoucherPurchaseId { get; set; }
        public string PurchaseVoucherVoucherNo { get; set; }
        public int? PurchaseVoucherPurchaseIdNum { get; set; }
        public string? PurchaseVoucherPurchaseRef { get; set; }
        public string? PurchaseVoucherPurchaseType { get; set; }
        public string? PurchaseVoucherGrNo { get; set; }
        public DateTime? PurchaseVoucherGrDate { get; set; }
        public string PurchaseVoucherGrDateString { get; set; }
        public int? PurchaseVoucherSpId { get; set; }
        public string PurchaseVoucherSpAccNo { get; set; }
        public decimal? PurchaseVoucherSpAmount { get; set; }
        public decimal? PurchaseVoucherFcSpAmount { get; set; }
        public DateTime? PurchaseVoucherPurchaseDate { get; set; }
        public string? PurchaseVoucherLpoNo { get; set; }
        public DateTime? PurchaseVoucherLpoDate { get; set; }
        public string? PurchaseVoucherQuatationNo { get; set; }
        public DateTime? PurchaseVoucherQuatationDate { get; set; }
        public decimal? PurchaseVoucherActualAmount { get; set; }
        public decimal? PurchaseVoucherNetAmount { get; set; }
        public decimal? PurchaseVoucherTransportCost { get; set; }
        public decimal? PurchaseVoucherHandlingCharges { get; set; }
        public decimal? PurchaseVoucherFcActualAmount { get; set; }
        public decimal? PurchaseVoucherFcNetAmount { get; set; }
        public string PurchaseVoucherDescription { get; set; }
        public decimal? PurchaseVoucherDrAccNo { get; set; }
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
        public string? PurchaseVoucherSupplyInvoiceNo { get; set; }
        public decimal? PurchaseVoucherDiscountPercentage { get; set; }
        public int? PurchaseVoucherPoNo { get; set; }
        public int? PurchaseVoucherCurrencyId { get; set; }
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
        //public int? PurchaseVoucherIssedId { get; set; }
        public bool? PurchaseVoucherJobDirectPurchase { get; set; }
        public string? PurchaseVoucherGrnoTmp { get; set; }
        public bool? PurchaseVoucherDelStatus { get; set; }
    }
}
