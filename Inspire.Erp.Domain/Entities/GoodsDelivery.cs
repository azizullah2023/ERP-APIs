using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GoodsDelivery
    {
        public int? GoodsDeliveryId { get; set; }
        public DateTime? GoodsDeliveryDateTime { get; set; }
        public int? GoodsDeliveryCpoId { get; set; }
        public string GoodsDeliveryReceivedBy { get; set; }
        public string GoodsDeliveryRemarks { get; set; }
        public int? GoodsDeliveryFsno { get; set; }
        public bool? GoodsDeliveryDelStatus { get; set; }
    }
}
