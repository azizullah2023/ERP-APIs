using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels.Store;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.MODULE;
using Inspire.Erp.Application.Store.implementations;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/ManufactureItems")]
    [Produces("application/json")]
    [ApiController]
    public class ManufactureItemsController : ControllerBase
    {
        private IManufactureItems _Manufacture;
        private IMapper _mapper;
        public ManufactureItemsController(IManufactureItems Manufacture, IMapper mapper)
        {
            _Manufacture = Manufacture;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetManufactureItemsById")]
        public IActionResult GetManufactureItemsById(int id)
        {
            try
            {
                var item = _Manufacture.GetManufactureItemsById(id);
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("InsertManufactureItems")]
        public ApiResponse<ManufactureItemsMaster> InsertManufactureItems([FromBody] ManufactureItemsMaster ManufactureItems)
        {

            var param1 = _mapper.Map<ManufactureItemsMaster>(ManufactureItems);
            var InsertManufactureItemsViewModel = _Manufacture.InsertManufactureItems(param1);

            ApiResponse<ManufactureItemsMaster> apiResponse = new ApiResponse<ManufactureItemsMaster>
            {
                Valid = true,
                Result = _mapper.Map<ManufactureItemsMaster>(InsertManufactureItemsViewModel),
                Message = ManufactureItemMessage.SaveManufactureItem
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetManufactureItems")]
        public ApiResponse<List<ManufactureItemsMaster>> GetManufactureItems()
        {
            ApiResponse<List<ManufactureItemsMaster>> apiResponse = new ApiResponse<List<ManufactureItemsMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<ManufactureItemsMaster>>(_Manufacture.GetManufactureItems()),
                Message = ""
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateManufactureItems")]
        public ApiResponse<ManufactureItemsMaster> UpdateManufactureItems([FromBody] ManufactureItemsMaster ManufactureItems)
        {
            var param1 = _mapper.Map<ManufactureItemsMaster>(ManufactureItems);
            var UpdateManufactureItemsViewModel = _Manufacture.UpdateManufactureItems(param1);


            ApiResponse<ManufactureItemsMaster> apiResponse = new ApiResponse<ManufactureItemsMaster>
            {
                Valid = true,
                Result = _mapper.Map<ManufactureItemsMaster>(UpdateManufactureItemsViewModel),
                Message = ManufactureItemMessage.UpdateManufactureItem

            };
            return apiResponse;

        }
        [HttpDelete]
        [Route("DeleteManufactureItems")]
        public ApiResponse<List<ManufactureItemsMaster>> DeleteManufactureItems(int Id)
        {

            var xs = _Manufacture.DeleteManufactureItems(Id);
            if (xs != null)
            {
                ApiResponse<List<ManufactureItemsMaster>> apiResponse = new ApiResponse<List<ManufactureItemsMaster>>
                {
                    Valid = true,
                   // Result = '',
                    Message = ManufactureItemMessage.DeleteManufactureItem
                };
                return apiResponse;
            }
            return null;
        }

        [HttpGet]
        [Route("GetManufactureItemVoucherNo/{id}")]
        public ApiResponse<ManufactureItemsMaster> GetManufactureItemVoucherNo(string id)
        {
            ManufactureItemsMaster Manufacture = _Manufacture.GetManufactureItemVoucherNo(id);

            if (Manufacture != null)
            {
                ApiResponse<ManufactureItemsMaster> apiResponse = new ApiResponse<ManufactureItemsMaster>
                {
                    Valid = true,
                    Result = Manufacture,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }
        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                var item = _Manufacture.GenerateVoucherNo(null);
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

    }
}
