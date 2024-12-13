using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerPurchaseOrderDetails
    {
        public int CustomerPurchaseOrderDetailsId { get; set; }
        public int CustomerPurchaseOrderId { get; set; }
        public string CustomerPurchaseOrderDetailsVoucherNo { get; set; }
        public int? CustomerPurchaseOrderDetailsSlno { get; set; }
        public long? CustomerPurchaseOrderDetailsItemId { get; set; }
        public string CustomerPurchaseOrderDetailsPartNo { get; set; }
        public string CustomerPurchaseOrderDetailsDescription { get; set; }
        public int CustomerPurchaseOrderDetailsUnitId { get; set; }
        public decimal? CustomerPurchaseOrderDetailsQty { get; set; }
        public decimal? CustomerPurchaseOrderDetailsUnitPrice { get; set; }
        public decimal? CustomerPurchaseOrderDetailsAmount { get; set; }
        public decimal? CustomerPurchaseOrderDetailsFcAmount { get; set; }
        public bool? CustomerPurchaseOrderDetailsIsEdited { get; set; }
        public int? CustomerPurchaseOrderDetailsFsno { get; set; }
        public decimal? CustomerPurchaseOrderDetailsDeliverdQty { get; set; }
        public long? CustomerPurchaseOrderDetailsPodId { get; set; }
        public long? CustomerPurchaseOrderDetailsQuotationDetailsId { get; set; }
        public long? CustomerPurchaseOrderDetailsPodId2 { get; set; }
        public string CustomerPurchaseOrderDetailsRemarks { get; set; }
        public decimal? CustomerPurchaseOrderDetailsFocQty { get; set; }
        public bool? CustomerPurchaseOrderDetailsDelStatus { get; set; }
        [NotMapped]
        public virtual CustomerPurchaseOrder CustomerPurchaseOrderDetailsNoNavigation { get; set; }
    }
}
