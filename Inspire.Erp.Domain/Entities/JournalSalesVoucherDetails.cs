using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JournalSalesVoucherDetails
    {
        public int? JournalSalesVoucherDetailsId { get; set; }
        public int? JournalSalesVoucherDetailsAccSno { get; set; }
        public string JournalSalesVoucherDetailsAccNo { get; set; }
        public string JournalSalesVoucherDetailsDrCr { get; set; }
        public double? JournalSalesVoucherDetailsFcRate { get; set; }
        public double? JournalSalesVoucherDetailsFcAmount { get; set; }
        public double? JournalSalesVoucherDetailsAmount { get; set; }
        public string JournalSalesVoucherDetailsNarration { get; set; }
        public int? JournalSalesVoucherDetailsLocationId { get; set; }
        public string JournalSalesVoucherDetailsReference { get; set; }
        public int? JournalSalesVoucherDetailsJobId { get; set; }
        public int? JournalSalesVoucherDetailsFsno { get; set; }
        public int? JournalSalesVoucherDetailsSno { get; set; }
        public double? JournalSalesVoucherDetailsDrCrGram { get; set; }
        public bool? JournalSalesVoucherDetailsDelStatus { get; set; }
    }
}
