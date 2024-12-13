using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobCastDetailsSummary
    {

        public string? AcHead { get; set; }
        public string? AcNo { get; set; }
        public string? VNo { get; set; }
        public string? VType { get; set; }
        public string? CostCenter { get; set; }
        public string? Particular { get; set; }
        public string? Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RunningBal { get; set; }

        public string? JobNo { get; set; }
        public string? JobValue { get; set; }
        public int? Material { get; set; }
        public string? Labour { get; set; }
        public string? OverHead { get; set; }
        public string? Miscellaneous { get; set; }
        public decimal? TotCost { get; set; }
        public decimal? Profit { get; set; }
        public decimal? InvoiceTot { get; set; }

    }
    public class JobCastFilterModel
    {
        public string? JobNo { get; set; }
        public long? JobLocId { get; set; }
        public bool IsDateChecked { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public bool? IsSummaryChecked { get; set; }

    }
}

