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
    public class TypeMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ITypeMasterService typeMasterService;
        public TypeMasterController(ITypeMasterService _typeMasterService, IMapper mapper)
        {

            typeMasterService = _typeMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllTypeMast")]
        public List<TypeMasterViewModel> GetAllTypeMast()
        {

            return typeMasterService.GetAllTypeMast().Select(k => new TypeMasterViewModel
            {
                TypeMasterTypeId = k.TypeMasterTypeId,
                TypeMasterVendorId = k.TypeMasterVendorId,
                TypeMasterTypeName = k.TypeMasterTypeName,
                TypeMasterUserId = k.TypeMasterUserId,
                TypeMasterDeleted = k.TypeMasterDeleted,
                TypeMasterStatus = k.TypeMasterStatus,
                TypeMasterDelStatus=k.TypeMasterDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllTypeMastById/{id}")]
        public List<TypeMasterViewModel> GetAllTypeMastById(int id)
        {
            return typeMasterService.GetAllTypeMastById(id).Select(k => new TypeMasterViewModel
            {
                TypeMasterTypeId = k.TypeMasterTypeId,
                TypeMasterVendorId = k.TypeMasterVendorId,
                TypeMasterTypeName = k.TypeMasterTypeName,
                TypeMasterUserId = k.TypeMasterUserId,
                TypeMasterDeleted = k.TypeMasterDeleted,
                TypeMasterStatus = k.TypeMasterStatus,
                TypeMasterDelStatus = k.TypeMasterDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertTypeMast")]
        public List<TypeMasterViewModel> InsertTypeMast([FromBody] TypeMasterViewModel typeMaster)
        {
            var result = _mapper.Map<TypeMaster>(typeMaster);
            return typeMasterService.InsertTypeMast(result).Select(k => new TypeMasterViewModel
            {
                TypeMasterTypeId = k.TypeMasterTypeId,
                TypeMasterVendorId = k.TypeMasterVendorId,
                TypeMasterTypeName = k.TypeMasterTypeName,
                TypeMasterUserId = k.TypeMasterUserId,
                TypeMasterDeleted = k.TypeMasterDeleted,
                TypeMasterStatus = k.TypeMasterStatus,
                TypeMasterDelStatus = k.TypeMasterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateTypeMast")]
        public List<TypeMasterViewModel> UpdateTypeMast([FromBody] TypeMasterViewModel typeMaster)
        {
            var result = _mapper.Map<TypeMaster>(typeMaster);
            return typeMasterService.UpdateTypeMast(result).Select(k => new TypeMasterViewModel
            {
                TypeMasterTypeId = k.TypeMasterTypeId,
                TypeMasterVendorId = k.TypeMasterVendorId,
                TypeMasterTypeName = k.TypeMasterTypeName,
                TypeMasterUserId = k.TypeMasterUserId,
                TypeMasterDeleted = k.TypeMasterDeleted,
                TypeMasterStatus = k.TypeMasterStatus,
                TypeMasterDelStatus = k.TypeMasterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteTypeMast")]
        public List<TypeMasterViewModel> DeleteTypeMast([FromBody] TypeMasterViewModel typeMaster)
        {
            var result = _mapper.Map<TypeMaster>(typeMaster);
            return typeMasterService.DeleteTypeMast(result).Select(k => new TypeMasterViewModel
            {
                TypeMasterTypeId = k.TypeMasterTypeId,
                TypeMasterVendorId = k.TypeMasterVendorId,
                TypeMasterTypeName = k.TypeMasterTypeName,
                TypeMasterUserId = k.TypeMasterUserId,
                TypeMasterDeleted = k.TypeMasterDeleted,
                TypeMasterStatus = k.TypeMasterStatus,
                TypeMasterDelStatus = k.TypeMasterDelStatus
            }).ToList();
        }
    }
}