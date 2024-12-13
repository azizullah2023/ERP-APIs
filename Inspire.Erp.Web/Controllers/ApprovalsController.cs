using Inspire.Erp.Application.Approvals.Interface;
using Inspire.Erp.Application.Settings.Interface;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Inspire.Erp.Domain.Entities;
using System.Collections.Generic;
using Inspire.Erp.Domain.Models;
using AutoMapper;
using NPOI.SS.Formula.Functions;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Inspire.Erp.Domain.Models.Approvals;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalsController : ControllerBase
    {
        private readonly IApprovalService _approvalService;
        private IRepository<UserFile> _userFileRepository;
        private IRepository<tbl_approvalforms> _approvalforms;
        private readonly IMapper _mapper;
        public ApprovalsController(IApprovalService approvalService, IMapper mapper, IRepository<UserFile> userFileRepository, IRepository<tbl_approvalforms> approvalforms)
        {
            _approvalService = approvalService;
            _userFileRepository = userFileRepository; _approvalforms = approvalforms;
            _mapper = mapper;
        }
        [HttpPost("GetApprovalsRecords")]
        public async Task<IActionResult> GetApprovalsRecords(GenericGridViewModel model)
        {
            return Ok(await _approvalService.GetApprovalsRecords(model));
        }

        [HttpGet]
        [Route("GetPermissionapById")]

        public List<Permissionapproval> GetPermissionapById(long id, int level)
        {
            var GetPermission = _approvalService.GetPermissionapById(id, level);

            return GetPermission;
        }
        [HttpPost]
        [Route("InsertPermissionApproval")]
        public ApiResponse<List<Permissionapproval>> InsertPermissionApproval([FromBody] List<Permissionapproval> Perm)
        {
            ApiResponse<List<Permissionapproval>> apiResponse = new ApiResponse<List<Permissionapproval>>();

            var param1 = _mapper.Map<List<Permissionapproval>>(Perm);
            var xs = _approvalService.InsertPermissionApproval(param1);

            List<Permissionapproval> Permission = new List<Permissionapproval>();
            Permission = _mapper.Map<List<Permissionapproval>>(xs);

            apiResponse = new ApiResponse<List<Permissionapproval>>
            {
                Valid = true,
                Result = Permission,
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetapprovalformsDropdown")]
        public ResponseInfo GetapprovalformsDropdown()
        {
            var objresponse = new ResponseInfo();

            var users = _userFileRepository.GetAsQueryable().AsNoTracking().Where(a => a.Deleted != true).Select(x => new
            {
                x.UserId,
                x.UserName
            }).ToList();
            var forms = _approvalforms.GetAsQueryable().AsNoTracking().Where(x => x.is_active == true).Select(a => new
            {
                a.id,
                a.Voucher_Type
            }).ToList();

            objresponse.ResultSet = new
            {
                users = users,
                forms = forms
            };
            return objresponse;
        }
    }
}
