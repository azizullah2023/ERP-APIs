using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/payment")]
    [Produces("application/json")]
    [ApiController]
    public class PaymentVoucherController : ControllerBase
    {
        private IPaymentVoucherService _paymentVoucherService;
        private readonly IMapper _mapper;
        private IRepository<CostCenterMaster> costcenterrepository;
        private IRepository<JobMaster> jobrepository; private IRepository<GeneralSettings> _generalSettingsRepo;
        private IRepository<CurrencyMaster> currencyrepository;
        private IChartofAccountsService chartofAccountsService;
        public PaymentVoucherController(IPaymentVoucherService paymentVoucherService, IMapper mapper, IRepository<CostCenterMaster> _countryrepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService
            , IRepository<GeneralSettings> generalSettingsRepo)
        {
            _paymentVoucherService = paymentVoucherService;
            _mapper = mapper;
            costcenterrepository = _countryrepository;
            jobrepository = _jobrepository; _generalSettingsRepo = generalSettingsRepo;
            currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }
        [HttpPost]
        [Route("InsertPaymentVoucher")]
        public ApiResponse<PaymentVoucher> InsertPaymentVoucher([FromBody] PaymentVoucher voucherCompositeView)
        {
            ApiResponse<PaymentVoucher> apiResponse = new ApiResponse<PaymentVoucher>();
            var param1 = _mapper.Map<PaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<PaymentVoucherDetails>>(voucherCompositeView.PaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var payAlloca = voucherCompositeView.allocationDetails;
            var xs = _paymentVoucherService.InsertPaymentVoucher(param1, param3, param2, payAlloca);
            apiResponse = new ApiResponse<PaymentVoucher>
            {
                Valid = true,
                Result = xs,
                Message = PaymentVoucherMessage.SaveVoucher
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("UpdatePaymentVoucher")]
        public ApiResponse<PaymentVoucher> UpdatePaymentVoucher([FromBody] PaymentVoucher voucherCompositeView)
        {

            var param1 = _mapper.Map<PaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<PaymentVoucherDetails>>(voucherCompositeView.PaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var payAlloca = voucherCompositeView.allocationDetails;
            var xs = _paymentVoucherService.UpdatePaymentVoucher(param1, param3, param2, payAlloca);
            ApiResponse<PaymentVoucher> apiResponse = new ApiResponse<PaymentVoucher>
            {
                Valid = true,
                Result = xs,
                Message = PaymentVoucherMessage.UpdateVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("DeletePaymentVoucher")]
        public ApiResponse<int> DeletePaymentVoucher([FromBody] PaymentVoucherViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PaymentVoucher>(voucherCompositeView);
            var param2 = _mapper.Map<List<PaymentVoucherDetails>>(voucherCompositeView.PaymentVoucherDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _paymentVoucherService.DeletePaymentVoucher(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PaymentVoucherMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_paymentVoucherService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetPaymentVouchers")]
        public ApiResponse<List<PaymentVoucher>> GetAllPayments()
        {
            ApiResponse<List<PaymentVoucher>> apiResponse = new ApiResponse<List<PaymentVoucher>>
            {
                Valid = true,
                Result = _mapper.Map<List<PaymentVoucher>>(_paymentVoucherService.GetPaymentVouchers()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedPaymentDetails/{id}")]
        public ApiResponse<PaymentVoucher> GetSavedPaymentDetails(string id)
        {
            PaymentVoucher paymentVoucher = _paymentVoucherService.GetSavedPaymentDetails(id);
            if (paymentVoucher != null)
            {
                ApiResponse<PaymentVoucher> apiResponse = new ApiResponse<PaymentVoucher>
                {
                    Valid = true,
                    Result = paymentVoucher,
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
                Message = PaymentVoucherMessage.VoucherAlreadyExist
            };
            var x = _paymentVoucherService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }
            return apiResponse;
        }

        [HttpGet]
        [Route("GenerateVoucherNo")]
        public IActionResult GenerateVoucherNo()
        {
            try
            {
                return Ok(_paymentVoucherService.GenerateVoucherNo());
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllocationDetails")]
        public IActionResult GetAllocationDetails(string accountNo, string voucherNo)
        {
            try
            {
                return Ok(_paymentVoucherService.GetAllocationDetails(accountNo, voucherNo));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserTracking")]
        public IActionResult GetUserTracking(string voucherNo)
        {
            try
            {
                return Ok(_paymentVoucherService.GetUserTracking(voucherNo));
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();

            var costcenterMasters = costcenterrepository.GetAsQueryable().Where(a => a.CostCenterMasterCostCenterDelStatus != true).Select(c => new
            {
                c.CostCenterMasterCostCenterId,
                c.CostCenterMasterCostCenterName,
            }).ToList();
            var jobMasters = jobrepository.GetAsQueryable().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                c.JobMasterJobName,
            }).ToList();
            var currencyMasters = currencyrepository.GetAsQueryable().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                c.CurrencyMasterCurrencyName,
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true && a.MaAccType == "A").Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();

            objectresponse.ResultSet = new
            {
                costcenterMasters = costcenterMasters,
                masterAccountsTables = masterAccountsTables,
                jobMasters = jobMasters,
                currencyMasters = currencyMasters,

            };
            return objectresponse;
        }
    }
}