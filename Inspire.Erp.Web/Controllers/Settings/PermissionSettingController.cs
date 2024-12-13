using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf.Exporting.XPS.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Settings
{
    [Route("api/PermissionSetting")]
    [ApiController]
    [Produces("application/json")]
    public class PermissionSettingController : ControllerBase
    {
        private IPermissionSettingService _repo;
        public PermissionSettingController(IPermissionSettingService repo)
        {
            _repo = repo;
        }
        [HttpPost]
        [Route("getPermissionSetting")]
        public IActionResult GetPermissionSetting(int groupId)
        {
            return Ok(_repo.GetPermissionSetting(groupId));
        }

        [HttpPost]
        [Route("updatePermissionSetting")]
        public async Task<IActionResult> updatePermissionSetting([FromBody] List<WorkGroupPermissions> workGroupPermissions)
        {
            return Ok(await _repo.UpdatePermissionSetting(workGroupPermissions));
        }
    }
}
