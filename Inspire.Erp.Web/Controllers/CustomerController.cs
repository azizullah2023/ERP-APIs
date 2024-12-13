using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Crypt.Dsig;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICustomerMasterService customerMasterService;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CountryMaster> countryrepository;
        private IRepository<CityMaster> cityrepository; private IRepository<LocationMaster> locationrepository; private IChartofAccountsService chartofAccountsService;
        private IRepository<PriceLevelMaster> priceLevelrepository; private IRepository<CustomerType> CustomerTyperepository;
        public CustomerController(ICustomerMasterService _customerMasterService, IMapper mapper,
             IRepository<CurrencyMaster> _currencyrepository, IRepository<CountryMaster> _countryrepository, IRepository<PriceLevelMaster> _priceLevelrepository
            , IRepository<LocationMaster> _locationrepository, IRepository<CityMaster> _Cityrepository, IChartofAccountsService _chartofAccountsService, IRepository<CustomerType> _CustomerTyperepository)
        {
            customerMasterService = _customerMasterService;
            _mapper = mapper;
            locationrepository = _locationrepository; chartofAccountsService = _chartofAccountsService; priceLevelrepository = _priceLevelrepository;
            currencyrepository = _currencyrepository; countryrepository = _countryrepository; cityrepository = _Cityrepository; CustomerTyperepository = _CustomerTyperepository;
        }
        [HttpGet]
        [Route("GetAllCustomer")]
        public List<CustomerMasterViewModel> GetAllCustomer()
        {
            return customerMasterService.GetAllCustomer().Select(k => new CustomerMasterViewModel
            {
                CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
                CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
                CustomerMasterCustomerName = k.CustomerMasterCustomerName,
                CustomerMasterCustomerType = k.CustomerMasterCustomerType,
                CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
                CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
                CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
                CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
                CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
                CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
                CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
                CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
                CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
                CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
                CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
                CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
                CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
                CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
                CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
                CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
                CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
                CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
                CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
                CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
                CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
                CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
                CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
                CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
                CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
                CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
                CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
                CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
                CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
                CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
                CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
                CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
                CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
                CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
                CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
                CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
                CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
                CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
                CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
                CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("GetAllCustomerForGrid")]
        public List<CustomerMasterViewModel> GetAllCustomerForGrid(GenericGridViewModel model)
        {
            return customerMasterService.GetAllCustomer().Select(k => new CustomerMasterViewModel
            {
                CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
                CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
                CustomerMasterCustomerName = k.CustomerMasterCustomerName,
                CustomerMasterCustomerType = k.CustomerMasterCustomerType,
                CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
                CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
                CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
                CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
                CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
                CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
                CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
                CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
                CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
                CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
                CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
                CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
                CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
                CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
                CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
                CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
                CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
                CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
                CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
                CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
                CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
                CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
                CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
                CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
                CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
                CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
                CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
                CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
                CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
                CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
                CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
                CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
                CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
                CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
                CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
                CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
                CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
                CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
                CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
                CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
            }).ToList();
        }
        //[HttpGet("{id}")]
        [HttpGet("GetAllCustomerById/{id}")]
        public ApiResponse<CustomerMasterViewModel> GetAllCustomerById(int id)
        {
            ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>();
            var data = customerMasterService.GetAllCustomerById(id);
            //    .Select(k => new CustomerMasterViewModel
            //{

            //    CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
            //    CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
            //    CustomerMasterCustomerName = k.CustomerMasterCustomerName,
            //    CustomerMasterCustomerType = k.CustomerMasterCustomerType,
            //    CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
            //    CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
            //    CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
            //    CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
            //    CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
            //    CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
            //    CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
            //    CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
            //    CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
            //    CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
            //    CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
            //    CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
            //    CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
            //    CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
            //    CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
            //    CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
            //    CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
            //    CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
            //    CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
            //    CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
            //    CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
            //    CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
            //    CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
            //    CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
            //    CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
            //    CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
            //    CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
            //    CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
            //    CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
            //    CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
            //    CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
            //    CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
            //    CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
            //    CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
            //    CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
            //    CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
            //    CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
            //    CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
            //    CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
            //    CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<CustomerMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerMasterViewModel>(data),
                Message = "data found",
            };
            return apiResponse;
        }



        [HttpPost("InsertCustomer")]
        public async Task<ApiResponse<CustomerMasterViewModel>> InsertCustomer([FromBody] CustomerMasterViewModel customerMaster)
        {
            try
            {
                ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>();
                var result = _mapper.Map<CustomerMaster>(customerMaster);
                var data = await customerMasterService.InsertCustomer(result);
                //    .Select(k => new CustomerMasterViewModel
                //{
                //    CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
                //    CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
                //    CustomerMasterCustomerName = k.CustomerMasterCustomerName,
                //    CustomerMasterCustomerType = k.CustomerMasterCustomerType,
                //    CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
                //    CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
                //    CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
                //    CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
                //    CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
                //    CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
                //    CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
                //    CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
                //    CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
                //    CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
                //    CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
                //    CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
                //    CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
                //    CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
                //    CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
                //    CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
                //    CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
                //    CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
                //    CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
                //    CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
                //    CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
                //    CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
                //    CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
                //    CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
                //    CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
                //    CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
                //    CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
                //    CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
                //    CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
                //    CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
                //    CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
                //    CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
                //    CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
                //    CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
                //    CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
                //    CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
                //    CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
                //    CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
                //    CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
                //    CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
                //}).ToList();
                apiResponse = new ApiResponse<CustomerMasterViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<CustomerMasterViewModel>(data),
                    Message = "data saved",
                };
                return apiResponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public  ApiResponse<CustomerMasterViewModel> UpdateCustomer([FromBody] CustomerMasterViewModel customerMaster)
        {
            ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>();
            var result = _mapper.Map<CustomerMaster>(customerMaster);
            var data =  customerMasterService.UpdateCustomer(result);
            //    .Select(k => new CustomerMasterViewModel
            //{
            //    CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
            //    CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
            //    CustomerMasterCustomerName = k.CustomerMasterCustomerName,
            //    CustomerMasterCustomerType = k.CustomerMasterCustomerType,
            //    CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
            //    CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
            //    CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
            //    CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
            //    CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
            //    CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
            //    CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
            //    CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
            //    CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
            //    CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
            //    CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
            //    CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
            //    CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
            //    CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
            //    CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
            //    CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
            //    CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
            //    CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
            //    CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
            //    CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
            //    CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
            //    CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
            //    CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
            //    CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
            //    CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
            //    CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
            //    CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
            //    CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
            //    CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
            //    CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
            //    CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
            //    CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
            //    CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
            //    CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
            //    CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
            //    CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
            //    CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
            //    CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
            //    CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
            //    CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<CustomerMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerMasterViewModel>(data),
                Message = "data update",
            };
            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteCustomer")]
        public ApiResponse<CustomerMasterViewModel> DeleteCustomer([FromBody] CustomerMasterViewModel customerMaster)
        {
            ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>();
            var result = _mapper.Map<CustomerMaster>(customerMaster);
            var data= customerMasterService.DeleteCustomer(result);
            //    .Select(k => new CustomerMasterViewModel
            //{
            //    CustomerMasterCustomerNo = k.CustomerMasterCustomerNo,
            //    CustomerMasterCustomerTitle = k.CustomerMasterCustomerTitle,
            //    CustomerMasterCustomerName = k.CustomerMasterCustomerName,
            //    CustomerMasterCustomerType = k.CustomerMasterCustomerType,
            //    CustomerMasterCustomerContactPerson = k.CustomerMasterCustomerContactPerson,
            //    CustomerMasterCustomerCountryId = k.CustomerMasterCustomerCountryId,
            //    CustomerMasterCustomerCityId = k.CustomerMasterCustomerCityId,
            //    CustomerMasterCustomerPoBox = k.CustomerMasterCustomerPoBox,
            //    CustomerMasterCustomerTel1 = k.CustomerMasterCustomerTel1,
            //    CustomerMasterCustomerTel2 = k.CustomerMasterCustomerTel2,
            //    CustomerMasterCustomerMobile = k.CustomerMasterCustomerMobile,
            //    CustomerMasterCustomerFax = k.CustomerMasterCustomerFax,
            //    CustomerMasterCustomerEmail = k.CustomerMasterCustomerEmail,
            //    CustomerMasterCustomerWebSite = k.CustomerMasterCustomerWebSite,
            //    CustomerMasterCustomerLocation = k.CustomerMasterCustomerLocation,
            //    CustomerMasterCustomerAddress = k.CustomerMasterCustomerAddress,
            //    CustomerMasterCustomerWhatsAppNo = k.CustomerMasterCustomerWhatsAppNo,
            //    CustomerMasterCustomerRemarks = k.CustomerMasterCustomerRemarks,
            //    CustomerMasterCustomerReffAcNo = k.CustomerMasterCustomerReffAcNo,
            //    CustomerMasterCustomerUserId = k.CustomerMasterCustomerUserId,
            //    CustomerMasterCustomerCurrencyId = k.CustomerMasterCustomerCurrencyId,
            //    CustomerMasterCustomerCreditLimit = k.CustomerMasterCustomerCreditLimit,
            //    CustomerMasterCustomerCreditDays = k.CustomerMasterCustomerCreditDays,
            //    CustomerMasterCustomerBlackList = k.CustomerMasterCustomerBlackList,
            //    CustomerMasterCustomerStatus = k.CustomerMasterCustomerStatus,
            //    CustomerMasterCustomerDeleteStatus = k.CustomerMasterCustomerDeleteStatus,
            //    CustomerMasterCustomerJoinDate = k.CustomerMasterCustomerJoinDate,
            //    CustomerMasterCustomerStatusValue = k.CustomerMasterCustomerStatusValue,
            //    CustomerMasterCustomerCreateAccount = k.CustomerMasterCustomerCreateAccount,
            //    CustomerMasterCustomerPriceLevel = k.CustomerMasterCustomerPriceLevel,
            //    CustomerMasterCustomerPriceLevelId = k.CustomerMasterCustomerPriceLevelId,
            //    CustomerMasterCustomerCustType = k.CustomerMasterCustomerCustType,
            //    CustomerMasterCustomerContactPerson2 = k.CustomerMasterCustomerContactPerson2,
            //    CustomerMasterCustomerContactPerson3 = k.CustomerMasterCustomerContactPerson3,
            //    CustomerMasterCustomerVatNo = k.CustomerMasterCustomerVatNo,
            //    CustomerMasterCustomerLoyaltyId = k.CustomerMasterCustomerLoyaltyId,
            //    CustomerMasterCustomerEarnPoints = k.CustomerMasterCustomerEarnPoints,
            //    CustomerMasterCustomerTotalPoints = k.CustomerMasterCustomerTotalPoints,
            //    CustomerMasterCustomerTotalValue = k.CustomerMasterCustomerTotalValue,
            //    CustomerMasterCustomerRedeemEarnPoints = k.CustomerMasterCustomerRedeemEarnPoints,
            //    CustomerMasterCustomerArabicName = k.CustomerMasterCustomerArabicName,
            //    CustomerMasterCustomerGroupAccNo = k.CustomerMasterCustomerGroupAccNo,
            //    CustomerMasterCustomerCtTypeId = k.CustomerMasterCustomerCtTypeId,
            //    CustomerMasterCustomerDelStatus = k.CustomerMasterCustomerDelStatus
            //}).ToList();
            apiResponse = new ApiResponse<CustomerMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerMasterViewModel>(data),
                Message = "data deleted Successfully",
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("LoadDropdown")]
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

            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true).Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();
            var priceLevelMasters = priceLevelrepository.GetAsQueryable().Where(a => a.PriveLevelMasterPriceLevelDelStatus != true).Select(c => new
            {
                c.PriceLevelMasterPriceLevelId,
                c.PriceLevelMasterPriceLevelName
            }).ToList();
            var customerType = CustomerTyperepository.GetAsQueryable().Where(a => a.CustomerTypeDelStatus != true).Select(c => new
            {
                c.CustomerTypeId,
                c.CustomerTypeName
            }).ToList();
            objresponse.ResultSet = new
            {
                currencyMasters = currencyMasters,
                countryMasters = countryMasters,
                cityMasters = cityMasters,
                locationMaster = locationMaster,
                masterAccountsTables = masterAccountsTables,
                priceLevelMasters = priceLevelMasters,
                customerType = customerType
            };
            return objresponse;
        }
    }
}
