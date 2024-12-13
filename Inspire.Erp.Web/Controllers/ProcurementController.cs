using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
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
    public class ProcurementController : ControllerBase
    {
        private readonly IApprovalService _approvalService;
        private readonly IPurchaseReturnService _purchaseReturn;
        public ProcurementController(IApprovalService approvalService,IPurchaseReturnService purchaseReturn)
        {
            _approvalService = approvalService;
            _purchaseReturn = purchaseReturn;
        }
        #region PO Approval 
        [HttpGet("GetDistinctPOApprovalStatus")]
        public async Task<IActionResult> GetDistinctPOApprovalStatus()
        {
            return Ok(await _approvalService.GetDistinctPOApprovalStatus());
        }
        [HttpPost("ApprovedPOApproval")]
        public async Task<IActionResult> ApprovedPOApproval(List<int> ids)
        {
            return Ok(await _approvalService.ApprovedPOApproval(ids));
        }
        [HttpPost("GetPOApprovals")]
        public async Task<IActionResult> GetPOApprovals(GenericGridViewModel model)
        {
            return Ok(await _approvalService.GetPOApprovals(model));
        }

        #endregion

        #region PR Approval 
        [HttpGet("GetDistinctPRApprovalStatus")]
        public async Task<IActionResult> GetDistinctPRApprovalStatus()
        {
            return Ok(await _approvalService.GetDistinctPRApprovalStatus());
        }
        [HttpPost("ApprovedPRApproval")]
        public async Task<IActionResult> ApprovedPRApproval(List<int> ids)
        {
            return Ok(await _approvalService.ApprovedPRApproval(ids));
        }
        [HttpPost("GetPRApprovals")]
        public async Task<IActionResult> GetPRApprovals(GenericGridViewModel model)
        {
            return Ok(await _approvalService.GetPRApprovals(model));
        }

        #endregion

        #region Purchase Return
        [HttpPost("GetPurchaseVoucherList")]
        public async Task<IActionResult> GetPurchaseVoucherList(GenericGridViewModel model)
        {
            return Ok(await _purchaseReturn.GetPurchaseVoucherList(model));
        }
        [HttpPost("GetPurchaseReturnList")]
        public async Task<IActionResult> GetPurchaseReturnList(GenericGridViewModel model)
        {
            return Ok(await _purchaseReturn.GetPurchaseReturnList(model));
        }
        [HttpGet("GetSpecificPurchaseVoucher")]
        public async Task<IActionResult> GetSpecificPurchaseVoucher(string id)
        {
            return Ok(await _purchaseReturn.GetSpecificPurchaseVoucher(id));
        }
        [HttpGet("GetLocationMasterDropdown")]
        public async Task<IActionResult> GetLocationMasterDropdown()
        {
            return Ok(await _purchaseReturn.GetLocationMasterDropdown());
        }
        [HttpGet("GetVoucherTypeDropdown")]
        public async Task<IActionResult> GetVoucherTypeDropdown()
        {
            return Ok(await _purchaseReturn.GetVoucherTypeDropdown());
        }
        [HttpGet("GetJObMasterDropdown")]
        public async Task<IActionResult> GetJObMasterDropdown()
        {
            return Ok(await _purchaseReturn.GetJObMasterDropdown());
        }
        [HttpGet("GetSupplierMasterDropdown")]
        public async Task<IActionResult> GetSupplierMasterDropdown()
        {
            return Ok(await _purchaseReturn.GetSupplierMasterDropdown());
        }
        [HttpGet("GetSpecificPurchaseReturn")]
        public async Task<IActionResult> GetSpecificPurchaseReturn(string id)
        {
            return Ok(await _purchaseReturn.GetSpecificPurchaseReturn(id));
        }
        [HttpPost("AddEditPurchaseReturn")]
        public async Task<IActionResult> AddEditPurchaseReturn(AddEditPurchaseReturnResponse model)
        {
            return Ok(await _purchaseReturn.AddEditPurchaseReturn(model));
        }
        #endregion

    }
}
