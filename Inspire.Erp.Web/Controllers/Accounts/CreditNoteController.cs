using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/CreditNote")]
    [Produces("application/json")]
    [ApiController]
    public class CreditNoteController : ControllerBase
    {
        private ICreditNoteService _creditNoteService;
        private readonly IMapper _mapper;
        public CreditNoteController(ICreditNoteService creditNoteService, IMapper mapper)
        {
            _creditNoteService = creditNoteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertCreditNote")]
        public ApiResponse<CreditNote> InsertCreditNote([FromBody] CreditNote voucherCompositeView)
        {
            ApiResponse<CreditNote> apiResponse = new ApiResponse<CreditNote>();
            var param1 = _mapper.Map<CreditNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<CreditNoteDetails>>(voucherCompositeView.CreditNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _creditNoteService.InsertCreditNote(param1, param3, param2);
            apiResponse = new ApiResponse<CreditNote>
            {
                Valid = true,
                Result = _mapper.Map<CreditNote>(xs),
                Message = CreditNoteMessage.SaveVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateCreditNote")]
        public ApiResponse<CreditNote> UpdateCreditNote([FromBody] CreditNote voucherCompositeView)
        {
            var param1 = _mapper.Map<CreditNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<CreditNoteDetails>>(voucherCompositeView.CreditNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _creditNoteService.UpdateCreditNote(param1, param3, param2);
            ApiResponse<CreditNote> apiResponse = new ApiResponse<CreditNote>
            {
                Valid = true,
                Result = _mapper.Map<CreditNote>(xs),
                Message = CreditNoteMessage.UpdateVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteCreditNote")]
        public ApiResponse<int> DeleteCreditNote([FromBody] CreditNote voucherCompositeView)
        {
            var param1 = _mapper.Map<CreditNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<CreditNoteDetails>>(voucherCompositeView.CreditNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _creditNoteService.DeleteCreditNote(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = CreditNoteMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_creditNoteService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetCreditNote")]
        public ApiResponse<List<CreditNote>> GetAllCreditNote()
        {
            ApiResponse<List<CreditNote>> apiResponse = new ApiResponse<List<CreditNote>>
            {
                Valid = true,
                Result = _mapper.Map<List<CreditNote>>(_creditNoteService.GetCreditNote()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedCreditNoteDetails/{id}")]
        public ApiResponse<CreditNote> GetSavedCreditNoteDetails(string id)
        {
            CreditNote creditNote = _creditNoteService.GetSavedCreditNoteDetails(id);

            if (creditNote != null)
            {
                ApiResponse<CreditNote> apiResponse = new ApiResponse<CreditNote>
                {
                    Valid = true,
                    Result = creditNote,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }

        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_creditNoteService.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(string id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = CreditNoteMessage.VoucherNumberExist
            };
            var x = _creditNoteService.GetVouchersNumbers(id);
            if (x == null)
            { 
                apiResponse.Result = false;
                apiResponse.Message = "";
            }
            return apiResponse;
        }
    }
}
