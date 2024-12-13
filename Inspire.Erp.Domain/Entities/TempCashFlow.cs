using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempCashFlow
    {
        public int? TempCashFlowId { get; set; }
        public string TempCashFlowAccNo { get; set; }
        public string TempCashFlowAccName { get; set; }
        public DateTime? TempCashFlowTransDate { get; set; }
        public double? TempCashFlowCashinDebit { get; set; }
        public double? TempCashFlowCashOutCredit { get; set; }
        public double? TempCashFlowBal { get; set; }
        public double? TempCashFlowRunBal { get; set; }
        public bool? TempCashFlowDelStatus { get; set; }
    }
}
