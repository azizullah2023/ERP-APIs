using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseJournalVoucherDetails
    {
        public int? PurchaseJournalVoucherDetailsJvid { get; set; }
        public int? PurchaseJournalVoucherDetailsAccSno { get; set; }
        public string PurchaseJournalVoucherDetailsAccNo { get; set; }
        public string PurchaseJournalVoucherDetailsDrCr { get; set; }
        public double? PurchaseJournalVoucherDetailsFcRate { get; set; }
        public double? PurchaseJournalVoucherDetailsFcAmount { get; set; }
        public double? PurchaseJournalVoucherDetailsAmount { get; set; }
        public string PurchaseJournalVoucherDetailsNarration { get; set; }
        public int? PurchaseJournalVoucherDetailsLocationId { get; set; }
        public string PurchaseJournalVoucherDetailsReference { get; set; }
        public int? PurchaseJournalVoucherDetailsJobId { get; set; }
        public int? PurchaseJournalVoucherDetailsFsno { get; set; }
        public int? PurchaseJournalVoucherDetailsSno { get; set; }
        public int? PurchaseJournalVoucherDetailsCostCenterId { get; set; }
        public bool? PurchaseJournalVoucherDetailsDelStatus { get; set; }
    }
}
