using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseRequisitionDetails1
    {
        public decimal PurchaseRequisitionDetailsId { get; set; }
        public decimal? PurchaseRequisitionId { get; set; }
        public string PurchaseRequisitionDetailsNo { get; set; }
        public decimal? PurchaseRequisitionDetailsSno { get; set; }
        public decimal? PurchaseRequisitionDetailsMatId { get; set; }
        public string PurchaseRequisitionDetailsItemName { get; set; }
        public decimal? PurchaseRequisitionDetailsUnitId { get; set; }
        public string PurchaseRequisitionDetailsUnitName { get; set; }
        public string PurchaseRequisitionDetailsBatchCode { get; set; }
        public DateTime? PurchaseRequisitionDetailsManfDate { get; set; }
        public DateTime? PurchaseRequisitionDetailsExpDate { get; set; }
        public decimal? PurchaseRequisitionDetailsQuantity { get; set; }
        public decimal? PurchaseRequisitionDetailsRate { get; set; }
        public decimal? PurchaseRequisitionDetailsGrossAmount { get; set; }
        public decimal? PurchaseRequisitionDetailsDiscAmount { get; set; }
        public decimal? PurchaseRequisitionDetailsActualAmount { get; set; }
        public decimal? PurchaseRequisitionDetailsVatPer { get; set; }
        public decimal? PurchaseRequisitionDetailsVatAmt { get; set; }
        public decimal? PurchaseRequisitionDetailsNetAmt { get; set; }
        public string PurchaseRequisitionDetailsRemarks { get; set; }
        public bool? PurchaseRequisitionDetailsIsEdit { get; set; }
        public int? PurchaseRequisitionDetailsRfqId { get; set; }
        public int? PurchaseRequisitionDetailsRfqdId { get; set; }
        public int? PurchaseRequisitionDetailsPrId { get; set; }
        public int? PurchaseRequisitionDetailsPrdId { get; set; }
        public int? PurchaseRequisitionDetailsPoId { get; set; }
        public int? PurchaseRequisitionDetailsPodId { get; set; }
        public int? PurchaseRequisitionDetailsQtnId { get; set; }
        public int? PurchaseRequisitionDetailsQtndId { get; set; }
        public decimal? PurchaseRequisitionDetailsSalesPrice { get; set; }
        public DateTime? PurchaseRequisitionDetailsReqDate { get; set; }
        public string PurchaseRequisitionDetailsReqStatus { get; set; }
        public bool? PurchaseRequisitionDetailsDelStatus { get; set; }

        public virtual PurchaseRequisition1 PurchaseRequisitionDetailsNoNavigation { get; set; }
    }
}
