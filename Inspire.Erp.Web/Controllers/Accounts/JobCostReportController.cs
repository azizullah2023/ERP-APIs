using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCostReportController : ControllerBase
    {
        private IJobCostReportService _jobCostRepo;
        public JobCostReportController(IJobCostReportService jobCostRepo)
        {
            _jobCostRepo = jobCostRepo;
        }
        [HttpPost]
        [Route("getJobCostReportDetails")]
        public async Task<IActionResult> JobCostReportDetails(JobCostReportSearchFilter filter)
        {
            return Ok(await _jobCostRepo.JobCostReportDetails(filter));
        }

    }
}
