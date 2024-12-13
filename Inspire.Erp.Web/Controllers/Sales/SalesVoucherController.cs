using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;

using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/SalesVoucher")]
    [Produces("application/json")]
    [ApiController]
    public class SalesVoucherController : ControllerBase
    {
        private ISalesVoucherService _salesVoucherService;
        private readonly IMapper _mapper;
        private IRepository<ItemMaster> itemrepository;
        private readonly IRepository<SuppliersMaster> supplierrepository;
        private IRepository<UnitMaster> unitrepository; private IRepository<VendorMaster> Brandrepository;
        private IRepository<CostCenterMaster> costcenterrepository; private IRepository<AccountSettings> _accountsSettingsRepo;
        private IRepository<JobMaster> jobrepository; private IRepository<LocationMaster> locationrepository;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CustomerMaster> _customerMasterRepository;
        private IChartofAccountsService chartofAccountsService; private IRepository<SalesManMaster> salesmanrepository;
        private IRepository<DepartmentMaster> departmentrepository; IRepository<UnitDetails> unitDetailRepository;


        private IRepository<UnitDetails> _UnitDetailsRepository;
        public SalesVoucherController(ISalesVoucherService salesVoucherService, IMapper mapper, IRepository<UnitDetails> unitDetailsRepository,
             IRepository<ItemMaster> _itemrepository, IRepository<UnitDetails> _unitDetailRepository,
            IRepository<SuppliersMaster> _supplierrepository, IRepository<VendorMaster> _Brandrepository, IRepository<AccountSettings> accountsSettingsRepo,
            IRepository<UnitMaster> _unitrepository, IRepository<LocationMaster> _locationrepository, IRepository<SalesManMaster> _salesmanrepository
            , IRepository<CostCenterMaster> _countryrepository, IRepository<DepartmentMaster> _departmentrepository, IRepository<CustomerMaster> customerMasterRepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService)
        {
            _salesVoucherService = salesVoucherService;
            _mapper = mapper;
            _UnitDetailsRepository = unitDetailsRepository; salesmanrepository = _salesmanrepository;
            supplierrepository = _supplierrepository; _customerMasterRepository = customerMasterRepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; Brandrepository = _Brandrepository;
            costcenterrepository = _countryrepository; departmentrepository = _departmentrepository; unitDetailRepository = _unitDetailRepository;
            jobrepository = _jobrepository; locationrepository = _locationrepository; _accountsSettingsRepo = accountsSettingsRepo;
            currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }

        [HttpGet]
        [Route("SalesVoucher_GetAllLocationMaster")]

        public List<LocationMaster> SalesVoucher_GetAllLocationMaster()
        {
            return _mapper.Map<List<LocationMaster>>(_salesVoucherService.SalesVoucher_GetAllLocationMaster());


        }

        [HttpGet]
        [Route("GetSalesVoucherDetailsByMasterNo")]
        public ApiResponse<SalesVoucher> GetSalesVoucherDetailsByMasterNo(string SalesVoucherNo)
        {
            var item = _salesVoucherService.GetSalesVoucherDetailsByMasterNo(SalesVoucherNo);
            ApiResponse<SalesVoucher> apiResponse = new ApiResponse<SalesVoucher>
            {
                Valid = true,
                Result = item,
                Message = ""
            };
            return apiResponse;

        }
        [HttpGet]
        [Route("SalesVoucher_GetAllSalesManMaster")]
        public List<CustomerMaster> SalesVoucher_GetAllSalesManMaster()
        {
            return _mapper.Map<List<CustomerMaster>>(_salesVoucherService.SalesVoucher_GetAllSalesManMaster());


        }


        [HttpGet]
        [Route("SalesVoucher_GetAllCustomerMaster")]
        public List<CustomerMaster> SalesVoucher_GetAllCustomerMaster()
        {
            return _mapper.Map<List<CustomerMaster>>(_salesVoucherService.SalesVoucher_GetAllCustomerMaster());


        }

        [HttpGet]
        [Route("SalesVoucher_GetAllDepartmentMaster")]
        public List<DepartmentMaster> SalesVoucher_GetAllDepartmentMaster()
        {
            return _mapper.Map<List<DepartmentMaster>>(_salesVoucherService.SalesVoucher_GetAllDepartmentMaster());


        }

        [HttpGet]
        [Route("SalesVoucher_GetAllSuppliersMaster")]
        public List<SuppliersMaster> SalesVoucher_GetAllSuppliersMaster()
        {
            return _mapper.Map<List<SuppliersMaster>>(_salesVoucherService.SalesVoucher_GetAllSuppliersMaster());


        }


        [HttpGet]
        [Route("SalesVoucher_GetAllUnitMaster")]
        public List<UnitMaster> SalesVoucher_GetAllUnitMaster()
        {
            return _mapper.Map<List<UnitMaster>>(_salesVoucherService.SalesVoucher_GetAllUnitMaster());


        }

        [HttpGet]
        [Route("SalesVoucher_GetAllItemMaster")]
        public List<ItemMaster> SalesVoucher_GetAllItemMaster()
        {
            return _mapper.Map<List<ItemMaster>>(_salesVoucherService.SalesVoucher_GetAllItemMaster());


        }

        [HttpPost]
        [Route("InsertSalesVoucher")]
        public async Task<ApiResponse<SalesVoucherViewModel>> InsertSalesVoucher([FromBody] SalesVoucherViewModel voucherCompositeView)
        {
            ApiResponse<SalesVoucherViewModel> apiResponse = new ApiResponse<SalesVoucherViewModel>();
            if (_salesVoucherService.GetVouchersNumbers(voucherCompositeView.SalesVoucherNo) == null)
            {
                var param1 = _mapper.Map<SalesVoucher>(voucherCompositeView);
                var param2 = _mapper.Map<List<SalesVoucherDetails>>(voucherCompositeView.SalesVoucherDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                List<StockRegister> param4 = new List<StockRegister>();

                var xs = await _salesVoucherService.InsertSalesVoucher(param1, param3, param2, param4);

                //costprice=itemAvgPrice * quantity * Conversion rate

                SalesVoucherViewModel salesVoucherViewModel = new SalesVoucherViewModel
                {


                    SalesVoucherId = xs.salesVoucher.SalesVoucherId,
                    SalesVoucherNo = xs.salesVoucher.SalesVoucherNo,
                    SalesVoucherDate = xs.salesVoucher.SalesVoucherDate,
                    SalesVoucherType = xs.salesVoucher.SalesVoucherType,
                    SalesVoucherPartyId = xs.salesVoucher.SalesVoucherPartyId,
                    SalesVoucherPartyName = xs.salesVoucher.SalesVoucherPartyName,
                    SalesVoucherPartyAddress = xs.salesVoucher.SalesVoucherPartyAddress,
                    SalesVoucherPartyVatNo = xs.salesVoucher.SalesVoucherPartyVatNo,
                    SalesVoucherContPerson = xs.salesVoucher.SalesVoucherContPerson,
                    SalesVoucherRefNo = xs.salesVoucher.SalesVoucherRefNo,
                    SalesVoucherDescription = xs.salesVoucher.SalesVoucherDescription,
                    SalesVoucherDlvNo = xs.salesVoucher.SalesVoucherDlvNo,
                    SalesVoucherDlvDate = xs.salesVoucher.SalesVoucherDlvDate,
                    SalesVoucherSono = xs.salesVoucher.SalesVoucherSONo,
                    SalesVoucherSodate = xs.salesVoucher.SalesVoucherSODate,
                    SalesVoucherQtnNo = xs.salesVoucher.SalesVoucherQtnNo,
                    SalesVoucherQtnDate = xs.salesVoucher.SalesVoucherQtnDate,
                    SalesVoucherExcludeVat = xs.salesVoucher.SalesVoucherExcludeVAT,
                    SalesVoucherSalesManId = xs.salesVoucher.SalesVoucherSalesManID,
                    SalesVoucherDptId = xs.salesVoucher.SalesVoucherDptID,
                    SalesVoucherEnqNo = xs.salesVoucher.SalesVoucherEnqNo,
                    SalesVoucherLocationId = xs.salesVoucher.SalesVoucherLocationID,
                    SalesVoucherUserId = xs.salesVoucher.SalesVoucherUserID,
                    SalesVoucherCurrencyId = xs.salesVoucher.SalesVoucherCurrencyId,
                    SalesVoucherCompanyId = xs.salesVoucher.SalesVoucherCompanyId,
                    SalesVoucherJobId = xs.salesVoucher.SalesVoucherJobId,
                    SalesVoucherFsno = xs.salesVoucher.SalesVoucherFSNO,
                    SalesVoucherFcRate = xs.salesVoucher.SalesVoucherFc_Rate,
                    SalesVoucherStatus = xs.salesVoucher.SalesVoucherStatus,
                    SalesVoucherTotalGrossAmount = xs.salesVoucher.SalesVoucherTotalGrossAmount,
                    SalesVoucherTotalItemDisAmount = xs.salesVoucher.SalesVoucherTotalItemDisAmount,
                    SalesVoucherTotalActualAmount = xs.salesVoucher.SalesVoucherTotalActualAmount,
                    SalesVoucherTotalDisPer = xs.salesVoucher.SalesVoucherTotalDisPer,
                    SalesVoucherTotalDisAmount = xs.salesVoucher.SalesVoucherTotalDisAmount,
                    SalesVoucherVatAmt = xs.salesVoucher.SalesVoucherVat_AMT,
                    SalesVoucherVatPer = xs.salesVoucher.SalesVoucherVat_Per,
                    SalesVoucherVatRoundSign = xs.salesVoucher.SalesVoucherVat_RoundSign,
                    SalesVoucherVatRountAmt = xs.salesVoucher.SalesVoucherVat_RountAmt,
                    SalesVoucherNetDisAmount = xs.salesVoucher.SalesVoucherNetDisAmount,
                    SalesVoucherNetAmount = xs.salesVoucher.SalesVoucherNetAmount,
                    SalesVoucherEnqDate = xs.salesVoucher.SalesVoucherEnqDate,
                    SalesVoucherShippinAddress = xs.salesVoucher.SalesVoucherShippinAddress,
                    SalesVoucherTermsAndCondition = xs.salesVoucher.SalesVoucherTermsAndCondition,
                    SalesVoucherRemarks2 = xs.salesVoucher.SalesVoucherRemarks2,
                    SalesVoucherHeader = xs.salesVoucher.SalesVoucherHeader,
                    SalesVoucherDelivery = xs.salesVoucher.SalesVoucherDelivery,
                    SalesVoucherNotes = xs.salesVoucher.SalesVoucherNotes,
                    SalesVoucherFooter = xs.salesVoucher.SalesVoucherFooter,
                    SalesVoucherPaymentTerms = xs.salesVoucher.SalesVoucherPaymentTerms,
                    SalesVoucherSubject = xs.salesVoucher.SalesVoucherSubject,
                    SalesVoucherContent = xs.salesVoucher.SalesVoucherContent,
                    SalesVoucherRemarks1 = xs.salesVoucher.SalesVoucherRemarks1,
                    SalesVoucherDelStatus = xs.salesVoucher.SalesVoucherDelStatus,
                };

                salesVoucherViewModel.SalesVoucherDetails = _mapper.Map<List<SalesVoucherDetailsViewModel>>(xs.salesVoucherDetails);
                salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<SalesVoucherViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<SalesVoucherViewModel>(salesVoucherViewModel),
                    Message = SalesVoucherMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<SalesVoucherViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = SalesVoucherMessage.VoucherAlreadyExist
                };

            }


            return apiResponse;


        }

        [HttpPost]
        [Route("UpdateSalesVoucher")]
        public async Task<ApiResponse<SalesVoucherViewModel>> UpdateSalesVoucher([FromBody] SalesVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesVoucherDetails>>(voucherCompositeView.SalesVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _salesVoucherService.UpdateSalesVoucher(param1, param3, param2
            //    //, param4
            //    );

            //==============
            //param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();

            //var allUnits = new List<UnitDetails>();
            //var unitDetails = new UnitDetails();
            //foreach (var item in param1.SalesVoucherDetails)
            //{
            //    unitDetails = _UnitDetailsRepository.GetAsQueryable().Where(c => c.UnitDetailsUnitId == item.SalesVoucherDetailsUnitId).FirstOrDefault();
            //    allUnits.Add(unitDetails);
            //}


            //clsAccountAndStock.SalesVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3, allUnits);

            var xs = await _salesVoucherService.UpdateSalesVoucher(param1, param3, param2
           , param4
           );
            //========================


            SalesVoucherViewModel salesVoucherViewModel = new SalesVoucherViewModel
            {
                SalesVoucherId = xs.salesVoucher.SalesVoucherId,
                SalesVoucherNo = xs.salesVoucher.SalesVoucherNo,
                SalesVoucherDate = xs.salesVoucher.SalesVoucherDate,
                SalesVoucherType = xs.salesVoucher.SalesVoucherType,
                SalesVoucherPartyId = xs.salesVoucher.SalesVoucherPartyId,
                SalesVoucherPartyName = xs.salesVoucher.SalesVoucherPartyName,
                SalesVoucherPartyAddress = xs.salesVoucher.SalesVoucherPartyAddress,
                SalesVoucherPartyVatNo = xs.salesVoucher.SalesVoucherPartyVatNo,
                SalesVoucherContPerson = xs.salesVoucher.SalesVoucherContPerson,
                SalesVoucherRefNo = xs.salesVoucher.SalesVoucherRefNo,
                SalesVoucherDescription = xs.salesVoucher.SalesVoucherDescription,
                SalesVoucherDlvNo = xs.salesVoucher.SalesVoucherDlvNo,
                SalesVoucherDlvDate = xs.salesVoucher.SalesVoucherDlvDate,
                SalesVoucherSono = xs.salesVoucher.SalesVoucherSONo,
                SalesVoucherSodate = xs.salesVoucher.SalesVoucherSODate,
                SalesVoucherQtnNo = xs.salesVoucher.SalesVoucherQtnNo,
                SalesVoucherQtnDate = xs.salesVoucher.SalesVoucherQtnDate,
                SalesVoucherExcludeVat = xs.salesVoucher.SalesVoucherExcludeVAT,
                SalesVoucherSalesManId = xs.salesVoucher.SalesVoucherSalesManID,
                SalesVoucherDptId = xs.salesVoucher.SalesVoucherDptID,
                SalesVoucherEnqNo = xs.salesVoucher.SalesVoucherEnqNo,
                SalesVoucherLocationId = xs.salesVoucher.SalesVoucherLocationID,
                SalesVoucherUserId = xs.salesVoucher.SalesVoucherUserID,
                SalesVoucherCurrencyId = xs.salesVoucher.SalesVoucherCurrencyId,
                SalesVoucherCompanyId = xs.salesVoucher.SalesVoucherCompanyId,
                SalesVoucherJobId = xs.salesVoucher.SalesVoucherJobId,
                SalesVoucherFsno = xs.salesVoucher.SalesVoucherFSNO,
                SalesVoucherFcRate = xs.salesVoucher.SalesVoucherFc_Rate,
                SalesVoucherStatus = xs.salesVoucher.SalesVoucherStatus,
                SalesVoucherTotalGrossAmount = xs.salesVoucher.SalesVoucherTotalGrossAmount,
                SalesVoucherTotalItemDisAmount = xs.salesVoucher.SalesVoucherTotalItemDisAmount,
                SalesVoucherTotalActualAmount = xs.salesVoucher.SalesVoucherTotalActualAmount,
                SalesVoucherTotalDisPer = xs.salesVoucher.SalesVoucherTotalDisPer,
                SalesVoucherTotalDisAmount = xs.salesVoucher.SalesVoucherTotalDisAmount,
                SalesVoucherVatAmt = xs.salesVoucher.SalesVoucherVat_AMT,
                SalesVoucherVatPer = xs.salesVoucher.SalesVoucherVat_Per,
                SalesVoucherVatRoundSign = xs.salesVoucher.SalesVoucherVat_RoundSign,
                SalesVoucherVatRountAmt = xs.salesVoucher.SalesVoucherVat_RountAmt,
                SalesVoucherNetDisAmount = xs.salesVoucher.SalesVoucherNetDisAmount,
                SalesVoucherNetAmount = xs.salesVoucher.SalesVoucherNetAmount,
                SalesVoucherEnqDate = xs.salesVoucher.SalesVoucherEnqDate,
                SalesVoucherShippinAddress = xs.salesVoucher.SalesVoucherShippinAddress,
                SalesVoucherTermsAndCondition = xs.salesVoucher.SalesVoucherTermsAndCondition,
                SalesVoucherRemarks2 = xs.salesVoucher.SalesVoucherRemarks2,
                SalesVoucherHeader = xs.salesVoucher.SalesVoucherHeader,
                SalesVoucherDelivery = xs.salesVoucher.SalesVoucherDelivery,
                SalesVoucherNotes = xs.salesVoucher.SalesVoucherNotes,
                SalesVoucherFooter = xs.salesVoucher.SalesVoucherFooter,
                SalesVoucherPaymentTerms = xs.salesVoucher.SalesVoucherPaymentTerms,
                SalesVoucherSubject = xs.salesVoucher.SalesVoucherSubject,
                SalesVoucherContent = xs.salesVoucher.SalesVoucherContent,
                SalesVoucherRemarks1 = xs.salesVoucher.SalesVoucherRemarks1,
                SalesVoucherDelStatus = xs.salesVoucher.SalesVoucherDelStatus,
            };

            salesVoucherViewModel.SalesVoucherDetails = _mapper.Map<List<SalesVoucherDetailsViewModel>>(xs.salesVoucherDetails);
            salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<SalesVoucherViewModel> apiResponse = new ApiResponse<SalesVoucherViewModel>
            {
                Valid = true,
                Result = _mapper.Map<SalesVoucherViewModel>(salesVoucherViewModel),
                Message = SalesVoucherMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteSalesVoucher")]
        public ApiResponse<int> DeleteSalesVoucher([FromBody] SalesVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<SalesVoucherDetails>>(voucherCompositeView.SalesVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _salesVoucherService.DeleteSalesVoucher(  param1,    param3, param2
            //    //, param4
            //    );

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.SalesVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _salesVoucherService.DeleteSalesVoucher(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = SalesVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }


        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        {



            ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<AccountsTransactions>>(_salesVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;




        }

        [HttpGet]
        [Route("GetSalesVoucher")]
        public ApiResponse<IEnumerable<SalesVoucher>> GetAllSalesVoucher()
        {
            ApiResponse<IEnumerable<SalesVoucher>> apiResponse = new ApiResponse<IEnumerable<SalesVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<IEnumerable<SalesVoucher>>(_salesVoucherService.GetSalesVoucher()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedSalesVoucherDetails/{id}")]
        public ApiResponse<SalesVoucherViewModel> GetSavedSalesVoucherDetails(string id)
        {
            SalesVoucherModel xs = _salesVoucherService.GetSavedSalesVoucherDetails(id);

            if (xs.salesVoucher != null)
            {
                SalesVoucherViewModel salesVoucherViewModel = new SalesVoucherViewModel
                {
                    SalesVoucherId = xs.salesVoucher.SalesVoucherId,
                    SalesVoucherNo = xs.salesVoucher.SalesVoucherNo,
                    SalesVoucherDate = xs.salesVoucher.SalesVoucherDate,
                    SalesVoucherType = xs.salesVoucher.SalesVoucherType,
                    SalesVoucherPartyId = xs.salesVoucher.SalesVoucherPartyId,
                    SalesVoucherPartyName = xs.salesVoucher.SalesVoucherPartyName,
                    SalesVoucherPartyAddress = xs.salesVoucher.SalesVoucherPartyAddress,
                    SalesVoucherPartyVatNo = xs.salesVoucher.SalesVoucherPartyVatNo,
                    SalesVoucherContPerson = xs.salesVoucher.SalesVoucherContPerson,
                    SalesVoucherRefNo = xs.salesVoucher.SalesVoucherRefNo,
                    SalesVoucherDescription = xs.salesVoucher.SalesVoucherDescription,
                    SalesVoucherDlvNo = xs.salesVoucher.SalesVoucherDlvNo,
                    SalesVoucherDlvDate = xs.salesVoucher.SalesVoucherDlvDate,
                    SalesVoucherSono = xs.salesVoucher.SalesVoucherSONo,
                    SalesVoucherSodate = xs.salesVoucher.SalesVoucherSODate,
                    SalesVoucherQtnNo = xs.salesVoucher.SalesVoucherQtnNo,
                    SalesVoucherQtnDate = xs.salesVoucher.SalesVoucherQtnDate,
                    SalesVoucherExcludeVat = xs.salesVoucher.SalesVoucherExcludeVAT,
                    SalesVoucherSalesManId = xs.salesVoucher.SalesVoucherSalesManID,
                    SalesVoucherDptId = xs.salesVoucher.SalesVoucherDptID,
                    SalesVoucherEnqNo = xs.salesVoucher.SalesVoucherEnqNo,
                    SalesVoucherLocationId = xs.salesVoucher.SalesVoucherLocationID,
                    SalesVoucherUserId = xs.salesVoucher.SalesVoucherUserID,
                    SalesVoucherCurrencyId = xs.salesVoucher.SalesVoucherCurrencyId,
                    SalesVoucherCompanyId = xs.salesVoucher.SalesVoucherCompanyId,
                    SalesVoucherJobId = xs.salesVoucher.SalesVoucherJobId,
                    SalesVoucherFsno = xs.salesVoucher.SalesVoucherFSNO,
                    SalesVoucherFcRate = xs.salesVoucher.SalesVoucherFc_Rate,
                    SalesVoucherStatus = xs.salesVoucher.SalesVoucherStatus,
                    SalesVoucherTotalGrossAmount = xs.salesVoucher.SalesVoucherTotalGrossAmount,
                    SalesVoucherTotalItemDisAmount = xs.salesVoucher.SalesVoucherTotalItemDisAmount,
                    SalesVoucherTotalActualAmount = xs.salesVoucher.SalesVoucherTotalActualAmount,
                    SalesVoucherTotalDisPer = xs.salesVoucher.SalesVoucherTotalDisPer,
                    SalesVoucherTotalDisAmount = xs.salesVoucher.SalesVoucherTotalDisAmount,
                    SalesVoucherVatAmt = xs.salesVoucher.SalesVoucherVat_AMT,
                    SalesVoucherVatPer = xs.salesVoucher.SalesVoucherVat_Per,
                    SalesVoucherVatRoundSign = xs.salesVoucher.SalesVoucherVat_RoundSign,
                    SalesVoucherVatRountAmt = xs.salesVoucher.SalesVoucherVat_RountAmt,
                    SalesVoucherNetDisAmount = xs.salesVoucher.SalesVoucherNetDisAmount,
                    SalesVoucherNetAmount = xs.salesVoucher.SalesVoucherNetAmount,
                    SalesVoucherEnqDate = xs.salesVoucher.SalesVoucherEnqDate,
                    SalesVoucherShippinAddress = xs.salesVoucher.SalesVoucherShippinAddress,
                    SalesVoucherTermsAndCondition = xs.salesVoucher.SalesVoucherTermsAndCondition,
                    SalesVoucherRemarks2 = xs.salesVoucher.SalesVoucherRemarks2,
                    SalesVoucherHeader = xs.salesVoucher.SalesVoucherHeader,
                    SalesVoucherDelivery = xs.salesVoucher.SalesVoucherDelivery,
                    SalesVoucherNotes = xs.salesVoucher.SalesVoucherNotes,
                    SalesVoucherFooter = xs.salesVoucher.SalesVoucherFooter,
                    SalesVoucherPaymentTerms = xs.salesVoucher.SalesVoucherPaymentTerms,
                    SalesVoucherSubject = xs.salesVoucher.SalesVoucherSubject,
                    SalesVoucherContent = xs.salesVoucher.SalesVoucherContent,
                    SalesVoucherRemarks1 = xs.salesVoucher.SalesVoucherRemarks1,
                    SalesVoucherDelStatus = xs.salesVoucher.SalesVoucherDelStatus,

                };
                salesVoucherViewModel.SalesVoucherDetails = _mapper.Map<List<SalesVoucherDetailsViewModel>>(xs.salesVoucherDetails);
                salesVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                ApiResponse<SalesVoucherViewModel> apiResponse = new ApiResponse<SalesVoucherViewModel>
                {
                    Valid = true,
                    Result = salesVoucherViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;



        }

        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(string id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = SalesVoucherMessage.VoucherNumberExist



            };
            var x = _salesVoucherService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }

        [HttpGet]
        [Route("GetSalesReportDetailWise")]
        public IActionResult GetSalesReportDetailWise()
        {
            try
            {
                return Ok(_salesVoucherService.GetSalesReportDetailWise());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetSalesReportSummaryWise")]
        public IActionResult GetSalesReportSummaryWise()
        {
            try
            {
                return Ok(_salesVoucherService.GetSalesReportSummaryWise());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
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
                                  itemMasterBarcode = unit.UnitDetailsBarcode
                              }).Distinct().ToList();
            var customerMasters = _customerMasterRepository.GetAsQueryable().AsNoTracking().Where(k => k.CustomerMasterCustomerDelStatus != true).Select(a => new
            {
                a.CustomerMasterCustomerNo,
                a.CustomerMasterCustomerName,          
                a.CustomerMasterCustomerVatNo,
                a.CustomerMasterCustomerContactPerson,
                a.CustomerMasterCustomerAddress,
                a.CustomerMasterCustomerReffAcNo,
                a.CustomerMasterCustomerEmail,
                a.CustomerMasterCustomerMobile,
                a.CustomerMasterCustomerTel1,
                a.CustomerMasterCustomerFax
            }).ToList();

            var unitMasters = unitrepository.GetAsQueryable().AsNoTracking().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                UnitMasterUnitFullName = x.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim()
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
            var DepartmentsList = departmentrepository.GetAsQueryable().AsNoTracking().Where(a => a.DepartmentMasterDepartmentDelStatus != true).Select(k => new
            {
                k.DepartmentMasterDepartmentId,
                k.DepartmentMasterDepartmentName,

            }).ToList();
            var costcenterMasters = costcenterrepository.GetAsQueryable().AsNoTracking().Where(a => a.CostCenterMasterCostCenterDelStatus != true).Select(c => new
            {
                c.CostCenterMasterCostCenterId,
                c.CostCenterMasterCostCenterName,
            }).ToList();
            var SaleManList = salesmanrepository.GetAsQueryable().AsNoTracking().Where(a => a.SalesManMasterSalesManDelStatus != true).Select(c => new
            {
                c.SalesManMasterSalesManId,
                c.SalesManMasterSalesManName,
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
                unitMasters = unitMasters,
                jobMasters = jobMasters,
                currencyMasters = currencyMasters,
                masterAccountsTables = masterAccountsTables,
                LocationMaster = LocationMaster,
                DepartmentsList = DepartmentsList,
                BrandMasters = BrandMasters,
                SaleManList = SaleManList,
                costcenterMasters = costcenterMasters,
                customerMasters = customerMasters,
                accountsSettings = accountsSettings
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }
    }
}








