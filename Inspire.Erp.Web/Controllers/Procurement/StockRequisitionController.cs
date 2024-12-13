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

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/StockRequisition")]
    [Produces("application/json")]
    [ApiController]
    public class StockRequisitionController : ControllerBase
    {
        private IStockRequisitionService _StockRequisitionService;
        private readonly IMapper _mapper;
        public StockRequisitionController(IStockRequisitionService StockRequisitionService, IMapper mapper)
        {
            _StockRequisitionService = StockRequisitionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertStockRequisition")]
        public ApiResponse<StockRequisitionViewModel> InsertStockRequisition([FromBody] StockRequisitionViewModel voucherCompositeView)
        {
            ApiResponse<StockRequisitionViewModel> apiResponse = new ApiResponse<StockRequisitionViewModel>();
            if (_StockRequisitionService.GetVouchersNumbers(voucherCompositeView.StockRequisitionNo) == null)
            {
                var param1 = _mapper.Map<StockRequisition>(voucherCompositeView);
                var param2 = _mapper.Map<List<StockRequisitionDetails>>(voucherCompositeView.StockRequisitionDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

                var xs = _StockRequisitionService.InsertStockRequisition(param1, param3, param2

                    );
                StockRequisitionViewModel StockRequisitionViewModel = new StockRequisitionViewModel();
                StockRequisitionViewModel = _mapper.Map<StockRequisitionViewModel>(xs.StockRequisition);
                apiResponse = new ApiResponse<StockRequisitionViewModel>
                {
                    Valid = true,
                    Result = StockRequisitionViewModel,
                    Message = StockRequisitionMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<StockRequisitionViewModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = null,
                    Message = StockRequisitionMessage.VoucherAlreadyExist
                };

            }
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateStockRequisition")]
        public ApiResponse<StockRequisitionViewModel> UpdateStockRequisition([FromBody] StockRequisitionViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<StockRequisition>(voucherCompositeView);
            var param2 = _mapper.Map<List<StockRequisitionDetails>>(voucherCompositeView.StockRequisitionDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();

            var StockRequisition = _StockRequisitionService.UpdateStockRequisition(param1, accountsTransactions, param2
                );

            //StockRequisitionViewModel StockRequisitionViewModel = new StockRequisitionViewModel
            //{
            //    StockRequisitionId = StockRequisition.StockRequisition.StockRequisitionId,
            //    StockRequisitionNo = StockRequisition.StockRequisition.StockRequisitionNo,
            //    StockRequisitionDate = StockRequisition.StockRequisition.StockRequisitionDate,
            //    StockRequisitionType = StockRequisition.StockRequisition.StockRequisitionType,
            //    StockRequisitionJobId = StockRequisition.StockRequisition.StockRequisitionJobId,
            //    StockRequisitionStatus = StockRequisition.StockRequisition.StockRequisitionStatus,
            //    StockRequisitionDelStatus = StockRequisition.StockRequisition.StockRequisitionDelStatus,
            //    StockRequisitionDetailsReqDate = StockRequisition.StockRequisition.StockRequisitionDetailsReqDate,
            //    StockRequisitionDetailsReqStatus = StockRequisition.StockRequisition.StockRequisitionDetailsReqStatus,
            //    StockRequisitionApprovedBy = StockRequisition.StockRequisition.StockRequisitionApprovedBy,
            //    StockRequisitionApprovedDate = StockRequisition.StockRequisition.StockRequisitionApprovedDate,
            //    StockRequisitionRemarks = StockRequisition.StockRequisition.StockRequisitionRemarks,
            //    StockRequisitionRequestedBy = StockRequisition.StockRequisition.StockRequisitionRequestedBy,
            //};
            StockRequisitionViewModel StockRequisitionViewModel = new StockRequisitionViewModel();
            StockRequisitionViewModel = _mapper.Map<StockRequisitionViewModel>(StockRequisition.StockRequisition);

            StockRequisitionViewModel.StockRequisitionDetails = _mapper.Map<List<StockRequisitionDetailsViewModel>>(StockRequisition.StockRequisitionDetails);
            StockRequisitionViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(StockRequisition.accountsTransactions);

            ApiResponse<StockRequisitionViewModel> apiResponse = new ApiResponse<StockRequisitionViewModel>
            {
                Valid = true,
                Result = StockRequisitionViewModel,
                Message = StockRequisitionMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeleteStockRequisition")]
        public ApiResponse<int> DeleteStockRequisition([FromBody] StockRequisitionViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<StockRequisition>(voucherCompositeView);
            var param2 = _mapper.Map<List<StockRequisitionDetails>>(voucherCompositeView.StockRequisitionDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);

            var xs = _StockRequisitionService.DeleteStockRequisition(param1, param3, param2

                );
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = StockRequisitionMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_StockRequisitionService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetStockRequisition")]
        public ApiResponse<List<StockRequisition>> GetAllStockRequisition()
        {
            ApiResponse<List<StockRequisition>> apiResponse = new ApiResponse<List<StockRequisition>>
            {
                Valid = true,
                Result = _mapper.Map<List<StockRequisition>>(_StockRequisitionService.GetStockRequisition()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedStockRequisitionDetails/{id}")]
        public ApiResponse<StockRequisitionViewModel> GetSavedStockRequisitionDetails(string id)
        {
            StockRequisitionModel StockRequisition = _StockRequisitionService.GetSavedStockRequisitionDetails(id);

            if (StockRequisition != null)
            {
                //StockRequisitionViewModel StockRequisitionViewModel = new StockRequisitionViewModel
                //{
                //    StockRequisitionId = StockRequisition.StockRequisition.StockRequisitionId,
                //    StockRequisitionNo = StockRequisition.StockRequisition.StockRequisitionNo,
                //    StockRequisitionDate = StockRequisition.StockRequisition.StockRequisitionDate,
                //    StockRequisitionType = StockRequisition.StockRequisition.StockRequisitionType,
                //    StockRequisitionJobId = StockRequisition.StockRequisition.StockRequisitionJobId,
                //    StockRequisitionStatus = StockRequisition.StockRequisition.StockRequisitionStatus,
                //    StockRequisitionDelStatus = StockRequisition.StockRequisition.StockRequisitionDelStatus,
                //    StockRequisitionDetailsReqDate = StockRequisition.StockRequisition.StockRequisitionDetailsReqDate,
                //    StockRequisitionDetailsReqStatus = StockRequisition.StockRequisition.StockRequisitionDetailsReqStatus,
                //    StockRequisitionApprovedBy = StockRequisition.StockRequisition.StockRequisitionApprovedBy,
                //    StockRequisitionApprovedDate = StockRequisition.StockRequisition.StockRequisitionApprovedDate,
                //    StockRequisitionRemarks = StockRequisition.StockRequisition.StockRequisitionRemarks,
                //    StockRequisitionRequestedBy = StockRequisition.StockRequisition.StockRequisitionRequestedBy,
                //};

                StockRequisitionViewModel StockRequisitionViewModel = new StockRequisitionViewModel();
                StockRequisitionViewModel = _mapper.Map<StockRequisitionViewModel>(StockRequisition.StockRequisition);

                StockRequisitionViewModel.StockRequisitionDetails = _mapper.Map<List<StockRequisitionDetailsViewModel>>(StockRequisition.StockRequisitionDetails);
                StockRequisitionViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(StockRequisition.accountsTransactions);
                ApiResponse<StockRequisitionViewModel> apiResponse = new ApiResponse<StockRequisitionViewModel>
                {
                    Valid = true,
                    Result = StockRequisitionViewModel,
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
                Message = StockRequisitionMessage.VoucherNumberExist
            };
            var x = _StockRequisitionService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }
    }
}
