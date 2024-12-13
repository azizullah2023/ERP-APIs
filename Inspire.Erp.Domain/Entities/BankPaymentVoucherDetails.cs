using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankPaymentVoucherDetails
    {
        public int BankPaymentVoucherDetailsId { get; set; }
        public string BankPaymentVoucherDetailsVoucherNo { get; set; }
        public int? BankPaymentVoucherDetailsSno { get; set; }
        public string BankPaymentVoucherDetailsAcNo { get; set; }
        public double? BankPaymentVoucherDetailsDrAmount { get; set; }
        public double? BankPaymentVoucherDetailsDrFcAmount { get; set; }
        public double? BankPaymentVoucherDetailsCrAmount { get; set; }
        public double? BankPaymentVoucherDetailsCrFcAmount { get; set; }
        public int? BankPaymentVoucherDetailsChequeNo { get; set; }
        public DateTime? BankPaymentVoucherDetailsChequeDate { get; set; }
        public string BankPaymentVoucherDetailsNarration { get; set; }
        public int? BankPaymentVoucherDetailsJobNo { get; set; }
        public int? BankPaymentVoucherDetailsCostCenterId { get; set; }
        public string BankPaymentVoucherDetailsInvNo { get; set; }
        public DateTime? BankPaymentVoucherDetailsInvDate { get; set; }
        public bool? BankPaymentVoucherDetailsPdc { get; set; }
        public int? BankPaymentVoucherDetailsPdcId { get; set; }
        public int? BankPaymentVoucherDetailsFsno { get; set; }
        public bool? BankPaymentVoucherDetailsDelStatus { get; set; }

        [JsonIgnore]
        public virtual VouchersNumbers BankPaymentVoucherDetailsVoucherNoNavigation { get; set; }
    }
}
