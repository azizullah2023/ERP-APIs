using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvIssueVoucherDetailsReturn
    {
        public int? AvIssueVoucherDetailsReturnIssueVoucherReturnNo { get; set; }
        public string AvIssueVoucherDetailsReturnIssueVoucherNo { get; set; }
        public int? AvIssueVoucherDetailsReturnSno { get; set; }
        public int? AvIssueVoucherDetailsReturnMaterialId { get; set; }
        public double? AvIssueVoucherDetailsReturnRate { get; set; }
        public double? AvIssueVoucherDetailsReturnQuantity { get; set; }
        public double? AvIssueVoucherDetailsReturnReturnedQuantity { get; set; }
        public double? AvIssueVoucherDetailsReturnReturnedAmount { get; set; }
        public double? AvIssueVoucherDetailsReturnFcAmount { get; set; }
        public int? AvIssueVoucherDetailsReturnUId { get; set; }
        public string AvIssueVoucherDetailsReturnExpenseAccount { get; set; }
        public int? AvIssueVoucherDetailsReturnFsno { get; set; }
        public bool? AvIssueVoucherDetailsReturnDelStatus { get; set; }
    }
}
