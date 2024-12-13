using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobDetails
    {
        public int JobDetailsDetailsId { get; set; }
        public int? JobDetailsJobId { get; set; }
        public string JobDetailsDescription { get; set; }
        public int? JobDetailsStatus { get; set; }
        public string JobDetailsRefNo { get; set; }
        public DateTime? JobDetailsStatusDate { get; set; }
        public bool? JobDetailsIsEdit { get; set; }
        public int? JobDetailsUnitId { get; set; }
        public double? JobDetailsQtyN { get; set; }
        public int? JobDetailsDescriptionId { get; set; }
        public double? JobDetailsAmount { get; set; }
        public bool? JobDetailsDelStatus { get; set; }
    }
}
