using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class WorkPeriods
    {
        public int WorkPeriodsId { get; set; }
        public string WorkPeriodsWorkPeriodName { get; set; }
        public DateTime? WorkPeriodsStartDate { get; set; }
        public DateTime? WorkPeriodsEndDate { get; set; }
        public TimeSpan? WorkPeriodsStartTime { get; set; }
        public TimeSpan? WorkPeriodsEndTime { get; set; }
        public string WorkPeriodsStatus { get; set; }
        public string WorkPeriodsStationId { get; set; }
        public string WorkPeriodsStationName { get; set; }
        public int? WorkPeriodsUserId { get; set; }
        public int? WorkPeriodsUserEnd { get; set; }
        public bool? WorkPeriodsDelStatus { get; set; }
    }
}
