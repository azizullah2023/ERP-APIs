using Inspire.Erp.Domain.Modals.MIS.TrialBalance;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.MIS.Interfaces
{
    public interface ITrialBalanceReport
    {

        public Task< Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceSummary(ReportFilter query);
        public Task<Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceDetailedView(ReportFilter query);
        public Task<Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceGroupView(ReportFilter query);
    }
}
