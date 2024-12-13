using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System.Security.Permissions;
using Microsoft.CodeAnalysis;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/bankreceipt")]
    [Produces("application/json")]
    [ApiController]
    public class BankReceiptVoucherController : ControllerBase
    {
        private IBankReceiptVoucherService _bankReceiptVoucherService;
        private readonly IMapper _mapper;
        private IRepository<MasterAccountsTable> _masterAccountsRepository;
        private IRepository<PdcDetails> _pDCDetailsRepository;

        public BankReceiptVoucherController(IBankReceiptVoucherService bankReceiptVoucherService, IMapper mapper, IRepository<MasterAccountsTable> masterAccountsRepository, IRepository<PdcDetails> pDCDetailsRepository)
        {
            _bankReceiptVoucherService = bankReceiptVoucherService;
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
                return Ok(_bankReceiptVoucherService.GenerateVoucherNo(null));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertBankReceiptVoucher")]
        public ApiResponse<BankReceiptVoucher> InsertBankReceiptVoucher([FromBody] BankReceiptVoucher voucherCompositeView)
        {
            ApiResponse<BankReceiptVoucher> apiResponse = new ApiResponse<BankReceiptVoucher>();
            var param1 = _mapper.Map<BankReceiptVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankReceiptVoucherDetails>>(voucherCompositeView.BankReceiptVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var allocaDetails = voucherCompositeView.AllocationDetails;
            var xs = _bankReceiptVoucherService.InsertBankReceiptVoucher(param1, param3, param2, allocaDetails);
            apiResponse = new ApiResponse<BankReceiptVoucher>
            {
                Valid = true,
                Result = _mapper.Map<BankReceiptVoucher>(xs),
                Message = BankReceiptVoucherMessage.SaveReceiptVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateBankReceiptVoucher")]
        public ApiResponse<BankReceiptVoucher> UpdateBankReceiptVoucher([FromBody] BankReceiptVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<BankReceiptVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankReceiptVoucherDetails>>(voucherCompositeView.BankReceiptVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var brAllo = voucherCompositeView.AllocationDetails;
            var xs = _bankReceiptVoucherService.UpdateBankReceiptVoucher(param1, param3, param2,brAllo);
            
            var apiResponse = new ApiResponse<BankReceiptVoucher>()
            {
                Valid = true,
                Result = _mapper.Map<BankReceiptVoucher>(xs),
                Message = BankReceiptVoucherMessage.UpdateReceiptVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteBankReceiptVoucher")]
        public ApiResponse<int> DeleteBankReceiptVoucher([FromBody] BankReceiptVoucher voucherCompositeView)
        {
            var param1 = _mapper.Map<BankReceiptVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<BankReceiptVoucherDetails>>(voucherCompositeView.BankReceiptVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _bankReceiptVoucherService.DeleteBankReceiptVoucher(param1, param3, param2);
  
            var  apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = BankReceiptVoucherMessage.DeleteReceiptVoucher
            };
           
            return apiResponse;

        }

        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        {

            var apiResponse = new ApiResponse<List<AccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<AccountsTransactions>>(_bankReceiptVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetBankReceiptVouchers")]
        public ApiResponse<List<BankReceiptVoucher>> GetAllBankPayments()
        {

            var apiResponse = new ApiResponse<List<BankReceiptVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<BankReceiptVoucher>>(_bankReceiptVoucherService.GetBankReceiptVouchers()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedBankReceiptDetails/{id}")]
        public ApiResponse<BankReceiptVoucher> GetSavedBankPaymentDetails(string id)
        {
            BankReceiptVoucher bankReceiptVoucher = _bankReceiptVoucherService.GetSavedBankReceiptDetails(id);
            var apiResposne = new ApiResponse<BankReceiptVoucher>
            {
                Valid = true,
                Result = bankReceiptVoucher,
                Message = ""
            };
            
            
            return apiResposne;
        }
    }
}