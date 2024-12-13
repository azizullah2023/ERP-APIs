using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IAvIssueVoucherService
    {
        public IQueryable GetDetailWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate);
        public IQueryable GetJobWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate);
        public IQueryable GetSummaryWiseReport(long? itemId, string partNo, long? departmentId, long? jobId, DateTime? dateFrom, DateTime? toDate);
    }
}