using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseQuotationDetails
    {
        public decimal PurchaseQuotationDetailsId { get; set; }
        public decimal? PurchaseQuotationId { get; set; }
        public string PurchaseQuotationDetailsNo { get; set; }
        public decimal? PurchaseQuotationDetailsSno { get; set; }
        public decimal? PurchaseQuotationDetailsMatId { get; set; }
        public string PurchaseQuotationDetailsItemName { get; set; }
        public decimal? PurchaseQuotationDetailsUnitId { get; set; }
        public string PurchaseQuotationDetailsUnitName { get; set; }
        public string PurchaseQuotationDetailsBatchCode { get; set; }
        public DateTime? PurchaseQuotationDetailsManfDate { get; set; }
        public DateTime? PurchaseQuotationDetailsExpDate { get; set; }
        public decimal? PurchaseQuotationDetailsQuantity { get; set; }
        public decimal? PurchaseQuotationDetailsRate { get; set; }
        public decimal? PurchaseQuotationDetailsGrossAmount { get; set; }
        public decimal? PurchaseQuotationDetailsDiscAmount { get; set; }
        public decimal? PurchaseQuotationDetailsActualAmount { get; set; }
        public decimal? PurchaseQuotationDetailsVatPer { get; set; }
        public decimal? PurchaseQuotationDetailsVatAmt { get; set; }
        public decimal? PurchaseQuotationDetailsNetAmt { get; set; }
        public string PurchaseQuotationDetailsRemarks { get; set; }
        public bool? PurchaseQuotationDetailsIsEdit { get; set; }
        public int? PurchaseQuotationDetailsRfqId { get; set; }
        public int? PurchaseQuotationDetailsRfqdId { get; set; }
        public int? PurchaseQuotationDetailsPrId { get; set; }
        public int? PurchaseQuotationDetailsPrdId { get; set; }
        public int? PurchaseQuotationDetailsPoId { get; set; }
        public int? PurchaseQuotationDetailsPodId { get; set; }
        public int? PurchaseQuotationDetailsQtnId { get; set; }
        public int? PurchaseQuotationDetailsQtndId { get; set; }
        public decimal? PurchaseQuotationDetailsSalesPrice { get; set; }
        public bool? PurchaseQuotationDetailsDelStatus { get; set; }

        public virtual PurchaseQuotation PurchaseQuotationDetailsNoNavigation { get; set; }
    }
}
