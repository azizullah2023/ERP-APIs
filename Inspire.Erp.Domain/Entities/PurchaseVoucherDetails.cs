using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseVoucherDetails
    {
        public long PurchaseVoucherDetailsPurcahseDetailsId { get; set; }
        public string PurchaseVoucherDetailsVoucherNo { get; set; }
        public string PurchaseVoucherDetailsBatchCode { get; set; }
        public int? PurchaseVoucherDetailsSno { get; set; }
        public int? PurchaseVoucherDetailsMaterialId { get; set; }
        public decimal? PurchaseVoucherDetailsRate { get; set; }
        public decimal? PurchaseVoucherDetailsQuantity { get; set; }
        public decimal? PurchaseVoucherDetailsAmount { get; set; }
        public decimal? PurchaseVoucherDetailsFcAmount { get; set; }
        public string PurchaseVoucherDetailsRemarks { get; set; }
        public string PurchaseVoucherDetailsUnit { get; set; }
        public int? PurchaseVoucherDetailsUnitId { get; set; }
        public DateTime? PurchaseVoucherDetailsManufactureDate { get; set; }
        public DateTime? PurchaseVoucherDetailsExpiryDate { get; set; }
        public string PurchaseVoucherDetailsAssetAcc { get; set; }
        public int? PurchaseVoucherDetailsFsno { get; set; }
        public int? PurchaseVoucherDetailsLoacationId { get; set; }
        public decimal? PurchaseVoucherDetailsSalesPrice1 { get; set; }
        public decimal? PurchaseVoucherDetailsSalesPrice2 { get; set; }
        public decimal? PurchaseVoucherDetailsSalesPrice3 { get; set; }
        public int? PurchaseVoucherDetailsCompanyId { get; set; }
        public int? PurchaseVoucherDetailsLpoId { get; set; }
        public string PurchaseVoucherDetailsGrnNo { get; set; }
        public int? PurchaseVoucherDetailsPodId { get; set; }
        public bool? PurchaseVoucherDetailsIsEdit { get; set; }
        public decimal? PurchaseVoucherDetailsLandingCost { get; set; }
        public decimal? PurchaseVoucherDetailsLandingCostLocalCurrency { get; set; }
        public double? PurchaseVoucherDetailsFoc { get; set; }
        public decimal? PurchaseVoucherDetailsVatAmount { get; set; }
        public decimal? PurchaseVoucherDetailsFcSmRate { get; set; }
        public decimal? PurchaseVoucherDetailsFcSmAmount { get; set; }
        public decimal? PurchaseVoucherDetailsDiscountAmoutPurchase { get; set; }
        public string PurchaseVoucherDetailsItemName { get; set; }
        public decimal? PurchaseVoucherDetailsGrossAmount { get; set; }
        public decimal? PurchaseVoucherDetailsVatPercentage { get; set; }
        public decimal? PurchaseVoucherDetailsNetAmount { get; set; }
        public int? PurchaseVoucherDetailsRfqId { get; set; }
        public int? PurchaseVoucherDetailsRfqdId { get; set; }
        public int? PurchaseVoucherDetailsPrId { get; set; }
        public int? PurchaseVoucherDetailsPrdId { get; set; }
        public int? PurchaseVoucherDetailsPoId { get; set; }
        public int? PurchaseVoucherDetailsQtnId { get; set; }
        public int? PurchaseVoucherDetailsQtndId { get; set; }
        public int? Purchase_Voucher_Details_Job_ID { get; set; }
        public bool? PurchaseVoucherDetailsDelStatus { get; set; }
        [NotMapped]
        public virtual PurchaseVoucher PurchaseVoucherDetailsPurchase { get; set; }


        [NotMapped]
        public decimal? PurchaseReturnQtyTill { get; set; }
    }
}
