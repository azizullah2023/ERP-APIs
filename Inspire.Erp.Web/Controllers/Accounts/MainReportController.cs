using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Web.Common;
namespace Inspire.Erp.Web.Controllers.Accounts
{

    [Route("api/MainReport")]
    [Produces("application/json")]
    [ApiController]
    public class MainReportController : ControllerBase
    {
        private IMainReportService _mainReportService;

        private readonly IMapper _mapper;
        public MainReportController(IMainReportService mainReportService, IMapper mapper)
        {
            _mainReportService = mainReportService;
            _mapper = mapper;
        }



        [HttpGet]
        [Route("MainReport_GetReportAccountsTransactions/{AccNo}/{Location}/{Job}/{CostCenter}")]
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
            var param2 = _mapper.Map<List<ViewAccountsTransactionsVoucherType>>(voucherCompositeView.ViewAccountsTransactionsVoucherType);

            ApiResponse<List<ReportAccountsTransactions>> apiResponse = new ApiResponse<List<ReportAccountsTransactions>>
            {

                Valid = true,

                Result = _mapper.Map<List<ReportAccountsTransactions>>(_mainReportService.MainReport_GetReportAccountsTransactions_PARAM_CLASS(
                     param1, param2
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



        [HttpGet]
        [Route("MainReport_GetAccountsTransactions_VoucherType")]
        public List<ViewAccountsTransactionsVoucherType> MainReport_GetAccountsTransactions_VoucherType()
        {
            return _mapper.Map<List<ViewAccountsTransactionsVoucherType>>(_mainReportService.MainReport_GetAccountsTransactions_VoucherType());


        }


    }
}