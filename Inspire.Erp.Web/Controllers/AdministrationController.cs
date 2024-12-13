using Inspire.Erp.Application.Settings.Interface;
using Inspire.Erp.Domain.Modals.Administration;
using Inspire.Erp.Domain.Modals.Common;
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
    public class AdministrationController : ControllerBase
    {
        private readonly IFinancialClosingService _fcClossing;
        private readonly IReportTypeService _reportType;
        public AdministrationController(IFinancialClosingService fcClossing, IReportTypeService reportType)
        {
            _fcClossing = fcClossing;
            _reportType = reportType;
        }

        #region Financial CLosing
        [HttpPost("GetFinancialClosingEquity")]
        public async Task<IActionResult> GetFinancialClosingEquity(GenericGridViewModel model)
        {
            return Ok(await _fcClossing.GetFinancialClosingEquity(model));
        }
        [HttpPost("GetFinancialClosingAsset")]
        public async Task<IActionResult> GetFinancialClosingAsset(GenericGridViewModel model)
        {
            return Ok(await _fcClossing.GetFinancialClosingAsset(model));
        }
        [HttpPost("GetFinancialClosingLiability")]
        public async Task<IActionResult> GetFinancialClosingLiability(GenericGridViewModel model)
        {
            return Ok(await _fcClossing.GetFinancialClosingLiability(model));
        }
        #endregion

        #region Report Type Master
        [HttpPost("GetReportTypes")]
        public async Task<IActionResult> GetReportTypes(GenericGridViewModel model)
        {
            return Ok(await _reportType.GetReportTypes(model));
        }
        [HttpPost("DeleteReportType")]
        public async Task<IActionResult> DeleteReportType(List<int> ids)
        {
            return Ok(await _reportType.DeleteReportType(ids));
        }
        [HttpGet("GetSpecificVoucherType")]
        public async Task<IActionResult> GetSpecificVoucherType(int id)
        {
            return Ok(await _reportType.GetSpecificVoucherType(id));
        }
        [HttpPost("AddEditReportType")]
        public async Task<IActionResult> AddEditReportType(AddEditReportTypeResponse model)
        {
            return Ok(await _reportType.AddEditReportType(model));
        }
        #endregion
    }
}
