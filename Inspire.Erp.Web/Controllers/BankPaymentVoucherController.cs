using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Account.Implementations;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/bankPayment")]
    [Produces("application/json")]
    [ApiController]
    public class BankPaymentVoucherController : ControllerBase
    {
        private IBankPaymentVoucherService _bankPaymentVoucherService;
        private IRepository<MasterAccountsTable> _masterAccountsRepository;
        private IRepository<PdcDetails> _pDCDetailsRepository;
        private readonly IMapper _mapper;
        public BankPaymentVoucherController(IBankPaymentVoucherService bankPaymentVoucherService, IMapper mapper, IRepository<MasterAccountsTable> masterAccountsRepository, IRepository<PdcDetails> pDCDetailsRepository)
        {
            _bankPaymentVoucherService = bankPaymentVoucherService;
            _mapper = mapper;
            _masterAccountsRepository = masterAccountsRepository;
            _pDCDetailsRepository = pDCDetailsRepository;
        }
        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_bankPaymentVoucherService.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertBankPaymentVoucher")]
        public ApiResponse<BankPaymentVoucher> InsertBankPaymentVoucher([FromBody] BankPaymentVoucher voucherCompositeView)
        {
            ApiResponse<BankPaymentVoucher> apiResponse = new ApiResponse<BankPaymentVoucher>();
            var param1 = _mapper.Map<BankPaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankPaymentVoucherDetails>>(voucherCompositeView.BankPaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var allocaDtls = voucherCompositeView.AllocationDetails;
            var xs = _bankPaymentVoucherService.InsertBankPaymentVoucher(param1, param3, param2,allocaDtls);
            apiResponse = new ApiResponse<BankPaymentVoucher>
            {
                Valid = true,
                Result = _mapper.Map<BankPaymentVoucher>(xs),
                Message = BankPaymentVoucherMessage.SaveVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateBankPaymentVoucher")]
        public ApiResponse<BankPaymentVoucher> UpdateBankPaymentVoucher([FromBody] BankPaymentVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<BankPaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankPaymentVoucherDetails>>(voucherCompositeView.BankPaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var allocaDtls = voucherCompositeView.AllocationDetails;
            var xs = _bankPaymentVoucherService.UpdateBankPaymentVoucher(param1, param3, param2, allocaDtls );

            ApiResponse<BankPaymentVoucher> apiResponse = new ApiResponse<BankPaymentVoucher>
            {
                Valid = true,
                Result = _mapper.Map<BankPaymentVoucher>(xs),
                Message = BankPaymentVoucherMessage.UpdateVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteBankPaymentVoucher")]
        public ApiResponse<int> DeleteBankPaymentVoucher([FromBody] BankPaymentVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<BankPaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankPaymentVoucherDetails>>(voucherCompositeView.BankPaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _bankPaymentVoucherService.DeleteBankPaymentVoucher(param1, param3, param2);

            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = BankPaymentVoucherMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_bankPaymentVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetBankPaymentVouchers")]
        public ApiResponse<List<BankPaymentVoucher>> GetAllBankPayments()
        {
            
            ApiResponse<List<BankPaymentVoucher>> apiResponse = new ApiResponse<List<BankPaymentVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<BankPaymentVoucher>>(_bankPaymentVoucherService.GetBankPaymentVouchers()),
               Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedBankPaymentDetails/{id}")]
        public ApiResponse<BankPaymentVoucher> GetSavedBankPaymentDetails(string id)
        {
            BankPaymentVoucher bankPaymentVoucher = _bankPaymentVoucherService.GetSavedBankPaymentDetails(id);
            if (bankPaymentVoucher != null)
            {
                ApiResponse<BankPaymentVoucher> apiResponse = new ApiResponse<BankPaymentVoucher>
                {
                    Valid = true,
                    Result = bankPaymentVoucher,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }

    }
}