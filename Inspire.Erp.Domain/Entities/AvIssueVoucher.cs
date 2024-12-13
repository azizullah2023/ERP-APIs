using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvIssueVoucher
    {
        public int AvIssueVoucherAvsvId { get; set; }
        public string AvIssueVoucherAvsvNo { get; set; }
        public DateTime? AvIssueVoucherAvsvDate { get; set; }
        public string AvIssueVoucherAvSalesAcNo { get; set; }
        public double? AvIssueVoucherTotalAmount { get; set; }
        public double? AvIssueVoucherFcTotalAmount { get; set; }
        public string AvIssueVoucherNarration { get; set; }
        public string AvIssueVoucherReqNo { get; set; }
        public string AvIssueVoucherStatus { get; set; }
        public int? AvIssueVoucherFsno { get; set; }
        public int? AvIssueVoucherUserId { get; set; }
        public double? AvIssueVoucherFcRate { get; set; }
        public int? AvIssueVoucherLocationId { get; set; }
        public int? AvIssueVoucherDepartmentId { get; set; }
        public int? AvIssueVoucherCostCenter { get; set; }
        public int? AvIssueVoucherJobId { get; set; }
        public int? AvIssueVoucherCurrencyId { get; set; }
        public string AvIssueVoucherDayBookNo { get; set; }
        public double? AvIssueVoucherDiscountPercentage { get; set; }
        public double? AvIssueVoucherDiscountAmount { get; set; }
        public double? AvIssueVoucherNetAmount { get; set; }
        public string AvIssueVoucherPoNo { get; set; }
        public string AvIssueVoucherAccNo { get; set; }
        public string AvIssueVoucherStockAccount { get; set; }
        public string AvIssueVoucherExpenseAccount { get; set; }
        public int? AvIssueVoucherIssueEqpmentIdN { get; set; }
        public string AvIssueVoucherPurchaseId { get; set; }
        public bool? AvIssueVoucherDelStatus { get; set; }
    }
}
