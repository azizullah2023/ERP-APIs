using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseJournalVoucher
    {
        public int PurchaseJournalVoucherJvsno { get; set; }
        public int? PurchaseJournalVoucherJvid { get; set; }
        public string PurchaseJournalVoucherJvidRef { get; set; }
        public DateTime? PurchaseJournalVoucherJvDate { get; set; }
        public double? PurchaseJournalVoucherJvAmount { get; set; }
        public string PurchaseJournalVoucherNarration { get; set; }
        public int? PurchaseJournalVoucherFsno { get; set; }
        public string PurchaseJournalVoucherStatus { get; set; }
        public int? PurchaseJournalVoucherUserId { get; set; }
        public string PurchaseJournalVoucherJvtype { get; set; }
        public int? PurchaseJournalVoucherLocationId { get; set; }
        public string PurchaseJournalVoucherRefNo { get; set; }
        public int? PurchaseJournalVoucherJobNo { get; set; }
        public int? PurchaseJournalVoucherCostCenterId { get; set; }
        public int? PurchaseJournalVoucherCurrencyId { get; set; }
        public bool? PurchaseJournalVoucherDelStatus { get; set; }
    }
}
