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
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/PurchaseJournal")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseJournalController : ControllerBase
    {
        private IPurchaseJournalService _purchaseJournalService;
        private IRepository<CostCenterMaster> costcenterrepository;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<JobMaster> jobrepository;
        private IChartofAccountsService chartofAccountsService;
        private readonly IMapper _mapper;
        public PurchaseJournalController(IPurchaseJournalService purchaseJournalService, IMapper mapper, IRepository<CostCenterMaster> _countryrepository
            , IRepository<JobMaster> _jobrepository, IRepository<CurrencyMaster> _currencyrepository, IChartofAccountsService _chartofAccountsService)
        {
            _purchaseJournalService = purchaseJournalService;
            _mapper = mapper; jobrepository = _jobrepository;
            costcenterrepository = _countryrepository; currencyrepository = _currencyrepository; chartofAccountsService = _chartofAccountsService;
        }
        [HttpPost]
        [Route("InsertPurchaseJournal")]
        public ApiResponse<PurchaseJournal> InsertPurchaseJournal([FromBody] PurchaseJournal purchaseJournal)
        {
            
            var param1 = _mapper.Map<PurchaseJournal>(purchaseJournal);
            var param2 = _mapper.Map<List<PurchaseJournalDetails>>(purchaseJournal.PurchaseJournalDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(purchaseJournal.AccountsTransactions);

            var xs = _purchaseJournalService.InsertPurchaseJournal(param1,param3,param2);
            var apiResponse = new ApiResponse<PurchaseJournal>()
            {
                Valid = true,
                Result = _mapper.Map<PurchaseJournal>(xs),
                Message = PurchaseJournalMessage.SaveVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("UpdatePurchaseJournal")]
        public ApiResponse<PurchaseJournal> UpdatePurchaseJournal([FromBody] PurchaseJournal purchaseJournal)
        {

            var param1 = _mapper.Map<PurchaseJournal>(purchaseJournal);
            var param2 = _mapper.Map<List<PurchaseJournalDetails>>(purchaseJournal.PurchaseJournalDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(purchaseJournal.AccountsTransactions);
            var xs = _purchaseJournalService.UpdatePurchaseJournal(param1, param3, param2);
            var apiResponse = new ApiResponse<PurchaseJournal>()
            {
                Valid = true,
                Result = _mapper.Map<PurchaseJournal>(xs),
                Message = PurchaseJournalMessage.SaveVoucher
            };

            return apiResponse;

        }

    
        [HttpPost]
        [Route("DeletePurchaseJournal")]
        public ApiResponse<int> DeletePurchaseJournal([FromBody] PurchaseJournalViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseJournal>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseJournalDetails>>(voucherCompositeView.PurchaseJournalDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _purchaseJournalService.DeletePurchaseJournal(param1, param3, param2);
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseJournalMessage.DeleteVoucher
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
                Result = _mapper.Map<List<AccountsTransactions>>(_purchaseJournalService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetPurchaseJournal")]
        public ApiResponse<List<PurchaseJournal>> GetAllPurchaseJournal()
        {
            ApiResponse<List<PurchaseJournal>> apiResponse = new ApiResponse<List<PurchaseJournal>>
            {
                Valid = true,
                Result = _mapper.Map<List<PurchaseJournal>>(_purchaseJournalService.GetPurchaseJournal()),
                Message = ""
            };
            return apiResponse;

        }
        [HttpGet]
        [Route("GetSavedPurchaseJournalDetails/{id}")]
        public ApiResponse<PurchaseJournal> GetSavedPurchaseJournalDetails(string id)
        {
            PurchaseJournal purchaseJournal = _purchaseJournalService.GetSavedPurchaseJournalDetails(id);

            if (purchaseJournal != null)
            {
                ApiResponse<PurchaseJournal> apiResponse = new ApiResponse<PurchaseJournal>
                {
                    Valid = true,
                    Result = purchaseJournal,
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
                Message = PurchaseJournalMessage.VoucherNumberExist
            };
            var x = _purchaseJournalService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }

        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objresponse = new ResponseInfo();

            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true && a.MaAccType == "A").Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();
            var costcenterMasters = costcenterrepository.GetAsQueryable().AsNoTracking().Where(a => a.CostCenterMasterCostCenterDelStatus != true).Select(c => new
            {
                c.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName=c.CostCenterMasterCostCenterName.Trim(),
            }).ToList();
            var jobMasters = jobrepository.GetAsQueryable().AsNoTracking().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                JobMasterJobName=c.JobMasterJobName.Trim(),
            }).ToList();
            var currencyMasters = currencyrepository.GetAsQueryable().AsNoTracking().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = c.CurrencyMasterCurrencyName.Trim(),
                c.CurrencyMasterCurrencyRate
            }).ToList();

            objresponse.ResultSet = new
            {
                costcenterMasters = costcenterMasters,
                masterAccountsTables = masterAccountsTables,
                jobMasters = jobMasters,
                currencyMasters = currencyMasters,

            };
            return objresponse;
        }

    }
}

