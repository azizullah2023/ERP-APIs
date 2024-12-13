using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
   public class AssetPurchaseVoucher
    {
        public AssetPurchaseVoucher()
        {
            AssetPurchaseVoucherDetails = new HashSet<AssetPurchaseVoucherDetails>();
        }
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

        [NotMapped]
        public virtual VouchersNumbers AssetPurchaseVoucherNoNavigation { get; set; }
        public virtual ICollection<AssetPurchaseVoucherDetails> AssetPurchaseVoucherDetails { get; set; }
    }
}
