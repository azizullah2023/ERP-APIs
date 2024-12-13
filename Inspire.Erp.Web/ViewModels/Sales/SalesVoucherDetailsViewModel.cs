using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public partial class SalesVoucherDetailsViewModel
    {
        public decimal SalesVoucherDetailsId { get; set; }
        public decimal? SalesVoucherId { get; set; }
        public string SalesVoucherDetailsNo { get; set; }
        public decimal? SalesVoucherDetailsSno { get; set; }
        public int? SalesVoucherDetailsMatId { get; set; }
        public string SalesVoucherDetailsItemName { get; set; }
        public int? SalesVoucherDetailsUnitId { get; set; }
        public string SalesVoucherDetailsUnitName { get; set; }
        public string SalesVoucherDetailsBatchCode { get; set; }
        public DateTime? SalesVoucherDetailsManfDate { get; set; }
        public DateTime? SalesVoucherDetailsExpDate { get; set; }
        public decimal? SalesVoucherDetailsQuantity { get; set; }
        public decimal? SalesVoucherDetailsRate { get; set; }
        public decimal? SalesVoucherDetailsGrossAmount { get; set; }
        public decimal? SalesVoucherDetailsDiscAmount { get; set; }
        public decimal? SalesVoucherDetailsActualAmount { get; set; }
        public decimal? SalesVoucherDetailsVatPer { get; set; }
        public decimal? SalesVoucherDetailsVatAmt { get; set; }
        public decimal? SalesVoucherDetailsNetAmt { get; set; }
        public string SalesVoucherDetailsRemarks { get; set; }
        public bool? SalesVoucherDetailsIsEdit { get; set; }
        public int? SalesVoucherDetailsDlvId { get; set; }
        public int? SalesVoucherDetailsDlvDId { get; set; }
        public int? SalesVoucherDetailsRfqId { get; set; }
        public int? SalesVoucherDetailsRfqdId { get; set; }
        public int? SalesVoucherDetailsEnqId { get; set; }
        public int? SalesVoucherDetailsEnqDId { get; set; }
        public int? SalesVoucherDetailsSoId { get; set; }
        public int? SalesVoucherDetailsSodId { get; set; }
        public int? SalesVoucherDetailsQtnId { get; set; }
        public int? SalesVoucherDetailsQtndId { get; set; }
        public decimal? SalesVoucherDetailsCostPrice { get; set; }
        public bool? SalesVoucherDetailsDelStatus { get; set; }


        //public virtual SalesVoucherController SalesVoucherDetailsNoNavigation { get; set; }
    }
}
