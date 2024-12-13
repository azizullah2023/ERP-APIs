using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseReturnVoucherDetails
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
        public bool? PurchaseReturnVoucherDetailsDelStatus { get; set; }
    }
}
