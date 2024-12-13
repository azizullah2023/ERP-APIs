using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Web.ViewModels;

namespace Inspire.Erp.Web.Controllers.Settings
{
    [Route("api/accountssettings")]
    [Produces("application/json")]
    [ApiController]
    public class AccountsSettingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IAccountsSettingsService _accountssettingSvc;
        public AccountsSettingsController(IAccountsSettingsService accountssettingSvc, IMapper mapper)
        {
            _accountssettingSvc = accountssettingSvc;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult GetAccountsSetting()
        {
            return Ok(_accountssettingSvc.GetAccountsSettings());
        }

        [HttpGet]
        [Route("GetAllAccountsbyKey/{key}")]
        public ApiResponse<List<AccountsSettingsViewModel>> GetAccountsSettingbyKey(string key)
        {
            List<AccountSettings> listaccountsSettings = _accountssettingSvc.GetAccountsSettingsbyKey(key).ToList();
            if (listaccountsSettings != null)
            {
                var x = _mapper.Map<List<AccountsSettingsViewModel>>(listaccountsSettings);

                ApiResponse<List<AccountsSettingsViewModel>> apiResponse = new ApiResponse<List<AccountsSettingsViewModel>>
                {
                    Valid = true,
                    Result = x,
                    Message = ""
                };

                return apiResponse;
            }
            return null;
            
        }

        [HttpPost]
        [Route("updateAccountsSettings")]
        public ApiResponse<List<AccountSettings>> UpdateAccountsSetting([FromBody] List<AccountSettings> accountsSettings)
        {
            bool result = _accountssettingSvc.updateAccountsSetting(accountsSettings);
            ApiResponse<List<AccountSettings>> apiResponse = new ApiResponse<List<AccountSettings>>
            {
                Valid = result,
                Result = _accountssettingSvc.GetAccountsSettings().ToList(),
                Message = ""
            };

            return apiResponse;
        }

    }
}
