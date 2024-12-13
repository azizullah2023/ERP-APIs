using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PaymentVoucher
    {
        public int PaymentVoucherSno { get; set; }
        public string PaymentVoucherVoucherNo { get; set; }
        public DateTime? PaymentVoucherDate { get; set; }
        public string PaymentVoucherVoucherRef { get; set; }
        public string PaymentVoucherCrAcNo { get; set; }
        public double? PaymentVoucherCrAmount { get; set; }
        public double? PaymentVoucherFcCrAmount { get; set; }
        public double? PaymentVoucherDbAmount { get; set; }
        public double? PaymentVoucherFcDbAmount { get; set; }
        public string PaymentVoucherNarration { get; set; }
        public int? PaymentVoucherFsno { get; set; }
        public int? PaymentVoucherUserId { get; set; }
        public int? PaymentVoucherCurrencyId { get; set; }
        public int? PaymentVoucherLocationId { get; set; }
        public int? PaymentVoucherAllocationId { get; set; }
        public bool? PaymentVoucherDelStatus { get; set; }
        [JsonIgnore]
        public virtual VouchersNumbers PaymentVoucherVoucherNoNavigation { get; set; }
        [NotMapped]
        public List<PaymentVoucherDetails> PaymentVoucherDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; } 
        [NotMapped]
        public List<AccountsTransactions> PaymentAllocationDetails { get; set; }
        [NotMapped]
        public  List<Domain.Models.AllocationDetails> allocationDetails { get; set; }

    }
}
