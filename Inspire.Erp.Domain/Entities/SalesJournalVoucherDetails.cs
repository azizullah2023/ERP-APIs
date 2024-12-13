using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesJournalVoucherDetails
    {
        public int? SalesJournalVoucherDetailsId { get; set; }
        public int? SalesJournalVoucherDetailsAccSno { get; set; }
        public string SalesJournalVoucherDetailsAccNo { get; set; }
        public string SalesJournalVoucherDetailsDrCr { get; set; }
        public double? SalesJournalVoucherDetailsFcRate { get; set; }
        public double? SalesJournalVoucherDetailsFcAmount { get; set; }
        public double? SalesJournalVoucherDetailsAmount { get; set; }
        public string SalesJournalVoucherDetailsNarration { get; set; }
        public int? SalesJournalVoucherDetailsLocationId { get; set; }
        public string SalesJournalVoucherDetailsReference { get; set; }
        public int? SalesJournalVoucherDetailsJobId { get; set; }
        public int? SalesJournalVoucherDetailsFsno { get; set; }
        public int? SalesJournalVoucherDetailsSno { get; set; }
        public int? SalesJournalVoucherDetailsCostCenterId { get; set; }
        public bool? SalesJournalVoucherDetailsDelStatus { get; set; }
    }
}
