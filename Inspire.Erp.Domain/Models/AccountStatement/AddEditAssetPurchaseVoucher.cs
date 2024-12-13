using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
   public class AddEditAssetPurchaseVoucher
    {
        public int AstPurID { get; set; }
        public string AssetPurchaseVoucherNo { get; set; }
        public string AstPurchaseRef { get; set; }
        public string AstPurchaseType { get; set; }
        public string GRNo { get; set; }
        public DateTime? GRDate { get; set; }
        public int? SPID { get; set; }
        public string SPAccNo { get; set; }
        public decimal? SPAmount { get; set; }
        public decimal? FCSPAmount { get; set; }
        public DateTime? AstPurDt { get; set; }
        public string LPONo { get; set; }
        public DateTime? LPODate { get; set; }
        public string QuotationNo { get; set; }
        public DateTime? QuotationDate { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? Handlingcharges { get; set; }
        public decimal? FcActualAmount { get; set; }
        public decimal? FcNetAmount { get; set; }
        public string Description { get; set; }
        public string DrAccNo { get; set; }
        public decimal? DrAmount { get; set; }
        public decimal? FcDrAmount { get; set; }
        public char DisYN { get; set; }
        public string DisAcNo { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? FcDisAmount { get; set; }
        public string Status { get; set; }
        public int? FSNO { get; set; }
        public long? UserID { get; set; }
        public decimal? FcRate { get; set; }
        public int? LocationID { get; set; }
        public string SupInvNo { get; set; }
        public decimal? DisPer { get; set; }
        public string PONO { get; set; }
        public int? CurrencyId { get; set; }
        public int? CompanyId { get; set; }
        public decimal? VatAMT { get; set; }
        public decimal? VatPer { get; set; }
        public string VatRoundSign { get; set; }
        public decimal? VatRountAmt { get; set; }
        public string AstVatNo { get; set; }
        public bool? AssetPurchaseVoucherDelStatus { get; set; }
        public List<AddEditAssetPurchaseVoucherDetail> AssetPurchaseVoucherDetail { get; set; }
    }
    public class AddEditAssetPurchaseVoucherDetail
    {
        public int AstPurDetID { get; set; }
        public int AstPurchaseID { get; set; }
        public string AssetPurchaseDetailsVoucherNo { get; set; }
        public int? Sno { get; set; }
        public int? AstID { get; set; }
        public decimal? Rate { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public decimal? FcAmount { get; set; }
        public string Remarks { get; set; }
        public string AstPurRefNo { get; set; }
        public decimal? LifrInYrs { get; set; }
        public DateTime AstPurDate { get; set; }
        public string DepMode { get; set; }
        public decimal? DepAmt { get; set; }
        public decimal? DepPerc { get; set; }
        public int? FSNO { get; set; }
        public int? LocationID { get; set; }
        public decimal? SalesPrice1 { get; set; }
        public decimal? SalesPrice2 { get; set; }
        public decimal? SalesPrice3 { get; set; }
        public int? CompanyId { get; set; }
        public int? LPOID { get; set; }
        public string GRNNO { get; set; }
        public string BatchCode { get; set; }
        public decimal? AccDepAmt { get; set; }
        public decimal? BookValue { get; set; }
        public string ModelNo { get; set; }
        public string Specification { get; set; }
        public string Manufacturer { get; set; }
        public string AssetSize { get; set; }
        public string Colour { get; set; }
        public decimal? OPCAccDepAmt { get; set; }
        public decimal? OPBookValue { get; set; }

        public bool? AssetPurchaseVoucherDetailsDelStatus { get; set; }
    }
}
