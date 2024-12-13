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
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IDepartmentMasterService departmentMasterService;
        public DepartmentController(IDepartmentMasterService _departmentMasterService, IMapper mapper)
        {

            departmentMasterService = _departmentMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllDepartment")]
        public List<DepartmentMasterViewModel> GetAllDepartment()
        {

            return departmentMasterService.GetAllDepartment().Select(k => new DepartmentMasterViewModel
            {

                DepartmentMasterDepartmentId = k.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = k.DepartmentMasterDepartmentName,
                DepartmentMasterDepartmentStatus = k.DepartmentMasterDepartmentStatus,
                DepartmentMasterDepartmentDelStatus= k.DepartmentMasterDepartmentDelStatus

            }).ToList();
        }


        [HttpGet]
        [Route("GetAllDepartmentById/{id}")]
        public List<DepartmentMasterViewModel> GetAllDepartmentById(int id)
        {
            return departmentMasterService.GetAllDepartmentById(id).Select(k => new DepartmentMasterViewModel
            {

                DepartmentMasterDepartmentId = k.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = k.DepartmentMasterDepartmentName,
                DepartmentMasterDepartmentStatus = k.DepartmentMasterDepartmentStatus,
                DepartmentMasterDepartmentDelStatus = k.DepartmentMasterDepartmentDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertDepartment")]
        public List<DepartmentMasterViewModel> InsertDepartment([FromBody] DepartmentMasterViewModel departmentMaster)
        {
            var result = _mapper.Map<DepartmentMaster>(departmentMaster);
            return departmentMasterService.InsertDepartment(result).Select(k => new DepartmentMasterViewModel
            {
                DepartmentMasterDepartmentId = k.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = k.DepartmentMasterDepartmentName,
                DepartmentMasterDepartmentStatus = k.DepartmentMasterDepartmentStatus,
                DepartmentMasterDepartmentDelStatus = k.DepartmentMasterDepartmentDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public List<DepartmentMasterViewModel> UpdateDepartment([FromBody] DepartmentMasterViewModel departmentMaster)
        {
            var result = _mapper.Map<DepartmentMaster>(departmentMaster);
            return departmentMasterService.UpdateDepartment(result).Select(k => new DepartmentMasterViewModel
            {
                DepartmentMasterDepartmentId = k.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = k.DepartmentMasterDepartmentName,
                DepartmentMasterDepartmentStatus = k.DepartmentMasterDepartmentStatus,
                DepartmentMasterDepartmentDelStatus = k.DepartmentMasterDepartmentDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteDepartment")]
        public List<DepartmentMasterViewModel> DeleteDepartment([FromBody] DepartmentMasterViewModel departmentMaster)
        {
            var result = _mapper.Map<DepartmentMaster>(departmentMaster);
            return departmentMasterService.DeleteDepartment(result).Select(k => new DepartmentMasterViewModel
            {
                DepartmentMasterDepartmentId = k.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = k.DepartmentMasterDepartmentName,
                DepartmentMasterDepartmentStatus = k.DepartmentMasterDepartmentStatus,
                DepartmentMasterDepartmentDelStatus = k.DepartmentMasterDepartmentDelStatus
            }).ToList();
        }
    }
}