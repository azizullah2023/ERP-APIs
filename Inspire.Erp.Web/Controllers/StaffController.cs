using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IStaffMasterService staffMasterService;
        public StaffController(IStaffMasterService _staffMasterService, IMapper mapper)
        {

            staffMasterService = _staffMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllStaff")]
        public List<StaffMasterViewModel> GetAllStaff()
        {

            return staffMasterService.GetAllStaff().Select(k => new StaffMasterViewModel
            {
                StaffMasterStaffId = k.StaffMasterStaffId,
                StaffMasterStaffCode = k.StaffMasterStaffCode,
                StaffMasterStaffName = k.StaffMasterStaffName,
                StaffMasterStaffDelStatus= k.StaffMasterStaffDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllStaffById/{id}")]
        public List<StaffMasterViewModel> GetAllStaffById(int id)
        {
            return staffMasterService.GetAllStaffById(id).Select(k => new StaffMasterViewModel
            {


                StaffMasterStaffId = k.StaffMasterStaffId,
                StaffMasterStaffCode = k.StaffMasterStaffCode,
                StaffMasterStaffName = k.StaffMasterStaffName,
                StaffMasterStaffDelStatus = k.StaffMasterStaffDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertStaff")]
        public List<StaffMasterViewModel> InsertStaff([FromBody] StaffMasterViewModel staffMaster)
        {
            var result = _mapper.Map<StaffMaster>(staffMaster);
            return staffMasterService.InsertStaff(result).Select(k => new StaffMasterViewModel
            {
                StaffMasterStaffId = k.StaffMasterStaffId,
                StaffMasterStaffCode = k.StaffMasterStaffCode,
                StaffMasterStaffName = k.StaffMasterStaffName,
                StaffMasterStaffDelStatus = k.StaffMasterStaffDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateStaff")]
        public List<StaffMasterViewModel> UpdateStaff([FromBody] StaffMasterViewModel staffMaster)
        {
            var result = _mapper.Map<StaffMaster>(staffMaster);
            return staffMasterService.UpdateStaff(result).Select(k => new StaffMasterViewModel
            {
                StaffMasterStaffId = k.StaffMasterStaffId,
                StaffMasterStaffCode = k.StaffMasterStaffCode,
                StaffMasterStaffName = k.StaffMasterStaffName,
                StaffMasterStaffDelStatus = k.StaffMasterStaffDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteStaff")]
        public List<StaffMasterViewModel> DeleteStaff([FromBody] StaffMasterViewModel staffMaster)
        {
            var result = _mapper.Map<StaffMaster>(staffMaster);
            return staffMasterService.DeleteStaff(result).Select(k => new StaffMasterViewModel
            {
                StaffMasterStaffId = k.StaffMasterStaffId,
                StaffMasterStaffCode = k.StaffMasterStaffCode,
                StaffMasterStaffName = k.StaffMasterStaffName,
                StaffMasterStaffDelStatus = k.StaffMasterStaffDelStatus
            }).ToList();
        }
    }
}