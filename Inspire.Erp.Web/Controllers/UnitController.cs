using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/master")]
    [Produces("application/json")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IUnitMasterService unitMasterService;
        public UnitController(IUnitMasterService _unitMasterService, IMapper mapper)
        {
            unitMasterService = _unitMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllUnit")]
        public List<UnitMasterViewModel> GetAllUnit()
        {
            return unitMasterService.GetAllUnit().Select(k => new UnitMasterViewModel
            {
                 UnitMasterUnitId = k.UnitMasterUnitId,
                 UnitMasterUnitShortName = k.UnitMasterUnitShortName.Trim(),
                 UnitMasterUnitFullName = k.UnitMasterUnitFullName.Trim(),
                 UnitMasterUnitDescription = k.UnitMasterUnitDescription,
                 UnitMasterUnitStatus = k.UnitMasterUnitStatus,
                UnitMasterUnitDelStatus=k.UnitMasterUnitDelStatus,
                UnitDetailsConversionType = k.UnitDetails.Select(x => x.UnitDetailsConversionType).FirstOrDefault()
            }).ToList();
        }

        [HttpGet]
        [Route("GetUnitDetails")]
        public async Task<IActionResult> GetUnitDetails(long itemId)
        {
            var response = await unitMasterService.GetUnitDetailsByItemId(itemId);
            if(response.Status=="Success")
            {
                ApiResponse<List<UnitMasterViewModel>> apiResponse = new ApiResponse<List<UnitMasterViewModel>>
                {
                    Valid = true,
                    Result = _mapper.Map<List<UnitMasterViewModel>>(response.Result),
                    Message = ""
                };
                return Ok(apiResponse);
            }
            else
            {
                return BadRequest(response.Result);
            }
      
        }

        [HttpGet]
        [Route("GetAllUnitById/{id}")]
        public List<UnitMasterViewModel> GetAllUnitById(int id)
        {
            return unitMasterService.GetAllUnitById(id).Select(k => new UnitMasterViewModel
            {
                UnitMasterUnitId = k.UnitMasterUnitId,
                UnitMasterUnitShortName = k.UnitMasterUnitShortName.Trim(),
                UnitMasterUnitFullName = k.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitDescription = k.UnitMasterUnitDescription,
                UnitMasterUnitStatus = k.UnitMasterUnitStatus,
                UnitMasterUnitDelStatus = k.UnitMasterUnitDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertUnit")]
        public List<UnitMasterViewModel> InsertUnit([FromBody] UnitMasterViewModel unitMaster)
        {
            var result = _mapper.Map<UnitMaster>(unitMaster);
            return unitMasterService.InsertUnit(result).Select(k => new UnitMasterViewModel
            {
                UnitMasterUnitId = k.UnitMasterUnitId,
                UnitMasterUnitShortName = k.UnitMasterUnitShortName.Trim(),
                UnitMasterUnitFullName = k.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitDescription = k.UnitMasterUnitDescription,
                UnitMasterUnitStatus = k.UnitMasterUnitStatus,
                UnitMasterUnitDelStatus = k.UnitMasterUnitDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateUnit")]
        public List<UnitMasterViewModel> UpdateUnit([FromBody] UnitMasterViewModel unitMaster)
        {
            var result = _mapper.Map<UnitMaster>(unitMaster);
            return unitMasterService.UpdateUnit(result).Select(k => new UnitMasterViewModel
            {
                UnitMasterUnitId = k.UnitMasterUnitId,
                UnitMasterUnitShortName = k.UnitMasterUnitShortName.Trim(),
                UnitMasterUnitFullName = k.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitDescription = k.UnitMasterUnitDescription,
                UnitMasterUnitStatus = k.UnitMasterUnitStatus,
                UnitMasterUnitDelStatus = k.UnitMasterUnitDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteUnit")]
        public List<UnitMasterViewModel> DeleteUnit([FromBody] UnitMasterViewModel unitMaster)
        {
            var result = _mapper.Map<UnitMaster>(unitMaster);
            return unitMasterService.DeleteUnit(result).Select(k => new UnitMasterViewModel
            {
                UnitMasterUnitId = k.UnitMasterUnitId,
                UnitMasterUnitShortName = k.UnitMasterUnitShortName.Trim(),
                UnitMasterUnitFullName = k.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitDescription = k.UnitMasterUnitDescription,
                UnitMasterUnitStatus = k.UnitMasterUnitStatus,
                UnitMasterUnitDelStatus = k.UnitMasterUnitDelStatus
            }).ToList();
        }
    }
}
