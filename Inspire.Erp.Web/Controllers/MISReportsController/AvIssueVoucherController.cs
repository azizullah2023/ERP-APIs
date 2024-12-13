using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inspire.Erp.Web.Controllers.MISReportsControllers
{
    [Route("api/AvIssueVoucher")]
    [Produces("application/json")]
    [ApiController]
    public class AvIssueVoucherController : ControllerBase
    {
        private IAvIssueVoucherService _service;

        public AvIssueVoucherController(IAvIssueVoucherService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("GetDetailWiseReport")]
        public IActionResult GetDetailWiseReport([FromBody] DTOIssueReportParam _DTOIssueReportParam)
        {
            try
            {
                var data = _service.GetDetailWiseReport(_DTOIssueReportParam.itemId, _DTOIssueReportParam.partNo, _DTOIssueReportParam.departmentId, _DTOIssueReportParam.jobId, _DTOIssueReportParam.dateFrom, _DTOIssueReportParam.toDate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("GetJobWiseReport")]
        public IActionResult GetJobWiseReport([FromBody] DTOIssueReportParam _DTOIssueReportParam)
        {
            try
            {
                var data = _service.GetJobWiseReport(_DTOIssueReportParam.itemId, _DTOIssueReportParam.partNo, _DTOIssueReportParam.departmentId, _DTOIssueReportParam.jobId, _DTOIssueReportParam.dateFrom, _DTOIssueReportParam.toDate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("GetSummaryWiseReport")]
        public IActionResult GetSummaryWiseReport([FromBody] DTOIssueReportParam _DTOIssueReportParam)
        {
            try
            {
                var data = _service.GetSummaryWiseReport(_DTOIssueReportParam.itemId, _DTOIssueReportParam.partNo, _DTOIssueReportParam.departmentId, _DTOIssueReportParam.jobId, _DTOIssueReportParam.dateFrom, _DTOIssueReportParam.toDate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}