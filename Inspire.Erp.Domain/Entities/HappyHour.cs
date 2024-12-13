using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class HappyHour
    {
        public int? HappyHourId { get; set; }
        public string HappyHourHappyHour { get; set; }
        public DateTime? HappyHourFromDate { get; set; }
        public DateTime? HappyHourToDate { get; set; }
        public string HappyHourRemarks { get; set; }
        public string HappyHourCreatdBy { get; set; }
        public string HappyHourEditedBy { get; set; }
        public int? HappyHourDays { get; set; }
        public string HappyHourExp1 { get; set; }
        public string HappyHourExp2 { get; set; }
        public string HappyHourExp3 { get; set; }
        public string HappyHourExp4 { get; set; }
        public bool? HappyHourDelStatus { get; set; }
    }
}
