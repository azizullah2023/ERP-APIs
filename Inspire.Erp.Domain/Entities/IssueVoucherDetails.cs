using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class IssueVoucherDetails
    {
        public decimal IssueVoucherDetailsId { get; set; }
        public decimal? IssueVoucherId { get; set; }
        public string IssueVoucherDetailsNo { get; set; }
        public decimal? IssueVoucherDetailsSno { get; set; }
        public int? IssueVoucherDetailsMatId { get; set; }
        public string IssueVoucherDetailsItemName { get; set; }
        public int? IssueVoucherDetailsUnitId { get; set; }
        public string IssueVoucherDetailsUnitName { get; set; }
        public string IssueVoucherDetailsBatchCode { get; set; }
        public DateTime? IssueVoucherDetailsManfDate { get; set; }
        public DateTime? IssueVoucherDetailsExpDate { get; set; }
        public decimal? IssueVoucherDetailsCurrentStockQty { get; set; }
        public decimal? IssueVoucherDetailsIvQtyForRet { get; set; }
        public decimal? IssueVoucherDetailsQuantity { get; set; }
        public decimal? IssueVoucherDetailsRate { get; set; }
        public decimal? IssueVoucherDetailsGrossAmount { get; set; }
        public decimal? IssueVoucherDetailsDiscAmount { get; set; }
        public decimal? IssueVoucherDetailsActualAmount { get; set; }
        public decimal? IssueVoucherDetailsVatPer { get; set; }
        public decimal? IssueVoucherDetailsVatAmt { get; set; }
        public decimal? IssueVoucherDetailsNetAmt { get; set; }
        public string IssueVoucherDetailsRemarks { get; set; }
        public bool? IssueVoucherDetailsIsEdit { get; set; }
        public int? IssueVoucherDetailsRfqId { get; set; }
        public int? IssueVoucherDetailsRfqdId { get; set; }
        public int? IssueVoucherDetailsPrId { get; set; }
        public int? IssueVoucherDetailsPrdId { get; set; }
        public int? IssueVoucherDetailsPoId { get; set; }
        public int? IssueVoucherDetailsPodId { get; set; }
        public int? IssueVoucherDetailsQtnId { get; set; }
        public int? IssueVoucherDetailsQtndId { get; set; }
        public int? IssueVoucherDetailsReqId { get; set; }
        public int? IssueVoucherDetailsReqDId { get; set; }
        public int? IssueVoucherDetailsPurId { get; set; }
        public int? IssueVoucherDetailsPurDId { get; set; }
        public decimal? IssueVoucherDetailsSalesPrice { get; set; }
        public bool? IssueVoucherDetailsDelStatus { get; set; }

        public virtual IssueVoucher IssueVoucherDetailsNoNavigation { get; set; }
    }
}
