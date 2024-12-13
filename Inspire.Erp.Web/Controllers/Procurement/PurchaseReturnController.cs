using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Application.Procurement.Interfaces;


using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.MODULE;
using Inspire.Erp.Application.Procurement.Interface;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/PurchaseReturn")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseReturnController : ControllerBase
    {
        private IPurchaseReturnService _purchaseReturnService;
        private readonly IMapper _mapper;
        public PurchaseReturnController(IPurchaseReturnService purchaseReturnService, IMapper mapper)
        {
            _purchaseReturnService = purchaseReturnService;
            _mapper = mapper;
        }

        ////[HttpGet]
        ////[Route("PurchaseReturn_GetReportPurchaseReturn")]

        ////public List<ReportPurchaseReturn> PurchaseReturn_GetReportPurchaseReturn()
        ////{
        ////    return _mapper.Map<List<ReportPurchaseReturn>>(_purchaseReturnService.PurchaseReturn_GetReportPurchaseReturn());


        ////}



        //     [HttpGet]
        //     [Route("PurchaseReturn_GetReportPurchaseReturn")]
        //     public ApiResponse<List<ReportPurchaseReturn>> PurchaseReturn_GetReportPurchaseReturn()
        //     {



        //         ApiResponse<List<ReportPurchaseReturn>> apiResponse = new ApiResponse<List<ReportPurchaseReturn>>
        //         {
        //             Valid = true,
        //             Result = _mapper.Map<List<ReportPurchaseReturn>>(_purchaseReturnService.PurchaseReturn_GetReportPurchaseReturn()),
        //             Message = ""
        //         };
        //         return apiResponse;


        //     }


        //     [HttpPost]
        //     [Route("InsertPurchaseReturn")]
        //     public ApiResponse<PurchaseReturnViewModel> InsertPurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        //     {


        //         ApiResponse<PurchaseReturnViewModel> apiResponse = new ApiResponse<PurchaseReturnViewModel>();
        //         if (_purchaseReturnService.GetVouchersNumbers(voucherCompositeView.PurchaseReturnNo) == null)
        //         {
        //             var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
        //             var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
        //             var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
        //             //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //             //var xs = _purchaseReturnService.InsertPurchaseReturn(param1, param3, param2
        //             //    //, param4
        //             //    );


        //             //==============
        //             param3 = new List<AccountsTransactions>();
        //             List<StockRegister> param4 = new List<StockRegister>();
        //             clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

        //             var xs = _purchaseReturnService.InsertPurchaseReturn(param1, param3, param2
        //            , param4
        //            );
        //             //========================


        //             PurchaseReturnViewModel purchaseReturnViewModel = new PurchaseReturnViewModel
        //             {


        //                 PurchaseReturnId = xs.purchaseReturn.PurchaseReturnId,
        //                 PurchaseReturnNo = xs.purchaseReturn.PurchaseReturnNo,
        //                 PurchaseReturnDate = xs.purchaseReturn.PurchaseReturnDate,
        //                 PurchaseReturnType = xs.purchaseReturn.PurchaseReturnType,
        //                 PurchaseReturnPartyId = xs.purchaseReturn.PurchaseReturnPartyId,
        //                 PurchaseReturnPartyName = xs.purchaseReturn.PurchaseReturnPartyName,
        //                 PurchaseReturnPartyAddress = xs.purchaseReturn.PurchaseReturnPartyAddress,
        //                 PurchaseReturnPartyVatNo = xs.purchaseReturn.PurchaseReturnPartyVatNo,
        //                 PurchaseReturnSupInvNo = xs.purchaseReturn.PurchaseReturnSupInvNo,
        //                 PurchaseReturnRefNo = xs.purchaseReturn.PurchaseReturnRefNo,
        //                 PurchaseReturnDescription = xs.purchaseReturn.PurchaseReturnDescription,
        //                 PurchaseReturnGrno = xs.purchaseReturn.PurchaseReturnGrno,
        //                 PurchaseReturnGrdate = xs.purchaseReturn.PurchaseReturnGrdate,
        //                 PurchaseReturnLpono = xs.purchaseReturn.PurchaseReturnLpono,
        //                 PurchaseReturnLpodate = xs.purchaseReturn.PurchaseReturnLpodate,
        //                 PurchaseReturnQtnNo = xs.purchaseReturn.PurchaseReturnQtnNo,
        //                 PurchaseReturnQtnDate = xs.purchaseReturn.PurchaseReturnQtnDate,
        //                 PurchaseReturnExcludeVat = xs.purchaseReturn.PurchaseReturnExcludeVat,
        //                 PurchaseReturnPono = xs.purchaseReturn.PurchaseReturnPono,
        //                 PurchaseReturnBatchCode = xs.purchaseReturn.PurchaseReturnBatchCode,
        //                 PurchaseReturnDayBookNo = xs.purchaseReturn.PurchaseReturnDayBookNo,
        //                 PurchaseReturnLocationId = xs.purchaseReturn.PurchaseReturnLocationId,
        //                 PurchaseReturnUserId = xs.purchaseReturn.PurchaseReturnUserId,
        //                 PurchaseReturnCurrencyId = xs.purchaseReturn.PurchaseReturnCurrencyId,
        //                 PurchaseReturnCompanyId = xs.purchaseReturn.PurchaseReturnCompanyId,
        //                 PurchaseReturnJobId = xs.purchaseReturn.PurchaseReturnJobId,
        //                 PurchaseReturnFsno = xs.purchaseReturn.PurchaseReturnFsno,
        //                 PurchaseReturnFcRate = xs.purchaseReturn.PurchaseReturnFcRate,
        //                 PurchaseReturnStatus = xs.purchaseReturn.PurchaseReturnStatus,
        //                 PurchaseReturnTotalGrossAmount = xs.purchaseReturn.PurchaseReturnTotalGrossAmount,
        //                 PurchaseReturnTotalItemDisAmount = xs.purchaseReturn.PurchaseReturnTotalItemDisAmount,
        //                 PurchaseReturnTotalActualAmount = xs.purchaseReturn.PurchaseReturnTotalActualAmount,
        //                 PurchaseReturnTotalDisPer = xs.purchaseReturn.PurchaseReturnTotalDisPer,
        //                 PurchaseReturnTotalDisAmount = xs.purchaseReturn.PurchaseReturnTotalDisAmount,
        //                 PurchaseReturnVatAmt = xs.purchaseReturn.PurchaseReturnVatAmt,
        //                 PurchaseReturnVatPer = xs.purchaseReturn.PurchaseReturnVatPer,
        //                 PurchaseReturnVatRoundSign = xs.purchaseReturn.PurchaseReturnVatRoundSign,
        //                 PurchaseReturnVatRountAmt = xs.purchaseReturn.PurchaseReturnVatRountAmt,
        //                 PurchaseReturnNetDisAmount = xs.purchaseReturn.PurchaseReturnNetDisAmount,
        //                 PurchaseReturnNetAmount = xs.purchaseReturn.PurchaseReturnNetAmount,
        //                 PurchaseReturnTransportCost = xs.purchaseReturn.PurchaseReturnTransportCost,
        //                 PurchaseReturnHandlingcharges = xs.purchaseReturn.PurchaseReturnHandlingcharges,
        //                 PurchaseReturnIssueId = xs.purchaseReturn.PurchaseReturnIssueId,
        //                 PurchaseReturnJobDirectPur = xs.purchaseReturn.PurchaseReturnJobDirectPur,
        //                 PurchaseReturnDelStatus = xs.purchaseReturn.PurchaseReturnDelStatus,

        //             };

        //             purchaseReturnViewModel.PurchaseReturnDetails = _mapper.Map<List<PurchaseReturnDetailsViewModel>>(xs.purchaseReturnDetails);
        //             purchaseReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
        //             apiResponse = new ApiResponse<PurchaseReturnViewModel>
        //             {
        //                 Valid = true,
        //                 Result = _mapper.Map<PurchaseReturnViewModel>(purchaseReturnViewModel),
        //                 Message = PurchaseReturnMessage.SaveVoucher
        //             };
        //         }
        //         else
        //         {
        //             apiResponse = new ApiResponse<PurchaseReturnViewModel>
        //             {
        //                 Valid = false,
        //                 Error = true,
        //                 Exception = null,
        //                 Message = PurchaseReturnMessage.VoucherAlreadyExist
        //             };

        //         }


        //         return apiResponse;

        //     }

        //     [HttpPost]
        //     [Route("UpdatePurchaseReturn")]
        //     public ApiResponse<PurchaseReturnViewModel> UpdatePurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        //     {
        //         var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
        //         var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
        //         var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

        //         //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //         //var xs = _purchaseReturnService.UpdatePurchaseReturn(param1, param3, param2
        //         // //, param4
        //         // );

        //         //==============
        //         param3 = new List<AccountsTransactions>();
        //         List<StockRegister> param4 = new List<StockRegister>();
        //         clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

        //         var xs = _purchaseReturnService.UpdatePurchaseReturn(param1, param3, param2
        //        , param4
        //        );
        //         //========================


        //PurchaseReturnViewModel purchaseReturnViewModel = new PurchaseReturnViewModel
        //         {
        //             PurchaseReturnId = xs.purchaseReturn.PurchaseReturnId,
        //             PurchaseReturnNo = xs.purchaseReturn.PurchaseReturnNo,
        //             PurchaseReturnDate = xs.purchaseReturn.PurchaseReturnDate,
        //             PurchaseReturnType = xs.purchaseReturn.PurchaseReturnType,
        //             PurchaseReturnPartyId = xs.purchaseReturn.PurchaseReturnPartyId,
        //             PurchaseReturnPartyName = xs.purchaseReturn.PurchaseReturnPartyName,
        //             PurchaseReturnPartyAddress = xs.purchaseReturn.PurchaseReturnPartyAddress,
        //             PurchaseReturnPartyVatNo = xs.purchaseReturn.PurchaseReturnPartyVatNo,
        //             PurchaseReturnSupInvNo = xs.purchaseReturn.PurchaseReturnSupInvNo,
        //             PurchaseReturnRefNo = xs.purchaseReturn.PurchaseReturnRefNo,
        //             PurchaseReturnDescription = xs.purchaseReturn.PurchaseReturnDescription,
        //             PurchaseReturnGrno = xs.purchaseReturn.PurchaseReturnGrno,
        //             PurchaseReturnGrdate = xs.purchaseReturn.PurchaseReturnGrdate,
        //             PurchaseReturnLpono = xs.purchaseReturn.PurchaseReturnLpono,
        //             PurchaseReturnLpodate = xs.purchaseReturn.PurchaseReturnLpodate,
        //             PurchaseReturnQtnNo = xs.purchaseReturn.PurchaseReturnQtnNo,
        //             PurchaseReturnQtnDate = xs.purchaseReturn.PurchaseReturnQtnDate,
        //             PurchaseReturnExcludeVat = xs.purchaseReturn.PurchaseReturnExcludeVat,
        //             PurchaseReturnPono = xs.purchaseReturn.PurchaseReturnPono,
        //             PurchaseReturnBatchCode = xs.purchaseReturn.PurchaseReturnBatchCode,
        //             PurchaseReturnDayBookNo = xs.purchaseReturn.PurchaseReturnDayBookNo,
        //             PurchaseReturnLocationId = xs.purchaseReturn.PurchaseReturnLocationId,
        //             PurchaseReturnUserId = xs.purchaseReturn.PurchaseReturnUserId,
        //             PurchaseReturnCurrencyId = xs.purchaseReturn.PurchaseReturnCurrencyId,
        //             PurchaseReturnCompanyId = xs.purchaseReturn.PurchaseReturnCompanyId,
        //             PurchaseReturnJobId = xs.purchaseReturn.PurchaseReturnJobId,
        //             PurchaseReturnFsno = xs.purchaseReturn.PurchaseReturnFsno,
        //             PurchaseReturnFcRate = xs.purchaseReturn.PurchaseReturnFcRate,
        //             PurchaseReturnStatus = xs.purchaseReturn.PurchaseReturnStatus,
        //             PurchaseReturnTotalGrossAmount = xs.purchaseReturn.PurchaseReturnTotalGrossAmount,
        //             PurchaseReturnTotalItemDisAmount = xs.purchaseReturn.PurchaseReturnTotalItemDisAmount,
        //             PurchaseReturnTotalActualAmount = xs.purchaseReturn.PurchaseReturnTotalActualAmount,
        //             PurchaseReturnTotalDisPer = xs.purchaseReturn.PurchaseReturnTotalDisPer,
        //             PurchaseReturnTotalDisAmount = xs.purchaseReturn.PurchaseReturnTotalDisAmount,
        //             PurchaseReturnVatAmt = xs.purchaseReturn.PurchaseReturnVatAmt,
        //             PurchaseReturnVatPer = xs.purchaseReturn.PurchaseReturnVatPer,
        //             PurchaseReturnVatRoundSign = xs.purchaseReturn.PurchaseReturnVatRoundSign,
        //             PurchaseReturnVatRountAmt = xs.purchaseReturn.PurchaseReturnVatRountAmt,
        //             PurchaseReturnNetDisAmount = xs.purchaseReturn.PurchaseReturnNetDisAmount,
        //             PurchaseReturnNetAmount = xs.purchaseReturn.PurchaseReturnNetAmount,
        //             PurchaseReturnTransportCost = xs.purchaseReturn.PurchaseReturnTransportCost,
        //             PurchaseReturnHandlingcharges = xs.purchaseReturn.PurchaseReturnHandlingcharges,
        //             PurchaseReturnIssueId = xs.purchaseReturn.PurchaseReturnIssueId,
        //             PurchaseReturnJobDirectPur = xs.purchaseReturn.PurchaseReturnJobDirectPur,
        //             PurchaseReturnDelStatus = xs.purchaseReturn.PurchaseReturnDelStatus,
        //         };

        //         purchaseReturnViewModel.PurchaseReturnDetails = _mapper.Map<List<PurchaseReturnDetailsViewModel>>(xs.purchaseReturnDetails);
        //         purchaseReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

        //         ApiResponse<PurchaseReturnViewModel> apiResponse = new ApiResponse<PurchaseReturnViewModel>
        //         {
        //             Valid = true,
        //             Result = _mapper.Map<PurchaseReturnViewModel>(purchaseReturnViewModel),
        //             Message = PurchaseReturnMessage.UpdateVoucher
        //         };

        //         return apiResponse;

        //     }

        //     [HttpPost]
        //     [Route("DeletePurchaseReturn")]
        //     public ApiResponse<int> DeletePurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        //     {
        //         var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
        //         var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
        //         var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
        //         //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
        //         //var xs = _purchaseReturnService.DeletePurchaseReturn(  param1,    param3, param2
        //         //    //, param4
        //         //    );

        //         //==============
        //         param3 = new List<AccountsTransactions>();
        //         List<StockRegister> param4 = new List<StockRegister>();
        //         //clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

        //         var xs = _purchaseReturnService.DeletePurchaseReturn(param1, param3, param2
        //        , param4
        //        );
        //         //========================


        //         ApiResponse<int> apiResponse = new ApiResponse<int>
        //         {
        //             Valid = true,
        //             Result = 0,
        //             Message = PurchaseReturnMessage.DeleteVoucher
        //         };

        //         return apiResponse;

        //     }



        //     [HttpGet]
        //     [Route("GetAllAccountTransaction")]
        //     public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        //     {



        //         ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
        //         {
        //             Valid = true,
        //             Result = _mapper.Map<List<AccountsTransactions>>(_purchaseReturnService.GetAllTransaction()),
        //             Message = ""
        //         };
        //         return apiResponse;

        //     }

        [HttpGet]
        [Route("GetPurchaseReturn")]
        public ApiResponse<List<PurchaseReturn>> GetAllPurchaseReturn()
        {


            ApiResponse<List<PurchaseReturn>> apiResponse = new ApiResponse<List<PurchaseReturn>>
            {
                Valid = true,
                Result = _mapper.Map<List<PurchaseReturn>>(_purchaseReturnService.GetPurchaseReturn()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetSavedPurchaseReturnDetails/{id}")]
        public ApiResponse<PurchaseReturnViewModel> GetSavedPurchaseReturnDetails(string id)
        {
            PurchaseReturnModel purchaseReturn = _purchaseReturnService.GetSavedPurchaseReturnDetails(id);

            if (purchaseReturn != null)
            {
                PurchaseReturnViewModel purchaseReturnViewModel = new PurchaseReturnViewModel
                {


                    PurchaseReturnId = purchaseReturn.purchaseReturn.PurchaseReturnId,
                    PurchaseReturnNo = purchaseReturn.purchaseReturn.PurchaseReturnNo,
                    PurchaseReturnDate = purchaseReturn.purchaseReturn.PurchaseReturnDate,
                    PurchaseReturnType = purchaseReturn.purchaseReturn.PurchaseReturnType,
                    PurchaseReturnPartyId = purchaseReturn.purchaseReturn.PurchaseReturnPartyId,
                    PurchaseReturnPartyName = purchaseReturn.purchaseReturn.PurchaseReturnPartyName,
                    PurchaseReturnPartyAddress = purchaseReturn.purchaseReturn.PurchaseReturnPartyAddress,
                    PurchaseReturnPartyVatNo = purchaseReturn.purchaseReturn.PurchaseReturnPartyVatNo,
                    PurchaseReturnSupInvNo = purchaseReturn.purchaseReturn.PurchaseReturnSupInvNo,
                    PurchaseReturnRefNo = purchaseReturn.purchaseReturn.PurchaseReturnRefNo,
                    PurchaseReturnDescription = purchaseReturn.purchaseReturn.PurchaseReturnDescription,
                    PurchaseReturnGrno = purchaseReturn.purchaseReturn.PurchaseReturnGrno,
                    PurchaseReturnGrdate = purchaseReturn.purchaseReturn.PurchaseReturnGrdate,
                    PurchaseReturnLpono = purchaseReturn.purchaseReturn.PurchaseReturnLpono,
                    PurchaseReturnLpodate = purchaseReturn.purchaseReturn.PurchaseReturnLpodate,
                    PurchaseReturnQtnNo = purchaseReturn.purchaseReturn.PurchaseReturnQtnNo,
                    PurchaseReturnQtnDate = purchaseReturn.purchaseReturn.PurchaseReturnQtnDate,
                    PurchaseReturnExcludeVat = purchaseReturn.purchaseReturn.PurchaseReturnExcludeVat,
                    PurchaseReturnPono = purchaseReturn.purchaseReturn.PurchaseReturnPono,
                    PurchaseReturnBatchCode = purchaseReturn.purchaseReturn.PurchaseReturnBatchCode,
                    PurchaseReturnDayBookNo = purchaseReturn.purchaseReturn.PurchaseReturnDayBookNo,
                    PurchaseReturnLocationId = purchaseReturn.purchaseReturn.PurchaseReturnLocationId,
                    PurchaseReturnUserId = purchaseReturn.purchaseReturn.PurchaseReturnUserId,
                    PurchaseReturnCurrencyId = purchaseReturn.purchaseReturn.PurchaseReturnCurrencyId,
                    PurchaseReturnCompanyId = purchaseReturn.purchaseReturn.PurchaseReturnCompanyId,
                    PurchaseReturnJobId = purchaseReturn.purchaseReturn.PurchaseReturnJobId,
                    PurchaseReturnFsno = purchaseReturn.purchaseReturn.PurchaseReturnFsno,
                    PurchaseReturnFcRate = purchaseReturn.purchaseReturn.PurchaseReturnFcRate,
                    PurchaseReturnStatus = purchaseReturn.purchaseReturn.PurchaseReturnStatus,
                    PurchaseReturnTotalGrossAmount = purchaseReturn.purchaseReturn.PurchaseReturnTotalGrossAmount,
                    PurchaseReturnTotalItemDisAmount = purchaseReturn.purchaseReturn.PurchaseReturnTotalItemDisAmount,
                    PurchaseReturnTotalActualAmount = purchaseReturn.purchaseReturn.PurchaseReturnTotalActualAmount,
                    PurchaseReturnTotalDisPer = purchaseReturn.purchaseReturn.PurchaseReturnTotalDisPer,
                    PurchaseReturnTotalDisAmount = purchaseReturn.purchaseReturn.PurchaseReturnTotalDisAmount,
                    PurchaseReturnVatAmt = purchaseReturn.purchaseReturn.PurchaseReturnVatAmt,
                    PurchaseReturnVatPer = purchaseReturn.purchaseReturn.PurchaseReturnVatPer,
                    PurchaseReturnVatRoundSign = purchaseReturn.purchaseReturn.PurchaseReturnVatRoundSign,
                    PurchaseReturnVatRountAmt = purchaseReturn.purchaseReturn.PurchaseReturnVatRountAmt,
                    PurchaseReturnNetDisAmount = purchaseReturn.purchaseReturn.PurchaseReturnNetDisAmount,
                    PurchaseReturnNetAmount = purchaseReturn.purchaseReturn.PurchaseReturnNetAmount,
                    PurchaseReturnTransportCost = purchaseReturn.purchaseReturn.PurchaseReturnTransportCost,
                    PurchaseReturnHandlingcharges = purchaseReturn.purchaseReturn.PurchaseReturnHandlingcharges,
                    PurchaseReturnIssueId = purchaseReturn.purchaseReturn.PurchaseReturnIssueId,
                    PurchaseReturnJobDirectPur = purchaseReturn.purchaseReturn.PurchaseReturnJobDirectPur,
                    PurchaseReturnDelStatus = purchaseReturn.purchaseReturn.PurchaseReturnDelStatus,

                };
                purchaseReturnViewModel.PurchaseReturnDetails = _mapper.Map<List<PurchaseReturnDetailsViewModel>>(purchaseReturn.purchaseReturnDetails);
                purchaseReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(purchaseReturn.accountsTransactions);
                ApiResponse<PurchaseReturnViewModel> apiResponse = new ApiResponse<PurchaseReturnViewModel>
                {
                    Valid = true,
                    Result = purchaseReturnViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;



        }


        [HttpPost]
        [Route("InsertPurchaseReturn")]
        public ApiResponse<PurchaseReturnViewModel> InsertPurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        {


            ApiResponse<PurchaseReturnViewModel> apiResponse = new ApiResponse<PurchaseReturnViewModel>();
            if (_purchaseReturnService.GetVouchersNumbers(voucherCompositeView.PurchaseReturnNo) == null)
            {
                var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
                var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
                //var xs = _purchaseReturnService.InsertPurchaseReturn(param1, param3, param2
                //    //, param4
                //    );


                //==============
                //param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
               // var currencyConversion = _purchaseReturnService.CurrencyConversion(param1);
                //var currencyRate = _purchaseReturnService.GetCurrencyRate(param1);


                //clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3, currencyConversion, currencyRate);

                var xs = _purchaseReturnService.InsertPurchaseReturn(param1, param3, param2
               , param4
               );
                //========================


                PurchaseReturnViewModel purchaseReturnViewModel = new PurchaseReturnViewModel
                {


                    PurchaseReturnId = xs.purchaseReturn.PurchaseReturnId,
                    PurchaseReturnNo = xs.purchaseReturn.PurchaseReturnNo,
                    PurchaseReturnDate = xs.purchaseReturn.PurchaseReturnDate,
                    PurchaseReturnType = xs.purchaseReturn.PurchaseReturnType,
                    PurchaseReturnPartyId = xs.purchaseReturn.PurchaseReturnPartyId,
                    PurchaseReturnPartyName = xs.purchaseReturn.PurchaseReturnPartyName,
                    PurchaseReturnPartyAddress = xs.purchaseReturn.PurchaseReturnPartyAddress,
                    PurchaseReturnPartyVatNo = xs.purchaseReturn.PurchaseReturnPartyVatNo,
                    PurchaseReturnSupInvNo = xs.purchaseReturn.PurchaseReturnSupInvNo,
                    PurchaseReturnRefNo = xs.purchaseReturn.PurchaseReturnRefNo,
                    PurchaseReturnDescription = xs.purchaseReturn.PurchaseReturnDescription,
                    PurchaseReturnGrno = xs.purchaseReturn.PurchaseReturnGrno,
                    PurchaseReturnGrdate = xs.purchaseReturn.PurchaseReturnGrdate,
                    PurchaseReturnLpono = xs.purchaseReturn.PurchaseReturnLpono,
                    PurchaseReturnLpodate = xs.purchaseReturn.PurchaseReturnLpodate,
                    PurchaseReturnQtnNo = xs.purchaseReturn.PurchaseReturnQtnNo,
                    PurchaseReturnQtnDate = xs.purchaseReturn.PurchaseReturnQtnDate,
                    PurchaseReturnExcludeVat = xs.purchaseReturn.PurchaseReturnExcludeVat,
                    PurchaseReturnPono = xs.purchaseReturn.PurchaseReturnPono,
                    PurchaseReturnBatchCode = xs.purchaseReturn.PurchaseReturnBatchCode,
                    PurchaseReturnDayBookNo = xs.purchaseReturn.PurchaseReturnDayBookNo,
                    PurchaseReturnLocationId = xs.purchaseReturn.PurchaseReturnLocationId,
                    PurchaseReturnUserId = xs.purchaseReturn.PurchaseReturnUserId,
                    PurchaseReturnCurrencyId = xs.purchaseReturn.PurchaseReturnCurrencyId,
                    PurchaseReturnCompanyId = xs.purchaseReturn.PurchaseReturnCompanyId,
                    PurchaseReturnJobId = xs.purchaseReturn.PurchaseReturnJobId,
                    PurchaseReturnFsno = xs.purchaseReturn.PurchaseReturnFsno,
                    PurchaseReturnFcRate = xs.purchaseReturn.PurchaseReturnFcRate,
                    PurchaseReturnStatus = xs.purchaseReturn.PurchaseReturnStatus,
                    PurchaseReturnTotalGrossAmount = xs.purchaseReturn.PurchaseReturnTotalGrossAmount,
                    PurchaseReturnTotalItemDisAmount = xs.purchaseReturn.PurchaseReturnTotalItemDisAmount,
                    PurchaseReturnTotalActualAmount = xs.purchaseReturn.PurchaseReturnTotalActualAmount,
                    PurchaseReturnTotalDisPer = xs.purchaseReturn.PurchaseReturnTotalDisPer,
                    PurchaseReturnTotalDisAmount = xs.purchaseReturn.PurchaseReturnTotalDisAmount,
                    PurchaseReturnVatAmt = xs.purchaseReturn.PurchaseReturnVatAmt,
                    PurchaseReturnVatPer = xs.purchaseReturn.PurchaseReturnVatPer,
                    PurchaseReturnVatRoundSign = xs.purchaseReturn.PurchaseReturnVatRoundSign,
                    PurchaseReturnVatRountAmt = xs.purchaseReturn.PurchaseReturnVatRountAmt,
                    PurchaseReturnNetDisAmount = xs.purchaseReturn.PurchaseReturnNetDisAmount,
                    PurchaseReturnNetAmount = xs.purchaseReturn.PurchaseReturnNetAmount,
                    PurchaseReturnTransportCost = xs.purchaseReturn.PurchaseReturnTransportCost,
                    PurchaseReturnHandlingcharges = xs.purchaseReturn.PurchaseReturnHandlingcharges,
                    PurchaseReturnIssueId = xs.purchaseReturn.PurchaseReturnIssueId,
                    PurchaseReturnJobDirectPur = xs.purchaseReturn.PurchaseReturnJobDirectPur,
                    PurchaseReturnDelStatus = xs.purchaseReturn.PurchaseReturnDelStatus,

                };

                purchaseReturnViewModel.PurchaseReturnDetails = _mapper.Map<List<PurchaseReturnDetailsViewModel>>(xs.purchaseReturnDetails);
                purchaseReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<PurchaseReturnViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<PurchaseReturnViewModel>(purchaseReturnViewModel),
                    Message = PurchaseReturnMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<PurchaseReturnViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = PurchaseReturnMessage.VoucherAlreadyExist
                };

            }


            return apiResponse;

        }


        [HttpPost]
        [Route("UpdatePurchaseReturn")]
        public ApiResponse<PurchaseReturnViewModel> UpdatePurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _purchaseReturnService.UpdatePurchaseReturn(param1, param3, param2
            // //, param4
            // );

            //==============

            //var currencyConversion = _purchaseReturnService.CurrencyConversion(param1);
            //var currencyRate = _purchaseReturnService.GetCurrencyRate(param1);

          //  param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
           // clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3, currencyConversion, currencyRate);

            var xs = _purchaseReturnService.UpdatePurchaseReturn(param1, param3, param2
           , param4
           );
            //========================


            PurchaseReturnViewModel purchaseReturnViewModel = new PurchaseReturnViewModel
            {
                PurchaseReturnId = xs.purchaseReturn.PurchaseReturnId,
                PurchaseReturnNo = xs.purchaseReturn.PurchaseReturnNo,
                PurchaseReturnDate = xs.purchaseReturn.PurchaseReturnDate,
                PurchaseReturnType = xs.purchaseReturn.PurchaseReturnType,
                PurchaseReturnPartyId = xs.purchaseReturn.PurchaseReturnPartyId,
                PurchaseReturnPartyName = xs.purchaseReturn.PurchaseReturnPartyName,
                PurchaseReturnPartyAddress = xs.purchaseReturn.PurchaseReturnPartyAddress,
                PurchaseReturnPartyVatNo = xs.purchaseReturn.PurchaseReturnPartyVatNo,
                PurchaseReturnSupInvNo = xs.purchaseReturn.PurchaseReturnSupInvNo,
                PurchaseReturnRefNo = xs.purchaseReturn.PurchaseReturnRefNo,
                PurchaseReturnDescription = xs.purchaseReturn.PurchaseReturnDescription,
                PurchaseReturnGrno = xs.purchaseReturn.PurchaseReturnGrno,
                PurchaseReturnGrdate = xs.purchaseReturn.PurchaseReturnGrdate,
                PurchaseReturnLpono = xs.purchaseReturn.PurchaseReturnLpono,
                PurchaseReturnLpodate = xs.purchaseReturn.PurchaseReturnLpodate,
                PurchaseReturnQtnNo = xs.purchaseReturn.PurchaseReturnQtnNo,
                PurchaseReturnQtnDate = xs.purchaseReturn.PurchaseReturnQtnDate,
                PurchaseReturnExcludeVat = xs.purchaseReturn.PurchaseReturnExcludeVat,
                PurchaseReturnPono = xs.purchaseReturn.PurchaseReturnPono,
                PurchaseReturnBatchCode = xs.purchaseReturn.PurchaseReturnBatchCode,
                PurchaseReturnDayBookNo = xs.purchaseReturn.PurchaseReturnDayBookNo,
                PurchaseReturnLocationId = xs.purchaseReturn.PurchaseReturnLocationId,
                PurchaseReturnUserId = xs.purchaseReturn.PurchaseReturnUserId,
                PurchaseReturnCurrencyId = xs.purchaseReturn.PurchaseReturnCurrencyId,
                PurchaseReturnCompanyId = xs.purchaseReturn.PurchaseReturnCompanyId,
                PurchaseReturnJobId = xs.purchaseReturn.PurchaseReturnJobId,
                PurchaseReturnFsno = xs.purchaseReturn.PurchaseReturnFsno,
                PurchaseReturnFcRate = xs.purchaseReturn.PurchaseReturnFcRate,
                PurchaseReturnStatus = xs.purchaseReturn.PurchaseReturnStatus,
                PurchaseReturnTotalGrossAmount = xs.purchaseReturn.PurchaseReturnTotalGrossAmount,
                PurchaseReturnTotalItemDisAmount = xs.purchaseReturn.PurchaseReturnTotalItemDisAmount,
                PurchaseReturnTotalActualAmount = xs.purchaseReturn.PurchaseReturnTotalActualAmount,
                PurchaseReturnTotalDisPer = xs.purchaseReturn.PurchaseReturnTotalDisPer,
                PurchaseReturnTotalDisAmount = xs.purchaseReturn.PurchaseReturnTotalDisAmount,
                PurchaseReturnVatAmt = xs.purchaseReturn.PurchaseReturnVatAmt,
                PurchaseReturnVatPer = xs.purchaseReturn.PurchaseReturnVatPer,
                PurchaseReturnVatRoundSign = xs.purchaseReturn.PurchaseReturnVatRoundSign,
                PurchaseReturnVatRountAmt = xs.purchaseReturn.PurchaseReturnVatRountAmt,
                PurchaseReturnNetDisAmount = xs.purchaseReturn.PurchaseReturnNetDisAmount,
                PurchaseReturnNetAmount = xs.purchaseReturn.PurchaseReturnNetAmount,
                PurchaseReturnTransportCost = xs.purchaseReturn.PurchaseReturnTransportCost,
                PurchaseReturnHandlingcharges = xs.purchaseReturn.PurchaseReturnHandlingcharges,
                PurchaseReturnIssueId = xs.purchaseReturn.PurchaseReturnIssueId,
                PurchaseReturnJobDirectPur = xs.purchaseReturn.PurchaseReturnJobDirectPur,
                PurchaseReturnDelStatus = xs.purchaseReturn.PurchaseReturnDelStatus,
            };

            purchaseReturnViewModel.PurchaseReturnDetails = _mapper.Map<List<PurchaseReturnDetailsViewModel>>(xs.purchaseReturnDetails);
            purchaseReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<PurchaseReturnViewModel> apiResponse = new ApiResponse<PurchaseReturnViewModel>
            {
                Valid = true,
                Result = _mapper.Map<PurchaseReturnViewModel>(purchaseReturnViewModel),
                Message = PurchaseReturnMessage.UpdateVoucher
            };

            return apiResponse;

        }



        [HttpPost]
        [Route("DeletePurchaseReturn")]
        public ApiResponse<int> DeletePurchaseReturn([FromBody] PurchaseReturnViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseReturn>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseReturnDetails>>(voucherCompositeView.PurchaseReturnDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            //var xs = _purchaseReturnService.DeletePurchaseReturn(  param1,    param3, param2
            //    //, param4
            //    );

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            clsAccountAndStock.PurchaseReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3, 0.0M,0.0M);

            var xs = _purchaseReturnService.DeletePurchaseReturn(param1, param3, param2
           , param4
           );
            //========================


            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseReturnMessage.DeleteVoucher
            };

            return apiResponse;

        }


        //     [HttpGet]
        //     [Route("CheckVnoExist/{id}")]
        //     public ApiResponse<bool> CheckVnoExist(string id)
        //     {
        //         ApiResponse<bool> apiResponse = new ApiResponse<bool>
        //         {
        //             Valid = true,
        //             Result = true,
        //             Message = PurchaseReturnMessage.VoucherNumberExist



        //         };
        //         var x = _purchaseReturnService.GetVouchersNumbers(id);
        //         if (x == null)
        //         {
        //             apiResponse.Result = false;
        //             apiResponse.Message = "";
        //         }

        //         return apiResponse;
        //     }



    }
}








