using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
  public  class GetCustomerPurchaseOrderTrackingResponse
    {
        public int CPOId { get; set; }

        public int CPODId { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? CPODate { get; set; }
        public string LPONo { get; set; }
        public string CustomerName { get; set; }
        public string PartNo { get; set; }
        public string MatDes { get; set; }
        public string UnitDes { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DeliveredQuantity { get; set; }
        public decimal? BalancedQuantity { get; set; }
        public decimal? StockQuantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? DeliveredId { get; set; }
     
    }
  
}
