using AutoMapper;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GRNController : ControllerBase
    {
        private IGRNService _grn;
        private readonly IMapper _mapper;

        private IRepository<ItemMaster> itemrepository;
        private readonly IRepository<SuppliersMaster> supplierrepository;
        private IRepository<UnitMaster> unitrepository; IRepository<UnitDetails> unitDetailRepository;
        private IRepository<CostCenterMaster> costcenterrepository; private IRepository<VendorMaster> Brandrepository;
        private IRepository<JobMaster> jobrepository; private IRepository<LocationMaster> locationrepository;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<AccountSettings> _accountsSettingsRepo;
        private IChartofAccountsService chartofAccountsService;
        public GRNController(IGRNService grn, IMapper mapper, IRepository<ItemMaster> _itemrepository,
            IRepository<SuppliersMaster> _supplierrepository, IRepository<VendorMaster> _Brandrepository,
            IRepository<UnitMaster> _unitrepository, IRepository<UnitDetails> _unitDetailRepository, IRepository<LocationMaster> _locationrepository
            , IRepository<CostCenterMaster> _countryrepository, IRepository<AccountSettings> accountsSettingsRepo
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService
            )
        {
            _grn = grn;
            _mapper = mapper;
            supplierrepository = _supplierrepository; Brandrepository = _Brandrepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; unitDetailRepository = _unitDetailRepository;
            costcenterrepository = _countryrepository; _accountsSettingsRepo = accountsSettingsRepo;
            jobrepository = _jobrepository; locationrepository = _locationrepository;
            currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }

        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_grn.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllGRN")]
        public IActionResult GetAllGRN()
        {
            try
            {
                return Ok(_grn.GetAllGRN());
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByID")]
        public IActionResult GetByID(string voucherNo)
        {
            try
            {
                return Ok(_grn.GetByID(voucherNo));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("saveGRNVoucher")]
        public ApiResponse<PurchaseVoucher> saveGRN([FromBody] PurchaseVoucher obj)
        {
            var x = _grn.saveGRN(obj);
            if (x != null)
            {
                ApiResponse<PurchaseVoucher> apiResponse = new ApiResponse<PurchaseVoucher>
                {
                    Valid = true,
                    Result = x,
                    Message = GRNMessage.SaveVoucher,
                };
                return apiResponse;
            }
            else
                return null;
            //return Ok(_grn.saveGRN(obj));
        }

        [HttpPost("updateGRN")]
        public ApiResponse<PurchaseVoucher> updateGRN([FromBody] PurchaseVoucher obj)
        {
            var x = _grn.updateGRN(obj, obj.AccountsTransactions.ToList());
            if (x != null)
            {
                ApiResponse<PurchaseVoucher> apiResponse = new ApiResponse<PurchaseVoucher>
                {
                    Valid = true,
                    Result = x,
                    Message = GRNMessage.UpdateVoucher,
                };
                return apiResponse;
            }
            else
                return null;
            //return Ok(_grn.updateGRN(obj, obj.AccountsTransactions.ToList()));
        }

        [HttpPost("DeleteGRN")]
        public IActionResult DeleteGRN([FromBody] PurchaseVoucher obj)
        {
            return Ok(_grn.DeleteGRN(obj));
        }


        [HttpPost("loadPO")]
        public async Task<IEnumerable<GRNResponse>> loadPO(GRNRequest obj)
        {
            return _grn.loadPo(obj);
        }


        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();
            var itemMaster = (from item in itemrepository.GetAsQueryable().AsNoTracking()
                              join unit in unitDetailRepository.GetAsQueryable().AsNoTracking()
                              on item.ItemMasterItemId equals unit.UnitDetailsItemId
                              where item.ItemMasterAccountNo != 0
                              && item.ItemMasterDelStatus != true
                              && item.ItemMasterItemType != ItemMasterStatus.Group
                              select new
                              {
                                  item.ItemMasterItemId,
                                  item.ItemMasterItemName,
                                  item.ItemMasterVenderId,
                                  item.ItemMasterItemSize,
                                  itemMasterBarcode = unit.UnitDetailsBarcode ?? ""
                              }).Distinct().ToList();

            var SuppliersMaster = supplierrepository.GetAsQueryable().AsNoTracking().Where(a => a.SuppliersMasterSupplierDelStatus != true).Select(c => new
            {
                c.SuppliersMasterSupplierId,
                c.SuppliersMasterSupplierName,
                c.SuppliersMasterSupplierVatNo,
                c.SuppliersMasterSupplierReffAcNo,
                c.SuppliersMasterSupplierAddress,
                c.SuppliersMasterSupplierContactPerson,
            }).ToList();

            var unitMasters = unitrepository.GetAsQueryable().AsNoTracking().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                UnitMasterUnitFullName = x.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim(),
            }).ToList();

            var jobMasters = jobrepository.GetAsQueryable().AsNoTracking().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                c.JobMasterJobName,
            }).ToList();
            var currencyMasters = currencyrepository.GetAsQueryable().AsNoTracking().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                c.CurrencyMasterCurrencyName,
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true).Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();
            var costcenterMasters = costcenterrepository.GetAsQueryable().AsNoTracking().Where(a => a.CostCenterMasterCostCenterDelStatus != true).Select(c => new
            {
                c.CostCenterMasterCostCenterId,
                c.CostCenterMasterCostCenterName,
            }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().AsNoTracking().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName,
            }).ToList();
            var BrandMasters = Brandrepository.GetAsQueryable().AsNoTracking().Where(a => a.VendorMasterVendorDelStatus != true).Select(c => new
            {
                c.VendorMasterVendorId,
                c.VendorMasterVendorName,
            }).ToList();
            var accountsSettings = _accountsSettingsRepo.GetAsQueryable().AsNoTracking().Where(a => a.AccountSettingsAccountDelStatus != true).Select(c => new
            {
                c.AccountSettingsAccountId,
                AccountSettingsAccountDescription = c.AccountSettingsAccountDescription.Trim(),
                c.AccountSettingsAccountTextValue
            }).ToList();

            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                SuppliersMaster = SuppliersMaster,
                unitMasters = unitMasters,
                jobMasters = jobMasters,
                currencyMasters = currencyMasters,
                masterAccountsTables = masterAccountsTables,
                costcenterMasters = costcenterMasters,
                LocationMaster = LocationMaster,
                accountsSettings = accountsSettings,
                BrandMasters = BrandMasters
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }

    }
}