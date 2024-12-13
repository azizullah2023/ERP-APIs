using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerEnquiryController : ControllerBase
    {
        private ICustomerEnquiryService _customerEnquiryService;
        private readonly IMapper _mapper; private IRepository<EnquiryStatus> enquiryStatusrepository; private IRepository<BusinessTypeMaster> businesstyperepository;
        private IRepository<ItemMaster> itemrepository; private IRepository<UnitMaster> unitrepository; private IRepository<LocationMaster> locationrepository; private IRepository<EnquiryAbout> enquiryAboutrepository;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CustomerMaster> _customerMasterRepository; private IRepository<SalesManMaster> salesmanrepository;
        public CustomerEnquiryController(ICustomerEnquiryService customerEnquiryService, IMapper mapper,
             IRepository<ItemMaster> _itemrepository, IRepository<BusinessTypeMaster> _countryrepository,
            IRepository<UnitMaster> _unitrepository, IRepository<LocationMaster> _locationrepository, IRepository<SalesManMaster> _salesmanrepository,
            IRepository<CustomerMaster> customerMasterRepository, IRepository<EnquiryAbout> _EnquiryAboutrepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IRepository<EnquiryStatus> _enquiryStatusrepository)
        {
            _customerEnquiryService = customerEnquiryService;
            _mapper = mapper;
            salesmanrepository = _salesmanrepository; enquiryAboutrepository = _EnquiryAboutrepository;
            _customerMasterRepository = customerMasterRepository; enquiryStatusrepository = _enquiryStatusrepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; businesstyperepository = _countryrepository;
            locationrepository = _locationrepository; currencyrepository = _currencyrepository;
        }


        [HttpPost]
        [Route("InsertCustomerEnquiry")]
        public ApiResponse<EnquiryMasterViewModel> InsertCustomerEnquiry([FromBody] EnquiryMasterViewModel customerEnquiryCompositeView)
        {
            ApiResponse<EnquiryMasterViewModel> apiResponse = new ApiResponse<EnquiryMasterViewModel>();
            if (_customerEnquiryService.GetVouchersNumbers(customerEnquiryCompositeView.EnquiryMasterEnquiryId) == null)
            {
                var param1 = _mapper.Map<EnquiryMaster>(customerEnquiryCompositeView);
                var param2 = _mapper.Map<List<EnquiryDetails>>(customerEnquiryCompositeView.CustomerEnquiryDetails);
                var xs = _customerEnquiryService.InsertCustomerEnquiry(param1, param2);
                EnquiryMasterViewModel customerEnquiryViewModel = new EnquiryMasterViewModel
                {

                    EnquiryMasterEnquiryId = xs.custEnquiry.EnquiryMasterEnquiryId,
                    EnquiryMasterEnquiryDate = xs.custEnquiry.EnquiryMasterEnquiryDate,
                    EnquiryMasterEnquiryAboutId = xs.custEnquiry.EnquiryMasterEnquiryAboutId,
                    EnquiryMasterRemarks = xs.custEnquiry.EnquiryMasterRemarks,
                    EnquiryMasterSalesManId = xs.custEnquiry.EnquiryMasterSalesManId,
                    EnquiryMasterCurrencyId = xs.custEnquiry.EnquiryMasterCurrencyId,
                    EnquiryMasterLocationId = xs.custEnquiry.EnquiryMasterLocationId,
                    EnquiryMasterCustomerReference = xs.custEnquiry.EnquiryMasterCustomerReference,
                    EnquiryMasterEnquiryStatusId = xs.custEnquiry.EnquiryMasterEnquiryStatusId,
                    EnquiryMasterConsultingEngineer = xs.custEnquiry.EnquiryMasterConsultingEngineer,
                    EnquiryMasterJobNo = xs.custEnquiry.EnquiryMasterJobNo,
                    EnquiryMasterBusineesTypeId = xs.custEnquiry.EnquiryMasterBusineesTypeId,
                    EnquiryMasterEnquiryVoucherNo = xs.custEnquiry.EnquiryMasterEnquiryVoucherNo,
                    EnquiryMasterVesselId = xs.custEnquiry.EnquiryMasterVesselId,
                    EnquiryMasterBuilder = xs.custEnquiry.EnquiryMasterBuilder,
                    EnquiryMasterSerialNo = xs.custEnquiry.EnquiryMasterSerialNo,
                    EnquiryMasterVendorId = xs.custEnquiry.EnquiryMasterVendorId,
                    EnquiryMasterTypeId = xs.custEnquiry.EnquiryMasterTypeId,
                    EnquiryMasterModelId = xs.custEnquiry.EnquiryMasterModelId,
                    EnquiryMasterContactId = xs.custEnquiry.EnquiryMasterContactId,
                    EnquiryMasterContactName = xs.custEnquiry.EnquiryMasterContactName,
                    EnquiryMasterContactEmail = xs.custEnquiry.EnquiryMasterContactEmail,
                    EnquiryMasterDelStatus = xs.custEnquiry.EnquiryMasterDelStatus

                };

                customerEnquiryViewModel.CustomerEnquiryDetails = _mapper.Map<List<EnquiryDetailsViewModel>>(xs.customerEnquiryDetails);
                apiResponse = new ApiResponse<EnquiryMasterViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<EnquiryMasterViewModel>(customerEnquiryViewModel),
                    Message = CustomerEnquiryMessage.SaveCustomerEnquiry
                };
            }
            else
            {
                apiResponse = new ApiResponse<EnquiryMasterViewModel>
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
        [Route("UpdateCustomerEnquiry")]
        public ApiResponse<EnquiryMasterViewModel> UpdateCustomerEnquiry([FromBody] EnquiryMasterViewModel customerEnquiryCompositeView)
        {
            var param1 = _mapper.Map<EnquiryMaster>(customerEnquiryCompositeView);
            var param2 = _mapper.Map<List<EnquiryDetails>>(customerEnquiryCompositeView.CustomerEnquiryDetails);
            var xs = _customerEnquiryService.UpdateCustomerEnquiry(param1, param2);

            EnquiryMasterViewModel customerEnquiryViewModel = new EnquiryMasterViewModel
            {

                EnquiryMasterEnquiryId = xs.custEnquiry.EnquiryMasterEnquiryId,
                EnquiryMasterEnquiryDate = xs.custEnquiry.EnquiryMasterEnquiryDate,
                EnquiryMasterEnquiryAboutId = xs.custEnquiry.EnquiryMasterEnquiryAboutId,
                EnquiryMasterRemarks = xs.custEnquiry.EnquiryMasterRemarks,
                EnquiryMasterSalesManId = xs.custEnquiry.EnquiryMasterSalesManId,
                EnquiryMasterCurrencyId = xs.custEnquiry.EnquiryMasterCurrencyId,
                EnquiryMasterLocationId = xs.custEnquiry.EnquiryMasterLocationId,
                EnquiryMasterCustomerReference = xs.custEnquiry.EnquiryMasterCustomerReference,
                EnquiryMasterEnquiryStatusId = xs.custEnquiry.EnquiryMasterEnquiryStatusId,
                EnquiryMasterConsultingEngineer = xs.custEnquiry.EnquiryMasterConsultingEngineer,
                EnquiryMasterJobNo = xs.custEnquiry.EnquiryMasterJobNo,
                EnquiryMasterBusineesTypeId = xs.custEnquiry.EnquiryMasterBusineesTypeId,
                EnquiryMasterEnquiryVoucherNo = xs.custEnquiry.EnquiryMasterEnquiryVoucherNo,
                EnquiryMasterVesselId = xs.custEnquiry.EnquiryMasterVesselId,
                EnquiryMasterBuilder = xs.custEnquiry.EnquiryMasterBuilder,
                EnquiryMasterSerialNo = xs.custEnquiry.EnquiryMasterSerialNo,
                EnquiryMasterVendorId = xs.custEnquiry.EnquiryMasterVendorId,
                EnquiryMasterTypeId = xs.custEnquiry.EnquiryMasterTypeId,
                EnquiryMasterModelId = xs.custEnquiry.EnquiryMasterModelId,
                EnquiryMasterContactId = xs.custEnquiry.EnquiryMasterContactId,
                EnquiryMasterContactName = xs.custEnquiry.EnquiryMasterContactName,
                EnquiryMasterContactEmail = xs.custEnquiry.EnquiryMasterContactEmail,
                EnquiryMasterDelStatus = xs.custEnquiry.EnquiryMasterDelStatus
            };

            customerEnquiryViewModel.CustomerEnquiryDetails = _mapper.Map<List<EnquiryDetailsViewModel>>(xs.customerEnquiryDetails);
            ApiResponse<EnquiryMasterViewModel> apiResponse = new ApiResponse<EnquiryMasterViewModel>
            {
                Valid = true,
                Result = _mapper.Map<EnquiryMasterViewModel>(customerEnquiryViewModel),
                Message = CustomerEnquiryMessage.UpdateCustomerEnquiry
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteCustomerEnquiry")]
        public ApiResponse<int> DeleteCustomerEnquiry([FromBody] EnquiryMasterViewModel customerEnquiryCompositeView)
        {
            var param1 = _mapper.Map<EnquiryMaster>(customerEnquiryCompositeView);
            var param2 = _mapper.Map<List<EnquiryDetails>>(customerEnquiryCompositeView.CustomerEnquiryDetails);
            var xs = _customerEnquiryService.DeleteCustomerEnquiry(param1, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = CustomerEnquiryMessage.DeleteCustomerEnquiry
            };

            return apiResponse;

        }


        [HttpGet]
        [Route("GetCustomerEnquiry")]
        public ApiResponse<List<EnquiryMaster>> GetAllCustomerEnquirys()
        {
            ApiResponse<List<EnquiryMaster>> apiResponse = new ApiResponse<List<EnquiryMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<EnquiryMaster>>(_customerEnquiryService.GetCustomerEnquiry()),
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetSavedCustEnquiryDetails/{id}")]
        public ApiResponse<EnquiryMasterViewModel> GetSavedCustEnquiryDetails(string id)
        {
            CustomerEnquiryModel custEnquiry = _customerEnquiryService.GetSavedCustEnquiryDetails(id);
            if (custEnquiry != null)
            {
                EnquiryMasterViewModel customerEnquiryViewModel = new EnquiryMasterViewModel
                {

                    EnquiryMasterEnquiryId = custEnquiry.custEnquiry.EnquiryMasterEnquiryId,
                    EnquiryMasterEnquiryDate = custEnquiry.custEnquiry.EnquiryMasterEnquiryDate,
                    EnquiryMasterEnquiryAboutId = custEnquiry.custEnquiry.EnquiryMasterEnquiryAboutId,
                    EnquiryMasterRemarks = custEnquiry.custEnquiry.EnquiryMasterRemarks,
                    EnquiryMasterSalesManId = custEnquiry.custEnquiry.EnquiryMasterSalesManId,
                    EnquiryMasterCurrencyId = custEnquiry.custEnquiry.EnquiryMasterCurrencyId,
                    EnquiryMasterLocationId = custEnquiry.custEnquiry.EnquiryMasterLocationId,
                    EnquiryMasterCustomerReference = custEnquiry.custEnquiry.EnquiryMasterCustomerReference,
                    EnquiryMasterEnquiryStatusId = custEnquiry.custEnquiry.EnquiryMasterEnquiryStatusId,
                    EnquiryMasterConsultingEngineer = custEnquiry.custEnquiry.EnquiryMasterConsultingEngineer,
                    EnquiryMasterJobNo = custEnquiry.custEnquiry.EnquiryMasterJobNo,
                    EnquiryMasterBusineesTypeId = custEnquiry.custEnquiry.EnquiryMasterBusineesTypeId,
                    EnquiryMasterEnquiryVoucherNo = custEnquiry.custEnquiry.EnquiryMasterEnquiryVoucherNo,
                    EnquiryMasterVesselId = custEnquiry.custEnquiry.EnquiryMasterVesselId,
                    EnquiryMasterBuilder = custEnquiry.custEnquiry.EnquiryMasterBuilder,
                    EnquiryMasterSerialNo = custEnquiry.custEnquiry.EnquiryMasterSerialNo,
                    EnquiryMasterVendorId = custEnquiry.custEnquiry.EnquiryMasterVendorId,
                    EnquiryMasterTypeId = custEnquiry.custEnquiry.EnquiryMasterTypeId,
                    EnquiryMasterModelId = custEnquiry.custEnquiry.EnquiryMasterModelId,
                    EnquiryMasterContactId = custEnquiry.custEnquiry.EnquiryMasterContactId,
                    EnquiryMasterContactName = custEnquiry.custEnquiry.EnquiryMasterContactName,
                    EnquiryMasterContactEmail = custEnquiry.custEnquiry.EnquiryMasterContactEmail,
                    EnquiryMasterDelStatus = custEnquiry.custEnquiry.EnquiryMasterDelStatus

                };
                customerEnquiryViewModel.CustomerEnquiryDetails = _mapper.Map<List<EnquiryDetailsViewModel>>(custEnquiry.customerEnquiryDetails);
                ApiResponse<EnquiryMasterViewModel> apiResponse = new ApiResponse<EnquiryMasterViewModel>
                {
                    Valid = true,
                    Result = customerEnquiryViewModel,
                    Message = ""
                };

                return apiResponse;
            }
            return null;

        }

        [HttpGet]
        [Route("CustomerEnquiry/LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();
            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                     && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
                     {
                         k.ItemMasterItemId,
                         k.ItemMasterItemName
                     }).ToList();

            var customerMasters = _customerMasterRepository.GetAsQueryable().Where(k => k.CustomerMasterCustomerDelStatus != true).Select(a => new
            {
                a.CustomerMasterCustomerNo,
                a.CustomerMasterCustomerName,
                a.CustomerMasterCustomerVatNo,
                a.CustomerMasterCustomerContactPerson,
                a.CustomerMasterCustomerAddress,
                a.CustomerMasterCustomerReffAcNo
            }).ToList();

            var unitMasters = unitrepository.GetAsQueryable().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                x.UnitMasterUnitFullName,
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim()
            }).ToList();
            var currencyMasters = currencyrepository.GetAsQueryable().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                c.CurrencyMasterCurrencyName,
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName,
            }).ToList();
            var SaleManList = salesmanrepository.GetAsQueryable().Where(a => a.SalesManMasterSalesManDelStatus != true).Select(c => new
            {
                c.SalesManMasterSalesManId,
                c.SalesManMasterSalesManName,
            }).ToList();
            var enquiryAbout = enquiryAboutrepository.GetAsQueryable().Where(a => a.EnquiryAboutEnquiryAbountDelStatus != true).Select(c => new
            {
                c.EnquiryAboutEnquiryAboutId,
                c.EnquiryAboutEnquiryAbout,
            }).ToList();
            var enquiryStatus = enquiryStatusrepository.GetAsQueryable().Where(a => a.EnquiryStatusEnquiryStatusDelStatus != true).Select(c => new
            {
                c.EnquiryStatusEnquiryStatusId,
                c.EnquiryStatusEnquiryStatus,
            }).ToList();
            var businesstypeMasters = businesstyperepository.GetAsQueryable().Where(a => a.BusinessTypeMasterBusinessTypeDelStatus != true).Select(c => new
            {
                c.BusinessTypeMasterBusinessTypeId,
                c.BusinessTypeMasterBusinessTypeName
            }).ToList();
            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                unitMasters = unitMasters,
                currencyMasters = currencyMasters,
                LocationMaster = LocationMaster,
                SaleManList = SaleManList,
                customerMasters= customerMasters,
                enquiryAbout = enquiryAbout,
                enquiryStatus = enquiryStatus,
                businesstypeMasters= businesstypeMasters
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }
    }
}
