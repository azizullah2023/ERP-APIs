using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class Approval
    {
        public int Id { get; set; }
        public string? VoucherType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? LocationId { get; set; }
        public List<ApprovalDetail> ApprovalDetails { get; set; }
    }
    public class PermissionApprovalDetail
    {
        public int Id { get; set; }
        public string VoucherType { get; set; }
        public string VoucherId { get; set; }
        public DateTime? VoucherDate { get; set; }
        public double? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? LevelId { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Remarks { get; set; }
    }
    public class tbl_approvalforms
    {
        public int id { get; set; }
        public string Voucher_Type { get; set; }
        public Boolean is_active { get; set; }
        public int NoOfLevel { get; set; }
    }
}
