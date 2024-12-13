using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Store
{
    [Route("api/StockLedger")]
    [Produces("application/json")]
    [ApiController]
    public class StockLedgerController : ControllerBase
    {
        private IStockLedger stockLedger;
        private IRepository<ItemMaster> itemrepository; private IRepository<LocationMaster> locationrepository;
        public StockLedgerController(IStockLedger stockLedger, IRepository<ItemMaster> _itemrepository, IRepository<LocationMaster> _locationrepository)
        {
            this.stockLedger = stockLedger;
            itemrepository = _itemrepository; locationrepository = _locationrepository;
        }

        [HttpPost]
        [Route("GetStockLedger")]
        public async Task<IActionResult> GetStockLedger(StockFilterModel stockFilterModel)
        {
            return Ok(await stockLedger.GetStockLedgerReport(stockFilterModel));
        }
        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();

            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                 && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
                 {
                     k.ItemMasterItemId,
                     k.ItemMasterItemName,
                     k.ItemMasterBarcode,
                     k.ItemMasterPartNo,
                     k.ItemMasterCurrentStock,
                     k.ItemMasterAccountNo
                 }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName
            }).ToList();
            var itemMasterGroup = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo == 0 && (k.ItemMasterDelStatus != true)
                && k.ItemMasterItemType == ItemMasterStatus.Group).Select(k => new
                {
                    k.ItemMasterItemId,
                    k.ItemMasterItemName,
                }).ToList();
            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                LocationMaster = LocationMaster,
                itemMasterGroup = itemMasterGroup
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }
    }
}
