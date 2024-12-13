using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public partial class SalesReturnDetailsViewModel
    {
        public decimal SalesReturnDetailsId { get; set; }
        public decimal? SalesReturnId { get; set; }
        public string SalesReturnDetailsNo { get; set; }
        public decimal? SalesReturnDetailsSno { get; set; }
        public int? SalesReturnDetailsMatId { get; set; }
        public string SalesReturnDetailsItemName { get; set; }
        public int? SalesReturnDetailsUnitId { get; set; }
        public string SalesReturnDetailsUnitName { get; set; }
        public string SalesReturnDetailsBatchCode { get; set; }
        public DateTime? SalesReturnDetailsManfDate { get; set; }
        public DateTime? SalesReturnDetailsExpDate { get; set; }
        public decimal? SalesReturnDetailsQuantity { get; set; }
        public decimal? SalesReturnDetailsRate { get; set; }
        public decimal? SalesReturnDetailsGrossAmount { get; set; }
        public decimal? SalesReturnDetailsDiscAmount { get; set; }
        public decimal? SalesReturnDetailsActualAmount { get; set; }
        public decimal? SalesReturnDetailsVatPer { get; set; }
        public decimal? SalesReturnDetailsVatAmt { get; set; }
        public decimal? SalesReturnDetailsNetAmt { get; set; }
        public string SalesReturnDetailsRemarks { get; set; }
        public bool? SalesReturnDetailsIsEdit { get; set; }
        public int? SalesReturnDetailsDlvId { get; set; }
        public int? SalesReturnDetailsDlvDId { get; set; }
        public int? SalesReturnDetailsRfqId { get; set; }
        public int? SalesReturnDetailsRfqdId { get; set; }
        public int? SalesReturnDetailsEnqId { get; set; }
        public int? SalesReturnDetailsEnqDId { get; set; }
        public int? SalesReturnDetailsSoId { get; set; }
        public int? SalesReturnDetailsSodId { get; set; }
        public int? SalesReturnDetailsQtnId { get; set; }
        public int? SalesReturnDetailsQtndId { get; set; }
        public decimal? SalesReturnDetailsCostPrice { get; set; }

        public decimal? SalesReturnDetailsBalanceQty { get; set; }
        public decimal? SalesReturnDetailsSalesQty { get; set; }
        public decimal? SalesReturnDetailsSalesUnitId { get; set; }
        public decimal? SalesReturnDetailsSalesRate { get; set; }
        public int? SalesReturnDetailsSvId { get; set; }
        public int? SalesReturnDetailsSvdId { get; set; }


        public bool? SalesReturnDetailsDelStatus { get; set; }

        public decimal? SalesReturnQtyTill { get; set; }
        //public virtual SalesReturnController SalesReturnDetailsNoNavigation { get; set; }
    }
}
