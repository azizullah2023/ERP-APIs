using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inspire.Erp.Web.Controllers.Accounts
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class OBVoucherController : ControllerBase
    {
        private IOBVoucherService _obVoucherService;
        private readonly IMapper _mapper;
        public OBVoucherController(IOBVoucherService obVoucherService, IMapper mapper)
        {
            _obVoucherService = obVoucherService;
            _mapper = mapper;
        }
        

        [HttpPost]
        [Route("InsertOBVoucher")]
        public ApiResponse<OpeningBalanceVoucherViewModel> InsertOBVoucher([FromBody] OpeningBalanceVoucherViewModel voucherCompositeView)
        {
            ApiResponse<OpeningBalanceVoucherViewModel> apiResponse = new ApiResponse<OpeningBalanceVoucherViewModel>();
            if (_obVoucherService.GetVouchersNumbers(Convert.ToString(voucherCompositeView.OpeningVoucherMasterId)) == null)
            {
                var param1 = _mapper.Map<OpeningVoucherMaster>(voucherCompositeView);
                var param2 = _mapper.Map<List<OpeningVoucherDetails>>(voucherCompositeView.openingVoucherDetails);
                var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
                var xs = _obVoucherService.InsertOBVoucher(param1, param3, param2);
                OpeningBalanceVoucherViewModel openingBalanceVoucherViewModel = new OpeningBalanceVoucherViewModel
                {

                    OpeningVoucherMasterId = xs.openingBalanceVoucher.OpeningVoucherMasterId,
                    OpeningVoucherMasterAccNo = xs.openingBalanceVoucher.OpeningVoucherMasterAccNo,
                    OpeningVoucherMasterOpBDate = xs.openingBalanceVoucher.OpeningVoucherMasterOpBDate,
                    OpeningVoucherMasterTotalDebit = xs.openingBalanceVoucher.OpeningVoucherMasterTotalDebit,
                    OpeningVoucherMasterTotalCredit = xs.openingBalanceVoucher.OpeningVoucherMasterTotalCredit,
                    OpeningVoucherMasterRemarks = xs.openingBalanceVoucher.OpeningVoucherMasterRemarks,
                    OpeningVoucherMasterCurrencyId = xs.openingBalanceVoucher.OpeningVoucherMasterCurrencyId,
                    OpeningVoucherMasterUserId = xs.openingBalanceVoucher.OpeningVoucherMasterUserId,
                    OpeningVoucherMasterLastUpdateTime = xs.openingBalanceVoucher.OpeningVoucherMasterLastUpdateTime,
                    OpeningVoucherMasterFsno = xs.openingBalanceVoucher.OpeningVoucherMasterFsno,
                    OpeningVoucherMasterDelStatus = xs.openingBalanceVoucher.OpeningVoucherMasterDelStatus

                };

                openingBalanceVoucherViewModel.openingVoucherDetails = _mapper.Map<List<OpeningBalanceVoucherDetailsViewModel>>(xs.openingVoucherDetails);
                openingBalanceVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<OpeningBalanceVoucherViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<OpeningBalanceVoucherViewModel>(openingBalanceVoucherViewModel),
                    Message = PaymentVoucherMessage.SaveVoucher
                };
            }
            else
            {
                apiResponse = new ApiResponse<OpeningBalanceVoucherViewModel>
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
        [Route("UpdateOBVoucher")]
        public ApiResponse<OpeningBalanceVoucherViewModel> UpdateOBVoucher([FromBody] OpeningBalanceVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<OpeningVoucherMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<OpeningVoucherDetails>>(voucherCompositeView.openingVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _obVoucherService.UpdateOBVoucher(param1, param3, param2);

            OpeningBalanceVoucherViewModel openingBalanceVoucherViewModel = new OpeningBalanceVoucherViewModel
            {
                OpeningVoucherMasterId = xs.openingBalanceVoucher.OpeningVoucherMasterId,
                OpeningVoucherMasterAccNo = xs.openingBalanceVoucher.OpeningVoucherMasterAccNo,
                OpeningVoucherMasterOpBDate = xs.openingBalanceVoucher.OpeningVoucherMasterOpBDate,
                OpeningVoucherMasterTotalDebit = xs.openingBalanceVoucher.OpeningVoucherMasterTotalDebit,
                OpeningVoucherMasterTotalCredit = xs.openingBalanceVoucher.OpeningVoucherMasterTotalCredit,
                OpeningVoucherMasterRemarks = xs.openingBalanceVoucher.OpeningVoucherMasterRemarks,
                OpeningVoucherMasterCurrencyId = xs.openingBalanceVoucher.OpeningVoucherMasterCurrencyId,
                OpeningVoucherMasterUserId = xs.openingBalanceVoucher.OpeningVoucherMasterUserId,
                OpeningVoucherMasterLastUpdateTime = xs.openingBalanceVoucher.OpeningVoucherMasterLastUpdateTime,
                OpeningVoucherMasterFsno = xs.openingBalanceVoucher.OpeningVoucherMasterFsno,
                OpeningVoucherMasterDelStatus = xs.openingBalanceVoucher.OpeningVoucherMasterDelStatus
            };

            openingBalanceVoucherViewModel.openingVoucherDetails = _mapper.Map<List<OpeningBalanceVoucherDetailsViewModel>>(xs.openingVoucherDetails);
            openingBalanceVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(xs.accountsTransactions);
            ApiResponse<OpeningBalanceVoucherViewModel> apiResponse = new ApiResponse<OpeningBalanceVoucherViewModel>
            {
                Valid = true,
                Result = _mapper.Map<OpeningBalanceVoucherViewModel>(openingBalanceVoucherViewModel),
                Message = PaymentVoucherMessage.UpdateVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteOBVoucher")]
        public ApiResponse<int> DeleteOBVoucher([FromBody] OpeningBalanceVoucherViewModel voucherCompositeView)
        {

            var param1 = _mapper.Map<OpeningVoucherMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<OpeningVoucherDetails>>(voucherCompositeView.openingVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _obVoucherService.DeleteOBVoucher(param1, param3, param2);

            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PaymentVoucherMessage.DeleteVoucher
            };

            return apiResponse;

        }

        //[HttpGet]
        //[Route("GetAllAccountTransaction")]
        //public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        //{
        //    ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<List<AccountsTransactions>>(_obVoucherService.GetAllTransaction()),
        //        Message = ""
        //    };
        //    return apiResponse;
        //}

        [HttpGet]
        [Route("GetOBVouchers")]
        public ApiResponse<List<OpeningVoucherMaster>> GetAllOBVouchers()
        {
            ApiResponse<List<OpeningVoucherMaster>> apiResponse = new ApiResponse<List<OpeningVoucherMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<OpeningVoucherMaster>>(_obVoucherService.GetOBVouchers()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedOBVoucherDetails/{id}")]
        public ApiResponse<OpeningBalanceVoucherViewModel> GetSavedOBVoucherDetails(string id)
        {
            OBVoucherModel openingBalanceVoucher = _obVoucherService.GetSavedOBVoucherDetails(id);
            if (openingBalanceVoucher != null)
            {
                OpeningBalanceVoucherViewModel openingBalanceVoucherViewModel = new OpeningBalanceVoucherViewModel
                {

                    OpeningVoucherMasterId = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterId,
                    OpeningVoucherMasterAccNo = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterAccNo,
                    OpeningVoucherMasterOpBDate = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterOpBDate,
                    OpeningVoucherMasterTotalDebit = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterTotalDebit,
                    OpeningVoucherMasterTotalCredit = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterTotalCredit,
                    OpeningVoucherMasterRemarks = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterRemarks,
                    OpeningVoucherMasterCurrencyId = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterCurrencyId,
                    OpeningVoucherMasterUserId = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterUserId,
                    OpeningVoucherMasterLastUpdateTime = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterLastUpdateTime,
                    OpeningVoucherMasterFsno = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterFsno,
                    OpeningVoucherMasterDelStatus = openingBalanceVoucher.openingBalanceVoucher.OpeningVoucherMasterDelStatus

                };
                openingBalanceVoucherViewModel.openingVoucherDetails = _mapper.Map<List<OpeningBalanceVoucherDetailsViewModel>>(openingBalanceVoucher.openingVoucherDetails);
                openingBalanceVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionsViewModel>>(openingBalanceVoucher.accountsTransactions);
                ApiResponse<OpeningBalanceVoucherViewModel> apiResponse = new ApiResponse<OpeningBalanceVoucherViewModel>
                {
                    Valid = true,
                    Result = openingBalanceVoucherViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;

        }

        //[HttpGet]
        //[Route("CheckVnoExist/{id}")]
        //public ApiResponse<bool> CheckVnoExist(string id)
        //{
        //    ApiResponse<bool> apiResponse = new ApiResponse<bool>
        //    {
        //        Valid = true,
        //        Result = true,
        //        Message = PaymentVoucherMessage.VoucherAlreadyExist
        //    };
        //    var x = _obVoucherService.GetVouchersNumbers(id);
        //    if (x == null)
        //    {
        //        apiResponse.Result = false;
        //        apiResponse.Message = "";
        //    }

        //    return apiResponse;
        //}
    }
}