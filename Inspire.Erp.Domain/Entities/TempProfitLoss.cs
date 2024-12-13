using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempProfitLoss
    {
        public string TempProfitLossMainHead { get; set; }
        public string TempProfitLossSubHead { get; set; }
        public string TempProfitLossAcNo { get; set; }
        public string TempProfitLossAcName { get; set; }
        public double? TempProfitLossTotalDebit { get; set; }
        public double? TempProfitLossTotalCredit { get; set; }
        public bool? TempProfitLossDelStatus { get; set; }
    }
}
