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
    public class BusinessTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBusinessTypeMasterService businessTypeMasterService;
        public BusinessTypeController(IBusinessTypeMasterService _businessMasterService, IMapper mapper)
        {

            businessTypeMasterService = _businessMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllBusinessType")]
        public List<BusinessMasterViewModel> GetAllBusinessType()
        {

            return businessTypeMasterService.GetAllBusinessType().Select(k => new BusinessMasterViewModel
            {
                BusinessTypeMasterBusinessTypeId = k.BusinessTypeMasterBusinessTypeId,
                BusinessTypeMasterBusinessTypeName = k.BusinessTypeMasterBusinessTypeName,
                BusinessTypeMasterBusinessTypeStatus = k.BusinessTypeMasterBusinessTypeStatus,
                BusinessTypeMasterBusinessTypeDelStatus=k.BusinessTypeMasterBusinessTypeDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllBusinessTypeById/{id}")]
        public List<BusinessMasterViewModel> GetAllBusinessTypeById(int id)
        {
            return businessTypeMasterService.GetAllBusinessTypeById(id).Select(k => new BusinessMasterViewModel
            {

                BusinessTypeMasterBusinessTypeId = k.BusinessTypeMasterBusinessTypeId,
                BusinessTypeMasterBusinessTypeName = k.BusinessTypeMasterBusinessTypeName,
                BusinessTypeMasterBusinessTypeStatus = k.BusinessTypeMasterBusinessTypeStatus,
                BusinessTypeMasterBusinessTypeDelStatus = k.BusinessTypeMasterBusinessTypeDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertBusinessType")]
        public List<BusinessMasterViewModel> InsertBusinessType([FromBody] BusinessMasterViewModel businessTypeMaster)
        {
            var result = _mapper.Map<BusinessTypeMaster>(businessTypeMaster);
            return businessTypeMasterService.InsertBusinessType(result).Select(k => new BusinessMasterViewModel
            {
                BusinessTypeMasterBusinessTypeId = k.BusinessTypeMasterBusinessTypeId,
                BusinessTypeMasterBusinessTypeName = k.BusinessTypeMasterBusinessTypeName,
                BusinessTypeMasterBusinessTypeStatus = k.BusinessTypeMasterBusinessTypeStatus,
                BusinessTypeMasterBusinessTypeDelStatus = k.BusinessTypeMasterBusinessTypeDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateBusinessType")]
        public List<BusinessMasterViewModel> UpdateBusinessType([FromBody] BusinessMasterViewModel businessTypeMaster)
        {
            var result = _mapper.Map<BusinessTypeMaster>(businessTypeMaster);
            return businessTypeMasterService.UpdateBusinessType(result).Select(k => new BusinessMasterViewModel
            {
                BusinessTypeMasterBusinessTypeId = k.BusinessTypeMasterBusinessTypeId,
                BusinessTypeMasterBusinessTypeName = k.BusinessTypeMasterBusinessTypeName,
                BusinessTypeMasterBusinessTypeStatus = k.BusinessTypeMasterBusinessTypeStatus,
                BusinessTypeMasterBusinessTypeDelStatus = k.BusinessTypeMasterBusinessTypeDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteBusinessType")]
        public List<BusinessMasterViewModel> DeleteBusinessType([FromBody] BusinessMasterViewModel businessTypeMaster)
        {
            var result = _mapper.Map<BusinessTypeMaster>(businessTypeMaster);
            return businessTypeMasterService.DeleteBusinessType(result).Select(k => new BusinessMasterViewModel
            {
                BusinessTypeMasterBusinessTypeId = k.BusinessTypeMasterBusinessTypeId,
                BusinessTypeMasterBusinessTypeName = k.BusinessTypeMasterBusinessTypeName,
                BusinessTypeMasterBusinessTypeStatus = k.BusinessTypeMasterBusinessTypeStatus,
                BusinessTypeMasterBusinessTypeDelStatus = k.BusinessTypeMasterBusinessTypeDelStatus
            }).ToList();
        }
    }
}