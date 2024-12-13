using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobOrderDetails
    {
        public int? JobOrderDetailsDetailsId { get; set; }
        public int? JobOrderDetailsOrderId { get; set; }
        public int? JobOrderDetailsSlno { get; set; }
        public int? JobOrderDetailsItemId { get; set; }
        public int? JobOrderDetailsUnitId { get; set; }
        public double? JobOrderDetailsQty { get; set; }
        public string JobOrderDetailsRemarks { get; set; }
        public bool? JobOrderDetailsIsEdited { get; set; }
        public bool? JobOrderDetailsDelStatus { get; set; }
    }
}
