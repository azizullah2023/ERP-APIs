using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking
{
   public class POTrackingSummary : POTrackingCommon
    {
        public DateTime? DeliveryFrom { get; set; }
        public DateTime? DeliveryTo { get; set; }
        public string Status { get; set; }
    }
}
