using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspire.Erp.Domain.Models.Approvals
{
    public class ApprovalResponse
    {
        public int Id { get; set; }
        public string? VoucherType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int? LocationId { get; set; }
        public List<ApprovalDetailResponse> ApprovalDetails { get; set; }
    }
    public class ApprovalDetailResponse
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public int? ApprovalId { get; set; }
    }
    public class Permissionapproval
    {
        public int ApId { get; set; }
        public int ApprovalformId { get; set; }
        public int? UserId { get; set; }
        public int? LevelId { get; set; }

        public string? Username { get; set; }
        public DateTime? CreatedDate { get; set; }

        [NotMapped]
        public Boolean isActive { get; set; }
       
    }
}
