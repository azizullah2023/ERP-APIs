using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankPaymentVoucher
    {
        public string BankPaymentVoucherVoucherNo { get; set; }
        public DateTime? BankPaymentVoucherVDt { get; set; }
        public string BankPaymentVoucherOptType { get; set; }
        public double? BankPaymentVoucherCrAmount { get; set; }
        public double? BankPaymentVoucherFrCrAmount { get; set; }
        public double? BankPaymentVoucherDrAmount { get; set; }
        public double? BankPaymentVoucherFrDrAmount { get; set; }
        public string BankPaymentVoucherNarration { get; set; }
        public string BankPaymentVoucherStatus { get; set; }
        public int? BankPaymentVoucherUserId { get; set; }
        public int? BankPaymentVoucherCurrencyId { get; set; }
        public double? BankPaymentVoucherFcRate { get; set; }
        public string BankPaymentVoucherCrAcNo { get; set; }
        public int? BankPaymentVoucherFsno { get; set; }
        public string BankPaymentVoucherRefNo { get; set; }
        public int? BankPaymentVoucherAllocId { get; set; }
        public int? BankPaymentVoucherLocationId { get; set; }
        public bool? BankPaymentVoucherDelStatus { get; set; }

        [NotMapped]
        public virtual VouchersNumbers BankPaymentVoucherVoucherNoNavigation { get; set; }
        [NotMapped]
        public List<BankPaymentVoucherDetails> BankPaymentVoucherDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
        [NotMapped]
        public List<UserTrackingDisplay> UserTrackingData { get; set; }
        [NotMapped]
        public List<Domain.Models.AllocationDetails> AllocationDetails { get; set; }
    }
}
