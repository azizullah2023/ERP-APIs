using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public partial class StockRequisitionDetailsViewModel
    {
        public decimal StockRequisitionDetailsId { get; set; }
        public decimal? StockRequisitionId { get; set; }
        public string StockRequisitionDetailsNo { get; set; }
        public decimal? StockRequisitionDetailsSno { get; set; }
        public decimal? StockRequisitionDetailsMatId { get; set; }
        public string StockRequisitionDetailsItemName { get; set; }
        public decimal? StockRequisitionDetailsUnitId { get; set; }
        public string StockRequisitionDetailsUnitName { get; set; }
        public string StockRequisitionDetailsBatchCode { get; set; }
        public DateTime? StockRequisitionDetailsManfDate { get; set; }
        public DateTime? StockRequisitionDetailsExpDate { get; set; }
        public decimal? StockRequisitionDetailsQuantity { get; set; }
        public decimal? StockRequisitionDetailsRate { get; set; }
        public decimal? StockRequisitionDetailsGrossAmount { get; set; }
        public decimal? StockRequisitionDetailsDiscAmount { get; set; }
        public decimal? StockRequisitionDetailsActualAmount { get; set; }
        public decimal? StockRequisitionDetailsVatPer { get; set; }
        public decimal? StockRequisitionDetailsVatAmt { get; set; }
        public decimal? StockRequisitionDetailsNetAmt { get; set; }
        public string StockRequisitionDetailsRemarks { get; set; }
        public bool? StockRequisitionDetailsIsEdit { get; set; }
        public int? StockRequisitionDetailsRfqId { get; set; }
        public int? StockRequisitionDetailsRfqdId { get; set; }
        public int? StockRequisitionDetailsPrId { get; set; }
        public int? StockRequisitionDetailsPrdId { get; set; }
        public int? StockRequisitionDetailsPoId { get; set; }
        public int? StockRequisitionDetailsPodId { get; set; }
        public int? StockRequisitionDetailsQtnId { get; set; }
        public int? StockRequisitionDetailsQtndId { get; set; }
        public decimal? StockRequisitionDetailsSalesPrice { get; set; }


        public DateTime? StockRequisitionDetailsReqDate { get; set; }

        public string? StockRequisitionDetailsReqStatus { get; set; }
        public bool? StockRequisitionDetailsDelStatus { get; set; }

        //public virtual StockRequisition StockRequisitionDetailsNoNavigation { get; set; }
    }
}
