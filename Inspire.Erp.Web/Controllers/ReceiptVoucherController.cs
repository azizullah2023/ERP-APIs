using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/receipt")]
    [Produces("application/json")]
    [ApiController]
    public class ReceiptVoucherController : ControllerBase
    {
        private IReceiptVoucherService _receiptVoucherService;
        private readonly IMapper _mapper;
        public ReceiptVoucherController(IReceiptVoucherService receiptVoucherService, IMapper mapper)
        {
            _receiptVoucherService = receiptVoucherService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("InsertReceiptVoucher")]
        public ApiResponse<ReceiptVoucherMaster> InsertReceiptVoucher([FromBody] ReceiptVoucherMaster voucherCompositeView)
        {
            ApiResponse<ReceiptVoucherMaster> apiResponse = new ApiResponse<ReceiptVoucherMaster>();
            var param1 = _mapper.Map<ReceiptVoucherMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<ReceiptVoucherDetails>>(voucherCompositeView.ReceiptVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var alloDetails = _mapper.Map<List<Domain.Models.AllocationDetails>>(voucherCompositeView.rvAllocationDetails);
            var xs = _receiptVoucherService.InsertReceiptVoucher(param1, param3, param2, alloDetails) ;
            apiResponse = new ApiResponse<ReceiptVoucherMaster>
            {
                Valid = true,
                Result = _mapper.Map<ReceiptVoucherMaster>(xs),
                Message = ReceiptVoucherMessage.SaveReceiptVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateReceiptVoucher")]
        public ApiResponse<ReceiptVoucherMaster> UpdateReceiptVoucher([FromBody] ReceiptVoucherMaster voucherCompositeView)
        {
            var param1 = _mapper.Map<ReceiptVoucherMaster>(voucherCompositeView);
            var param2 = _mapper.Map<List<ReceiptVoucherDetails>>(voucherCompositeView.ReceiptVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var alloDetails = _mapper.Map<List<Domain.Models.AllocationDetails>>(voucherCompositeView.rvAllocationDetails);
            var xs = _receiptVoucherService.UpdateReceiptVoucher(param1, param3, param2,alloDetails);
            ApiResponse<ReceiptVoucherMaster> apiResponse = new ApiResponse<ReceiptVoucherMaster>
            {
                Valid = true,
                Result = _mapper.Map<ReceiptVoucherMaster>(xs),
                Message = ReceiptVoucherMessage.UpdateReceiptVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteReceiptVoucher")]
        public ApiResponse<int> DeleteReceiptVoucher([FromBody] ReceiptVoucherMaster voucherCompositeView)
        {
            var param1 = voucherCompositeView;
            var param2 = voucherCompositeView.ReceiptVoucherDetails;
            var param3 = voucherCompositeView.AccountsTransactions;
            var xs = _receiptVoucherService.DeleteReceiptVoucher(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = ReceiptVoucherMessage.DeleteReceiptVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_receiptVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetReceiptVouchers")]
        public ApiResponse<List<ReceiptVoucherMaster>> GetAllReceipts()
        {
            ApiResponse<List<ReceiptVoucherMaster>> apiResponse = new ApiResponse<List<ReceiptVoucherMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<ReceiptVoucherMaster>>(_receiptVoucherService.GetReceiptVouchers()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_receiptVoucherService.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSavedReceiptDetails/{id}")]
        public ApiResponse<ReceiptVoucherMaster> GetSavedReceiptDetails(string id)
        {
            ReceiptVoucherMaster receiptVoucher = _receiptVoucherService.GetSavedReceiptDetails(id);
            if (receiptVoucher != null)
            {
                ApiResponse<ReceiptVoucherMaster> apiResponse = new ApiResponse<ReceiptVoucherMaster>
                {
                    Valid = true,
                    Result = receiptVoucher,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }
    }
}
