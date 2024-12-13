using AutoMapper;
using Inspire.Erp.Application.Account.Interface;
using Inspire.Erp.Application.StoreWareHouse.Implementation;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
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
    public class DamageEntryController : ControllerBase
    {

        private IDamageEntry DamageEntry;
        private readonly IMapper _mapper;
        public DamageEntryController(IDamageEntry _DamageEntry, IMapper mapper)
        {
            _mapper = mapper;
            DamageEntry = _DamageEntry;
  
        }
        [HttpPost]
        [Route("InsertDamageEntry")]       
        public ApiResponse<DamageMaster> InsertDamageEntry([FromBody] DamageMaster DamageViewModel)
        {

            var param1 = _mapper.Map<DamageMaster>(DamageViewModel);
            var InsertDamageViewModelViewModel = DamageEntry.InsertDamageEntry(param1);

            ApiResponse<DamageMaster> apiResponse = new ApiResponse<DamageMaster>
            {
                Valid = true,
                Result = _mapper.Map<DamageMaster>(InsertDamageViewModelViewModel),
                Message = DamageMessage.SaveDamage
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetDamageById")]
        public IActionResult GetJobDamageId(int id)
        {
            try
            {
                var item = DamageEntry.GetDamageById(id);
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetDamageEntry")]
        public ApiResponse<List<DamageMaster>> GetDamageEntry()
        {
            ApiResponse<List<DamageMaster>> apiResponse = new ApiResponse<List<DamageMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<DamageMaster>>(DamageEntry.GetDamageEntry()),
                Message = ""
            };
            return apiResponse;
        }
        [HttpDelete]
        [Route("DeleteDamageEntry")]
        public IActionResult DeleteDamageEntry(string Id)
        {
            try
            {
                return Ok(DamageEntry.DeleteDamageEntry(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("UpdateDamageEntry")]       
        public ApiResponse<DamageMaster> UpdateDamageEntry([FromBody] DamageMaster DamageViewModel)
        {
            var param1 = _mapper.Map<DamageMaster>(DamageViewModel);
            var UpdateDamageViewModel = DamageEntry.UpdateDamageEntry(param1);


            ApiResponse<DamageMaster> apiResponse = new ApiResponse<DamageMaster>
            {
                Valid = true,
                Result = _mapper.Map<DamageMaster>(UpdateDamageViewModel),
                Message = DamageMessage.UpdateDamage

            };
            return apiResponse;

        }
        [HttpGet]
        [Route("GetDamageEntryVoucherNo/{id}")]
        public ApiResponse<DamageMaster> GetDamageEntryVoucherNo(string id)
        {
            DamageMaster salesJournal = DamageEntry.GetDamageEntryVoucherNo(id);

            if (salesJournal != null)
            {
                ApiResponse<DamageMaster> apiResponse = new ApiResponse<DamageMaster>
                {
                    Valid = true,
                    Result = salesJournal,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }


    }
}