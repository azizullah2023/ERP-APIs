using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvIssueVoucherReturn
    {
        public int AvIssueVoucherReturnIssueVoucherId { get; set; }
        public string AvIssueVoucherReturnIssueVoucherReturnNo { get; set; }
        public DateTime? AvIssueVoucherReturnIssueVoucherReturnDate { get; set; }
        public string AvIssueVoucherReturnIssueVoucherNo { get; set; }
        public DateTime? AvIssueVoucherReturnIssueVoucherDate { get; set; }
        public string AvIssueVoucherReturnSalesAcNo { get; set; }
        public double? AvIssueVoucherReturnTotalAmount { get; set; }
        public double? AvIssueVoucherReturnFcTotalAmount { get; set; }
        public string AvIssueVoucherReturnNarration { get; set; }
        public string AvIssueVoucherReturnPoNo { get; set; }
        public string AvIssueVoucherReturnStatus { get; set; }
        public int? AvIssueVoucherReturnFsno { get; set; }
        public int? AvIssueVoucherReturnUserId { get; set; }
        public double? AvIssueVoucherReturnFcRate { get; set; }
        public int? AvIssueVoucherReturnLoactionId { get; set; }
        public int? AvIssueVoucherReturnJobId { get; set; }
        public bool? AvIssueVoucherReturnDelStatus { get; set; }
    }
}
