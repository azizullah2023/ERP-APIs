using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JournalSalesVoucher
    {
        public int JournalSalesVoucherSno { get; set; }
        public int? JournalSalesVoucherVid { get; set; }
        public string JournalSalesVoucherVreference { get; set; }
        public DateTime? JournalSalesVoucherDate { get; set; }
        public double? JournalSalesVoucherAmount { get; set; }
        public string JournalSalesVoucherNarration { get; set; }
        public int? JournalSalesVoucherFsno { get; set; }
        public string JournalSalesVoucherStatus { get; set; }
        public int? JournalSalesVoucherUserId { get; set; }
        public string JournalSalesVoucherType { get; set; }
        public string JournalSalesVoucherRefNo { get; set; }
        public double? JournalSalesVoucherOunce { get; set; }
        public double? JournalSalesVoucherNetWeight { get; set; }
        public double? JournalSalesVoucherCurrencyValue { get; set; }
        public double? JournalSalesVoucherMultiFactor { get; set; }
        public double? JournalSalesVoucherMakingCharge { get; set; }
        public int? JournalSalesVoucherSupplierId { get; set; }
        public bool? JournalSalesVoucherDelStatus { get; set; }
    }
}
