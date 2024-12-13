using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;

using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.MODULE;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Web.Controllers.Store
{
    [Route("api/IssueVoucher")]
    [Produces("application/json")]
    [ApiController]
    public class IssueVoucherController : ControllerBase
    {
        private IIssueVoucherService _issueVoucherService;
        private readonly IMapper _mapper;
        private IRepository<ItemMaster> itemrepository;
        private readonly IRepository<SuppliersMaster> supplierrepository;
        private IRepository<UnitMaster> unitrepository; private IRepository<VendorMaster> Brandrepository;
        private IRepository<CostCenterMaster> costcenterrepository;
        private IRepository<JobMaster> jobrepository; private IRepository<LocationMaster> locationrepository;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CustomerMaster> _customerMasterRepository;
        private IChartofAccountsService chartofAccountsService; private IRepository<SalesManMaster> salesmanrepository;
        private IRepository<DepartmentMaster> departmentrepository;
        private IRepository<UnitDetails> _UnitDetailsRepository; private IRepository<IssueVoucher> _issueVoucherRepository;
        public IssueVoucherController(IIssueVoucherService issueVoucherService, IMapper mapper,
             IRepository<ItemMaster> _itemrepository, IRepository<UnitDetails> unitDetailsRepository,
            IRepository<SuppliersMaster> _supplierrepository, IRepository<VendorMaster> _Brandrepository, IRepository<IssueVoucher> issueVoucherRepository,
            IRepository<UnitMaster> _unitrepository, IRepository<LocationMaster> _locationrepository, IRepository<SalesManMaster> _salesmanrepository
            , IRepository<CostCenterMaster> _countryrepository, IRepository<DepartmentMaster> _departmentrepository, IRepository<CustomerMaster> customerMasterRepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService)
        {
            _issueVoucherService = issueVoucherService;
            _mapper = mapper; _issueVoucherRepository = issueVoucherRepository;
            _UnitDetailsRepository = unitDetailsRepository; salesmanrepository = _salesmanrepository;
            supplierrepository = _supplierrepository; _customerMasterRepository = customerMasterRepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; Brandrepository = _Brandrepository;
            costcenterrepository = _countryrepository; departmentrepository = _departmentrepository;
            jobrepository = _jobrepository; locationrepository = _locationrepository;
            currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }


        [HttpPost]
        [Route("InsertIssueVoucher")]
        public ApiResponse<IssueVoucherViewModel> InsertIssueVoucher([FromBody] IssueVoucherViewModel voucherCompositeView)
        {

            ApiResponse<IssueVoucherViewModel> apiResponse = new ApiResponse<IssueVoucherViewModel>();

            var param1 = _mapper.Map<IssueVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<IssueVoucherDetails>>(voucherCompositeView.IssueVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();

            var xs = _issueVoucherService.InsertIssueVoucher(param1, param3, param2
           , param4
           );

            IssueVoucherViewModel issueVoucherViewModel = new IssueVoucherViewModel
            {

                IssueVoucherId = xs.issueVoucher.IssueVoucherId,
                IssueVoucherNo = xs.issueVoucher.IssueVoucherNo,
                IssueVoucherDate = xs.issueVoucher.IssueVoucherDate,
                IssueVoucherCreditAccNo = xs.issueVoucher.IssueVoucherCreditAccNo,
                IssueVoucherDepartmentId = xs.issueVoucher.IssueVoucherDepartmentId,
                IssueVoucherBufferRemark1 = xs.issueVoucher.IssueVoucherBufferRemark1,
                IssueVoucherCostCenterId = xs.issueVoucher.IssueVoucherCostCenterId,
                IssueVoucherDebitAccNo = xs.issueVoucher.IssueVoucherDebitAccNo,
                IssueVoucherIvNoForRet = xs.issueVoucher.IssueVoucherIvNoForRet,
                IssueVoucherRefNo = xs.issueVoucher.IssueVoucherRefNo,
                IssueVoucherDescription = xs.issueVoucher.IssueVoucherDescription,
                IssueVoucherGrno = xs.issueVoucher.IssueVoucherGrno,
                IssueVoucherGrdate = xs.issueVoucher.IssueVoucherGrdate,
                IssueVoucherLpono = xs.issueVoucher.IssueVoucherLpono,
                IssueVoucherLpodate = xs.issueVoucher.IssueVoucherLpodate,
                IssueVoucherQtnNo = xs.issueVoucher.IssueVoucherQtnNo,
                IssueVoucherQtnDate = xs.issueVoucher.IssueVoucherQtnDate,
                IssueVoucherIvDateForRet = xs.issueVoucher.IssueVoucherIvDateForRet,
                IssueVoucherReqNo = xs.issueVoucher.IssueVoucherReqNo,
                IssueVoucherReqDate = xs.issueVoucher.IssueVoucherReqDate,
                IssueVoucherDayBookNo = xs.issueVoucher.IssueVoucherDayBookNo,
                IssueVoucherLocationId = xs.issueVoucher.IssueVoucherLocationId,
                IssueVoucherUserId = xs.issueVoucher.IssueVoucherUserId,
                IssueVoucherCurrencyId = xs.issueVoucher.IssueVoucherCurrencyId,
                IssueVoucherCompanyId = xs.issueVoucher.IssueVoucherCompanyId,
                IssueVoucherJobId = xs.issueVoucher.IssueVoucherJobId,
                IssueVoucherFsno = xs.issueVoucher.IssueVoucherFsno,
                IssueVoucherFcRate = xs.issueVoucher.IssueVoucherFcRate,
                IssueVoucherStatus = xs.issueVoucher.IssueVoucherStatus,
                IssueVoucherTotalGrossAmount = xs.issueVoucher.IssueVoucherTotalGrossAmount,
                IssueVoucherTotalItemDisAmount = xs.issueVoucher.IssueVoucherTotalItemDisAmount,
                IssueVoucherTotalActualAmount = xs.issueVoucher.IssueVoucherTotalActualAmount,
                IssueVoucherTotalDisPer = xs.issueVoucher.IssueVoucherTotalDisPer,
                IssueVoucherTotalDisAmount = xs.issueVoucher.IssueVoucherTotalDisAmount,
                IssueVoucherVatAmt = xs.issueVoucher.IssueVoucherVatAmt,
                IssueVoucherVatPer = xs.issueVoucher.IssueVoucherVatPer,
                IssueVoucherVatRoundSign = xs.issueVoucher.IssueVoucherVatRoundSign,
                IssueVoucherVatRountAmt = xs.issueVoucher.IssueVoucherVatRountAmt,
                IssueVoucherNetDisAmount = xs.issueVoucher.IssueVoucherNetDisAmount,
                IssueVoucherNetAmount = xs.issueVoucher.IssueVoucherNetAmount,
                IssueVoucherBufferRemark12 = xs.issueVoucher.IssueVoucherBufferRemark12,
                IssueVoucherBufferRemark13 = xs.issueVoucher.IssueVoucherBufferRemark13,
                IssueVoucherBufferPurNo = xs.issueVoucher.IssueVoucherBufferPurNo,
                IssueVoucherBufferReqNo = xs.issueVoucher.IssueVoucherBufferReqNo,
                IssueVoucherDelStatus = xs.issueVoucher.IssueVoucherDelStatus,


            };

            issueVoucherViewModel.IssueVoucherDetails = _mapper.Map<List<IssueVoucherDetailsViewModel>>(xs.issueVoucherDetails);
            issueVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
            apiResponse = new ApiResponse<IssueVoucherViewModel>
            {
                Valid = true,
                Result = _mapper.Map<IssueVoucherViewModel>(issueVoucherViewModel),
                Message = IssueVoucherMessage.SaveVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("UpdateIssueVoucher")]
        public ApiResponse<IssueVoucherViewModel> UpdateIssueVoucher([FromBody] IssueVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<IssueVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<IssueVoucherDetails>>(voucherCompositeView.IssueVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            // clsAccountAndStock.IssueVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _issueVoucherService.UpdateIssueVoucher(param1, param3, param2
           , param4
           );
            //========================
            IssueVoucherViewModel issueVoucherViewModel = new IssueVoucherViewModel
            {
                IssueVoucherId = xs.issueVoucher.IssueVoucherId,
                IssueVoucherNo = xs.issueVoucher.IssueVoucherNo,
                IssueVoucherDate = xs.issueVoucher.IssueVoucherDate,
                IssueVoucherCreditAccNo = xs.issueVoucher.IssueVoucherCreditAccNo,
                IssueVoucherDepartmentId = xs.issueVoucher.IssueVoucherDepartmentId,
                IssueVoucherBufferRemark1 = xs.issueVoucher.IssueVoucherBufferRemark1,
                IssueVoucherCostCenterId = xs.issueVoucher.IssueVoucherCostCenterId,
                IssueVoucherDebitAccNo = xs.issueVoucher.IssueVoucherDebitAccNo,
                IssueVoucherIvNoForRet = xs.issueVoucher.IssueVoucherIvNoForRet,
                IssueVoucherRefNo = xs.issueVoucher.IssueVoucherRefNo,
                IssueVoucherDescription = xs.issueVoucher.IssueVoucherDescription,
                IssueVoucherGrno = xs.issueVoucher.IssueVoucherGrno,
                IssueVoucherGrdate = xs.issueVoucher.IssueVoucherGrdate,
                IssueVoucherLpono = xs.issueVoucher.IssueVoucherLpono,
                IssueVoucherLpodate = xs.issueVoucher.IssueVoucherLpodate,
                IssueVoucherQtnNo = xs.issueVoucher.IssueVoucherQtnNo,
                IssueVoucherQtnDate = xs.issueVoucher.IssueVoucherQtnDate,
                IssueVoucherIvDateForRet = xs.issueVoucher.IssueVoucherIvDateForRet,
                IssueVoucherReqNo = xs.issueVoucher.IssueVoucherReqNo,
                IssueVoucherReqDate = xs.issueVoucher.IssueVoucherReqDate,
                IssueVoucherDayBookNo = xs.issueVoucher.IssueVoucherDayBookNo,
                IssueVoucherLocationId = xs.issueVoucher.IssueVoucherLocationId,
                IssueVoucherUserId = xs.issueVoucher.IssueVoucherUserId,
                IssueVoucherCurrencyId = xs.issueVoucher.IssueVoucherCurrencyId,
                IssueVoucherCompanyId = xs.issueVoucher.IssueVoucherCompanyId,
                IssueVoucherJobId = xs.issueVoucher.IssueVoucherJobId,
                IssueVoucherFsno = xs.issueVoucher.IssueVoucherFsno,
                IssueVoucherFcRate = xs.issueVoucher.IssueVoucherFcRate,
                IssueVoucherStatus = xs.issueVoucher.IssueVoucherStatus,
                IssueVoucherTotalGrossAmount = xs.issueVoucher.IssueVoucherTotalGrossAmount,
                IssueVoucherTotalItemDisAmount = xs.issueVoucher.IssueVoucherTotalItemDisAmount,
                IssueVoucherTotalActualAmount = xs.issueVoucher.IssueVoucherTotalActualAmount,
                IssueVoucherTotalDisPer = xs.issueVoucher.IssueVoucherTotalDisPer,
                IssueVoucherTotalDisAmount = xs.issueVoucher.IssueVoucherTotalDisAmount,
                IssueVoucherVatAmt = xs.issueVoucher.IssueVoucherVatAmt,
                IssueVoucherVatPer = xs.issueVoucher.IssueVoucherVatPer,
                IssueVoucherVatRoundSign = xs.issueVoucher.IssueVoucherVatRoundSign,
                IssueVoucherVatRountAmt = xs.issueVoucher.IssueVoucherVatRountAmt,
                IssueVoucherNetDisAmount = xs.issueVoucher.IssueVoucherNetDisAmount,
                IssueVoucherNetAmount = xs.issueVoucher.IssueVoucherNetAmount,
                IssueVoucherBufferRemark12 = xs.issueVoucher.IssueVoucherBufferRemark12,
                IssueVoucherBufferRemark13 = xs.issueVoucher.IssueVoucherBufferRemark13,
                IssueVoucherBufferPurNo = xs.issueVoucher.IssueVoucherBufferPurNo,
                IssueVoucherBufferReqNo = xs.issueVoucher.IssueVoucherBufferReqNo,
                IssueVoucherDelStatus = xs.issueVoucher.IssueVoucherDelStatus,
            };

            issueVoucherViewModel.IssueVoucherDetails = _mapper.Map<List<IssueVoucherDetailsViewModel>>(xs.issueVoucherDetails);
            issueVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<IssueVoucherViewModel> apiResponse = new ApiResponse<IssueVoucherViewModel>
            {
                Valid = true,
                Result = _mapper.Map<IssueVoucherViewModel>(issueVoucherViewModel),
                Message = IssueVoucherMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteIssueVoucher")]
        public ApiResponse<int> DeleteIssueVoucher([FromBody] IssueVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<IssueVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<IssueVoucherDetails>>(voucherCompositeView.IssueVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.IssueVoucher_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _issueVoucherService.DeleteIssueVoucher(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = IssueVoucherMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_issueVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetIssueVoucher")]
        public ApiResponse<List<IssueVoucher>> GetAllIssueVoucher()
        {


            ApiResponse<List<IssueVoucher>> apiResponse = new ApiResponse<List<IssueVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<IssueVoucher>>(_issueVoucherService.GetIssueVoucher()),
                Message = ""
            };
            return apiResponse;


        }

        [HttpGet]
        [Route("GetSavedIssueVoucherDetails/{id}")]
        public ApiResponse<IssueVoucherViewModel> GetSavedIssueVoucherDetails(string id)
        {
            IssueVoucherModel issueVoucher = _issueVoucherService.GetSavedIssueVoucherDetails(id);

            if (issueVoucher != null)
            {
                IssueVoucherViewModel issueVoucherViewModel = new IssueVoucherViewModel
                {


                    IssueVoucherId = issueVoucher.issueVoucher.IssueVoucherId,
                    IssueVoucherNo = issueVoucher.issueVoucher.IssueVoucherNo,
                    IssueVoucherDate = issueVoucher.issueVoucher.IssueVoucherDate,
                    IssueVoucherCreditAccNo = issueVoucher.issueVoucher.IssueVoucherCreditAccNo,
                    IssueVoucherDepartmentId = issueVoucher.issueVoucher.IssueVoucherDepartmentId,
                    IssueVoucherBufferRemark1 = issueVoucher.issueVoucher.IssueVoucherBufferRemark1,
                    IssueVoucherCostCenterId = issueVoucher.issueVoucher.IssueVoucherCostCenterId,
                    IssueVoucherDebitAccNo = issueVoucher.issueVoucher.IssueVoucherDebitAccNo,
                    IssueVoucherIvNoForRet = issueVoucher.issueVoucher.IssueVoucherIvNoForRet,
                    IssueVoucherRefNo = issueVoucher.issueVoucher.IssueVoucherRefNo,
                    IssueVoucherDescription = issueVoucher.issueVoucher.IssueVoucherDescription,
                    IssueVoucherGrno = issueVoucher.issueVoucher.IssueVoucherGrno,
                    IssueVoucherGrdate = issueVoucher.issueVoucher.IssueVoucherGrdate,
                    IssueVoucherLpono = issueVoucher.issueVoucher.IssueVoucherLpono,
                    IssueVoucherLpodate = issueVoucher.issueVoucher.IssueVoucherLpodate,
                    IssueVoucherQtnNo = issueVoucher.issueVoucher.IssueVoucherQtnNo,
                    IssueVoucherQtnDate = issueVoucher.issueVoucher.IssueVoucherQtnDate,
                    IssueVoucherIvDateForRet = issueVoucher.issueVoucher.IssueVoucherIvDateForRet,
                    IssueVoucherReqNo = issueVoucher.issueVoucher.IssueVoucherReqNo,
                    IssueVoucherReqDate = issueVoucher.issueVoucher.IssueVoucherReqDate,
                    IssueVoucherDayBookNo = issueVoucher.issueVoucher.IssueVoucherDayBookNo,
                    IssueVoucherLocationId = issueVoucher.issueVoucher.IssueVoucherLocationId,
                    IssueVoucherUserId = issueVoucher.issueVoucher.IssueVoucherUserId,
                    IssueVoucherCurrencyId = issueVoucher.issueVoucher.IssueVoucherCurrencyId,
                    IssueVoucherCompanyId = issueVoucher.issueVoucher.IssueVoucherCompanyId,
                    IssueVoucherJobId = issueVoucher.issueVoucher.IssueVoucherJobId,
                    IssueVoucherFsno = issueVoucher.issueVoucher.IssueVoucherFsno,
                    IssueVoucherFcRate = issueVoucher.issueVoucher.IssueVoucherFcRate,
                    IssueVoucherStatus = issueVoucher.issueVoucher.IssueVoucherStatus,
                    IssueVoucherTotalGrossAmount = issueVoucher.issueVoucher.IssueVoucherTotalGrossAmount,
                    IssueVoucherTotalItemDisAmount = issueVoucher.issueVoucher.IssueVoucherTotalItemDisAmount,
                    IssueVoucherTotalActualAmount = issueVoucher.issueVoucher.IssueVoucherTotalActualAmount,
                    IssueVoucherTotalDisPer = issueVoucher.issueVoucher.IssueVoucherTotalDisPer,
                    IssueVoucherTotalDisAmount = issueVoucher.issueVoucher.IssueVoucherTotalDisAmount,
                    IssueVoucherVatAmt = issueVoucher.issueVoucher.IssueVoucherVatAmt,
                    IssueVoucherVatPer = issueVoucher.issueVoucher.IssueVoucherVatPer,
                    IssueVoucherVatRoundSign = issueVoucher.issueVoucher.IssueVoucherVatRoundSign,
                    IssueVoucherVatRountAmt = issueVoucher.issueVoucher.IssueVoucherVatRountAmt,
                    IssueVoucherNetDisAmount = issueVoucher.issueVoucher.IssueVoucherNetDisAmount,
                    IssueVoucherNetAmount = issueVoucher.issueVoucher.IssueVoucherNetAmount,
                    IssueVoucherBufferRemark12 = issueVoucher.issueVoucher.IssueVoucherBufferRemark12,
                    IssueVoucherBufferRemark13 = issueVoucher.issueVoucher.IssueVoucherBufferRemark13,
                    IssueVoucherBufferPurNo = issueVoucher.issueVoucher.IssueVoucherBufferPurNo,
                    IssueVoucherBufferReqNo = issueVoucher.issueVoucher.IssueVoucherBufferReqNo,
                    IssueVoucherDelStatus = issueVoucher.issueVoucher.IssueVoucherDelStatus,


                };
                issueVoucherViewModel.IssueVoucherDetails = _mapper.Map<List<IssueVoucherDetailsViewModel>>(issueVoucher.issueVoucherDetails);
                issueVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(issueVoucher.accountsTransactions);
                ApiResponse<IssueVoucherViewModel> apiResponse = new ApiResponse<IssueVoucherViewModel>
                {
                    Valid = true,
                    Result = issueVoucherViewModel,
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
                Message = IssueVoucherMessage.VoucherNumberExist



            };
            var x = _issueVoucherService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }

        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();
            var itemMaster = itemrepository.GetAsQueryable().AsNoTracking().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                     && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
                     {
                         k.ItemMasterItemId,
                         k.ItemMasterItemName
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
            var unitMasters = unitrepository.GetAsQueryable().AsNoTracking().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                x.UnitMasterUnitFullName,
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim()
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

            var DepartmentsList = departmentrepository.GetAsQueryable().AsNoTracking().Where(a => a.DepartmentMasterDepartmentDelStatus != true).Select(k => new
            {
                k.DepartmentMasterDepartmentId,
                k.DepartmentMasterDepartmentName,

            }).ToList();
            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true).Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();

            var issueVoucher = _issueVoucherRepository.GetAsQueryable().AsNoTracking().Where(a => a.IssueVoucherDelStatus != true).Select(x => new
            {
                x.IssueVoucherId,
                x.IssueVoucherNo
            }).ToList();
            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                jobMasters = jobMasters,
                currencyMasters = currencyMasters,
                costcenterMasters = costcenterMasters,
                LocationMaster = LocationMaster,
                DepartmentsList = DepartmentsList,
                unitMasters = unitMasters,
                masterAccountsTables = masterAccountsTables,
                issueVoucher= issueVoucher
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }

    }
}