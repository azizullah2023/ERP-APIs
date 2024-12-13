using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IUtilityService _utilityService;
        public UtilityController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }
        #region User Activity
        [HttpPost("AddUserTrackingLog")]
        public async Task<IActionResult> AddUserTrackingLog(AddActivityLogViewModel model)
        {
            return Ok(await _utilityService.AddUserTrackingLog(model));
        }
        #endregion

        [HttpPost("SendEmailAsync")]
        public async Task<IActionResult> SendEmailAsync(EmailRequestViewModel model)
        {
            return Ok(await _utilityService.SendEmailAsync(model));
        }
    }
}
