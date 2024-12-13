using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class HappyHourDeatils
    {
        public int HappyHourDeatilsId { get; set; }
        public int? HappyHourDeatilsHappyHourId { get; set; }
        public int? HappyHourDeatilsItemId { get; set; }
        public DateTime? HappyHourDeatilsStartDate { get; set; }
        public DateTime? HappyHourDeatilsEndDate { get; set; }
        public bool? HappyHourDetailsDelStatus { get; set; }
    }
}
