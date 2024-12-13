using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/master")]
    [Produces("application/json")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICurrencyMasterService currencyMasterService;
        public CurrencyController(ICurrencyMasterService _currencyMasterService, IMapper mapper)
        {

            currencyMasterService = _currencyMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllCurrency")]
        public List<CurrencyMasterViewModel> GetAllCurrency()
        {
       
            return currencyMasterService.GetAllCurrency().Select(k => new CurrencyMasterViewModel
            {
                CurrencyMasterCurrencyId = k.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = k.CurrencyMasterCurrencyName,
                CurrencyMasterCurrencySymbol = k.CurrencyMasterCurrencySymbol,
                CurrencyMasterCurrencyRate = k.CurrencyMasterCurrencyRate,
                CurrencyMasterCurrencyRemarks = k.CurrencyMasterCurrencyRemarks,
                CurrencyMasterCurrencyType = k.CurrencyMasterCurrencyType,
                CurrencyMasterCurrencyUserId = k.CurrencyMasterCurrencyUserId,
                CurrencyMasterCurrencyStatus = k.CurrencyMasterCurrencyStatus,
                CurrencyMasterCurrencyShortName = k.CurrencyMasterCurrencyShortName,
                CurrencyMasterCurrencyDenomination = k.CurrencyMasterCurrencyDenomination,
                CurrencyMasterCurrencyDelStatus=k.CurrencyMasterCurrencyDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllCurrencyById/{id}")]
        public List<CurrencyMasterViewModel> GetAllCurrencyById(int id)
        {
            return currencyMasterService.GetAllCurrencyById(id).Select(k => new CurrencyMasterViewModel
            {

                CurrencyMasterCurrencyId = k.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = k.CurrencyMasterCurrencyName,
                CurrencyMasterCurrencySymbol = k.CurrencyMasterCurrencySymbol,
                CurrencyMasterCurrencyRate = k.CurrencyMasterCurrencyRate,
                CurrencyMasterCurrencyRemarks = k.CurrencyMasterCurrencyRemarks,
                CurrencyMasterCurrencyType = k.CurrencyMasterCurrencyType,
                CurrencyMasterCurrencyUserId = k.CurrencyMasterCurrencyUserId,
                CurrencyMasterCurrencyStatus = k.CurrencyMasterCurrencyStatus,
                CurrencyMasterCurrencyShortName = k.CurrencyMasterCurrencyShortName,
                CurrencyMasterCurrencyDenomination = k.CurrencyMasterCurrencyDenomination,
                CurrencyMasterCurrencyDelStatus = k.CurrencyMasterCurrencyDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertCurrency")]
        public List<CurrencyMasterViewModel> InsertCurrency([FromBody] CurrencyMasterViewModel currencyMaster)
        {
            var result = _mapper.Map<CurrencyMaster>(currencyMaster);
            return currencyMasterService.InsertCurrency(result).Select(k => new CurrencyMasterViewModel
            {
                CurrencyMasterCurrencyId = k.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = k.CurrencyMasterCurrencyName,
                CurrencyMasterCurrencySymbol = k.CurrencyMasterCurrencySymbol,
                CurrencyMasterCurrencyRate = k.CurrencyMasterCurrencyRate,
                CurrencyMasterCurrencyRemarks = k.CurrencyMasterCurrencyRemarks,
                CurrencyMasterCurrencyType = k.CurrencyMasterCurrencyType,
                CurrencyMasterCurrencyUserId = k.CurrencyMasterCurrencyUserId,
                CurrencyMasterCurrencyStatus = k.CurrencyMasterCurrencyStatus,
                CurrencyMasterCurrencyShortName = k.CurrencyMasterCurrencyShortName,
                CurrencyMasterCurrencyDenomination = k.CurrencyMasterCurrencyDenomination,
                CurrencyMasterCurrencyDelStatus = k.CurrencyMasterCurrencyDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateCurrency")]
        public List<CurrencyMasterViewModel> UpdateCurrency([FromBody] CurrencyMasterViewModel currencyMaster)
        {
            var result = _mapper.Map<CurrencyMaster>(currencyMaster);
            return currencyMasterService.UpdateCurrency(result).Select(k => new CurrencyMasterViewModel
            {
                CurrencyMasterCurrencyId = k.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = k.CurrencyMasterCurrencyName,
                CurrencyMasterCurrencySymbol = k.CurrencyMasterCurrencySymbol,
                CurrencyMasterCurrencyRate = k.CurrencyMasterCurrencyRate,
                CurrencyMasterCurrencyRemarks = k.CurrencyMasterCurrencyRemarks,
                CurrencyMasterCurrencyType = k.CurrencyMasterCurrencyType,
                CurrencyMasterCurrencyUserId = k.CurrencyMasterCurrencyUserId,
                CurrencyMasterCurrencyStatus = k.CurrencyMasterCurrencyStatus,
                CurrencyMasterCurrencyShortName = k.CurrencyMasterCurrencyShortName,
                CurrencyMasterCurrencyDenomination = k.CurrencyMasterCurrencyDenomination,
                CurrencyMasterCurrencyDelStatus = k.CurrencyMasterCurrencyDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteCurrency")]
        public List<CurrencyMasterViewModel> DeleteCurrency([FromBody] CurrencyMasterViewModel currencyMaster)
        {
            var result = _mapper.Map<CurrencyMaster>(currencyMaster);
            return currencyMasterService.DeleteCurrency(result).Select(k => new CurrencyMasterViewModel
            {
                CurrencyMasterCurrencyId = k.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = k.CurrencyMasterCurrencyName,
                CurrencyMasterCurrencySymbol = k.CurrencyMasterCurrencySymbol,
                CurrencyMasterCurrencyRate = k.CurrencyMasterCurrencyRate,
                CurrencyMasterCurrencyRemarks = k.CurrencyMasterCurrencyRemarks,
                CurrencyMasterCurrencyType = k.CurrencyMasterCurrencyType,
                CurrencyMasterCurrencyUserId = k.CurrencyMasterCurrencyUserId,
                CurrencyMasterCurrencyStatus = k.CurrencyMasterCurrencyStatus,
                CurrencyMasterCurrencyShortName = k.CurrencyMasterCurrencyShortName,
                CurrencyMasterCurrencyDenomination = k.CurrencyMasterCurrencyDenomination,
                CurrencyMasterCurrencyDelStatus = k.CurrencyMasterCurrencyDelStatus
            }).ToList();
        }
    }
}