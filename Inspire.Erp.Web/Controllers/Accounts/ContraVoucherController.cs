using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/ContraVoucher")]
    [Produces("application/json")]
    [ApiController]
    public class ContraVoucherController : ControllerBase
    {
        private IContraVoucherService _contraVoucherService;
        private readonly IMapper _mapper;
        public ContraVoucherController(IContraVoucherService contraVoucherService, IMapper mapper)
        {
            _contraVoucherService = contraVoucherService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_contraVoucherService.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertContraVoucher")]
        public ApiResponse<ContraVoucher> InsertContraVoucher([FromBody] ContraVoucher voucherCompositeView)
        {
            ApiResponse<ContraVoucher> apiResponse = new ApiResponse<ContraVoucher>();
            var param1 = _mapper.Map<ContraVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<ContraVoucherDetails>>(voucherCompositeView.ContraVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _contraVoucherService.InsertContraVoucher(param1, param3, param2);
            apiResponse = new ApiResponse<ContraVoucher>
            {
                Valid = true,
                Result = _mapper.Map<ContraVoucher>(xs),
                Message = ContraVoucherMessage.SaveVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateContraVoucher")]
        public ApiResponse<ContraVoucher> UpdateContraVoucher([FromBody] ContraVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<ContraVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<ContraVoucherDetails>>(voucherCompositeView.ContraVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _contraVoucherService.UpdateContraVoucher(param1, param3, param2);
            ApiResponse<ContraVoucher> apiResponse = new ApiResponse<ContraVoucher>
            {
                Valid = true,
                Result = _mapper.Map<ContraVoucher>(xs),
                Message = ContraVoucherMessage.UpdateVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteContraVoucher")]
        public ApiResponse<int> DeleteContraVoucher([FromBody] ContraVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<ContraVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<ContraVoucherDetails>>(voucherCompositeView.ContraVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _contraVoucherService.DeleteContraVoucher(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = ContraVoucherMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_contraVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetContraVoucher")]
        public ApiResponse<List<ContraVoucher>> GetAllContraVoucher()
        {
            ApiResponse<List<ContraVoucher>> apiResponse = new ApiResponse<List<ContraVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<ContraVoucher>>(_contraVoucherService.GetContraVoucher()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedContraVoucherDetails/{id}")]
        public ApiResponse<ContraVoucher> GetSavedContraVoucherDetails(string id)
        {
            ContraVoucher contraVoucher = _contraVoucherService.GetSavedContraVoucherDetails(id);
            if (contraVoucher != null)
            {
                ApiResponse<ContraVoucher> apiResponse = new ApiResponse<ContraVoucher>
                {
                    Valid = true,
                    Result = contraVoucher,
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
                Message = ContraVoucherMessage.VoucherNumberExist
            };
            var x = _contraVoucherService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }
            return apiResponse;
        }
    }
}

