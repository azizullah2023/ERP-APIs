using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;

namespace Inspire.Erp.Web.Controllers.Settings
{
    [Route("api/settings")]
    [Produces("application/json")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IErpSettings _settingSvc;
        private IRepository<GeneralSettings> _generalSettingsRepo;
        public SettingsController(IErpSettings settingSvc, IMapper mapper, IRepository<GeneralSettings> generalSettingsRepo)
        {
            _settingSvc = settingSvc;
            _mapper = mapper;
            _generalSettingsRepo = generalSettingsRepo;
        }
        [HttpGet]
        [Route("general")]
        public IActionResult GetGeneralSetting()
        {
            return Ok(_settingSvc.GetGeneralSettings());
        }

        [HttpGet]
        [Route("program")]
        public IActionResult GetProgramSetting()
        {
            return Ok(_settingSvc.GetProgramSettings());
        }

        [HttpGet]
        [Route("GetProgramSettingbyKey/{key}")]
        public ApiResponse<List<ProgramSettingsViewModel>> GetProgramSettingbyKey(string key)
        {
            List<ProgramSettings> listprogramSettings = _settingSvc.GetProgramSettingsbyKey(key).ToList();
            if (listprogramSettings != null)
            {
                var x = _mapper.Map<List<ProgramSettingsViewModel>>(listprogramSettings);

                ApiResponse<List<ProgramSettingsViewModel>> apiResponse = new ApiResponse<List<ProgramSettingsViewModel>>
                {
                    Valid = true,
                    Result = x,
                    Message = ""
                };

                return apiResponse;
            }
            return null;

        }

        [HttpGet]
        [Route("GetGeneralSettingbyKey/{key}")]
        public ApiResponse<List<GeneralSettingsViewModel>> GetGeneralSettingbyKey(string key)
        {
            List<GeneralSettings> listgeneralSettings = _settingSvc.GetGeneralSettingsbyKey(key).ToList();
            if (listgeneralSettings != null)
            {
                var x = _mapper.Map<List<GeneralSettingsViewModel>>(listgeneralSettings);

                ApiResponse<List<GeneralSettingsViewModel>> apiResponse = new ApiResponse<List<GeneralSettingsViewModel>>
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
        [Route("updateProgram")]
        public ApiResponse<List<ProgramSettings>> UpdateProgramSetting([FromBody] List<ProgramSettings> programSettings)
        {
            bool result = _settingSvc.updateProgramSettings(programSettings);
            ApiResponse<List<ProgramSettings>> apiResponse = new ApiResponse<List<ProgramSettings>>
            {
                Valid = result,
                Result = _settingSvc.GetProgramSettings().ToList(),
                Message = ""
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("updateGeneral")]
        public ApiResponse<List<GeneralSettings>> UpdateGeneralSetting([FromBody] List<GeneralSettings> generalSettings)
        {
            bool result = _settingSvc.updateGeneralSettings(generalSettings);
            ApiResponse<List<GeneralSettings>> apiResponse = new ApiResponse<List<GeneralSettings>>
            {
                Valid = result,
                Result = _settingSvc.GetGeneralSettings().ToList(),
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();

            var GeneralSettings = _generalSettingsRepo.GetAsQueryable().Where(k => k.GeneralSettingsKeyValue.Trim() == "DEFAULTCURRENCY" || k.GeneralSettingsKeyValue.Trim() == "VAT_Calculation" ||
            k.GeneralSettingsKeyValue.Trim() == "DEFAULT_LOCATION" || k.GeneralSettingsKeyValue.Trim() == "DEFAULT_COST_CENTER" || k.GeneralSettingsKeyValue.Trim() == "DEFAULTINTIALACCOUNT" || k.GeneralSettingsKeyValue.Trim() == "DEFAULT_Type"
            || k.GeneralSettingsKeyValue.Trim() == "HIDE_JOBCOST"
            ).Select(c => new
            {
                c.GeneralSettingsId,
                c.GeneralSettingsTextValue,
                GeneralSettingsKeyValue = c.GeneralSettingsKeyValue.Trim(),
                c.GeneralSettingsBoolValue
            }).ToList();

            objectresponse.ResultSet = new
            {
                GeneralSettings = GeneralSettings,
            };
            return objectresponse;
        }
    }
}
