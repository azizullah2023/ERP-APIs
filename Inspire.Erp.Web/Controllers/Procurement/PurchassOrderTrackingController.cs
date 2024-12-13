using AutoMapper;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/PurchassOrderTracking")]
    [Produces("application/json")]
    [ApiController]
    public class PurchassOrderTrackingController : Controller
    {

        private IPurchaseOrdertracking purchaseOrdertracking;
        private IRepository<UserTracking> _UserTracking;
        private IRepository<UserFile> _UserFile;
        private IMapper _mapper;
        private IRepository<ItemMaster> itemrepository; private IRepository<PurchaseOrder> _purchaseOrderRepository;
        private readonly IRepository<SuppliersMaster> supplierrepository;
        private IRepository<UnitMaster> unitrepository;
        private IRepository<CostCenterMaster> costcenterrepository;
        private IRepository<JobMaster> jobrepository; private IRepository<LocationMaster> locationrepository;
        private IRepository<CurrencyMaster> currencyrepository;
        private IChartofAccountsService chartofAccountsService;
        public PurchassOrderTrackingController(IRepository<UserFile> UserFile, IPurchaseOrdertracking purchaseOrdertracking, IRepository<UserTracking> UserTracking, IMapper mapper,
             IRepository<ItemMaster> _itemrepository,
            IRepository<SuppliersMaster> _supplierrepository,
            IRepository<UnitMaster> _unitrepository, IRepository<LocationMaster> _locationrepository
            , IRepository<CostCenterMaster> _countryrepository, IRepository<PurchaseOrder> purchaseOrderRepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService)
        {
            this.purchaseOrdertracking = purchaseOrdertracking;
            _mapper = mapper;
            _UserTracking = UserTracking;
            _UserFile = UserFile;
            supplierrepository = _supplierrepository; _purchaseOrderRepository = purchaseOrderRepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository;
            costcenterrepository = _countryrepository;
            jobrepository = _jobrepository; locationrepository = _locationrepository;
            currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }

        //Irfan 08 Dec 2023
        /// <summary>
        /// Name : getPurchaseOrderTrackingDetails
        /// Desc : used to get purchase order tracking details 
        /// </summary>
        /// <param name="reportFilter">
        /// need to set these attrubutes to load data accordingly
        /// PONO, fromDate, toDate, SupplierId, JobId, LocationId, ItemId
        /// </param>
        /// <returns> 
        /// </returns>
        [HttpPost]
        [Route("GetPurchaseOrderTrackingDetails")]
        public async Task<IActionResult> getPurchaseOrderTrackingDetails(ReportFilter reportFilter)
        {
            return Ok(await purchaseOrdertracking.GetPurchaseOrderTrackingDetails(reportFilter));
        }

        //Irfan 08 Dec 2023
        /// <summary>
        /// Name : GetPurchaseOrderTrackingSummary
        /// Desc : used to get purchase order tracking Summary 
        /// </summary>
        /// <param name="reportFilter">
        ///  need to set these attrubutes to load data accordingly
        /// PONO, fromDate, toDate, SupplierId, JobId, LocationId, ItemId</param>
        /// <returns> 
        /// </returns>
        [HttpPost]
        [Route("GetPurchaseOrderTrackingSummary")]
        public async Task<IActionResult> getPurchaseOrderTrackingSummary(ReportFilter reportFilter)
        {
            return Ok(await purchaseOrdertracking.GetPurchaseOrderTrackingSummary(reportFilter));
        }

        [HttpPost]
        [Route("GetUserTrackingDetails")]
        public async Task<IActionResult> GetUserTrackingDetails(UserTrackFilter reportFilter)
        {
            return Ok(await purchaseOrdertracking.GetUserTrackingDetails(reportFilter));
        }

        [HttpGet("GetTrackingUserVPType")]
        public ResponseInfo GetTrackingUserVPType()
        {
            var objectresponse = new ResponseInfo();
            var response = _UserTracking.GetAsQueryable().Select(ut => new
            {
                VpType = ut.UserTrackingUserVpType ?? "",
            }).Distinct().ToList();
            objectresponse.ResultSet = new
            {
                response = response
            };
            return objectresponse;
        }


        [HttpGet("GetTrackingUserVPAction")]
        public ResponseInfo GetTrackingUserVPAction()
        {
            var objectresponse = new ResponseInfo();
            var response = _UserTracking.GetAsQueryable().Select(ut => new
            {
                UserTrackingUserVpAction = ut.UserTrackingUserVpAction ?? ""

            }).Distinct().ToList();
            objectresponse.ResultSet = new
            {
                response = response
            };
            return objectresponse;
        }
        [HttpGet("GetTrackingUserUserFile")]
        public ResponseInfo GetTrackingUserUserFile()
        {
            var objectresponse = new ResponseInfo();
            var response = _UserFile.GetAsQueryable().Select(ut => new
            {
                LogInId = ut.UserId,
                username = ut.UserName

            }).Distinct().ToList();
            objectresponse.ResultSet = new
            {
                response = response
            };
            return objectresponse;
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
                         k.ItemMasterItemName
                     }).ToList();

            var SuppliersMaster = supplierrepository.GetAsQueryable().Where(a => a.SuppliersMasterSupplierDelStatus != true).Select(c => new
            {
                c.SuppliersMasterSupplierId,
                c.SuppliersMasterSupplierName,
                c.SuppliersMasterSupplierVatNo
            }).ToList();

            var jobMasters = jobrepository.GetAsQueryable().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                c.JobMasterJobName,
            }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName,
            }).ToList();

            var purchaseOrder = _purchaseOrderRepository.GetAsQueryable().Where(k => k.PurchaseOrderDelStatus != true).Select(c => new
            {
                c.PurchaseOrderId,
                c.PurchaseOrderNo
            }).ToList();
            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                SuppliersMaster = SuppliersMaster,
                jobMasters = jobMasters,
                LocationMaster = LocationMaster,
                purchaseOrder = purchaseOrder
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }
    }
}
