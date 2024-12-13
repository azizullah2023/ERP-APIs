using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JournalVoucherDetails
    {
        public int JournalVoucherDetailsId { get; set; }
        public string JournalVoucherDetailVrefNo { get; set; }
        public int JournalVoucherID { get; set; }
        public string JournalVoucherDetailsAccNo { get; set; }
        public string JournalVoucherDetailsNarration { get; set; }
        public string JournalVoucherDetailsReferenceNo { get; set; }
        public decimal? JournalVoucherDetailsDrAmount { get; set; }
        public decimal? JournalVoucherDetailsFcDrAmount { get; set; }
        public decimal? JournalVoucherDetailsCrAmount { get; set; }
        public decimal? JournalVoucherDetailsFcCrAmount { get; set; }
        public long? JournalVoucherDetailsJobId { get; set; }
        public int? JournalVoucherDetailsCostCenterId { get; set; }
        public bool? JournalVoucherDetailsDelStatus { get; set; }

    }
}
