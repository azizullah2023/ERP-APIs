using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseReturnVoucher
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
    }
}
