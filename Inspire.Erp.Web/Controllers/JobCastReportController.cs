using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/JobCastReport")]
    [Produces("application/json")]
    [ApiController]
    public class JobCastReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IJobCastReport IJobCastReport;
        public JobCastReportController(IJobCastReport _IJobCastReport, IMapper mapper)
        {
            IJobCastReport = _IJobCastReport;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("GetjobCastDetials")]
        public async Task<IActionResult> GetjobCastDetials(JobCastFilterModel model)
        {
            var results = await IJobCastReport.GetjobCastDetials(model);
            return Ok(results);
        }
    }
}
