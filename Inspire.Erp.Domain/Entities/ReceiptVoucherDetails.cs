using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReceiptVoucherDetails
    {
        public int ReceiptVoucherDetailsId { get; set; }
        public string ReceiptVoucherDetailsVoucherNo { get; set; }
        public int? ReceiptVoucherDetailsSlNo { get; set; }
        public string ReceiptVoucherDetailsCrAcNo { get; set; }
        public double? ReceiptVoucherDetailsCrAmount { get; set; }
        public double? ReceiptVoucherDetailsFcCrAmount { get; set; }
        public double? ReceiptVoucherDetailsDbAmount { get; set; }
        public int? ReceiptVoucherDetailsJobId { get; set; }
        public int? ReceiptVoucherDetailsDepId { get; set; }
        public string ReceiptVoucherDetailsNarration { get; set; }
        public int? ReceiptVoucherDetailsFsno { get; set; }
        public bool? ReceiptVoucherDetailsDelStatus { get; set; }
        [JsonIgnore]
        public virtual VouchersNumbers ReceiptVoucherDetailsVoucherNoNavigation { get; set; }
    }
}
