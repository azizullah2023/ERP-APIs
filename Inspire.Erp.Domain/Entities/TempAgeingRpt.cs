using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempAgeingRpt
    {
        public string TempAgeingRptName { get; set; }
        public double? TempAgeingRptCreditLimit { get; set; }
        public double? TempAgeingRptA030days { get; set; }
        public double? TempAgeingRptA3160days { get; set; }
        public double? TempAgeingRptA6190days { get; set; }
        public double? TempAgeingRptA91180days { get; set; }
        public double? TempAgeingRptA181270days { get; set; }
        public double? TempAgeingRptA271360days { get; set; }
        public double? TempAgeingRptOver360Days { get; set; }
        public double? TempAgeingRptCurBalance { get; set; }
        public double? TempAgeingRptUnallocBal { get; set; }
        public double? TempAgeingRptOutstandingBal { get; set; }
        public string TempAgeingRptAccNo { get; set; }
        public bool? TempAgeingRpt1 { get; set; }
    }
}
