using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobOrder
    {
        public int? JobOrderId { get; set; }
        public int? JobOrderCpoId { get; set; }
        public DateTime? JobOrderJobStartDateTime { get; set; }
        public DateTime? JobOrderExpectedEndDateTime { get; set; }
        public int? JobOrderStaffCode { get; set; }
        public string JobOrderRemarks { get; set; }
        public bool? JobOrderDelStatus { get; set; }
    }
}
