using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempSalesJournalDetails
    {
        public int? TempSalesJournalDetailsId { get; set; }
        public int? TempSalesJournalDetailsSno { get; set; }
        public double? TempSalesJournalDetailsPack { get; set; }
        public double? TempSalesJournalDetailsAmtDh { get; set; }
        public double? TempSalesJournalDetailsAmtDollar { get; set; }
        public double? TempSalesJournalDetailsGrossWt { get; set; }
        public double? TempSalesJournalDetailsCargoValue { get; set; }
        public double? TempSalesJournalDetailsTotalAmtDh { get; set; }
        public double? TempSalesJournalDetailsTotAmtDollar { get; set; }
        public bool? TempSalesJournalDetailsDelStatus { get; set; }
    }
}
