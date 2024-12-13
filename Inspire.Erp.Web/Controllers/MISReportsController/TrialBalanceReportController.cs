
using AutoMapper;
using Inspire.Erp.Application.MIS.Interfaces;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.MIS.TrialBalance;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/TrialBalanceReport")]
    [Produces("application/json")]
    [ApiController]
    public class TrialBalanceReportController : ControllerBase
    {
        private readonly IMapper mapper;
        private ITrialBalanceReport trialBalanceReportService;
        public TrialBalanceReportController(ITrialBalanceReport trialBalanceReport, IMapper mapper)
        {
            this.trialBalanceReportService = trialBalanceReport;
            this.mapper = mapper;
        }


  
        [HttpPost]
        [Route("TrailBalanceSummary")]
        public async Task<Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceSummary([FromBody] ReportFilterViewModel reportFilterViewModel)
        {
            var reportQuery = mapper.Map<ReportFilter>(reportFilterViewModel);
            return await trialBalanceReportService.TrailBalanceSummary(reportQuery);
        }
        
        [HttpPost]
        [Route("TrailBalanceDetailedView")]
        public async Task<Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceDetailedView([FromBody] ReportFilterViewModel reportFilterViewModel)
        {
            var reportQuery = mapper.Map<ReportFilter>(reportFilterViewModel);
            return await trialBalanceReportService.TrailBalanceDetailedView(reportQuery);
        }
        [HttpPost]
        [Route("TrailBalanceGroupView")]
        public async Task<Response<GridWrapperResponse<List<GetTrialBalanceReportModel>>>> TrailBalanceGroupView([FromBody] ReportFilterViewModel reportFilterViewModel)
        {
            var reportQuery = mapper.Map<ReportFilter>(reportFilterViewModel);
            return await trialBalanceReportService.TrailBalanceGroupView(reportQuery);
        }
    }
}