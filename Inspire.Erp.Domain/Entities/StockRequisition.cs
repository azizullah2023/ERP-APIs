using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockRequisition
    {
        public StockRequisition()
        {
            StockRequisitionDetails = new HashSet<StockRequisitionDetails>();
        }

        public decimal StockRequisitionId { get; set; }
        public string? StockRequisitionNo { get; set; }
        public DateTime StockRequisitionDate { get; set; }
        public string? StockRequisitionType { get; set; }
        public long? StockRequisitionJobId { get; set; }
        public string? StockRequisitionStatus { get; set; }
        public bool? StockRequisitionDelStatus { get; set; }
        public DateTime? StockRequisitionDetailsReqDate { get; set; }
        public string? StockRequisitionDetailsReqStatus { get; set; }
        public int? StockRequisitionApprovedBy { get; set; }
        public DateTime? StockRequisitionApprovedDate { get; set; }
        public string? StockRequisitionRemarks { get; set; }
        public int? StockRequisitionRequestedBy { get; set; }
        public virtual ICollection<StockRequisitionDetails> StockRequisitionDetails { get; set; }
    }
}
