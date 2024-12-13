using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvIssueVoucherDetails
    {
        public int AvIssueVoucherDetailsAvSvSno { get; set; }
        public string AvIssueVoucherDetailsAvSvNo { get; set; }
        public int? AvIssueVoucherDetailsSno { get; set; }
        public int? AvIssueVoucherDetailsMaterialId { get; set; }
        public double? AvIssueVoucherDetailsRate { get; set; }
        public double? AvIssueVoucherDetailsQuantity { get; set; }
        public double? AvIssueVoucherDetailsSoldQuantity { get; set; }
        public double? AvIssueVoucherDetailsSoldAmount { get; set; }
        public int? AvIssueVoucherDetailsUId { get; set; }
        public string AvIssueVoucherDetailsExpenseAccount { get; set; }
        public string AvIssueVoucherDetailsRemakrs { get; set; }
        public int? AvIssueVoucherDetailsFsno { get; set; }
        public int? AvIssueVoucherDetailsSrdId { get; set; }
        public double? AvIssueVoucherDetailsStock { get; set; }
        public bool? AvIssueVoucherDetailsDelStatus { get; set; }
    }
}
