using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ISupplierMasterService supplierMasterService;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CountryMaster> countryrepository;
        private IRepository<CityMaster> cityrepository; private IRepository<LocationMaster> locationrepository;
        public SupplierController(ISupplierMasterService _SupplierMasterService, IMapper mapper, IRepository<CurrencyMaster> _currencyrepository, IRepository<CountryMaster> _countryrepository
            , IRepository<LocationMaster> _locationrepository, IRepository<CityMaster> _Cityrepository)
        {

            supplierMasterService = _SupplierMasterService;
            _mapper = mapper; locationrepository = _locationrepository;
            currencyrepository = _currencyrepository; countryrepository = _countryrepository; cityrepository = _Cityrepository;
        }
        [HttpGet]
        [Route("GetAllSupplier")]
        public List<SuppliersMasterViewModel> GetAllSupplier()
        {
            return supplierMasterService.GetAllSupplier().Select(k => new SuppliersMasterViewModel
            {
                SuppliersMasterSupplierId = k.SuppliersMasterSupplierId,
                //SuppliersMasterSupplierCode= k.SuppliersMasterSupplierCode,
                SuppliersMasterSupplierName = k.SuppliersMasterSupplierName,
                SuppliersMasterSupplierContactPerson = k.SuppliersMasterSupplierContactPerson,
                SuppliersMasterSupplierCountryId = k.SuppliersMasterSupplierCountryId,
                SuppliersMasterSupplierCityId = k.SuppliersMasterSupplierCityId,
                SuppliersMasterSupplierPoBox = k.SuppliersMasterSupplierPoBox,
                SuppliersMasterSupplierTel1 = k.SuppliersMasterSupplierTel1,
                SuppliersMasterSupplierTel2 = k.SuppliersMasterSupplierTel2,
                SuppliersMasterSupplierFax = k.SuppliersMasterSupplierFax,
                SuppliersMasterSupplierMobile = k.SuppliersMasterSupplierMobile,
                SuppliersMasterSupplierEmail = k.SuppliersMasterSupplierEmail,
                SuppliersMasterSupplierWebSite = k.SuppliersMasterSupplierWebSite,
                SuppliersMasterSupplierLocationId = k.SuppliersMasterSupplierLocationId ?? -1,
                SuppliersMasterSupplierAddress = k.SuppliersMasterSupplierAddress,
                SuppliersMasterSupplierWhatsAppNo = k.SuppliersMasterSupplierWhatsAppNo,
                SuppliersMasterSupplierRemarks = k.SuppliersMasterSupplierRemarks,
                SuppliersMasterSupplierReffAcNo = k.SuppliersMasterSupplierReffAcNo,
                SuppliersMasterSupplierType = k.SuppliersMasterSupplierType,
                SuppliersMasterSupplierUserId = k.SuppliersMasterSupplierUserId,
                SuppliersMasterSupplierCurrencyId = k.SuppliersMasterSupplierCurrencyId,
                SuppliersMasterSupplierConsup = k.SuppliersMasterSupplierConsup,
                SuppliersMasterSupplierCrl = k.SuppliersMasterSupplierCrl,
                SuppliersMasterSupplierStatus = k.SuppliersMasterSupplierStatus,
                SuppliersMasterSupplierDeleteStatus = k.SuppliersMasterSupplierDeleteStatus,
                SuppliersMasterSupplierStatusValue = k.SuppliersMasterSupplierStatusValue,
                SuppliersMasterSupplierPaymentTerms = k.SuppliersMasterSupplierPaymentTerms,
                SuppliersMasterSupplierCreditDays = k.SuppliersMasterSupplierCreditDays,
                SuppliersMasterSupplierCreditLimit = k.SuppliersMasterSupplierCreditLimit,
                SuppliersMasterSupplierVatNo = k.SuppliersMasterSupplierVatNo,
                SuppliersMasterSupplierDelStatus = k.SuppliersMasterSupplierDelStatus
            }).ToList();
        }


        //[HttpGet("{id}")]
        [HttpGet("GetAllSupplierById/{id}")]
        public ApiResponse<SuppliersMasterViewModel> GetAllSupplierById(int id)
        {
            ApiResponse<SuppliersMasterViewModel> apiResponse = new ApiResponse<SuppliersMasterViewModel>();

            var xs = supplierMasterService.GetAllSupplierById(id);
            //    .Select(k => new SuppliersMasterViewModel
            //{
            //    SuppliersMasterSupplierId = k.SuppliersMasterSupplierId,
            //    //SuppliersMasterSupplierCode = k.SuppliersMasterSupplierCode,
            //    SuppliersMasterSupplierName = k.SuppliersMasterSupplierName,
            //    SuppliersMasterSupplierContactPerson = k.SuppliersMasterSupplierContactPerson,
            //    SuppliersMasterSupplierCountryId = k.SuppliersMasterSupplierCountryId,
            //    SuppliersMasterSupplierCityId = k.SuppliersMasterSupplierCityId,
            //    SuppliersMasterSupplierPoBox = k.SuppliersMasterSupplierPoBox,
            //    SuppliersMasterSupplierTel1 = k.SuppliersMasterSupplierTel1,
            //    SuppliersMasterSupplierTel2 = k.SuppliersMasterSupplierTel2,
            //    SuppliersMasterSupplierFax = k.SuppliersMasterSupplierFax,
            //    SuppliersMasterSupplierMobile = k.SuppliersMasterSupplierMobile,
            //    SuppliersMasterSupplierEmail = k.SuppliersMasterSupplierEmail,
            //    SuppliersMasterSupplierWebSite = k.SuppliersMasterSupplierWebSite,
            //    SuppliersMasterSupplierLocationId = k.SuppliersMasterSupplierLocationId,
            //    SuppliersMasterSupplierAddress = k.SuppliersMasterSupplierAddress,
            //    SuppliersMasterSupplierWhatsAppNo = k.SuppliersMasterSupplierWhatsAppNo,
            //    SuppliersMasterSupplierRemarks = k.SuppliersMasterSupplierRemarks,
            //    SuppliersMasterSupplierReffAcNo = k.SuppliersMasterSupplierReffAcNo,
            //    SuppliersMasterSupplierType = k.SuppliersMasterSupplierType,
            //    SuppliersMasterSupplierUserId = k.SuppliersMasterSupplierUserId,
            //    SuppliersMasterSupplierCurrencyId = k.SuppliersMasterSupplierCurrencyId,
            //    SuppliersMasterSupplierConsup = k.SuppliersMasterSupplierConsup,
            //    SuppliersMasterSupplierCrl = k.SuppliersMasterSupplierCrl,
            //    SuppliersMasterSupplierStatus = k.SuppliersMasterSupplierStatus,
            //    SuppliersMasterSupplierDeleteStatus = k.SuppliersMasterSupplierDeleteStatus,
            //    SuppliersMasterSupplierStatusValue = k.SuppliersMasterSupplierStatusValue,
            //    SuppliersMasterSupplierPaymentTerms = k.SuppliersMasterSupplierPaymentTerms,
            //    SuppliersMasterSupplierCreditDays = k.SuppliersMasterSupplierCreditDays,
            //    SuppliersMasterSupplierCreditLimit = k.SuppliersMasterSupplierCreditLimit,
            //    SuppliersMasterSupplierVatNo = k.SuppliersMasterSupplierVatNo,
            //    SuppliersMasterSupplierDelStatus = k.SuppliersMasterSupplierDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<SuppliersMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<SuppliersMasterViewModel>(xs),
                Message = "data found",
            };
            return apiResponse;


        }

        [HttpPost]
        [Route("InsertSupplier")]
        public async Task<ApiResponse<SuppliersMasterViewModel>> InsertSupplier([FromBody] SuppliersMasterViewModel suppliersMaster)
        {
            ApiResponse<SuppliersMasterViewModel> apiResponse = new ApiResponse<SuppliersMasterViewModel>();
            var result = _mapper.Map<SuppliersMaster>(suppliersMaster);
            result.SuppliersMasterSupplierLocationId = result.SuppliersMasterSupplierLocationId == null ? 1 : result.SuppliersMasterSupplierLocationId;
            var xs =await  supplierMasterService.InsertSupplier(result);
            var data = _mapper.Map<SuppliersMasterViewModel>(xs);
            // (await supplierMasterService.InsertSupplier(result)).Select(k => new SuppliersMasterViewModel
            //{
            //    SuppliersMasterSupplierId = k.SuppliersMasterSupplierId,
            //    //SuppliersMasterSupplierCode = k.SuppliersMasterSupplierCode,
            //    SuppliersMasterSupplierName = k.SuppliersMasterSupplierName,
            //    SuppliersMasterSupplierContactPerson = k.SuppliersMasterSupplierContactPerson,
            //    SuppliersMasterSupplierCountryId = k.SuppliersMasterSupplierCountryId,
            //    SuppliersMasterSupplierCityId = k.SuppliersMasterSupplierCityId,
            //    SuppliersMasterSupplierPoBox = k.SuppliersMasterSupplierPoBox,
            //    SuppliersMasterSupplierTel1 = k.SuppliersMasterSupplierTel1,
            //    SuppliersMasterSupplierTel2 = k.SuppliersMasterSupplierTel2,
            //    SuppliersMasterSupplierFax = k.SuppliersMasterSupplierFax,
            //    SuppliersMasterSupplierMobile = k.SuppliersMasterSupplierMobile,
            //    SuppliersMasterSupplierEmail = k.SuppliersMasterSupplierEmail,
            //    SuppliersMasterSupplierWebSite = k.SuppliersMasterSupplierWebSite,
            //    SuppliersMasterSupplierLocationId = k.SuppliersMasterSupplierLocationId,
            //    SuppliersMasterSupplierAddress = k.SuppliersMasterSupplierAddress,
            //    SuppliersMasterSupplierWhatsAppNo = k.SuppliersMasterSupplierWhatsAppNo,
            //    SuppliersMasterSupplierRemarks = k.SuppliersMasterSupplierRemarks,
            //    SuppliersMasterSupplierReffAcNo = k.SuppliersMasterSupplierReffAcNo,
            //    SuppliersMasterSupplierType = k.SuppliersMasterSupplierType,
            //    SuppliersMasterSupplierUserId = k.SuppliersMasterSupplierUserId,
            //    SuppliersMasterSupplierCurrencyId = k.SuppliersMasterSupplierCurrencyId,
            //    SuppliersMasterSupplierConsup = k.SuppliersMasterSupplierConsup,
            //    SuppliersMasterSupplierCrl = k.SuppliersMasterSupplierCrl,
            //    SuppliersMasterSupplierStatus = k.SuppliersMasterSupplierStatus,
            //    SuppliersMasterSupplierDeleteStatus = k.SuppliersMasterSupplierDeleteStatus,
            //    SuppliersMasterSupplierStatusValue = k.SuppliersMasterSupplierStatusValue,
            //    SuppliersMasterSupplierPaymentTerms = k.SuppliersMasterSupplierPaymentTerms,
            //    SuppliersMasterSupplierCreditDays = k.SuppliersMasterSupplierCreditDays,
            //    SuppliersMasterSupplierCreditLimit = k.SuppliersMasterSupplierCreditLimit,
            //    SuppliersMasterSupplierVatNo = k.SuppliersMasterSupplierVatNo,
            //    SuppliersMasterSupplierDelStatus = k.SuppliersMasterSupplierDelStatus
            //}).ToList();

            apiResponse = new ApiResponse<SuppliersMasterViewModel>
            {
                Valid = true,
                Result = data,
                Message = "data saved",
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateSupplier")]
        public ApiResponse<SuppliersMasterViewModel> UpdateSupplier([FromBody] SuppliersMasterViewModel SuppliersMaster)
        {
            ApiResponse<SuppliersMasterViewModel> apiResponse = new ApiResponse<SuppliersMasterViewModel>();
            var result = _mapper.Map<SuppliersMaster>(SuppliersMaster);
            var xs = supplierMasterService.UpdateSupplier(result);
            //    .Select(k => new SuppliersMasterViewModel
            //{
            //    SuppliersMasterSupplierId = k.SuppliersMasterSupplierId,
            //    //SuppliersMasterSupplierCode = k.SuppliersMasterSupplierCode,
            //    SuppliersMasterSupplierName = k.SuppliersMasterSupplierName,
            //    SuppliersMasterSupplierContactPerson = k.SuppliersMasterSupplierContactPerson,
            //    SuppliersMasterSupplierCountryId = k.SuppliersMasterSupplierCountryId,
            //    SuppliersMasterSupplierCityId = k.SuppliersMasterSupplierCityId,
            //    SuppliersMasterSupplierPoBox = k.SuppliersMasterSupplierPoBox,
            //    SuppliersMasterSupplierTel1 = k.SuppliersMasterSupplierTel1,
            //    SuppliersMasterSupplierTel2 = k.SuppliersMasterSupplierTel2,
            //    SuppliersMasterSupplierFax = k.SuppliersMasterSupplierFax,
            //    SuppliersMasterSupplierMobile = k.SuppliersMasterSupplierMobile,
            //    SuppliersMasterSupplierEmail = k.SuppliersMasterSupplierEmail,
            //    SuppliersMasterSupplierWebSite = k.SuppliersMasterSupplierWebSite,
            //    SuppliersMasterSupplierLocationId = k.SuppliersMasterSupplierLocationId,
            //    SuppliersMasterSupplierAddress = k.SuppliersMasterSupplierAddress,
            //    SuppliersMasterSupplierWhatsAppNo = k.SuppliersMasterSupplierWhatsAppNo,
            //    SuppliersMasterSupplierRemarks = k.SuppliersMasterSupplierRemarks,
            //    SuppliersMasterSupplierReffAcNo = k.SuppliersMasterSupplierReffAcNo,
            //    SuppliersMasterSupplierType = k.SuppliersMasterSupplierType,
            //    SuppliersMasterSupplierUserId = k.SuppliersMasterSupplierUserId,
            //    SuppliersMasterSupplierCurrencyId = k.SuppliersMasterSupplierCurrencyId,
            //    SuppliersMasterSupplierConsup = k.SuppliersMasterSupplierConsup,
            //    SuppliersMasterSupplierCrl = k.SuppliersMasterSupplierCrl,
            //    SuppliersMasterSupplierStatus = k.SuppliersMasterSupplierStatus,
            //    SuppliersMasterSupplierDeleteStatus = k.SuppliersMasterSupplierDeleteStatus,
            //    SuppliersMasterSupplierStatusValue = k.SuppliersMasterSupplierStatusValue,
            //    SuppliersMasterSupplierPaymentTerms = k.SuppliersMasterSupplierPaymentTerms,
            //    SuppliersMasterSupplierCreditDays = k.SuppliersMasterSupplierCreditDays,
            //    SuppliersMasterSupplierCreditLimit = k.SuppliersMasterSupplierCreditLimit,
            //    SuppliersMasterSupplierVatNo = k.SuppliersMasterSupplierVatNo,
            //    SuppliersMasterSupplierDelStatus = k.SuppliersMasterSupplierDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<SuppliersMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<SuppliersMasterViewModel>(xs),
                Message = "data update",
            };
            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteSupplier")]
        public ApiResponse<SuppliersMasterViewModel> DeleteSupplier([FromBody] SuppliersMasterViewModel SuppliersMaster)
        {
            ApiResponse<SuppliersMasterViewModel> apiResponse = new ApiResponse<SuppliersMasterViewModel>();
            var result = _mapper.Map<SuppliersMaster>(SuppliersMaster);
            var xs = supplierMasterService.DeleteSupplier(result);
            //    .Select(k => new SuppliersMasterViewModel
            //{
            //    SuppliersMasterSupplierId = k.SuppliersMasterSupplierId,
            //    //SuppliersMasterSupplierCode = k.SuppliersMasterSupplierCode,
            //    SuppliersMasterSupplierName = k.SuppliersMasterSupplierName,
            //    SuppliersMasterSupplierContactPerson = k.SuppliersMasterSupplierContactPerson,
            //    SuppliersMasterSupplierCountryId = k.SuppliersMasterSupplierCountryId,
            //    SuppliersMasterSupplierCityId = k.SuppliersMasterSupplierCityId,
            //    SuppliersMasterSupplierPoBox = k.SuppliersMasterSupplierPoBox,
            //    SuppliersMasterSupplierTel1 = k.SuppliersMasterSupplierTel1,
            //    SuppliersMasterSupplierTel2 = k.SuppliersMasterSupplierTel2,
            //    SuppliersMasterSupplierFax = k.SuppliersMasterSupplierFax,
            //    SuppliersMasterSupplierMobile = k.SuppliersMasterSupplierMobile,
            //    SuppliersMasterSupplierEmail = k.SuppliersMasterSupplierEmail,
            //    SuppliersMasterSupplierWebSite = k.SuppliersMasterSupplierWebSite,
            //    SuppliersMasterSupplierLocationId = k.SuppliersMasterSupplierLocationId,
            //    SuppliersMasterSupplierAddress = k.SuppliersMasterSupplierAddress,
            //    SuppliersMasterSupplierWhatsAppNo = k.SuppliersMasterSupplierWhatsAppNo,
            //    SuppliersMasterSupplierRemarks = k.SuppliersMasterSupplierRemarks,
            //    SuppliersMasterSupplierReffAcNo = k.SuppliersMasterSupplierReffAcNo,
            //    SuppliersMasterSupplierType = k.SuppliersMasterSupplierType,
            //    SuppliersMasterSupplierUserId = k.SuppliersMasterSupplierUserId,
            //    SuppliersMasterSupplierCurrencyId = k.SuppliersMasterSupplierCurrencyId,
            //    SuppliersMasterSupplierConsup = k.SuppliersMasterSupplierConsup,
            //    SuppliersMasterSupplierCrl = k.SuppliersMasterSupplierCrl,
            //    SuppliersMasterSupplierStatus = k.SuppliersMasterSupplierStatus,
            //    SuppliersMasterSupplierDeleteStatus = k.SuppliersMasterSupplierDeleteStatus,
            //    SuppliersMasterSupplierStatusValue = k.SuppliersMasterSupplierStatusValue,
            //    SuppliersMasterSupplierPaymentTerms = k.SuppliersMasterSupplierPaymentTerms,
            //    SuppliersMasterSupplierCreditDays = k.SuppliersMasterSupplierCreditDays,
            //    SuppliersMasterSupplierCreditLimit = k.SuppliersMasterSupplierCreditLimit,
            //    SuppliersMasterSupplierVatNo = k.SuppliersMasterSupplierVatNo,
            //    SuppliersMasterSupplierDelStatus = k.SuppliersMasterSupplierDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<SuppliersMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<SuppliersMasterViewModel>(xs),
                Message = "data delete",
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetSupplierDetailsByItemId/{id}")]
        public ApiResponse<List<ItemMasterSupplierDetais>> GetSupplierDetailsByItemId(int id)
        {
            List<ItemMasterSupplierDetais> supplierDetailsViewModels = new List<ItemMasterSupplierDetais>();
            var x = this.supplierMasterService.GetUpdatedSupplierDetailsByItem(id).ToList();
            ApiResponse<List<ItemMasterSupplierDetais>> apiResponse = new ApiResponse<List<ItemMasterSupplierDetais>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpPost("getSupplierFilteredList")]
        public async Task<IActionResult> GetSupplierFilteredList(string supplierName)
        {
            return Ok(await this.supplierMasterService.GetSupplierFilteredList(supplierName));
        }

        [HttpGet]
        [Route("Supplier/LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objresponse = new ResponseInfo();

            var currencyMasters = currencyrepository.GetAsQueryable().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                c.CurrencyMasterCurrencyName,
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var countryMasters = countryrepository.GetAsQueryable().Where(a => a.CountryMasterCountryDelStatus != true).Select(c => new
            {
                c.CountryMasterCountryId,
                c.CountryMasterCountryName
            }).ToList();
            var cityMasters = cityrepository.GetAsQueryable().Where(a => a.CityMasterCityDelStatus != true).Select(c => new
            {
                c.CityMasterCityId,
                c.CityMasterCityName,
                c.CityMasterCityCountryId
            }).ToList();
            var locationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName,
            }).ToList();
            objresponse.ResultSet = new
            {
                currencyMasters = currencyMasters,
                countryMasters = countryMasters,
                cityMasters = cityMasters,
                locationMaster = locationMaster
            };
            return objresponse;
        }
    }
}
