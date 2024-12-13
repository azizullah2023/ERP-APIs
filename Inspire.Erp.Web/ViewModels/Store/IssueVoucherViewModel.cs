using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public partial class IssueVoucherViewModel
    {
        //public IssueVoucherViewModel()
        //{
        //    IssueVoucherDetails = new HashSet<IssueVoucherDetailsViewModel>();
        //}
        //
        public decimal IssueVoucherId { get; set; }
        public string IssueVoucherNo { get; set; }
        public DateTime IssueVoucherDate { get; set; }
        public string IssueVoucherCreditAccNo { get; set; }
        public string IssueVoucherDebitAccNo { get; set; }
        public decimal? IssueVoucherDepartmentId { get; set; }
        public decimal? IssueVoucherCostCenterId { get; set; }
        public int? IssueVoucherLocationId { get; set; }
        public int? IssueVoucherCurrencyId { get; set; }
        public int? IssueVoucherJobId { get; set; }
        public string IssueVoucherRefNo { get; set; }
        public string IssueVoucherGrno { get; set; }
        public DateTime? IssueVoucherGrdate { get; set; }
        public string IssueVoucherLpono { get; set; }
        public DateTime? IssueVoucherLpodate { get; set; }
        public string IssueVoucherQtnNo { get; set; }
        public DateTime? IssueVoucherQtnDate { get; set; }
        public string IssueVoucherDescription { get; set; }
        public string IssueVoucherReqNo { get; set; }
        public DateTime? IssueVoucherReqDate { get; set; }
        public string IssueVoucherDayBookNo { get; set; }
        public long? IssueVoucherUserId { get; set; }
        public int? IssueVoucherCompanyId { get; set; }
        public decimal? IssueVoucherFsno { get; set; }
        public decimal? IssueVoucherFcRate { get; set; }
        public string IssueVoucherIvNoForRet { get; set; }
        public DateTime? IssueVoucherIvDateForRet { get; set; }
        public string IssueVoucherBufferRemark1 { get; set; }
        public string IssueVoucherBufferRemark12 { get; set; }
        public string IssueVoucherBufferRemark13 { get; set; }
        public string IssueVoucherBufferPurNo { get; set; }
        public string IssueVoucherBufferReqNo { get; set; }
        public string IssueVoucherStatus { get; set; }
        public decimal? IssueVoucherTotalGrossAmount { get; set; }
        public decimal? IssueVoucherTotalItemDisAmount { get; set; }
        public decimal? IssueVoucherTotalActualAmount { get; set; }
        public decimal? IssueVoucherTotalDisPer { get; set; }
        public decimal? IssueVoucherTotalDisAmount { get; set; }
        public decimal? IssueVoucherVatAmt { get; set; }
        public decimal? IssueVoucherVatPer { get; set; }
        public string IssueVoucherVatRoundSign { get; set; }
        public decimal? IssueVoucherVatRountAmt { get; set; }
        public decimal? IssueVoucherNetDisAmount { get; set; }
        public decimal? IssueVoucherNetAmount { get; set; }
        public bool? IssueVoucherDelStatus { get; set; }


        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<IssueVoucherDetailsViewModel> IssueVoucherDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }

    }
}
