using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/DebitNote")]
    [Produces("application/json")]
    [ApiController]
    public class DebitNoteController : ControllerBase
    {
        private IDebitNoteService _debitNoteService;
        private readonly IMapper _mapper;
        public DebitNoteController(IDebitNoteService debitNoteService, IMapper mapper)
        {
            _debitNoteService = debitNoteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertDebitNote")]
        public ApiResponse<DebitNote> InsertDebitNote([FromBody] DebitNote voucherCompositeView)
        {
            ApiResponse<DebitNote> apiResponse = new ApiResponse<DebitNote>();
            var param1 = _mapper.Map<DebitNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<DebitNoteDetails>>(voucherCompositeView.DebitNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _debitNoteService.InsertDebitNote(param1, param3, param2);
            apiResponse = new ApiResponse<DebitNote>
            {
                Valid = true,
                Result = xs,
                Message = DebitNoteMessage.SaveVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateDebitNote")]
        public ApiResponse<DebitNote> UpdateDebitNote([FromBody] DebitNote voucherCompositeView)
        {
            var param1 = _mapper.Map<DebitNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<DebitNoteDetails>>(voucherCompositeView.DebitNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _debitNoteService.UpdateDebitNote(param1, param3, param2);
            ApiResponse<DebitNote> apiResponse = new ApiResponse<DebitNote>
            {
                Valid = true,
                Result = xs,
                Message = DebitNoteMessage.UpdateVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteDebitNote")]
        public ApiResponse<int> DeleteDebitNote([FromBody] DebitNote voucherCompositeView)
        {
            var param1 = _mapper.Map<DebitNote>(voucherCompositeView);
            var param2 = _mapper.Map<List<DebitNoteDetails>>(voucherCompositeView.DebitNoteDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _debitNoteService.DeleteDebitNote(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = DebitNoteMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_debitNoteService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetDebitNote")]
        public ApiResponse<List<DebitNote>> GetAllDebitNote()
        {
            ApiResponse<List<DebitNote>> apiResponse = new ApiResponse<List<DebitNote>>
            {
                Valid = true,
                Result = _mapper.Map<List<DebitNote>>(_debitNoteService.GetDebitNote()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedDebitNoteDetails/{id}")]
        public ApiResponse<DebitNote> GetSavedDebitNoteDetails(string id)
        {
            DebitNote debitNote = _debitNoteService.GetSavedDebitNoteDetails(id);

            if (debitNote != null)
            {
                ApiResponse<DebitNote> apiResponse = new ApiResponse<DebitNote>
                {
                    Valid = true,
                    Result = debitNote,
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
                return Ok(_debitNoteService.GenerateVoucherNo(null));
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
                Message = DebitNoteMessage.VoucherNumberExist
            };
            var x = _debitNoteService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }



    }
}

