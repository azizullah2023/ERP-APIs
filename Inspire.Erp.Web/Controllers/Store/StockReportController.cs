////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;
////using Microsoft.AspNetCore.Mvc;

////namespace Inspire.Erp.Web.Controllers.Store
////{
////    public class OpeningStockController : Controller
////    {

////    }
////}

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
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Modals.Common;

using Inspire.Erp.Domain.Modals.Stock;
using NPOI.OpenXmlFormats.Dml;
using System.Net.Http.Headers;
using Inspire.Erp.Infrastructure.Database.Repositoy;

namespace Inspire.Erp.Web.Controllers.Store
{
    [Route("api/StockReport")]
    [Produces("application/json")]
    [ApiController]
    public class StockReportController : ControllerBase
    {
        private IStockService _openingStockService;
        private readonly IMapper _mapper;
        private IRepository<ItemMaster> itemrepository;
        public StockReportController(IStockService openingStockService, IRepository<ItemMaster> _itemrepository, IMapper mapper)
        {
            _openingStockService = openingStockService;
            _mapper = mapper;
            itemrepository = _itemrepository;
        }

        [HttpPost]
        [Route("GetStockReportBaseUnit")]
        public async Task<IActionResult> GetStockReportBaseUnit(StockFilterModel model)
        {

            var results = await _openingStockService.GetStockReportBaseUnit(model);
            return Ok(results);

        }

        [HttpPost]
        [Route("GetAveragePurchasePrice")]
        public async Task<IActionResult> GetAveragePurchasePrice(int itemId)
        {

            var results = await _openingStockService.GetAveragePurchasePrice(itemId,null);
            return Ok(results);

        }

        [HttpPost]
        [Route("GetAveragePurchasePriceGPC")]
        public async Task<IActionResult> GetAveragePurchasePriceGPC(int itemId, int unitDetailId, decimal? quantity)
        {

            var results = await _openingStockService.GetAveragePurchasePriceGPC(itemId, unitDetailId,quantity);
            return Ok(results);

        }
        [HttpGet]
        [Route("GetStockReportBaseUnitByItemId/{id}")]
        public async Task<IActionResult> GetStockReportBaseUnit(int id)
        {

            var results = await _openingStockService.GetStockReportBaseUnit(new StockFilterModel()
            {
                ItemId = id,
                ExcludeZeroBalance = false,
                ExcludeZeroOpeningBalance = false,
                IsDateChecked = false,
                ItemGroupId = 0,
                ItemName = string.Empty,
                LocationId = 0,
                PartNo = string.Empty,
                OrderByGroup = false,
                PrintReceivedQtyReport = false,
            });
            return Ok(results);
            //ApiResponse<List<StockReportBaseUnitResponse>> apiResponse = new ApiResponse<List<StockReportBaseUnitResponse>>
            //{
            //    Valid = true,
            //    Result = _mapper.Map<List<StockReportBaseUnitResponse>>(_openingStockService.GetStockReportBaseUnitList()),
            //    Message = ""
            //};
            //return apiResponse;
        }


        [HttpPost]
        [Route("GetStockMovementReport")]
        public async Task<IActionResult> GetStockMovementReport(StockFilterModel model)
        {
            var results = await _openingStockService.GetStockMovementReport(model);
            return Ok(results);
        }

        [HttpPost]
        [Route("GetStockReportBaseUnitWithValue")]
        public async Task<IActionResult> GetStockReportBaseUnitWithValue(GenericGridViewModel model)
        {
            return Ok(await _openingStockService.GetStockReportBaseUnitWithValue(model));
        }

        [HttpPost]
        [Route("GetStockMovement")]
        public async Task<IActionResult> GetStockMovement(StockFilterModel model)
        {

            var results = await _openingStockService.GetStockMovement(model);
            return Ok(results);

        }
        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();
            var itemMasterGroup = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo == 0 && (k.ItemMasterDelStatus != true)
                     && k.ItemMasterItemType == ItemMasterStatus.Group).Select(k => new
                     {
                         k.ItemMasterItemId,
                         k.ItemMasterItemName
                     }).ToList();
            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
             && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
             {
                 k.ItemMasterItemId,
                 k.ItemMasterItemName
             }).ToList();

            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }


    }
}