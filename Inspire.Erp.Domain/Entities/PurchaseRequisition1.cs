using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseRequisition1
    {
        public PurchaseRequisition1()
        {
            PurchaseRequisitionDetails1 = new HashSet<PurchaseRequisitionDetails1>();
        }

        public decimal PurchaseRequisitionId { get; set; }
        public string PurchaseRequisitionNo { get; set; }
        public DateTime? PurchaseRequisitionDate { get; set; }
        public string PurchaseRequisitionType { get; set; }
        public decimal? PurchaseRequisitionPartyId { get; set; }
        public string PurchaseRequisitionPartyName { get; set; }
        public string PurchaseRequisitionPartyAddress { get; set; }
        public string PurchaseRequisitionPartyVatNo { get; set; }
        public string PurchaseRequisitionRefNo { get; set; }
        public string PurchaseRequisitionSupInvNo { get; set; }
        public string PurchaseRequisitionGrno { get; set; }
        public DateTime? PurchaseRequisitionGrdate { get; set; }
        public string PurchaseRequisitionLpono { get; set; }
        public DateTime? PurchaseRequisitionLpodate { get; set; }
        public string PurchaseRequisitionQtnNo { get; set; }
        public DateTime? PurchaseRequisitionQtnDate { get; set; }
        public string PurchaseRequisitionDescription { get; set; }
        public bool? PurchaseRequisitionExcludeVat { get; set; }
        public string PurchaseRequisitionPono { get; set; }
        public string PurchaseRequisitionBatchCode { get; set; }
        public string PurchaseRequisitionDayBookNo { get; set; }
        public int? PurchaseRequisitionLocationId { get; set; }
        public long? PurchaseRequisitionUserId { get; set; }
        public int? PurchaseRequisitionCurrencyId { get; set; }
        public int? PurchaseRequisitionCompanyId { get; set; }
        public long? PurchaseRequisitionJobId { get; set; }
        public decimal? PurchaseRequisitionFsno { get; set; }
        public decimal? PurchaseRequisitionFcRate { get; set; }
        public string PurchaseRequisitionStatus { get; set; }
        public decimal? PurchaseRequisitionTotalGrossAmount { get; set; }
        public decimal? PurchaseRequisitionTotalItemDisAmount { get; set; }
        public decimal? PurchaseRequisitionTotalActualAmount { get; set; }
        public decimal? PurchaseRequisitionTotalDisPer { get; set; }
        public decimal? PurchaseRequisitionTotalDisAmount { get; set; }
        public decimal? PurchaseRequisitionVatAmt { get; set; }
        public decimal? PurchaseRequisitionVatPer { get; set; }
        public string PurchaseRequisitionVatRoundSign { get; set; }
        public decimal? PurchaseRequisitionVatRountAmt { get; set; }
        public decimal? PurchaseRequisitionNetDisAmount { get; set; }
        public decimal? PurchaseRequisitionNetAmount { get; set; }
        public decimal? PurchaseRequisitionTransportCost { get; set; }
        public decimal? PurchaseRequisitionHandlingcharges { get; set; }
        public string PurchaseRequisitionIssueId { get; set; }
        public bool? PurchaseRequisitionJobDirectPur { get; set; }
        public bool? PurchaseRequisitionDelStatus { get; set; }
        public DateTime? PurchaseRequisitionDetailsReqDate { get; set; }
        public string PurchaseRequisitionDetailsReqStatus { get; set; }

        public virtual ICollection<PurchaseRequisitionDetails1> PurchaseRequisitionDetails1 { get; set; }
    }
}
