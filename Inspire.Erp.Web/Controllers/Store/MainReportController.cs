//using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;


using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.ViewModels.Store;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;

namespace Inspire.Erp.Web.Controllers.Store
{

    [Route("api/MainReport")]
    [Produces("application/json")]
    [ApiController]
    public class MainReportController : ControllerBase
    {
        private IMainReportService _mainReportService;

        private readonly IMapper _mapper;
        private IRepository<CostCenterMaster> costcenterrepository;
        private IRepository<JobMaster> jobrepository; private IRepository<LocationMaster> locationrepository; private IRepository<CurrencyMaster> currencyrepository;
        private IChartofAccountsService chartofAccountsService; private IRepository<ViewAccountsTransactionsVoucherType> _viewAccountsTransactionsVoucherTypeRepository;
        public MainReportController(IMainReportService mainReportService, IMapper mapper, IRepository<CostCenterMaster> _countryrepository
            , IRepository<JobMaster> _jobrepository, IRepository<LocationMaster> _locationrepository, IChartofAccountsService _chartofAccountsService,
            IRepository<ViewAccountsTransactionsVoucherType> viewAccountsTransactionsVoucherTypeRepository)
        {
            _mainReportService = mainReportService;
            _mapper = mapper; _viewAccountsTransactionsVoucherTypeRepository = viewAccountsTransactionsVoucherTypeRepository;
            costcenterrepository = _countryrepository;
            jobrepository = _jobrepository; locationrepository = _locationrepository;
            chartofAccountsService = _chartofAccountsService;
        }



        [HttpGet]
        //[Route("MainReport_GetReportAccountsTransactions/{AccNo}/{Location}/{Job}/{CostCenter}/{FromDate}/{ToDate}/{HideOpeningBal}/{Narration}/{Description}/{TypeList}")]
        [Route("MainReport_GetReportAccountsTransactions/{AccNo}/{Location}/{Job}/{CostCenter}")]
        //public ApiResponse<List<ReportAccountsTransactions>> MainReport_GetReportAccountsTransactions(string AccNo, int Location, int Job, int CostCenter, DateTime FromDate, DateTime ToDate, Boolean HideOpeningBal, string Narration, string Description,string TypeList)
        public ApiResponse<List<ReportAccountsTransactions>> MainReport_GetReportAccountsTransactions(string AccNo, int Location, int Job, int CostCenter)
        {



            ApiResponse<List<ReportAccountsTransactions>> apiResponse = new ApiResponse<List<ReportAccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<ReportAccountsTransactions>>(_mainReportService.MainReport_GetReportAccountsTransactions(
                     //AccNo, Location,  Job,  CostCenter,  FromDate,ToDate,  HideOpeningBal,  Narration,  Description, TypeList
                     AccNo, Location, Job, CostCenter
                )),
                Message = ""
            };
            return apiResponse;

        }

        [HttpPost]
        [Route("MainReport_GetReportAccountsTransactions_PARAM_CLASS")]
        public ApiResponse<List<ReportAccountsTransactions>> MainReport_GetReportAccountsTransactions_PARAM_CLASS([FromBody] AccountStatementParametersViewModels voucherCompositeView)
        {
            var param1 = _mapper.Map<AccountStatementParameters>(voucherCompositeView);
            //var param2 = _mapper.Map<List<ViewAccountsTransactionsVoucherType>>(voucherCompositeView.ViewAccountsTransactionsVoucherType);

            ApiResponse<List<ReportAccountsTransactions>> apiResponse = new ApiResponse<List<ReportAccountsTransactions>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportAccountsTransactions>>(_mainReportService.MainReport_GetReportAccountsTransactions_PARAM_CLASS(
                     param1
                //, param2 
                )),
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("MainReport_GetReportStockRegister")]
        public ApiResponse<List<ReportStockRegister>> MainReport_GetReportStockRegister()
        {
            ApiResponse<List<ReportStockRegister>> apiResponse = new ApiResponse<List<ReportStockRegister>>
            {
                Valid = true,
                Result = _mapper.Map<List<ReportStockRegister>>(_mainReportService.MainReport_GetReportStockRegister()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("MainReport_GetReportStockLedger_PARAM_CLASS")]
        public ApiResponse<List<ReportStockBaseUnit>> MainReport_GetReportStockLedger_PARAM_CLASS([FromBody] StockLedgerParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<StockLedgerParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);
            ApiResponse<List<ReportStockBaseUnit>> apiResponse = new ApiResponse<List<ReportStockBaseUnit>>
            {
                Valid = true,
                Result = _mapper.Map<List<ReportStockBaseUnit>>(_mainReportService.MainReport_GetReportStockLedger_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("MainReport_GetReportStockLedger_Location_PARAM_CLASS")]
        public ApiResponse<List<ReportStockBaseUnit>> MainReport_GetReportStockLedger_Location_PARAM_CLASS([FromBody] StockLedgerParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<StockLedgerParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);

            ApiResponse<List<ReportStockBaseUnit>> apiResponse = new ApiResponse<List<ReportStockBaseUnit>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportStockBaseUnit>>(_mainReportService.MainReport_GetReportStockLedger_Location_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("MainReport_GetReportSalesVoucher_PARAM_CLASS")]
        public ApiResponse<List<ReportSalesVoucher>> MainReport_GetReportSalesVoucher_PARAM_CLASS([FromBody] SalesvoucherreportParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<SalesvoucherreportParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);

            ApiResponse<List<ReportSalesVoucher>> apiResponse = new ApiResponse<List<ReportSalesVoucher>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportSalesVoucher>>(_mainReportService.MainReport_GetReportSalesVoucher_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;

        }



        [HttpPost]
        [Route("MainReport_GetReportPurchaseVoucher_PARAM_CLASS")]
        public ApiResponse<List<ReportPurchaseVoucher>> MainReport_GetReportPurchaseVoucher_PARAM_CLASS([FromBody] PurchasevoucherreportParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchasevoucherreportParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);

            ApiResponse<List<ReportPurchaseVoucher>> apiResponse = new ApiResponse<List<ReportPurchaseVoucher>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportPurchaseVoucher>>(_mainReportService.MainReport_GetReportPurchaseVoucher_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("MainReport_GetReportPurchaseOrder_PARAM_CLASS")]
        public ApiResponse<List<ReportPurchaseOrder>> MainReport_GetReportPurchaseOrder_PARAM_CLASS([FromBody] PurchaseOrderReportParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseOrderReportParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);

            ApiResponse<List<ReportPurchaseOrder>> apiResponse = new ApiResponse<List<ReportPurchaseOrder>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportPurchaseOrder>>(_mainReportService.MainReport_GetReportPurchaseOrder_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;

        }



        [HttpPost]
        [Route("MainReport_GetReportPurchaseRequisition_PARAM_CLASS")]
        public ApiResponse<List<ReportPurchaseRequisition>> MainReport_GetReportPurchaseRequisition_PARAM_CLASS([FromBody] PurchaserequisitionReportParametersViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaserequisitionReportParameters>(voucherCompositeView);
            var param2 = _mapper.Map<List<ViewStockTransferType>>(voucherCompositeView.viewStockTransferType);

            ApiResponse<List<ReportPurchaseRequisition>> apiResponse = new ApiResponse<List<ReportPurchaseRequisition>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportPurchaseRequisition>>(_mainReportService.MainReport_GetReportPurchaseRequisition_PARAM_CLASS(
                     param1, param2
                )),
                Message = ""
            };
            return apiResponse;

        }


        [HttpGet]
        [Route("MainReport_GetAllItemMaster")]
        public List<ItemMaster> MainReport_GetAllItemMaster()
        {
            return _mapper.Map<List<ItemMaster>>(_mainReportService.MainReport_GetAllItemMaster());


        }

        [HttpGet]
        [Route("MainReport_GetAllItemMaster_Group")]
        public List<ItemMaster> MainReport_GetAllItemMaster_Group()
        {
            return _mapper.Map<List<ItemMaster>>(_mainReportService.MainReport_GetAllItemMaster_Group());

        }


        [HttpGet]
        [Route("MainReport_GetAccountsTransactions_VoucherType")]
        public List<ViewAccountsTransactionsVoucherType> MainReport_GetAccountsTransactions_VoucherType()
        {
            return _mapper.Map<List<ViewAccountsTransactionsVoucherType>>(_mainReportService.MainReport_GetAccountsTransactions_VoucherType());

        }


        [HttpGet]
        [Route("AccountsTransactionsLoadDropdown")]
        public ResponseInfo AccountsTransactionsLoadDropdown()
        {
            var objectresponse = new ResponseInfo();

            var costcenterMasters = costcenterrepository.GetAsQueryable().Where(a => a.CostCenterMasterCostCenterDelStatus != true).Select(c => new
            {
                c.CostCenterMasterCostCenterId,
                c.CostCenterMasterCostCenterName,
            }).ToList();
            var jobMasters = jobrepository.GetAsQueryable().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                c.JobMasterJobName,
            }).ToList();
            var locationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName,
            }).ToList();

            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaAccType == "A" && a.MaDelStatus != true).Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();

            var vouchers = _viewAccountsTransactionsVoucherTypeRepository.GetAsQueryable().ToList();
            objectresponse.ResultSet = new
            {
                costcenterMasters = costcenterMasters,
                masterAccountsTables = masterAccountsTables,
                jobMasters = jobMasters,
                locationMaster = locationMaster,
                vouchers = vouchers
            };
            return objectresponse;
        }
    }
}








