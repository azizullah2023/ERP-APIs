using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Procurement
{
   public class PurchaseRequisitionStatus
    {

        public PurchaseRequisition PurchaseRequisition { get; set; }
        public PurchaseRequisitionDetails PurchaseRequisitionDetails { get; set; }
        public string PartNo { get; set; }
        public string MaterialName { get; set; }
        public string MaterailUnit { get; set; }
        public decimal? StockQty { get; set; }
        public decimal? PurchaseOrderQty { get; set; }
        public decimal BalanceToOrderQty { get; set; }
        public double? IssueQty { get; set; }
        public decimal? BalanceToIssueQty { get; set; }
        public string POStatus { get; set; }
    }
}
