using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Procurement
{
   public class AddEditPurchaseReturnResponse
    {
        public int PurchaseReturnVoucherId { get; set; }
        public string PurchaseReturnVoucherRetId { get; set; }
        public DateTime? PurchaseReturnVoucherRetDate { get; set; }
        public string PurchaseReturnVoucherVoucherId { get; set; }
        public DateTime? PurchaseReturnVoucherVoucherDate { get; set; }
        public string PurchaseReturnVoucherSupInvNo { get; set; }
        public string PurchaseReturnVoucherVoucherType { get; set; }
        public int? PurchaseReturnVoucherSpId { get; set; }
        public int? PurchaseReturnVoucherLocationId { get; set; }
        public double? PurchaseReturnVoucherGrossAmount { get; set; }
        public double? PurchaseReturnVoucherDiscount { get; set; }
        public int? PurchaseReturnVoucherDetailsJobId { get; set; }
        public double? PurchaseReturnVoucherFcDiscAmount { get; set; }
        public double? PurchaseReturnVoucherNetAmount { get; set; }
        public double? PurchaseReturnVoucherFcNetAmount { get; set; }
        public string PurchaseReturnVoucherNarration { get; set; }
        public double? PurchaseReturnVoucherTransportCost { get; set; }
        public double? PurchaseReturnVoucherHandlingCharges { get; set; }
        public double? PurchaseReturnVoucherFcRate { get; set; }
        public int? PurchaseReturnVoucherUserId { get; set; }
        public int? PurchaseReturnVoucherFsno { get; set; }
        public string PurchaseReturnVoucherStatus { get; set; }
        public string PurchaseReturnVoucherGrno { get; set; }
        public double? PurchaseReturnVoucherVatAmount { get; set; }
        public double? PurchaseReturnVoucherVatPercentage { get; set; }
        public string PurchaseReturnVoucherVatRoundSign { get; set; }
        public double? PurchaseReturnVoucherVatRoundAmt { get; set; }
        public bool? PurchaseReturnVoucherExcludeVat { get; set; }
        public string PurchaseReturnVoucherVatNo { get; set; }
        public bool? PurchaseReturnVoucherDelStatus { get; set; }
        public List<AddEditPurchaseReturnDetailResponse> PurchaseReturnVoucherDetails { get; set; }
    }
    public class AddEditPurchaseReturnDetailResponse
    {
        public string PurchaseReturnVoucherDetailsRetId { get; set; }
        public string PurchaseReturnVoucherDetailsVoucherNo { get; set; }
        public int? PurchaseReturnVoucherDetailsSno { get; set; }
        public int? PurchaseReturnVoucherDetailsMaterialId { get; set; }
        public double? PurchaseReturnVoucherDetailsRate { get; set; }
        public string PurchaseReturnVoucherDetailsBatchCode { get; set; }
        public double? PurchaseReturnVoucherDetailsQty { get; set; }
        public double? PurchaseReturnVoucherDetailsAmount { get; set; }
        public double? PurchaseReturnVoucherDetailsFcAmount { get; set; }
        public string PurchaseReturnVoucherDetailsRemarks { get; set; }
        public int? PurchaseReturnVoucherDetailsUnitId { get; set; }
        public int? PurchaseReturnVoucherDetailsJobId { get; set; }
        public double? PurchaseReturnVoucherDetailsPurQty { get; set; }
        public DateTime? PurchaseReturnVoucherDetailsExpDate { get; set; }
        public double? PurchaseReturnVoucherDetailsFocQty { get; set; }
        public double? PurchaseReturnVoucherDetailsVatPercetage { get; set; }
        public double? PurchaseReturnVoucherDetailsVatAmount { get; set; }
        public string PurchaseReturnVoucherDetailsBatch { get; set; }
        public double? PurchaseReturnVoucherDetailsAmountNew { get; set; }
        public bool? PurchaseReturnVoucherDetailsDelStatus { get; set; }
    }
}
