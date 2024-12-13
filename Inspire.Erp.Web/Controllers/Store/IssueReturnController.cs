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




namespace Inspire.Erp.Web.Controllers.Store

{
    [Route("api/IssueReturn")]
    [Produces("application/json")]
    [ApiController]
    public class IssueReturnController : ControllerBase
    {
        private IIssueReturnService _issueReturnService;
        private readonly IMapper _mapper;
        public IssueReturnController(IIssueReturnService issueReturnService, IMapper mapper)
        {
            _issueReturnService = issueReturnService;
            _mapper = mapper;
        }


        //[HttpGet]
        //[Route("IssueReturn_GetReportIssueReturn")]
        //public ApiResponse<List<ReportIssueReturn>> IssueReturn_GetReportIssueReturn()
        //{



        //    ApiResponse<List<ReportIssueReturn>> apiResponse = new ApiResponse<List<ReportIssueReturn>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<List<ReportIssueReturn>>(_issueReturnService.IssueReturn_GetReportIssueReturn()),
        //        Message = ""
        //    };
        //    return apiResponse;




        //}





        [HttpPost]
        [Route("InsertIssueReturn")]
        public ApiResponse<IssueReturnViewModel> InsertIssueReturn([FromBody] IssueReturnViewModel voucherCompositeView)
        {
           

            ApiResponse<IssueReturnViewModel> apiResponse = new ApiResponse<IssueReturnViewModel>();
            if (_issueReturnService.GetVouchersNumbers(voucherCompositeView.IssueReturnNo) == null)
            {
                var param1 = _mapper.Map<IssueReturn>(voucherCompositeView);
                var param2 = _mapper.Map<List<IssueReturnDetails>>(voucherCompositeView.IssueReturnDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                //List<StockRegister> param4 = new List<StockRegister>();
                //var xs = _issueReturnService.InsertIssueReturn(param1, param3, param2
                //    , param4
                //    );

                //==============
                param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                clsAccountAndStock.IssueReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

                var xs = _issueReturnService.InsertIssueReturn(param1, param3, param2
               , param4
               );
                //========================


                IssueReturnViewModel issueReturnViewModel = new IssueReturnViewModel
                {





                    IssueReturnId = xs.issueReturn.IssueReturnId,
                    IssueReturnNo = xs.issueReturn.IssueReturnNo,
                    IssueReturnDate = xs.issueReturn.IssueReturnDate,
                    IssueReturnCreditAccNo = xs.issueReturn.IssueReturnCreditAccNo,
                    IssueReturnDepartmentId = xs.issueReturn.IssueReturnDepartmentId,
                    IssueReturnBufferRemark1 = xs.issueReturn.IssueReturnBufferRemark1,
                    IssueReturnCostCenterId = xs.issueReturn.IssueReturnCostCenterId,
                    IssueReturnDebitAccNo = xs.issueReturn.IssueReturnDebitAccNo,
                    IssueReturnIvNoForRet = xs.issueReturn.IssueReturnIvNoForRet,
                    IssueReturnRefNo = xs.issueReturn.IssueReturnRefNo,
                    IssueReturnDescription = xs.issueReturn.IssueReturnDescription,
                    IssueReturnGrno = xs.issueReturn.IssueReturnGrno,
                    IssueReturnGrdate = xs.issueReturn.IssueReturnGrdate,
                    IssueReturnLpono = xs.issueReturn.IssueReturnLpono,
                    IssueReturnLpodate = xs.issueReturn.IssueReturnLpodate,
                    IssueReturnQtnNo = xs.issueReturn.IssueReturnQtnNo,
                    IssueReturnQtnDate = xs.issueReturn.IssueReturnQtnDate,
                    IssueReturnIvDateForRet = xs.issueReturn.IssueReturnIvDateForRet,
                    IssueReturnReqNo = xs.issueReturn.IssueReturnReqNo,
                    IssueReturnReqDate = xs.issueReturn.IssueReturnReqDate,
                    IssueReturnDayBookNo = xs.issueReturn.IssueReturnDayBookNo,
                    IssueReturnLocationId = xs.issueReturn.IssueReturnLocationId,
                    IssueReturnUserId = xs.issueReturn.IssueReturnUserId,
                    IssueReturnCurrencyId = xs.issueReturn.IssueReturnCurrencyId,
                    IssueReturnCompanyId = xs.issueReturn.IssueReturnCompanyId,
                    IssueReturnJobId = xs.issueReturn.IssueReturnJobId,
                    IssueReturnFsno = xs.issueReturn.IssueReturnFsno,
                    IssueReturnFcRate = xs.issueReturn.IssueReturnFcRate,
                    IssueReturnStatus = xs.issueReturn.IssueReturnStatus,
                    IssueReturnTotalGrossAmount = xs.issueReturn.IssueReturnTotalGrossAmount,
                    IssueReturnTotalItemDisAmount = xs.issueReturn.IssueReturnTotalItemDisAmount,
                    IssueReturnTotalActualAmount = xs.issueReturn.IssueReturnTotalActualAmount,
                    IssueReturnTotalDisPer = xs.issueReturn.IssueReturnTotalDisPer,
                    IssueReturnTotalDisAmount = xs.issueReturn.IssueReturnTotalDisAmount,
                    IssueReturnVatAmt = xs.issueReturn.IssueReturnVatAmt,
                    IssueReturnVatPer = xs.issueReturn.IssueReturnVatPer,
                    IssueReturnVatRoundSign = xs.issueReturn.IssueReturnVatRoundSign,
                    IssueReturnVatRountAmt = xs.issueReturn.IssueReturnVatRountAmt,
                    IssueReturnNetDisAmount = xs.issueReturn.IssueReturnNetDisAmount,
                    IssueReturnNetAmount = xs.issueReturn.IssueReturnNetAmount,
                    IssueReturnBufferRemark12 = xs.issueReturn.IssueReturnBufferRemark12,
                    IssueReturnBufferRemark13 = xs.issueReturn.IssueReturnBufferRemark13,
                    IssueReturnBufferPurNo = xs.issueReturn.IssueReturnBufferPurNo,
                    IssueReturnBufferReqNo = xs.issueReturn.IssueReturnBufferReqNo,
                    IssueReturnDelStatus = xs.issueReturn.IssueReturnDelStatus,





                };

                issueReturnViewModel.IssueReturnDetails = _mapper.Map<List<IssueReturnDetailsViewModel>>(xs.issueReturnDetails);
                issueReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<IssueReturnViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<IssueReturnViewModel>(issueReturnViewModel),
                    Message = IssueReturnMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<IssueReturnViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = IssueReturnMessage.VoucherAlreadyExist
                };

            }


            return apiResponse;






        }

        [HttpPost]
        [Route("UpdateIssueReturn")]
        public ApiResponse<IssueReturnViewModel> UpdateIssueReturn([FromBody] IssueReturnViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<IssueReturn>(voucherCompositeView);
            var param2 = _mapper.Map<List<IssueReturnDetails>>(voucherCompositeView.IssueReturnDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //List<StockRegister> param4 = new List<StockRegister>();
            //var xs = _issueReturnService.UpdateIssueReturn(param1, param3, param2
            //    , param4
            //    );





            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            clsAccountAndStock.IssueReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _issueReturnService.UpdateIssueReturn(param1, param3, param2
           , param4
           );
            //========================










            IssueReturnViewModel issueReturnViewModel = new IssueReturnViewModel
            {
                IssueReturnId = xs.issueReturn.IssueReturnId,
                IssueReturnNo = xs.issueReturn.IssueReturnNo,
                IssueReturnDate = xs.issueReturn.IssueReturnDate,
                IssueReturnCreditAccNo = xs.issueReturn.IssueReturnCreditAccNo,
                IssueReturnDepartmentId = xs.issueReturn.IssueReturnDepartmentId,
                IssueReturnBufferRemark1 = xs.issueReturn.IssueReturnBufferRemark1,
                IssueReturnCostCenterId = xs.issueReturn.IssueReturnCostCenterId,
                IssueReturnDebitAccNo = xs.issueReturn.IssueReturnDebitAccNo,
                IssueReturnIvNoForRet = xs.issueReturn.IssueReturnIvNoForRet,
                IssueReturnRefNo = xs.issueReturn.IssueReturnRefNo,
                IssueReturnDescription = xs.issueReturn.IssueReturnDescription,
                IssueReturnGrno = xs.issueReturn.IssueReturnGrno,
                IssueReturnGrdate = xs.issueReturn.IssueReturnGrdate,
                IssueReturnLpono = xs.issueReturn.IssueReturnLpono,
                IssueReturnLpodate = xs.issueReturn.IssueReturnLpodate,
                IssueReturnQtnNo = xs.issueReturn.IssueReturnQtnNo,
                IssueReturnQtnDate = xs.issueReturn.IssueReturnQtnDate,
                IssueReturnIvDateForRet = xs.issueReturn.IssueReturnIvDateForRet,
                IssueReturnReqNo = xs.issueReturn.IssueReturnReqNo,
                IssueReturnReqDate = xs.issueReturn.IssueReturnReqDate,
                IssueReturnDayBookNo = xs.issueReturn.IssueReturnDayBookNo,
                IssueReturnLocationId = xs.issueReturn.IssueReturnLocationId,
                IssueReturnUserId = xs.issueReturn.IssueReturnUserId,
                IssueReturnCurrencyId = xs.issueReturn.IssueReturnCurrencyId,
                IssueReturnCompanyId = xs.issueReturn.IssueReturnCompanyId,
                IssueReturnJobId = xs.issueReturn.IssueReturnJobId,
                IssueReturnFsno = xs.issueReturn.IssueReturnFsno,
                IssueReturnFcRate = xs.issueReturn.IssueReturnFcRate,
                IssueReturnStatus = xs.issueReturn.IssueReturnStatus,
                IssueReturnTotalGrossAmount = xs.issueReturn.IssueReturnTotalGrossAmount,
                IssueReturnTotalItemDisAmount = xs.issueReturn.IssueReturnTotalItemDisAmount,
                IssueReturnTotalActualAmount = xs.issueReturn.IssueReturnTotalActualAmount,
                IssueReturnTotalDisPer = xs.issueReturn.IssueReturnTotalDisPer,
                IssueReturnTotalDisAmount = xs.issueReturn.IssueReturnTotalDisAmount,
                IssueReturnVatAmt = xs.issueReturn.IssueReturnVatAmt,
                IssueReturnVatPer = xs.issueReturn.IssueReturnVatPer,
                IssueReturnVatRoundSign = xs.issueReturn.IssueReturnVatRoundSign,
                IssueReturnVatRountAmt = xs.issueReturn.IssueReturnVatRountAmt,
                IssueReturnNetDisAmount = xs.issueReturn.IssueReturnNetDisAmount,
                IssueReturnNetAmount = xs.issueReturn.IssueReturnNetAmount,
                IssueReturnBufferRemark12 = xs.issueReturn.IssueReturnBufferRemark12,
                IssueReturnBufferRemark13 = xs.issueReturn.IssueReturnBufferRemark13,
                IssueReturnBufferPurNo = xs.issueReturn.IssueReturnBufferPurNo,
                IssueReturnBufferReqNo = xs.issueReturn.IssueReturnBufferReqNo,
                IssueReturnDelStatus = xs.issueReturn.IssueReturnDelStatus,
            };

            issueReturnViewModel.IssueReturnDetails = _mapper.Map<List<IssueReturnDetailsViewModel>>(xs.issueReturnDetails);
            issueReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<IssueReturnViewModel> apiResponse = new ApiResponse<IssueReturnViewModel>
            {
                Valid = true,
                Result = _mapper.Map<IssueReturnViewModel>(issueReturnViewModel),
                Message = IssueReturnMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteIssueReturn")]
        public ApiResponse<int> DeleteIssueReturn([FromBody] IssueReturnViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<IssueReturn>(voucherCompositeView);
            var param2 = _mapper.Map<List<IssueReturnDetails>>(voucherCompositeView.IssueReturnDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //List<StockRegister> param4 = new List<StockRegister>();
            //var xs = _issueReturnService.DeleteIssueReturn(  param1,    param3, param2
            //    , param4
            //    );

            //==============
            param3 = new List<AccountsTransactions>();
            List<StockRegister> param4 = new List<StockRegister>();
            //clsAccountAndStock.IssueReturn_Accounts_STOCK_Transactions("", "", param1, param2, ref param4, ref param3);

            var xs = _issueReturnService.DeleteIssueReturn(param1, param3, param2
           , param4
           );
            //========================

            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = IssueReturnMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_issueReturnService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetIssueReturn")]
        public ApiResponse<List<IssueReturn>> GetAllIssueReturn()
        {


            ApiResponse<List<IssueReturn>> apiResponse = new ApiResponse<List<IssueReturn>>
            {
                Valid = true,
                Result = _mapper.Map<List<IssueReturn>>(_issueReturnService.GetIssueReturn()),
                Message = ""
            };
            return apiResponse;


        }

        [HttpGet]
        [Route("GetSavedIssueReturnDetails/{id}")]
        public ApiResponse<IssueReturnViewModel> GetSavedIssueReturnDetails(string id)
        {
            IssueReturnModel issueReturn = _issueReturnService.GetSavedIssueReturnDetails(id);

            if (issueReturn != null)
            {
                IssueReturnViewModel issueReturnViewModel = new IssueReturnViewModel
                {


                    IssueReturnId = issueReturn.issueReturn.IssueReturnId,
                    IssueReturnNo = issueReturn.issueReturn.IssueReturnNo,
                    IssueReturnDate = issueReturn.issueReturn.IssueReturnDate,
                    IssueReturnCreditAccNo = issueReturn.issueReturn.IssueReturnCreditAccNo,
                    IssueReturnDepartmentId = issueReturn.issueReturn.IssueReturnDepartmentId,
                    IssueReturnBufferRemark1 = issueReturn.issueReturn.IssueReturnBufferRemark1,
                    IssueReturnCostCenterId = issueReturn.issueReturn.IssueReturnCostCenterId,
                    IssueReturnDebitAccNo = issueReturn.issueReturn.IssueReturnDebitAccNo,
                    IssueReturnIvNoForRet = issueReturn.issueReturn.IssueReturnIvNoForRet,
                    IssueReturnRefNo = issueReturn.issueReturn.IssueReturnRefNo,
                    IssueReturnDescription = issueReturn.issueReturn.IssueReturnDescription,
                    IssueReturnGrno = issueReturn.issueReturn.IssueReturnGrno,
                    IssueReturnGrdate = issueReturn.issueReturn.IssueReturnGrdate,
                    IssueReturnLpono = issueReturn.issueReturn.IssueReturnLpono,
                    IssueReturnLpodate = issueReturn.issueReturn.IssueReturnLpodate,
                    IssueReturnQtnNo = issueReturn.issueReturn.IssueReturnQtnNo,
                    IssueReturnQtnDate = issueReturn.issueReturn.IssueReturnQtnDate,
                    IssueReturnIvDateForRet = issueReturn.issueReturn.IssueReturnIvDateForRet,
                    IssueReturnReqNo = issueReturn.issueReturn.IssueReturnReqNo,
                    IssueReturnReqDate = issueReturn.issueReturn.IssueReturnReqDate,
                    IssueReturnDayBookNo = issueReturn.issueReturn.IssueReturnDayBookNo,
                    IssueReturnLocationId = issueReturn.issueReturn.IssueReturnLocationId,
                    IssueReturnUserId = issueReturn.issueReturn.IssueReturnUserId,
                    IssueReturnCurrencyId = issueReturn.issueReturn.IssueReturnCurrencyId,
                    IssueReturnCompanyId = issueReturn.issueReturn.IssueReturnCompanyId,
                    IssueReturnJobId = issueReturn.issueReturn.IssueReturnJobId,
                    IssueReturnFsno = issueReturn.issueReturn.IssueReturnFsno,
                    IssueReturnFcRate = issueReturn.issueReturn.IssueReturnFcRate,
                    IssueReturnStatus = issueReturn.issueReturn.IssueReturnStatus,
                    IssueReturnTotalGrossAmount = issueReturn.issueReturn.IssueReturnTotalGrossAmount,
                    IssueReturnTotalItemDisAmount = issueReturn.issueReturn.IssueReturnTotalItemDisAmount,
                    IssueReturnTotalActualAmount = issueReturn.issueReturn.IssueReturnTotalActualAmount,
                    IssueReturnTotalDisPer = issueReturn.issueReturn.IssueReturnTotalDisPer,
                    IssueReturnTotalDisAmount = issueReturn.issueReturn.IssueReturnTotalDisAmount,
                    IssueReturnVatAmt = issueReturn.issueReturn.IssueReturnVatAmt,
                    IssueReturnVatPer = issueReturn.issueReturn.IssueReturnVatPer,
                    IssueReturnVatRoundSign = issueReturn.issueReturn.IssueReturnVatRoundSign,
                    IssueReturnVatRountAmt = issueReturn.issueReturn.IssueReturnVatRountAmt,
                    IssueReturnNetDisAmount = issueReturn.issueReturn.IssueReturnNetDisAmount,
                    IssueReturnNetAmount = issueReturn.issueReturn.IssueReturnNetAmount,
                    IssueReturnBufferRemark12 = issueReturn.issueReturn.IssueReturnBufferRemark12,
                    IssueReturnBufferRemark13 = issueReturn.issueReturn.IssueReturnBufferRemark13,
                    IssueReturnBufferPurNo = issueReturn.issueReturn.IssueReturnBufferPurNo,
                    IssueReturnBufferReqNo = issueReturn.issueReturn.IssueReturnBufferReqNo,
                    IssueReturnDelStatus = issueReturn.issueReturn.IssueReturnDelStatus,


                };
                issueReturnViewModel.IssueReturnDetails = _mapper.Map<List<IssueReturnDetailsViewModel>>(issueReturn.issueReturnDetails);
                issueReturnViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(issueReturn.accountsTransactions);
                ApiResponse<IssueReturnViewModel> apiResponse = new ApiResponse<IssueReturnViewModel>
                {
                    Valid = true,
                    Result = issueReturnViewModel,
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
                Message = IssueReturnMessage.VoucherNumberExist



            };
            var x = _issueReturnService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}