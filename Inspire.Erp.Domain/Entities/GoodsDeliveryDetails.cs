using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GoodsDeliveryDetails
    {
        public int? GoodsDeliveryDetailsDetailsId { get; set; }
        public int? GoodsDeliveryDetailsDeliveryId { get; set; }
        public int? GoodsDeliveryDetailsSlno { get; set; }
        public int? GoodsDeliveryDetailsItemId { get; set; }
        public int? GoodsDeliveryDetailsUnitId { get; set; }
        public double? GoodsDeliveryDetailsQty { get; set; }
        public string GoodsDeliveryDetailsNarration { get; set; }
        public int? GoodsDeliveryDetailsFsno { get; set; }
        public bool? GoodsDeliveryDetailsDelStatus { get; set; }
    }
}
