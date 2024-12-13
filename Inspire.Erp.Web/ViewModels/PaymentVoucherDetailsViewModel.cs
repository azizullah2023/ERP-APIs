using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class PaymentVoucherDetailsViewModel
    {
        public int PaymentVoucherDetailsId { get; set; }
        public string PaymentVoucherDetailsVoucherNo { get; set; }
        public int? PaymentVoucherDetailsSno { get; set; }
        public string PaymentVoucherDetailsAcNo { get; set; }
        public double? PaymentVoucherDetailsDbAmount { get; set; }
        public double? PaymentVoucherDetailsDbFcAmount { get; set; }
        public double? PaymentVoucherDetailsCrAmount { get; set; }
        public double? PaymentVoucherDetailsCrFcAmount { get; set; }
        public int? PaymentVoucherDetailsJobId { get; set; }
        public int? PaymentVoucherDetailsDepartmentId { get; set; }
        public string PaymentVoucherDetailsNarration { get; set; }
        public int? PaymentVoucherDetailsFsno { get; set; }
        public int? PaymentVoucherDetailsLocationId { get; set; }
        public bool? PaymentVocherDetailsDelStatus { get; set; }
    }
}
