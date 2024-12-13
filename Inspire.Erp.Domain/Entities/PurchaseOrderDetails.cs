using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseOrderDetails
    {
        public decimal PurchaseOrderDetailsId { get; set; }
        public int? Purchase_Order_Details_Job_Id { get; set; }
        
        public decimal? PurchaseOrderId { get; set; }
        public string PurchaseOrderDetailsNo { get; set; }
        public decimal? PurchaseOrderDetailsSno { get; set; }
        public int? PurchaseOrderDetailsMatId { get; set; }
        public string PurchaseOrderDetailsItemName { get; set; }
        public decimal? PurchaseOrderDetailsUnitId { get; set; }
        public string PurchaseOrderDetailsUnitName { get; set; }
        public string PurchaseOrderDetailsBatchCode { get; set; }
        public DateTime? PurchaseOrderDetailsManfDate { get; set; }
        public DateTime? PurchaseOrderDetailsExpDate { get; set; }
        public decimal? PurchaseOrderDetailsQuantity { get; set; }
        public decimal? PurchaseOrderDetailsDelQty { get; set; }
        public decimal? PurchaseOrderDetailsRate { get; set; }
        public decimal? PurchaseOrderDetailsGrossAmount { get; set; }
        public decimal? PurchaseOrderDetailsDiscAmount { get; set; }
        public decimal? PurchaseOrderDetailsActualAmount { get; set; }
        public decimal? PurchaseOrderDetailsVatPer { get; set; }
        public decimal? PurchaseOrderDetailsVatAmt { get; set; }
        public decimal? PurchaseOrderDetailsNetAmt { get; set; }
        public string PurchaseOrderDetailsRemarks { get; set; }
        public bool? PurchaseOrderDetailsIsEdit { get; set; }
        public int? PurchaseOrderDetailsRfqId { get; set; }
        public int? PurchaseOrderDetailsRfqdId { get; set; }
        public int? PurchaseOrderDetailsPrId { get; set; }
        public int? PurchaseOrderDetailsPrdId { get; set; }
        public int? PurchaseOrderDetailsPoId { get; set; }
        public int? PurchaseOrderDetailsPodId { get; set; }
        public int? PurchaseOrderDetailsQtnId { get; set; }
        public int? PurchaseOrderDetailsQtndId { get; set; }
        public decimal? PurchaseOrderDetailsSalesPrice { get; set; }
        public bool? PurchaseOrderDetailsDelStatus { get; set; }

        ////public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual PurchaseOrder PurchaseOrderDetailsNoNavigation { get; set; }
        [NotMapped]
        public decimal? PurchaseOrderNetStock { get; set; }

        [NotMapped]
        public decimal? PurchaseOrderTillNowQTY { get; set; }
    }
}
