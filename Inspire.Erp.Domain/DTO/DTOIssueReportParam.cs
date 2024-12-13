using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Entities;

namespace Inspire.Erp.Domain.DTO
{
    public class DTOIssueReportParam
    {
        public long? itemId { get; set; }
        public string partNo { get; set; }
        public long? departmentId { get; set; }
        public long? jobId { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? toDate { get; set; }
    }

    public class DTOIssueReport
    {
        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }
        public long? ItemId { get; set; }
        public string IssueNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public string PartNo { get; set; }
        public string ItemName { get; set; }
        public double? Quantity { get; set; }
        public double? Rate { get; set; }
        public double? Amount { get; set; }
    }

    public class DTOAllocationDetails
    {
        public AllocationVoucherDetails VoucherAllocation { get; set; }
        public AccountsTransactions AccountTransaction { get; set; }
    }

    public class DTOUserTracking
    {
        public string VPNo { get; set; }
        public DateTime? ChangeTime { get; set; }
        public DateTime? ChangeDt { get; set; }
        public string VPType { get; set; }
        public long UserId { get; set; }
        public string VPAction { get; set; }
    }
}