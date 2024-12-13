using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PrApprovalDetails
    {
        public int PrApprovalDetailsId { get; set; }
        public int? PrApprovalDetailsReqId { get; set; }
        public int? PrApprovalDetailsUserId { get; set; }
        public string PrApprovalDetailsStatus { get; set; }
        public bool? PrApprovalDetailsDelStatus { get; set; }
    }
}
