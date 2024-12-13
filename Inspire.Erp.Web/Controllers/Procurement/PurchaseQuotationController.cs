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
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;


using Microsoft.AspNetCore.Mvc.Rendering;




namespace Inspire.Erp.Web.Controllers
{
    [Route("api/PurchaseQuotation")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseQuotationController : ControllerBase
    {
        private IPurchaseQuotationService _purchaseQuotationService;
        private readonly IMapper _mapper;
        public PurchaseQuotationController(IPurchaseQuotationService purchaseQuotationService, IMapper mapper)
        {
            _purchaseQuotationService = purchaseQuotationService;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Route("PurchaseQuotation_GetReportPurchaseQuotation")]

        //public List<ReportPurchaseQuotation> PurchaseQuotation_GetReportPurchaseQuotation()
        //{
        //    return _mapper.Map<List<ReportPurchaseQuotation>>(_purchaseQuotationService.PurchaseQuotation_GetReportPurchaseQuotation());


        //}







        ////[HttpGet]
        ////[Route("PurchaseQuotation_GetReportPurchaseQuotation")]
        ////public ApiResponse<List<ReportPurchaseQuotation>> PurchaseQuotation_GetReportPurchaseQuotation()
        ////{



        ////    ApiResponse<List<ReportPurchaseQuotation>> apiResponse = new ApiResponse<List<ReportPurchaseQuotation>>
        ////    {
        ////        Valid = true,
        ////        Result = _mapper.Map<List<ReportPurchaseQuotation>>(_purchaseQuotationService.PurchaseQuotation_GetReportPurchaseQuotation()),
        ////        Message = ""
        ////    };
        ////    return apiResponse;




        ////}




        [HttpPost]
        [Route("InsertPurchaseQuotation")]
        public ApiResponse<PurchaseQuotationViewModel> InsertPurchaseQuotation([FromBody] PurchaseQuotationViewModel voucherCompositeView)
        {
           

            ApiResponse<PurchaseQuotationViewModel> apiResponse = new ApiResponse<PurchaseQuotationViewModel>();
            if (_purchaseQuotationService.GetVouchersNumbers(voucherCompositeView.PurchaseQuotationNo) == null)
            {
                var param1 = _mapper.Map<PurchaseQuotation>(voucherCompositeView);
                var param2 = _mapper.Map<List<PurchaseQuotationDetails>>(voucherCompositeView.PurchaseQuotationDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
                var xs = _purchaseQuotationService.InsertPurchaseQuotation(param1, param3, param2
                    //, param4
                    );
                PurchaseQuotationViewModel purchaseQuotationViewModel = new PurchaseQuotationViewModel
                {



                    PurchaseQuotationId = xs.purchaseQuotation.PurchaseQuotationId,
                    PurchaseQuotationNo = xs.purchaseQuotation.PurchaseQuotationNo,
                    PurchaseQuotationDate = xs.purchaseQuotation.PurchaseQuotationDate,
                    PurchaseQuotationType = xs.purchaseQuotation.PurchaseQuotationType,
                    PurchaseQuotationPartyId = xs.purchaseQuotation.PurchaseQuotationPartyId,
                    PurchaseQuotationPartyName = xs.purchaseQuotation.PurchaseQuotationPartyName,
                    PurchaseQuotationPartyAddress = xs.purchaseQuotation.PurchaseQuotationPartyAddress,
                    PurchaseQuotationPartyVatNo = xs.purchaseQuotation.PurchaseQuotationPartyVatNo,
                    PurchaseQuotationSupInvNo = xs.purchaseQuotation.PurchaseQuotationSupInvNo,
                    PurchaseQuotationRefNo = xs.purchaseQuotation.PurchaseQuotationRefNo,
                    PurchaseQuotationDescription = xs.purchaseQuotation.PurchaseQuotationDescription,
                    PurchaseQuotationGrno = xs.purchaseQuotation.PurchaseQuotationGrno,
                    PurchaseQuotationGrdate = xs.purchaseQuotation.PurchaseQuotationGrdate,
                    PurchaseQuotationLpono = xs.purchaseQuotation.PurchaseQuotationLpono,
                    PurchaseQuotationLpodate = xs.purchaseQuotation.PurchaseQuotationLpodate,
                    PurchaseQuotationQtnNo = xs.purchaseQuotation.PurchaseQuotationQtnNo,
                    PurchaseQuotationQtnDate = xs.purchaseQuotation.PurchaseQuotationQtnDate,
                    PurchaseQuotationExcludeVat = xs.purchaseQuotation.PurchaseQuotationExcludeVat,
                    PurchaseQuotationPono = xs.purchaseQuotation.PurchaseQuotationPono,
                    PurchaseQuotationBatchCode = xs.purchaseQuotation.PurchaseQuotationBatchCode,
                    PurchaseQuotationDayBookNo = xs.purchaseQuotation.PurchaseQuotationDayBookNo,
                    PurchaseQuotationLocationId = xs.purchaseQuotation.PurchaseQuotationLocationId,
                    PurchaseQuotationUserId = xs.purchaseQuotation.PurchaseQuotationUserId,
                    PurchaseQuotationCurrencyId = xs.purchaseQuotation.PurchaseQuotationCurrencyId,
                    PurchaseQuotationCompanyId = xs.purchaseQuotation.PurchaseQuotationCompanyId,
                    PurchaseQuotationJobId = xs.purchaseQuotation.PurchaseQuotationJobId,
                    PurchaseQuotationFsno = xs.purchaseQuotation.PurchaseQuotationFsno,
                    PurchaseQuotationFcRate = xs.purchaseQuotation.PurchaseQuotationFcRate,
                    PurchaseQuotationStatus = xs.purchaseQuotation.PurchaseQuotationStatus,
                    PurchaseQuotationTotalGrossAmount = xs.purchaseQuotation.PurchaseQuotationTotalGrossAmount,
                    PurchaseQuotationTotalItemDisAmount = xs.purchaseQuotation.PurchaseQuotationTotalItemDisAmount,
                    PurchaseQuotationTotalActualAmount = xs.purchaseQuotation.PurchaseQuotationTotalActualAmount,
                    PurchaseQuotationTotalDisPer = xs.purchaseQuotation.PurchaseQuotationTotalDisPer,
                    PurchaseQuotationTotalDisAmount = xs.purchaseQuotation.PurchaseQuotationTotalDisAmount,
                    PurchaseQuotationVatAmt = xs.purchaseQuotation.PurchaseQuotationVatAmt,
                    PurchaseQuotationVatPer = xs.purchaseQuotation.PurchaseQuotationVatPer,
                    PurchaseQuotationVatRoundSign = xs.purchaseQuotation.PurchaseQuotationVatRoundSign,
                    PurchaseQuotationVatRountAmt = xs.purchaseQuotation.PurchaseQuotationVatRountAmt,
                    PurchaseQuotationNetDisAmount = xs.purchaseQuotation.PurchaseQuotationNetDisAmount,
                    PurchaseQuotationNetAmount = xs.purchaseQuotation.PurchaseQuotationNetAmount,
                    PurchaseQuotationTransportCost = xs.purchaseQuotation.PurchaseQuotationTransportCost,
                    PurchaseQuotationHandlingcharges = xs.purchaseQuotation.PurchaseQuotationHandlingcharges,
                    PurchaseQuotationIssueId = xs.purchaseQuotation.PurchaseQuotationIssueId,
                    PurchaseQuotationJobDirectPur = xs.purchaseQuotation.PurchaseQuotationJobDirectPur,
                    PurchaseQuotationDelStatus = xs.purchaseQuotation.PurchaseQuotationDelStatus,





                };

                purchaseQuotationViewModel.PurchaseQuotationDetails = _mapper.Map<List<PurchaseQuotationDetailsViewModel>>(xs.purchaseQuotationDetails);
                purchaseQuotationViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<PurchaseQuotationViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<PurchaseQuotationViewModel>(purchaseQuotationViewModel),
                    Message = PurchaseQuotationMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<PurchaseQuotationViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = PurchaseQuotationMessage.VoucherAlreadyExist
                };

            }


            return apiResponse;






        }

        [HttpPost]
        [Route("UpdatePurchaseQuotation")]
        public ApiResponse<PurchaseQuotationViewModel> UpdatePurchaseQuotation([FromBody] PurchaseQuotationViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseQuotation>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseQuotationDetails>>(voucherCompositeView.PurchaseQuotationDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            var xs = _purchaseQuotationService.UpdatePurchaseQuotation(param1, param3, param2
                //, param4
                );

            PurchaseQuotationViewModel purchaseQuotationViewModel = new PurchaseQuotationViewModel
            {
                PurchaseQuotationId = xs.purchaseQuotation.PurchaseQuotationId,
                PurchaseQuotationNo = xs.purchaseQuotation.PurchaseQuotationNo,
                PurchaseQuotationDate = xs.purchaseQuotation.PurchaseQuotationDate,
                PurchaseQuotationType = xs.purchaseQuotation.PurchaseQuotationType,
                PurchaseQuotationPartyId = xs.purchaseQuotation.PurchaseQuotationPartyId,
                PurchaseQuotationPartyName = xs.purchaseQuotation.PurchaseQuotationPartyName,
                PurchaseQuotationPartyAddress = xs.purchaseQuotation.PurchaseQuotationPartyAddress,
                PurchaseQuotationPartyVatNo = xs.purchaseQuotation.PurchaseQuotationPartyVatNo,
                PurchaseQuotationSupInvNo = xs.purchaseQuotation.PurchaseQuotationSupInvNo,
                PurchaseQuotationRefNo = xs.purchaseQuotation.PurchaseQuotationRefNo,
                PurchaseQuotationDescription = xs.purchaseQuotation.PurchaseQuotationDescription,
                PurchaseQuotationGrno = xs.purchaseQuotation.PurchaseQuotationGrno,
                PurchaseQuotationGrdate = xs.purchaseQuotation.PurchaseQuotationGrdate,
                PurchaseQuotationLpono = xs.purchaseQuotation.PurchaseQuotationLpono,
                PurchaseQuotationLpodate = xs.purchaseQuotation.PurchaseQuotationLpodate,
                PurchaseQuotationQtnNo = xs.purchaseQuotation.PurchaseQuotationQtnNo,
                PurchaseQuotationQtnDate = xs.purchaseQuotation.PurchaseQuotationQtnDate,
                PurchaseQuotationExcludeVat = xs.purchaseQuotation.PurchaseQuotationExcludeVat,
                PurchaseQuotationPono = xs.purchaseQuotation.PurchaseQuotationPono,
                PurchaseQuotationBatchCode = xs.purchaseQuotation.PurchaseQuotationBatchCode,
                PurchaseQuotationDayBookNo = xs.purchaseQuotation.PurchaseQuotationDayBookNo,
                PurchaseQuotationLocationId = xs.purchaseQuotation.PurchaseQuotationLocationId,
                PurchaseQuotationUserId = xs.purchaseQuotation.PurchaseQuotationUserId,
                PurchaseQuotationCurrencyId = xs.purchaseQuotation.PurchaseQuotationCurrencyId,
                PurchaseQuotationCompanyId = xs.purchaseQuotation.PurchaseQuotationCompanyId,
                PurchaseQuotationJobId = xs.purchaseQuotation.PurchaseQuotationJobId,
                PurchaseQuotationFsno = xs.purchaseQuotation.PurchaseQuotationFsno,
                PurchaseQuotationFcRate = xs.purchaseQuotation.PurchaseQuotationFcRate,
                PurchaseQuotationStatus = xs.purchaseQuotation.PurchaseQuotationStatus,
                PurchaseQuotationTotalGrossAmount = xs.purchaseQuotation.PurchaseQuotationTotalGrossAmount,
                PurchaseQuotationTotalItemDisAmount = xs.purchaseQuotation.PurchaseQuotationTotalItemDisAmount,
                PurchaseQuotationTotalActualAmount = xs.purchaseQuotation.PurchaseQuotationTotalActualAmount,
                PurchaseQuotationTotalDisPer = xs.purchaseQuotation.PurchaseQuotationTotalDisPer,
                PurchaseQuotationTotalDisAmount = xs.purchaseQuotation.PurchaseQuotationTotalDisAmount,
                PurchaseQuotationVatAmt = xs.purchaseQuotation.PurchaseQuotationVatAmt,
                PurchaseQuotationVatPer = xs.purchaseQuotation.PurchaseQuotationVatPer,
                PurchaseQuotationVatRoundSign = xs.purchaseQuotation.PurchaseQuotationVatRoundSign,
                PurchaseQuotationVatRountAmt = xs.purchaseQuotation.PurchaseQuotationVatRountAmt,
                PurchaseQuotationNetDisAmount = xs.purchaseQuotation.PurchaseQuotationNetDisAmount,
                PurchaseQuotationNetAmount = xs.purchaseQuotation.PurchaseQuotationNetAmount,
                PurchaseQuotationTransportCost = xs.purchaseQuotation.PurchaseQuotationTransportCost,
                PurchaseQuotationHandlingcharges = xs.purchaseQuotation.PurchaseQuotationHandlingcharges,
                PurchaseQuotationIssueId = xs.purchaseQuotation.PurchaseQuotationIssueId,
                PurchaseQuotationJobDirectPur = xs.purchaseQuotation.PurchaseQuotationJobDirectPur,
                PurchaseQuotationDelStatus = xs.purchaseQuotation.PurchaseQuotationDelStatus,
            };

            purchaseQuotationViewModel.PurchaseQuotationDetails = _mapper.Map<List<PurchaseQuotationDetailsViewModel>>(xs.purchaseQuotationDetails);
            purchaseQuotationViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<PurchaseQuotationViewModel> apiResponse = new ApiResponse<PurchaseQuotationViewModel>
            {
                Valid = true,
                Result = _mapper.Map<PurchaseQuotationViewModel>(purchaseQuotationViewModel),
                Message = PurchaseQuotationMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeletePurchaseQuotation")]
        public ApiResponse<int> DeletePurchaseQuotation([FromBody] PurchaseQuotationViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseQuotation>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseQuotationDetails>>(voucherCompositeView.PurchaseQuotationDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            var xs = _purchaseQuotationService.DeletePurchaseQuotation(  param1,    param3, param2
                //, param4
                );
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseQuotationMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_purchaseQuotationService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetPurchaseQuotation")]
        public ApiResponse<List<PurchaseQuotation>> GetAllPurchaseQuotation()
        {


            ApiResponse<List<PurchaseQuotation>> apiResponse = new ApiResponse<List<PurchaseQuotation>>
            {
                Valid = true,
                Result = _mapper.Map<List<PurchaseQuotation>>(_purchaseQuotationService.GetPurchaseQuotation()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetSavedPurchaseQuotationDetails/{id}")]
        public ApiResponse<PurchaseQuotationViewModel> GetSavedPurchaseQuotationDetails(string id)
        {
            PurchaseQuotationModel purchaseQuotation = _purchaseQuotationService.GetSavedPurchaseQuotationDetails(id);

            if (purchaseQuotation != null)
            {
                PurchaseQuotationViewModel purchaseQuotationViewModel = new PurchaseQuotationViewModel
                {


                    PurchaseQuotationId = purchaseQuotation.purchaseQuotation.PurchaseQuotationId,
                    PurchaseQuotationNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationNo,
                    PurchaseQuotationDate = purchaseQuotation.purchaseQuotation.PurchaseQuotationDate,
                    PurchaseQuotationType = purchaseQuotation.purchaseQuotation.PurchaseQuotationType,
                    PurchaseQuotationPartyId = purchaseQuotation.purchaseQuotation.PurchaseQuotationPartyId,
                    PurchaseQuotationPartyName = purchaseQuotation.purchaseQuotation.PurchaseQuotationPartyName,
                    PurchaseQuotationPartyAddress = purchaseQuotation.purchaseQuotation.PurchaseQuotationPartyAddress,
                    PurchaseQuotationPartyVatNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationPartyVatNo,
                    PurchaseQuotationSupInvNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationSupInvNo,
                    PurchaseQuotationRefNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationRefNo,
                    PurchaseQuotationDescription = purchaseQuotation.purchaseQuotation.PurchaseQuotationDescription,
                    PurchaseQuotationGrno = purchaseQuotation.purchaseQuotation.PurchaseQuotationGrno,
                    PurchaseQuotationGrdate = purchaseQuotation.purchaseQuotation.PurchaseQuotationGrdate,
                    PurchaseQuotationLpono = purchaseQuotation.purchaseQuotation.PurchaseQuotationLpono,
                    PurchaseQuotationLpodate = purchaseQuotation.purchaseQuotation.PurchaseQuotationLpodate,
                    PurchaseQuotationQtnNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationQtnNo,
                    PurchaseQuotationQtnDate = purchaseQuotation.purchaseQuotation.PurchaseQuotationQtnDate,
                    PurchaseQuotationExcludeVat = purchaseQuotation.purchaseQuotation.PurchaseQuotationExcludeVat,
                    PurchaseQuotationPono = purchaseQuotation.purchaseQuotation.PurchaseQuotationPono,
                    PurchaseQuotationBatchCode = purchaseQuotation.purchaseQuotation.PurchaseQuotationBatchCode,
                    PurchaseQuotationDayBookNo = purchaseQuotation.purchaseQuotation.PurchaseQuotationDayBookNo,
                    PurchaseQuotationLocationId = purchaseQuotation.purchaseQuotation.PurchaseQuotationLocationId,
                    PurchaseQuotationUserId = purchaseQuotation.purchaseQuotation.PurchaseQuotationUserId,
                    PurchaseQuotationCurrencyId = purchaseQuotation.purchaseQuotation.PurchaseQuotationCurrencyId,
                    PurchaseQuotationCompanyId = purchaseQuotation.purchaseQuotation.PurchaseQuotationCompanyId,
                    PurchaseQuotationJobId = purchaseQuotation.purchaseQuotation.PurchaseQuotationJobId,
                    PurchaseQuotationFsno = purchaseQuotation.purchaseQuotation.PurchaseQuotationFsno,
                    PurchaseQuotationFcRate = purchaseQuotation.purchaseQuotation.PurchaseQuotationFcRate,
                    PurchaseQuotationStatus = purchaseQuotation.purchaseQuotation.PurchaseQuotationStatus,
                    PurchaseQuotationTotalGrossAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationTotalGrossAmount,
                    PurchaseQuotationTotalItemDisAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationTotalItemDisAmount,
                    PurchaseQuotationTotalActualAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationTotalActualAmount,
                    PurchaseQuotationTotalDisPer = purchaseQuotation.purchaseQuotation.PurchaseQuotationTotalDisPer,
                    PurchaseQuotationTotalDisAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationTotalDisAmount,
                    PurchaseQuotationVatAmt = purchaseQuotation.purchaseQuotation.PurchaseQuotationVatAmt,
                    PurchaseQuotationVatPer = purchaseQuotation.purchaseQuotation.PurchaseQuotationVatPer,
                    PurchaseQuotationVatRoundSign = purchaseQuotation.purchaseQuotation.PurchaseQuotationVatRoundSign,
                    PurchaseQuotationVatRountAmt = purchaseQuotation.purchaseQuotation.PurchaseQuotationVatRountAmt,
                    PurchaseQuotationNetDisAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationNetDisAmount,
                    PurchaseQuotationNetAmount = purchaseQuotation.purchaseQuotation.PurchaseQuotationNetAmount,
                    PurchaseQuotationTransportCost = purchaseQuotation.purchaseQuotation.PurchaseQuotationTransportCost,
                    PurchaseQuotationHandlingcharges = purchaseQuotation.purchaseQuotation.PurchaseQuotationHandlingcharges,
                    PurchaseQuotationIssueId = purchaseQuotation.purchaseQuotation.PurchaseQuotationIssueId,
                    PurchaseQuotationJobDirectPur = purchaseQuotation.purchaseQuotation.PurchaseQuotationJobDirectPur,
                    PurchaseQuotationDelStatus = purchaseQuotation.purchaseQuotation.PurchaseQuotationDelStatus,


                };
                purchaseQuotationViewModel.PurchaseQuotationDetails = _mapper.Map<List<PurchaseQuotationDetailsViewModel>>(purchaseQuotation.purchaseQuotationDetails);
                purchaseQuotationViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(purchaseQuotation.accountsTransactions);
                ApiResponse<PurchaseQuotationViewModel> apiResponse = new ApiResponse<PurchaseQuotationViewModel>
                {
                    Valid = true,
                    Result = purchaseQuotationViewModel,
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
                Message = PurchaseQuotationMessage.VoucherNumberExist
                


            };
            var x = _purchaseQuotationService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}