using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class IssueReturn
    {
        public IssueReturn()
        {
            IssueReturnDetails = new HashSet<IssueReturnDetails>();
        }

        public decimal IssueReturnId { get; set; }
        public string IssueReturnNo { get; set; }
        public DateTime IssueReturnDate { get; set; }
        public string IssueReturnCreditAccNo { get; set; }
        public string IssueReturnDebitAccNo { get; set; }
        public decimal? IssueReturnDepartmentId { get; set; }
        public decimal? IssueReturnCostCenterId { get; set; }
        public int? IssueReturnLocationId { get; set; }
        public int? IssueReturnCurrencyId { get; set; }
        public int? IssueReturnJobId { get; set; }
        public string IssueReturnRefNo { get; set; }
        public string IssueReturnGrno { get; set; }
        public DateTime? IssueReturnGrdate { get; set; }
        public string IssueReturnLpono { get; set; }
        public DateTime? IssueReturnLpodate { get; set; }
        public string IssueReturnQtnNo { get; set; }
        public DateTime? IssueReturnQtnDate { get; set; }
        public string IssueReturnDescription { get; set; }
        public string IssueReturnReqNo { get; set; }
        public DateTime? IssueReturnReqDate { get; set; }
        public string IssueReturnDayBookNo { get; set; }
        public long? IssueReturnUserId { get; set; }
        public int? IssueReturnCompanyId { get; set; }
        public decimal? IssueReturnFsno { get; set; }
        public decimal? IssueReturnFcRate { get; set; }
        public string IssueReturnIvNoForRet { get; set; }
        public DateTime? IssueReturnIvDateForRet { get; set; }
        public string IssueReturnBufferRemark1 { get; set; }
        public string IssueReturnBufferRemark12 { get; set; }
        public string IssueReturnBufferRemark13 { get; set; }
        public string IssueReturnBufferPurNo { get; set; }
        public string IssueReturnBufferReqNo { get; set; }
        public string IssueReturnStatus { get; set; }
        public decimal? IssueReturnTotalGrossAmount { get; set; }
        public decimal? IssueReturnTotalItemDisAmount { get; set; }
        public decimal? IssueReturnTotalActualAmount { get; set; }
        public decimal? IssueReturnTotalDisPer { get; set; }
        public decimal? IssueReturnTotalDisAmount { get; set; }
        public decimal? IssueReturnVatAmt { get; set; }
        public decimal? IssueReturnVatPer { get; set; }
        public string IssueReturnVatRoundSign { get; set; }
        public decimal? IssueReturnVatRountAmt { get; set; }
        public decimal? IssueReturnNetDisAmount { get; set; }
        public decimal? IssueReturnNetAmount { get; set; }
        public bool? IssueReturnDelStatus { get; set; }

        public virtual ICollection<IssueReturnDetails> IssueReturnDetails { get; set; }
    }
}
