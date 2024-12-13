using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master.Implementations;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inspire.Erp.Web.Controllers.Accounts
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class NewCustomerController : ControllerBase
    {
        private INewCustomerService _newCustomerService;
        private readonly IMapper _mapper;
        public NewCustomerController(INewCustomerService newCustomerService, IMapper mapper)
        {
            _newCustomerService = newCustomerService;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("InsertNewCustomer")]
        public ApiResponse<CustomerMasterViewModel> InsertNewCustomer([FromBody] CustomerMasterViewModel voucherCompositeView)
        {
            ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>();

            ////if (_newCustomerService.GetVouchersNumbers(Convert.ToString(voucherCompositeView.CustomerMasterCustomerNo)) == null)
            if ( voucherCompositeView.CustomerMasterCustomerNo == null)
                {
                    var param1 = _mapper.Map<CustomerMaster>(voucherCompositeView);
                var param2 = _mapper.Map<List<CustomerContacts>>(voucherCompositeView.CustomerContacts);
                var param3 = _mapper.Map<List<CustomerDepartments>>(voucherCompositeView.CustomerDepartment);
                var xs = _newCustomerService.InsertNewCustomer(param1, param2, param3);
                CustomerMasterViewModel customerMasterViewModel = new CustomerMasterViewModel
                {

                    CustomerMasterCustomerNo = xs.customerMaster.CustomerMasterCustomerNo,
                    CustomerMasterCustomerTitle = xs.customerMaster.CustomerMasterCustomerTitle,
                    CustomerMasterCustomerName = xs.customerMaster.CustomerMasterCustomerName,
                    CustomerMasterCustomerType = xs.customerMaster.CustomerMasterCustomerType,
                    CustomerMasterCustomerContactPerson = xs.customerMaster.CustomerMasterCustomerContactPerson,
                    CustomerMasterCustomerCountryId = xs.customerMaster.CustomerMasterCustomerCountryId,
                    CustomerMasterCustomerCityId = xs.customerMaster.CustomerMasterCustomerCityId,
                    CustomerMasterCustomerPoBox = xs.customerMaster.CustomerMasterCustomerPoBox,
                    CustomerMasterCustomerTel1 = xs.customerMaster.CustomerMasterCustomerTel1,
                    CustomerMasterCustomerTel2 = xs.customerMaster.CustomerMasterCustomerTel2,
                    CustomerMasterCustomerMobile = xs.customerMaster.CustomerMasterCustomerMobile,
                    CustomerMasterCustomerFax = xs.customerMaster.CustomerMasterCustomerFax,
                    CustomerMasterCustomerEmail = xs.customerMaster.CustomerMasterCustomerEmail,
                    CustomerMasterCustomerWebSite = xs.customerMaster.CustomerMasterCustomerWebSite,
                    CustomerMasterCustomerLocation = xs.customerMaster.CustomerMasterCustomerLocation,
                    CustomerMasterCustomerAddress = xs.customerMaster.CustomerMasterCustomerAddress,
                    CustomerMasterCustomerWhatsAppNo = xs.customerMaster.CustomerMasterCustomerWhatsAppNo,
                    CustomerMasterCustomerRemarks = xs.customerMaster.CustomerMasterCustomerRemarks,
                    CustomerMasterCustomerReffAcNo = xs.customerMaster.CustomerMasterCustomerReffAcNo,
                    CustomerMasterCustomerUserId = xs.customerMaster.CustomerMasterCustomerUserId,
                    CustomerMasterCustomerCurrencyId = xs.customerMaster.CustomerMasterCustomerCurrencyId,
                    CustomerMasterCustomerCreditLimit = xs.customerMaster.CustomerMasterCustomerCreditLimit,
                    CustomerMasterCustomerCreditDays = xs.customerMaster.CustomerMasterCustomerCreditDays,
                    CustomerMasterCustomerBlackList = xs.customerMaster.CustomerMasterCustomerBlackList,
                    CustomerMasterCustomerStatus = xs.customerMaster.CustomerMasterCustomerStatus,
                    CustomerMasterCustomerDeleteStatus = xs.customerMaster.CustomerMasterCustomerDeleteStatus,
                    CustomerMasterCustomerJoinDate = xs.customerMaster.CustomerMasterCustomerJoinDate,
                    CustomerMasterCustomerStatusValue = xs.customerMaster.CustomerMasterCustomerStatusValue,
                    CustomerMasterCustomerCreateAccount = xs.customerMaster.CustomerMasterCustomerCreateAccount,
                    CustomerMasterCustomerPriceLevel = xs.customerMaster.CustomerMasterCustomerPriceLevel,
                    CustomerMasterCustomerPriceLevelId = xs.customerMaster.CustomerMasterCustomerPriceLevelId,
                    CustomerMasterCustomerCustType = xs.customerMaster.CustomerMasterCustomerCustType,
                    CustomerMasterCustomerContactPerson2 = xs.customerMaster.CustomerMasterCustomerContactPerson2,
                    CustomerMasterCustomerContactPerson3 = xs.customerMaster.CustomerMasterCustomerContactPerson3,
                    CustomerMasterCustomerVatNo = xs.customerMaster.CustomerMasterCustomerVatNo,
                    CustomerMasterCustomerLoyaltyId = xs.customerMaster.CustomerMasterCustomerLoyaltyId,
                    CustomerMasterCustomerEarnPoints = xs.customerMaster.CustomerMasterCustomerEarnPoints,
                    CustomerMasterCustomerTotalPoints = xs.customerMaster.CustomerMasterCustomerTotalPoints,
                    CustomerMasterCustomerTotalValue = xs.customerMaster.CustomerMasterCustomerTotalValue,
                    CustomerMasterCustomerRedeemEarnPoints = xs.customerMaster.CustomerMasterCustomerRedeemEarnPoints,
                    CustomerMasterCustomerArabicName = xs.customerMaster.CustomerMasterCustomerArabicName,
                    CustomerMasterCustomerGroupAccNo = xs.customerMaster.CustomerMasterCustomerGroupAccNo,
                    CustomerMasterCustomerCtTypeId = xs.customerMaster.CustomerMasterCustomerCtTypeId,
                    CustomerMasterCustomerDelStatus = xs.customerMaster.CustomerMasterCustomerDelStatus

                };

                customerMasterViewModel.CustomerContacts = _mapper.Map<List<CustomerContactViewModel>>(xs.customercontacts);
                customerMasterViewModel.CustomerDepartment = _mapper.Map<List<CustomerDepartmentsViewModel>>(xs.customerdepartments);
                apiResponse = new ApiResponse<CustomerMasterViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<CustomerMasterViewModel>(customerMasterViewModel),
                    Message = PaymentVoucherMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<CustomerMasterViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = PaymentVoucherMessage.VoucherAlreadyExist
                };

            }

            return apiResponse;

        }

        [HttpPost]
        [Route("UpdateNewCustomer")]
        public ApiResponse<CustomerMasterViewModel> UpdateOBVoucher([FromBody] CustomerMasterViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<CustomerMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerContacts>>(voucherCompositeView.CustomerContacts);
            var param3 = _mapper.Map<List<CustomerDepartments>>(voucherCompositeView.CustomerDepartment);
            var xs = _newCustomerService.UpdateNewCustomer(param1, param2, param3);

            CustomerMasterViewModel customerMasterViewModel = new CustomerMasterViewModel
            {
                CustomerMasterCustomerNo = xs.customerMaster.CustomerMasterCustomerNo,
                CustomerMasterCustomerTitle = xs.customerMaster.CustomerMasterCustomerTitle,
                CustomerMasterCustomerName = xs.customerMaster.CustomerMasterCustomerName,
                CustomerMasterCustomerType = xs.customerMaster.CustomerMasterCustomerType,
                CustomerMasterCustomerContactPerson = xs.customerMaster.CustomerMasterCustomerContactPerson,
                CustomerMasterCustomerCountryId = xs.customerMaster.CustomerMasterCustomerCountryId,
                CustomerMasterCustomerCityId = xs.customerMaster.CustomerMasterCustomerCityId,
                CustomerMasterCustomerPoBox = xs.customerMaster.CustomerMasterCustomerPoBox,
                CustomerMasterCustomerTel1 = xs.customerMaster.CustomerMasterCustomerTel1,
                CustomerMasterCustomerTel2 = xs.customerMaster.CustomerMasterCustomerTel2,
                CustomerMasterCustomerMobile = xs.customerMaster.CustomerMasterCustomerMobile,
                CustomerMasterCustomerFax = xs.customerMaster.CustomerMasterCustomerFax,
                CustomerMasterCustomerEmail = xs.customerMaster.CustomerMasterCustomerEmail,
                CustomerMasterCustomerWebSite = xs.customerMaster.CustomerMasterCustomerWebSite,
                CustomerMasterCustomerLocation = xs.customerMaster.CustomerMasterCustomerLocation,
                CustomerMasterCustomerRemarks = xs.customerMaster.CustomerMasterCustomerRemarks,
                CustomerMasterCustomerReffAcNo = xs.customerMaster.CustomerMasterCustomerReffAcNo,
                CustomerMasterCustomerAddress = xs.customerMaster.CustomerMasterCustomerAddress,
                CustomerMasterCustomerWhatsAppNo = xs.customerMaster.CustomerMasterCustomerWhatsAppNo,
                CustomerMasterCustomerUserId = xs.customerMaster.CustomerMasterCustomerUserId,
                CustomerMasterCustomerCurrencyId = xs.customerMaster.CustomerMasterCustomerCurrencyId,
                CustomerMasterCustomerCreditLimit = xs.customerMaster.CustomerMasterCustomerCreditLimit,
                CustomerMasterCustomerCreditDays = xs.customerMaster.CustomerMasterCustomerCreditDays,
                CustomerMasterCustomerBlackList = xs.customerMaster.CustomerMasterCustomerBlackList,
                CustomerMasterCustomerStatus = xs.customerMaster.CustomerMasterCustomerStatus,
                CustomerMasterCustomerDeleteStatus = xs.customerMaster.CustomerMasterCustomerDeleteStatus,
                CustomerMasterCustomerJoinDate = xs.customerMaster.CustomerMasterCustomerJoinDate,
                CustomerMasterCustomerStatusValue = xs.customerMaster.CustomerMasterCustomerStatusValue,
                CustomerMasterCustomerCreateAccount = xs.customerMaster.CustomerMasterCustomerCreateAccount,
                CustomerMasterCustomerPriceLevel = xs.customerMaster.CustomerMasterCustomerPriceLevel,
                CustomerMasterCustomerPriceLevelId = xs.customerMaster.CustomerMasterCustomerPriceLevelId,
                CustomerMasterCustomerCustType = xs.customerMaster.CustomerMasterCustomerCustType,
                CustomerMasterCustomerContactPerson2 = xs.customerMaster.CustomerMasterCustomerContactPerson2,
                CustomerMasterCustomerContactPerson3 = xs.customerMaster.CustomerMasterCustomerContactPerson3,
                CustomerMasterCustomerVatNo = xs.customerMaster.CustomerMasterCustomerVatNo,
                CustomerMasterCustomerLoyaltyId = xs.customerMaster.CustomerMasterCustomerLoyaltyId,
                CustomerMasterCustomerEarnPoints = xs.customerMaster.CustomerMasterCustomerEarnPoints,
                CustomerMasterCustomerTotalPoints = xs.customerMaster.CustomerMasterCustomerTotalPoints,
                CustomerMasterCustomerTotalValue = xs.customerMaster.CustomerMasterCustomerTotalValue,
                CustomerMasterCustomerRedeemEarnPoints = xs.customerMaster.CustomerMasterCustomerRedeemEarnPoints,
                CustomerMasterCustomerArabicName = xs.customerMaster.CustomerMasterCustomerArabicName,
                CustomerMasterCustomerGroupAccNo = xs.customerMaster.CustomerMasterCustomerGroupAccNo,
                CustomerMasterCustomerCtTypeId = xs.customerMaster.CustomerMasterCustomerCtTypeId,
                CustomerMasterCustomerDelStatus = xs.customerMaster.CustomerMasterCustomerDelStatus

            };

            customerMasterViewModel.CustomerContacts = _mapper.Map<List<CustomerContactViewModel>>(xs.customercontacts);
            customerMasterViewModel.CustomerDepartment = _mapper.Map<List<CustomerDepartmentsViewModel>>(xs.customerdepartments);
            ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<CustomerMasterViewModel>(customerMasterViewModel),
                Message = PaymentVoucherMessage.UpdateVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteNewCustomers")]
        public ApiResponse<int> DeleteNewCustomers([FromBody] CustomerMasterViewModel voucherCompositeView)
        {

            var param1 = _mapper.Map<CustomerMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<CustomerContacts>>(voucherCompositeView.CustomerContacts);
            var param3 = _mapper.Map<List<CustomerDepartments>>(voucherCompositeView.CustomerDepartment);
            var xs = _newCustomerService.DeleteNewCustomers(param1, param2, param3);

            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PaymentVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }


        [HttpGet]
        [Route("GetAllCustomers")]
        public ApiResponse<List<CustomerMaster>> GetAllCustomers()
        {
            ApiResponse<List<CustomerMaster>> apiResponse = new ApiResponse<List<CustomerMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<CustomerMaster>>(_newCustomerService.GetAllCustomers()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedCustomers/{id}")]
        public ApiResponse<CustomerMasterViewModel> GetSavedCustomers(int id)
        {
            CustomerViewModel customerViewModel = _newCustomerService.GetSavedCustomers(id);
            if (customerViewModel != null)
            {
                CustomerMasterViewModel customerMasterViewModel = new CustomerMasterViewModel
                {

                    CustomerMasterCustomerNo = customerViewModel.customerMaster.CustomerMasterCustomerNo,
                    CustomerMasterCustomerTitle = customerViewModel.customerMaster.CustomerMasterCustomerTitle,
                    CustomerMasterCustomerName = customerViewModel.customerMaster.CustomerMasterCustomerName,
                    CustomerMasterCustomerType = customerViewModel.customerMaster.CustomerMasterCustomerType,
                    CustomerMasterCustomerContactPerson = customerViewModel.customerMaster.CustomerMasterCustomerContactPerson,
                    CustomerMasterCustomerCountryId = customerViewModel.customerMaster.CustomerMasterCustomerCountryId,
                    CustomerMasterCustomerCityId = customerViewModel.customerMaster.CustomerMasterCustomerCityId,
                    CustomerMasterCustomerPoBox = customerViewModel.customerMaster.CustomerMasterCustomerPoBox,
                    CustomerMasterCustomerTel1 = customerViewModel.customerMaster.CustomerMasterCustomerTel1,
                    CustomerMasterCustomerTel2 = customerViewModel.customerMaster.CustomerMasterCustomerTel2,
                    CustomerMasterCustomerMobile = customerViewModel.customerMaster.CustomerMasterCustomerMobile,
                    CustomerMasterCustomerFax = customerViewModel.customerMaster.CustomerMasterCustomerFax,
                    CustomerMasterCustomerAddress = customerViewModel.customerMaster.CustomerMasterCustomerAddress,
                    CustomerMasterCustomerWhatsAppNo = customerViewModel.customerMaster.CustomerMasterCustomerWhatsAppNo,
                    CustomerMasterCustomerEmail = customerViewModel.customerMaster.CustomerMasterCustomerEmail,
                    CustomerMasterCustomerWebSite = customerViewModel.customerMaster.CustomerMasterCustomerWebSite,
                    CustomerMasterCustomerLocation = customerViewModel.customerMaster.CustomerMasterCustomerLocation,
                    CustomerMasterCustomerRemarks = customerViewModel.customerMaster.CustomerMasterCustomerRemarks,
                    CustomerMasterCustomerReffAcNo = customerViewModel.customerMaster.CustomerMasterCustomerReffAcNo,
                    CustomerMasterCustomerUserId = customerViewModel.customerMaster.CustomerMasterCustomerUserId,
                    CustomerMasterCustomerCurrencyId = customerViewModel.customerMaster.CustomerMasterCustomerCurrencyId,
                    CustomerMasterCustomerCreditLimit = customerViewModel.customerMaster.CustomerMasterCustomerCreditLimit,
                    CustomerMasterCustomerCreditDays = customerViewModel.customerMaster.CustomerMasterCustomerCreditDays,
                    CustomerMasterCustomerBlackList = customerViewModel.customerMaster.CustomerMasterCustomerBlackList,
                    CustomerMasterCustomerStatus = customerViewModel.customerMaster.CustomerMasterCustomerStatus,
                    CustomerMasterCustomerDeleteStatus = customerViewModel.customerMaster.CustomerMasterCustomerDeleteStatus,
                    CustomerMasterCustomerJoinDate = customerViewModel.customerMaster.CustomerMasterCustomerJoinDate,
                    CustomerMasterCustomerStatusValue = customerViewModel.customerMaster.CustomerMasterCustomerStatusValue,
                    CustomerMasterCustomerCreateAccount = customerViewModel.customerMaster.CustomerMasterCustomerCreateAccount,
                    CustomerMasterCustomerPriceLevel = customerViewModel.customerMaster.CustomerMasterCustomerPriceLevel,
                    CustomerMasterCustomerPriceLevelId = customerViewModel.customerMaster.CustomerMasterCustomerPriceLevelId,
                    CustomerMasterCustomerCustType = customerViewModel.customerMaster.CustomerMasterCustomerCustType,
                    CustomerMasterCustomerContactPerson2 = customerViewModel.customerMaster.CustomerMasterCustomerContactPerson2,
                    CustomerMasterCustomerContactPerson3 = customerViewModel.customerMaster.CustomerMasterCustomerContactPerson3,
                    CustomerMasterCustomerVatNo = customerViewModel.customerMaster.CustomerMasterCustomerVatNo,
                    CustomerMasterCustomerLoyaltyId = customerViewModel.customerMaster.CustomerMasterCustomerLoyaltyId,
                    CustomerMasterCustomerEarnPoints = customerViewModel.customerMaster.CustomerMasterCustomerEarnPoints,
                    CustomerMasterCustomerTotalPoints = customerViewModel.customerMaster.CustomerMasterCustomerTotalPoints,
                    CustomerMasterCustomerTotalValue = customerViewModel.customerMaster.CustomerMasterCustomerTotalValue,
                    CustomerMasterCustomerRedeemEarnPoints = customerViewModel.customerMaster.CustomerMasterCustomerRedeemEarnPoints,
                    CustomerMasterCustomerArabicName = customerViewModel.customerMaster.CustomerMasterCustomerArabicName,
                    CustomerMasterCustomerGroupAccNo = customerViewModel.customerMaster.CustomerMasterCustomerGroupAccNo,
                    CustomerMasterCustomerCtTypeId = customerViewModel.customerMaster.CustomerMasterCustomerCtTypeId,
                    CustomerMasterCustomerDelStatus = customerViewModel.customerMaster.CustomerMasterCustomerDelStatus

                };
                customerMasterViewModel.CustomerContacts = _mapper.Map<List<CustomerContactViewModel>>(customerViewModel.customercontacts);
                customerMasterViewModel.CustomerDepartment = _mapper.Map<List<CustomerDepartmentsViewModel>>(customerViewModel.customerdepartments);
                ApiResponse<CustomerMasterViewModel> apiResponse = new ApiResponse<CustomerMasterViewModel>
                {
                    Valid = true,
                    Result = customerMasterViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;

        }
    }
}