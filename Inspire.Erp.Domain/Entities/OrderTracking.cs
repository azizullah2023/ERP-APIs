using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OrderTracking
    {
        public int OrderTrackingId { get; set; }
        public int? OrderTrackingMaterialId { get; set; }
        public DateTime? OrderTrackingVoucherDate { get; set; }
        public string OrderTrackingVoucherNo { get; set; }
        public string OrderTrackingVoucherType { get; set; }
        public double? OrderTrackingQty { get; set; }
        public double? OrderTrackingInQty { get; set; }
        public double? OrderTrackingOutOty { get; set; }
        public int? OrderTrackingUnit { get; set; }
        public int? OrderTrackingLocationId { get; set; }
        public int? OrderTrackingJobId { get; set; }
        public int? OrderTrackingFsno { get; set; }
        public bool? OrderTrackingDelStatus { get; set; }
    }
}
