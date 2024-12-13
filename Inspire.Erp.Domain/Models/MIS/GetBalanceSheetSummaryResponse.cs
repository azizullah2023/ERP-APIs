using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.MIS
{
   public class GetBalanceSheetSummaryResponse
    {
        public string head { get; set; }
        public string accName { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string amount { get; set; }
        public List<SubHeadBalanceSheetSummary> __children { get; set; }
    }
    public class SubHeadBalanceSheetSummary
    {
        public string accName { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string amount { get; set; }
        public List<AccNameBalanceSheetSummary> __children { get; set; }
    }
    public class AccNameBalanceSheetSummary
    {
        public string accName { get; set; }
        public string debit { get; set; }
        public string credit { get; set; }
        public string amount { get; set; }
    }
}
