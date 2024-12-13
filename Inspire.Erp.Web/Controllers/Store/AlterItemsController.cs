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
using Inspire.Erp.Application.StoreWareHouse.Interface;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/AlterItems")]
    [Produces("application/json")]
    [ApiController]
    public class AlterItemsController : ControllerBase
    {
        private IAlterItemsService _altitemService;
        private IMapper _mapper;
        public AlterItemsController(IAlterItemsService Manufacture, IMapper mapper)
        {
            _altitemService = Manufacture;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAlterItemsById")]
        public IActionResult GetAlterItemsById(int id)
        {
            try
            {
                var item = _altitemService.GetByID(id);
                return Ok(item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPost]
        [Route("InsertAlterItems")]
        public ApiResponse<AltItemMaster> InsertAlterItems([FromBody] AltItemMaster ManufactureItems)
        {

            var param1 = _mapper.Map<AltItemMaster>(ManufactureItems);
            var InsertManufactureItemsViewModel = _altitemService.save(param1);

            ApiResponse<AltItemMaster> apiResponse = new ApiResponse<AltItemMaster>
            {
                Valid = true,
                Result = _mapper.Map<AltItemMaster>(InsertManufactureItemsViewModel),
                Message = "Saved Successful"
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetAlterItems")]
        public ApiResponse<List<AltItemMaster>> GetAlterItems()
        {
            ApiResponse<List<AltItemMaster>> apiResponse = new ApiResponse<List<AltItemMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<AltItemMaster>>(_altitemService.GetAll()),
                Message = ""
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateAlterItems")]
        public ApiResponse<AltItemMaster> UpdateAlterItems([FromBody] AltItemMaster ManufactureItems)
        {            
            var UpdateManufactureItemsViewModel = _altitemService.update(ManufactureItems, ManufactureItems.AltItemDetails);


            ApiResponse<AltItemMaster> apiResponse = new ApiResponse<AltItemMaster>
            {
                Valid = true,
                Result = UpdateManufactureItemsViewModel,
                Message = ManufactureItemMessage.UpdateManufactureItem

            };
            return apiResponse;

        }
        [HttpDelete]
        [Route("DeleteAlterItems")]
        public ApiResponse<List<AltItemMaster>> DeleteAlterItems(int Id)
        {

            var xs = _altitemService.Delete(Id);
            if (xs != null)
            {
                ApiResponse<List<AltItemMaster>> apiResponse = new ApiResponse<List<AltItemMaster>>
                {
                    Valid = true,
                   // Result = '',
                    Message ="Deleted Successful"
                };
                return apiResponse;
            }
            return null;
        }     
    }
}
