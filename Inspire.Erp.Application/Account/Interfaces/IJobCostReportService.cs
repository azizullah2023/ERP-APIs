using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models.Sales.DeliveryNoteReport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.DTO.Job_Master;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IJobCostReportService
    {
        public Task<Response<List<JobMasterExpenseDto>>> JobCostReportDetails(JobCostReportSearchFilter filter);

    }
}
