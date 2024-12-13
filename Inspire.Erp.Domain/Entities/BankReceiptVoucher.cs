using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankReceiptVoucher
    {
        public string BankReceiptVoucherVoucherNo { get; set; }
        public DateTime? BankReceiptVoucherVDate { get; set; }
        public string BankReceiptVoucherDrAcNo { get; set; }
        public double? BankReceiptVoucherDrAmount { get; set; }
        public double? BankReceiptVoucherFrDrAmount { get; set; }
        public double? BankReceiptVoucherCrAmount { get; set; }
        public double? BankReceiptVoucherFrCrAmount { get; set; }
        public string BankReceiptVoucherNarration { get; set; }
        public string BankReceiptVoucherStatus { get; set; }
        public string BankReceiptVoucherRefNo { get; set; }
        public int? BankReceiptVoucherUserId { get; set; }
        public int? BankReceiptVoucherCurrencyId { get; set; }
        public double? BankReceiptVoucherFcRate { get; set; }
        public int? BankReceiptVoucherFsno { get; set; }
        public int? BankReceiptVoucherAllocId { get; set; }
        public bool? BankReceiptVoucherDelStatus { get; set; }
        public int? BankReceiptVoucherLocationId { get; set; }
        [NotMapped]
        public virtual VouchersNumbers BankReceiptVoucherVoucherNoNavigation { get; set; }
        [NotMapped]
        public List<BankReceiptVoucherDetails> BankReceiptVoucherDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
        [NotMapped]
        public List<UserTrackingDisplay> UserTrackingData { get; set; }
        [NotMapped]
        public List<Domain.Models.AllocationDetails> AllocationDetails { get; set; }
    }
}
