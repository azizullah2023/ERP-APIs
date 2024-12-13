using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerOrderDetails
    {
        public int? CustomerOrderDetailsId { get; set; }
        public int? CustomerOrderDetailsSno { get; set; }
        public string CustomerOrderDetailsDescription { get; set; }
        public string CustomerOrderDetailsMaterialDescription { get; set; }
        public double? CustomerOrderDetailsQuantity { get; set; }
        public double? CustomerOrderDetailsRate { get; set; }
        public double? CustomerOrderDetailsAmount { get; set; }
        public double? CustomerOrderDetailsFcAmount { get; set; }
        public int? CustomerOrderDetailsUnitId { get; set; }
        public int? CustomerOrderDetailsMaterialId { get; set; }
        public bool? CustomerOrderDetailsDelStatus { get; set; }
    }
}
