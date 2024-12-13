using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesJournalVoucher
    {
        public int? SalesJournalVoucherSno { get; set; }
        public string SalesJournalVoucherId { get; set; }
        public string SalesJournalVoucherIdRef { get; set; }
        public DateTime? SalesJournalVoucherDate { get; set; }
        public double? SalesJournalVoucherAmount { get; set; }
        public string SalesJournalVoucherNarration { get; set; }
        public int? SalesJournalVoucherFsno { get; set; }
        public string SalesJournalVoucherStatus { get; set; }
        public int? SalesJournalVoucherUserId { get; set; }
        public string SalesJournalVoucherJvtype { get; set; }
        public int? SalesJournalVoucherLocationId { get; set; }
        public string SalesJournalVoucherRefNo { get; set; }
        public int? SalesJournalVoucherJobNo { get; set; }
        public int? SalesJournalVoucherCostCenterId { get; set; }
        public int? SalesJournalVoucherCurrencyId { get; set; }
        public bool? SalesJournalVoucherDelStatus { get; set; }
    }
}
