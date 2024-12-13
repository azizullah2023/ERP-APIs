using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempPlforecast
    {
        public string TempPlforecastMainHead { get; set; }
        public string TempPlforecastSubHead { get; set; }
        public string TempPlforecastAccNo { get; set; }
        public string TempPlforecastAccName { get; set; }
        public double? TempPlforecastMonthToday { get; set; }
        public double? TempPlforecastYearToday { get; set; }
        public bool? TempPlforecastDelStatus { get; set; }
    }
}
