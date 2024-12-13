using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempJobCostSummary
    {
        public string TempJobCostSummaryJobNo { get; set; }
        public double? TempJobCostSummaryJobValue { get; set; }
        public double? TempJobCostSummaryMatCost { get; set; }
        public double? TempJobCostSummaryLabourCost { get; set; }
        public double? TempJobCostSummaryOverHead { get; set; }
        public double? TempJobCostSummaryTotCost { get; set; }
        public double? TempJobCostSummaryProfit { get; set; }
        public double? TempJobCostSummaryInvoiceTotal { get; set; }
        public bool? TempJobCostSummaryDelStatus { get; set; }
    }
}
