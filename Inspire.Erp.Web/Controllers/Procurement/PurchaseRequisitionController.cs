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
using Inspire.Erp.Application.Procurement.Implimentation;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Application.Procurement.Implementation;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/PurchaseRequisition")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseRequisitionController : ControllerBase
    {
        private IPurchaseRequisitionService _purchaseRequisitionService;
        private readonly IMapper _mapper;
        public PurchaseRequisitionController(IPurchaseRequisitionService purchaseRequisitionService, IMapper mapper)
        {
            _purchaseRequisitionService = purchaseRequisitionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertPurchaseRequisition")]
        public ApiResponse<PurchaseRequisitionViewModel> InsertPurchaseRequisition([FromBody] PurchaseRequisitionViewModel voucherCompositeView)
        {
            ApiResponse<PurchaseRequisitionViewModel> apiResponse = new ApiResponse<PurchaseRequisitionViewModel>();
            if (_purchaseRequisitionService.GetVouchersNumbers(voucherCompositeView.PurchaseRequisitionNo) == null)
            {
                var param1 = _mapper.Map<PurchaseRequisition>(voucherCompositeView);
                var param2 = _mapper.Map<List<PurchaseRequisitionDetails>>(voucherCompositeView.PurchaseRequisitionDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
                var xs = _purchaseRequisitionService.InsertPurchaseRequisition(param1, param3, param2
                    //, param4
                    );
                PurchaseRequisitionViewModel purchaseRequisitionViewModel = new PurchaseRequisitionViewModel
                {

                    PurchaseRequisitionId = xs.purchaseRequisition.PurchaseRequisitionId,
                    PurchaseRequisitionNo = xs.purchaseRequisition.PurchaseRequisitionNo,
                    PurchaseRequisitionDate = xs.purchaseRequisition.PurchaseRequisitionDate,
                    PurchaseRequisitionType = xs.purchaseRequisition.PurchaseRequisitionType,
                    PurchaseRequisitionPartyId = xs.purchaseRequisition.PurchaseRequisitionPartyId,
                    PurchaseRequisitionPartyName = xs.purchaseRequisition.PurchaseRequisitionPartyName,
                    PurchaseRequisitionPartyAddress = xs.purchaseRequisition.PurchaseRequisitionPartyAddress,
                    PurchaseRequisitionPartyVatNo = xs.purchaseRequisition.PurchaseRequisitionPartyVatNo,
                    PurchaseRequisitionSupInvNo = xs.purchaseRequisition.PurchaseRequisitionSupInvNo,
                    PurchaseRequisitionRefNo = xs.purchaseRequisition.PurchaseRequisitionRefNo,
                    PurchaseRequisitionDescription = xs.purchaseRequisition.PurchaseRequisitionDescription,
                    PurchaseRequisitionGrno = xs.purchaseRequisition.PurchaseRequisitionGrno,
                    PurchaseRequisitionGrdate = xs.purchaseRequisition.PurchaseRequisitionGrdate,
                    PurchaseRequisitionLpono = xs.purchaseRequisition.PurchaseRequisitionLpono,
                    PurchaseRequisitionLpodate = xs.purchaseRequisition.PurchaseRequisitionLpodate,
                    PurchaseRequisitionQtnNo = xs.purchaseRequisition.PurchaseRequisitionQtnNo,
                    PurchaseRequisitionQtnDate = xs.purchaseRequisition.PurchaseRequisitionQtnDate,
                    PurchaseRequisitionExcludeVat = xs.purchaseRequisition.PurchaseRequisitionExcludeVat,
                    PurchaseRequisitionPono = xs.purchaseRequisition.PurchaseRequisitionPono,
                    PurchaseRequisitionBatchCode = xs.purchaseRequisition.PurchaseRequisitionBatchCode,
                    PurchaseRequisitionDayBookNo = xs.purchaseRequisition.PurchaseRequisitionDayBookNo,
                    PurchaseRequisitionLocationId = xs.purchaseRequisition.PurchaseRequisitionLocationId,
                    PurchaseRequisitionUserId = xs.purchaseRequisition.PurchaseRequisitionUserId,
                    PurchaseRequisitionCurrencyId = xs.purchaseRequisition.PurchaseRequisitionCurrencyId,
                    PurchaseRequisitionCompanyId = xs.purchaseRequisition.PurchaseRequisitionCompanyId,
                    PurchaseRequisitionJobId = xs.purchaseRequisition.PurchaseRequisitionJobId,
                    PurchaseRequisitionFsno = xs.purchaseRequisition.PurchaseRequisitionFsno,
                    PurchaseRequisitionFcRate = xs.purchaseRequisition.PurchaseRequisitionFcRate,
                    PurchaseRequisitionStatus = xs.purchaseRequisition.PurchaseRequisitionStatus,
                    PurchaseRequisitionTotalGrossAmount = xs.purchaseRequisition.PurchaseRequisitionTotalGrossAmount,
                    PurchaseRequisitionTotalItemDisAmount = xs.purchaseRequisition.PurchaseRequisitionTotalItemDisAmount,
                    PurchaseRequisitionTotalActualAmount = xs.purchaseRequisition.PurchaseRequisitionTotalActualAmount,
                    PurchaseRequisitionTotalDisPer = xs.purchaseRequisition.PurchaseRequisitionTotalDisPer,
                    PurchaseRequisitionTotalDisAmount = xs.purchaseRequisition.PurchaseRequisitionTotalDisAmount,
                    PurchaseRequisitionVatAmt = xs.purchaseRequisition.PurchaseRequisitionVatAmt,
                    PurchaseRequisitionVatPer = xs.purchaseRequisition.PurchaseRequisitionVatPer,
                    PurchaseRequisitionVatRoundSign = xs.purchaseRequisition.PurchaseRequisitionVatRoundSign,
                    PurchaseRequisitionVatRountAmt = xs.purchaseRequisition.PurchaseRequisitionVatRountAmt,
                    PurchaseRequisitionNetDisAmount = xs.purchaseRequisition.PurchaseRequisitionNetDisAmount,
                    PurchaseRequisitionNetAmount = xs.purchaseRequisition.PurchaseRequisitionNetAmount,
                    PurchaseRequisitionTransportCost = xs.purchaseRequisition.PurchaseRequisitionTransportCost,
                    PurchaseRequisitionHandlingcharges = xs.purchaseRequisition.PurchaseRequisitionHandlingcharges,
                    PurchaseRequisitionIssueId = xs.purchaseRequisition.PurchaseRequisitionIssueId,
                    PurchaseRequisitionJobDirectPur = xs.purchaseRequisition.PurchaseRequisitionJobDirectPur,
                    PurchaseRequisitionDelStatus = xs.purchaseRequisition.PurchaseRequisitionDelStatus,

                };

                purchaseRequisitionViewModel.PurchaseRequisitionDetails = _mapper.Map<List<PurchaseRequisitionDetailsViewModel>>(xs.purchaseRequisitionDetails);
                purchaseRequisitionViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<PurchaseRequisitionViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<PurchaseRequisitionViewModel>(purchaseRequisitionViewModel),
                    Message = PurchaseRequisitionMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<PurchaseRequisitionViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = PurchaseRequisitionMessage.VoucherAlreadyExist
                };

            }
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdatePurchaseRequisition")]
        public ApiResponse<PurchaseRequisitionViewModel> UpdatePurchaseRequisition([FromBody] PurchaseRequisitionViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseRequisition>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseRequisitionDetails>>(voucherCompositeView.PurchaseRequisitionDetails);
           var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();

            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            var xs = _purchaseRequisitionService.UpdatePurchaseRequisition(param1, accountsTransactions, param2
                //, param4
                );
            
            //var xs = _purchaseRequisitionService.UpdatePurchaseRequisition(param1, param3, param2
            //    //, param4
            //    );

            PurchaseRequisitionViewModel purchaseRequisitionViewModel = new PurchaseRequisitionViewModel
            {
                PurchaseRequisitionId = xs.purchaseRequisition.PurchaseRequisitionId,
                PurchaseRequisitionNo = xs.purchaseRequisition.PurchaseRequisitionNo,
                PurchaseRequisitionDate = xs.purchaseRequisition.PurchaseRequisitionDate,
                PurchaseRequisitionType = xs.purchaseRequisition.PurchaseRequisitionType,
                PurchaseRequisitionPartyId = xs.purchaseRequisition.PurchaseRequisitionPartyId,
                PurchaseRequisitionPartyName = xs.purchaseRequisition.PurchaseRequisitionPartyName,
                PurchaseRequisitionPartyAddress = xs.purchaseRequisition.PurchaseRequisitionPartyAddress,
                PurchaseRequisitionPartyVatNo = xs.purchaseRequisition.PurchaseRequisitionPartyVatNo,
                PurchaseRequisitionSupInvNo = xs.purchaseRequisition.PurchaseRequisitionSupInvNo,
                PurchaseRequisitionRefNo = xs.purchaseRequisition.PurchaseRequisitionRefNo,
                PurchaseRequisitionDescription = xs.purchaseRequisition.PurchaseRequisitionDescription,
                PurchaseRequisitionGrno = xs.purchaseRequisition.PurchaseRequisitionGrno,
                PurchaseRequisitionGrdate = xs.purchaseRequisition.PurchaseRequisitionGrdate,
                PurchaseRequisitionLpono = xs.purchaseRequisition.PurchaseRequisitionLpono,
                PurchaseRequisitionLpodate = xs.purchaseRequisition.PurchaseRequisitionLpodate,
                PurchaseRequisitionQtnNo = xs.purchaseRequisition.PurchaseRequisitionQtnNo,
                PurchaseRequisitionQtnDate = xs.purchaseRequisition.PurchaseRequisitionQtnDate,
                PurchaseRequisitionExcludeVat = xs.purchaseRequisition.PurchaseRequisitionExcludeVat,
                PurchaseRequisitionPono = xs.purchaseRequisition.PurchaseRequisitionPono,
                PurchaseRequisitionBatchCode = xs.purchaseRequisition.PurchaseRequisitionBatchCode,
                PurchaseRequisitionDayBookNo = xs.purchaseRequisition.PurchaseRequisitionDayBookNo,
                PurchaseRequisitionLocationId = xs.purchaseRequisition.PurchaseRequisitionLocationId,
                PurchaseRequisitionUserId = xs.purchaseRequisition.PurchaseRequisitionUserId,
                PurchaseRequisitionCurrencyId = xs.purchaseRequisition.PurchaseRequisitionCurrencyId,
                PurchaseRequisitionCompanyId = xs.purchaseRequisition.PurchaseRequisitionCompanyId,
                PurchaseRequisitionJobId = xs.purchaseRequisition.PurchaseRequisitionJobId,
                PurchaseRequisitionFsno = xs.purchaseRequisition.PurchaseRequisitionFsno,
                PurchaseRequisitionFcRate = xs.purchaseRequisition.PurchaseRequisitionFcRate,
                PurchaseRequisitionStatus = xs.purchaseRequisition.PurchaseRequisitionStatus,
                PurchaseRequisitionTotalGrossAmount = xs.purchaseRequisition.PurchaseRequisitionTotalGrossAmount,
                PurchaseRequisitionTotalItemDisAmount = xs.purchaseRequisition.PurchaseRequisitionTotalItemDisAmount,
                PurchaseRequisitionTotalActualAmount = xs.purchaseRequisition.PurchaseRequisitionTotalActualAmount,
                PurchaseRequisitionTotalDisPer = xs.purchaseRequisition.PurchaseRequisitionTotalDisPer,
                PurchaseRequisitionTotalDisAmount = xs.purchaseRequisition.PurchaseRequisitionTotalDisAmount,
                PurchaseRequisitionVatAmt = xs.purchaseRequisition.PurchaseRequisitionVatAmt,
                PurchaseRequisitionVatPer = xs.purchaseRequisition.PurchaseRequisitionVatPer,
                PurchaseRequisitionVatRoundSign = xs.purchaseRequisition.PurchaseRequisitionVatRoundSign,
                PurchaseRequisitionVatRountAmt = xs.purchaseRequisition.PurchaseRequisitionVatRountAmt,
                PurchaseRequisitionNetDisAmount = xs.purchaseRequisition.PurchaseRequisitionNetDisAmount,
                PurchaseRequisitionNetAmount = xs.purchaseRequisition.PurchaseRequisitionNetAmount,
                PurchaseRequisitionTransportCost = xs.purchaseRequisition.PurchaseRequisitionTransportCost,
                PurchaseRequisitionHandlingcharges = xs.purchaseRequisition.PurchaseRequisitionHandlingcharges,
                PurchaseRequisitionIssueId = xs.purchaseRequisition.PurchaseRequisitionIssueId,
                PurchaseRequisitionJobDirectPur = xs.purchaseRequisition.PurchaseRequisitionJobDirectPur,
                PurchaseRequisitionDelStatus = xs.purchaseRequisition.PurchaseRequisitionDelStatus,
               
            };

            purchaseRequisitionViewModel.PurchaseRequisitionDetails = _mapper.Map<List<PurchaseRequisitionDetailsViewModel>>(xs.purchaseRequisitionDetails);
            purchaseRequisitionViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<PurchaseRequisitionViewModel> apiResponse = new ApiResponse<PurchaseRequisitionViewModel>
            {
                Valid = true,
                Result = _mapper.Map<PurchaseRequisitionViewModel>(purchaseRequisitionViewModel),
                Message = PurchaseRequisitionMessage.UpdateVoucher
            };

            return apiResponse;

        }


        [HttpPost]
        [Route("DeletePurchaseRequisition")]
        public ApiResponse<int> DeletePurchaseRequisition([FromBody] PurchaseRequisitionViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseRequisition>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseRequisitionDetails>>(voucherCompositeView.PurchaseRequisitionDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            //var param4 = _mapper.Map<List<StockRegister>>(voucherCompositeView.StockRegister);
            var xs = _purchaseRequisitionService.DeletePurchaseRequisition(param1, param3, param2
                //, param4
                );
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseRequisitionMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_purchaseRequisitionService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;




        }

        [HttpGet]
        [Route("GetPurchaseRequisition")]
        public ApiResponse<List<PurchaseRequisition>> GetAllPurchaseRequisition()
        {


            ApiResponse<List<PurchaseRequisition>> apiResponse = new ApiResponse<List<PurchaseRequisition>>
            {
                Valid = true,
                Result = _mapper.Map<List<PurchaseRequisition>>(_purchaseRequisitionService.GetPurchaseRequisition()),
                Message = ""
            };
            return apiResponse;





        }

        [HttpGet]
        [Route("GetSavedPurchaseRequisitionDetails/{id}")]
        public ApiResponse<PurchaseRequisitionViewModel> GetSavedPurchaseRequisitionDetails(string id)
        {
            PurchaseRequisitionModel purchaseRequisition = _purchaseRequisitionService.GetSavedPurchaseRequisitionDetails(id);

            if (purchaseRequisition != null)
            {
                PurchaseRequisitionViewModel purchaseRequisitionViewModel = new PurchaseRequisitionViewModel
                {


                    PurchaseRequisitionId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionId,
                    PurchaseRequisitionNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionNo,
                    PurchaseRequisitionDate = purchaseRequisition.purchaseRequisition.PurchaseRequisitionDate,
                    PurchaseRequisitionType = purchaseRequisition.purchaseRequisition.PurchaseRequisitionType,
                    PurchaseRequisitionPartyId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionPartyId,
                    PurchaseRequisitionPartyName = purchaseRequisition.purchaseRequisition.PurchaseRequisitionPartyName,
                    PurchaseRequisitionPartyAddress = purchaseRequisition.purchaseRequisition.PurchaseRequisitionPartyAddress,
                    PurchaseRequisitionPartyVatNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionPartyVatNo,
                    PurchaseRequisitionSupInvNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionSupInvNo,
                    PurchaseRequisitionRefNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionRefNo,
                    PurchaseRequisitionDescription = purchaseRequisition.purchaseRequisition.PurchaseRequisitionDescription,
                    PurchaseRequisitionGrno = purchaseRequisition.purchaseRequisition.PurchaseRequisitionGrno,
                    PurchaseRequisitionGrdate = purchaseRequisition.purchaseRequisition.PurchaseRequisitionGrdate,
                    PurchaseRequisitionLpono = purchaseRequisition.purchaseRequisition.PurchaseRequisitionLpono,
                    PurchaseRequisitionLpodate = purchaseRequisition.purchaseRequisition.PurchaseRequisitionLpodate,
                    PurchaseRequisitionQtnNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionQtnNo,
                    PurchaseRequisitionQtnDate = purchaseRequisition.purchaseRequisition.PurchaseRequisitionQtnDate,
                    PurchaseRequisitionExcludeVat = purchaseRequisition.purchaseRequisition.PurchaseRequisitionExcludeVat,
                    PurchaseRequisitionPono = purchaseRequisition.purchaseRequisition.PurchaseRequisitionPono,
                    PurchaseRequisitionBatchCode = purchaseRequisition.purchaseRequisition.PurchaseRequisitionBatchCode,
                    PurchaseRequisitionDayBookNo = purchaseRequisition.purchaseRequisition.PurchaseRequisitionDayBookNo,
                    PurchaseRequisitionLocationId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionLocationId,
                    PurchaseRequisitionUserId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionUserId,
                    PurchaseRequisitionCurrencyId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionCurrencyId,
                    PurchaseRequisitionCompanyId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionCompanyId,
                    PurchaseRequisitionJobId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionJobId,
                    PurchaseRequisitionFsno = purchaseRequisition.purchaseRequisition.PurchaseRequisitionFsno,
                    PurchaseRequisitionFcRate = purchaseRequisition.purchaseRequisition.PurchaseRequisitionFcRate,
                    PurchaseRequisitionStatus = purchaseRequisition.purchaseRequisition.PurchaseRequisitionStatus,
                    PurchaseRequisitionTotalGrossAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTotalGrossAmount,
                    PurchaseRequisitionTotalItemDisAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTotalItemDisAmount,
                    PurchaseRequisitionTotalActualAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTotalActualAmount,
                    PurchaseRequisitionTotalDisPer = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTotalDisPer,
                    PurchaseRequisitionTotalDisAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTotalDisAmount,
                    PurchaseRequisitionVatAmt = purchaseRequisition.purchaseRequisition.PurchaseRequisitionVatAmt,
                    PurchaseRequisitionVatPer = purchaseRequisition.purchaseRequisition.PurchaseRequisitionVatPer,
                    PurchaseRequisitionVatRoundSign = purchaseRequisition.purchaseRequisition.PurchaseRequisitionVatRoundSign,
                    PurchaseRequisitionVatRountAmt = purchaseRequisition.purchaseRequisition.PurchaseRequisitionVatRountAmt,
                    PurchaseRequisitionNetDisAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionNetDisAmount,
                    PurchaseRequisitionNetAmount = purchaseRequisition.purchaseRequisition.PurchaseRequisitionNetAmount,
                    PurchaseRequisitionTransportCost = purchaseRequisition.purchaseRequisition.PurchaseRequisitionTransportCost,
                    PurchaseRequisitionHandlingcharges = purchaseRequisition.purchaseRequisition.PurchaseRequisitionHandlingcharges,
                    PurchaseRequisitionIssueId = purchaseRequisition.purchaseRequisition.PurchaseRequisitionIssueId,
                    PurchaseRequisitionJobDirectPur = purchaseRequisition.purchaseRequisition.PurchaseRequisitionJobDirectPur,
                    PurchaseRequisitionDelStatus = purchaseRequisition.purchaseRequisition.PurchaseRequisitionDelStatus,


                };
                purchaseRequisitionViewModel.PurchaseRequisitionDetails = _mapper.Map<List<PurchaseRequisitionDetailsViewModel>>(purchaseRequisition.purchaseRequisitionDetails);
                purchaseRequisitionViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(purchaseRequisition.accountsTransactions);
                ApiResponse<PurchaseRequisitionViewModel> apiResponse = new ApiResponse<PurchaseRequisitionViewModel>
                {
                    Valid = true,
                    Result = purchaseRequisitionViewModel,
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
                Message = PurchaseRequisitionMessage.VoucherNumberExist



            };
            var x = _purchaseRequisitionService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }

        [HttpPost]
        [Route("GetPurchaseRequisitionStatus")]
        public async Task<IActionResult> GetPurchaseRequisitionStatus(PurChaseRequisitionStatusFilterReport model)
        {

            var response = await _purchaseRequisitionService.GetPurchaseRequisitionStatus(model);
            ApiResponse<List<PurChaseRequisitionStatus>> apiResponse = new ApiResponse<List<PurChaseRequisitionStatus>>
            {
                Valid = true,
                Result = response.Result,
                Message = ""
            };
            return Ok(apiResponse);
        }
    
    [HttpPost]
    [Route("GetPurchaseReqJobId")]
    public async Task<IActionResult> GetPurchaseReqJobId(PurChaseRequisitionFilterReport model)
    {

        var response = await _purchaseRequisitionService.GetPurchaseReqJobId(model);
        ApiResponse<List<PurChaseReqFields>> apiResponse = new ApiResponse<List<PurChaseReqFields>>
        {
            Valid = true,
            Result = response.Result,
            Message = ""
        };
        return Ok(apiResponse);
    }
  }

}
