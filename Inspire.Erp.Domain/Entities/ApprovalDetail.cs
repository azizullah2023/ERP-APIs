using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class ApprovalDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool? Status { get; set; }
        public int? ApprovalId { get; set; }
        public int? LevelOrder { get; set; }

        // Navigation property
        public Approval Approval { get; set; }
    }
}
