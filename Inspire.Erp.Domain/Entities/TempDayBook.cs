using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempDayBook
    {
        public int? TempDayBookSlNo { get; set; }
        public string TempDayBookVno { get; set; }
        public DateTime? TempDayBookVdate { get; set; }
        public string TempDayBookParticulars { get; set; }
        public string TempDayBookDescription { get; set; }
        public string TempDayBookRefNo { get; set; }
        public double? TempDayBookDebit { get; set; }
        public double? TempDayBookCredit { get; set; }
        public bool? TempDayBookDelStatus { get; set; }
    }
}
