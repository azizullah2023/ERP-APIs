using Inspire.Erp.Application.Settings.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Inspire.Erp.Web.ViewModels.Settings;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Settings
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OptionsMasterController : ControllerBase

    {
        private readonly IMapper _mapper;
        private IOptionsSettings _optionsSvc;
        public OptionsMasterController(IOptionsSettings optionsSvc, IMapper mapper)
        {
            _optionsSvc = optionsSvc;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetOptionSetting")]
        public IActionResult GetOptionSetting(string search)
        {
            return Ok(_optionsSvc.GetOptionsSettings(search));
        }


        [HttpGet]
        [Route("getAllOPtionSettings")]
        public async Task<IActionResult> getAllOPtionSettings()
        {
            return Ok(await _optionsSvc.GetAllOptionSettings());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("GetOptionsSettingbyKey/{key}")]
        public async Task<IActionResult> GetOptionsSettingbyKey(string key)
        {
            return Ok(await _optionsSvc.GetOptionsSettingsbyKey(key));
        }

        //[HttpPost]
        //[Route("updateOptions")]
        //public async Task<IActionResult> UpdateOptionsSetting([FromBody] List<OptionsMaster> optionsSettings)
        //{
        //    bool result = _optionsSvc.updateOptionsSettings(optionsSettings);
        //    return Ok(await _optionsSvc.GetOptionsSettings(""));
        //}


        [HttpPost]
        [Route("updateOptions")]
        public ApiResponse<List<OptionsMaster>> UpdateOptionsSetting([FromBody] List<OptionsMaster> optionsSettings)
        {
            bool result = _optionsSvc.updateOptionsSettings(optionsSettings);
            ApiResponse<List<OptionsMaster>> apiResponse = new ApiResponse<List<OptionsMaster>>
            {
                Valid = result,
                Result = _optionsSvc.GetOptionsSettings("").Result.ToList(),
                Message = ""
            };
            return apiResponse;
        }



        [HttpPost]
        [Route("ImportOptionSettingsFromFile")]
        public IActionResult ImportOptionSettingsFromFile()
        {
            ReadingFiles<OptionsMaster> readingFiles = new ReadingFiles<OptionsMaster>();
            var options = readingFiles.ReadFile("OptionsSettings.json");
            bool result = _optionsSvc.ImportOptionsSettingsFromFile(options);
            return Ok(result);
        }
    }
}

