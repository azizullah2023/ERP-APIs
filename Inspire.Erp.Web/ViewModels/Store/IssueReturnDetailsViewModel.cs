using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public partial class IssueReturnDetailsViewModel
    {
        public decimal IssueReturnDetailsId { get; set; }
        public decimal? IssueReturnId { get; set; }
        public string IssueReturnDetailsNo { get; set; }
        public decimal? IssueReturnDetailsSno { get; set; }
        public decimal? IssueReturnDetailsMatId { get; set; }
        public string IssueReturnDetailsItemName { get; set; }
        public decimal? IssueReturnDetailsUnitId { get; set; }
        public string IssueReturnDetailsUnitName { get; set; }
        public string IssueReturnDetailsBatchCode { get; set; }
        public DateTime? IssueReturnDetailsManfDate { get; set; }
        public DateTime? IssueReturnDetailsExpDate { get; set; }
        public decimal? IssueReturnDetailsCurrentStockQty { get; set; }
        public decimal? IssueReturnDetailsIvQtyForRet { get; set; }
        public decimal? IssueReturnDetailsQuantity { get; set; }
        public decimal? IssueReturnDetailsRate { get; set; }
        public decimal? IssueReturnDetailsGrossAmount { get; set; }
        public decimal? IssueReturnDetailsDiscAmount { get; set; }
        public decimal? IssueReturnDetailsActualAmount { get; set; }
        public decimal? IssueReturnDetailsVatPer { get; set; }
        public decimal? IssueReturnDetailsVatAmt { get; set; }
        public decimal? IssueReturnDetailsNetAmt { get; set; }
        public string IssueReturnDetailsRemarks { get; set; }
        public bool? IssueReturnDetailsIsEdit { get; set; }
        public int? IssueReturnDetailsRfqId { get; set; }
        public int? IssueReturnDetailsRfqdId { get; set; }
        public int? IssueReturnDetailsPrId { get; set; }
        public int? IssueReturnDetailsPrdId { get; set; }
        public int? IssueReturnDetailsPoId { get; set; }
        public int? IssueReturnDetailsPodId { get; set; }
        public int? IssueReturnDetailsQtnId { get; set; }
        public int? IssueReturnDetailsQtndId { get; set; }
        public int? IssueReturnDetailsReqId { get; set; }
        public int? IssueReturnDetailsReqDId { get; set; }
        public int? IssueReturnDetailsPurId { get; set; }
        public int? IssueReturnDetailsPurDId { get; set; }
        public decimal? IssueReturnDetailsSalesPrice { get; set; }
        public bool? IssueReturnDetailsDelStatus { get; set; }

        //public virtual IssueReturnController IssueReturnDetailsNoNavigation { get; set; }
    }
}
