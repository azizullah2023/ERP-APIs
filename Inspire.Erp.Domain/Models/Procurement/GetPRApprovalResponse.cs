using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Procurement
{
 public   class GetPRApprovalResponse
    {
        public int PurchaseRequisitionPurchaseRequesId { get; set; }
        public DateTime? PurchaseRequisitionRequisitionDate { get; set; }
        public int? PurchaseRequisitionStaffCode { get; set; }
        public string PurchaseRequisitionRemarks { get; set; }
        public int? PurchaseRequisitionSupplierCode { get; set; }
        public int? PurchaseRequisitionJobId { get; set; }
        public string PurchaseRequisitionJobName { get; set; }
        public int? PurchaseRequisitionSubJobId { get; set; }
        public int? PurchaseRequisitionRequestedBy { get; set; }
        public bool? PurchaseRequisitionDelStatus { get; set; }
        public string PurchaseRequisitionStatus { get; set; }
        public DateTime? PurchaseRequisitionApprovedDate { get; set; }
        public int? PurchaseRequisitionApprovedBy { get; set; }
    }
}
