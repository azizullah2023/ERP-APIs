using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Procurement
{
    public class PurchaseOrderReport
    {
        public string PONO { get; set; }
        public DateTime? Date { get; set; }
        public string Supplier { get; set; }
        public string JobNumner { get; set; }
        public string ProjectName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }

    public class PurchaseOrderList
    {
        public decimal ID { get; set; }
        public DateTime? LPODate { get; set; }
        public string PONO{ get; set; }
        public string SupplierName { get; set; }
        public string JobName { get; set; }
        public string PartNo { get; set; }
        public string ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }
}
