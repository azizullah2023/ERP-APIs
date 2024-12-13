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
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICountryMasterService countryMasterService;
        public CountryController(ICountryMasterService _countryMasterService, IMapper mapper)
        {

            countryMasterService = _countryMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllCountry")]
        public List<CountryMasterViewModel> GetAllCountry()
        {

            return countryMasterService.GetAllCountry().Select(k => new CountryMasterViewModel
            {

                CountryMasterCountryId = k.CountryMasterCountryId,
                CountryMasterCountryName = k.CountryMasterCountryName,
                CountryMasterCountryIsdCode = k.CountryMasterCountryIsdCode,
                CountryMasterCountryUserId = k.CountryMasterCountryUserId,
                CountryMasterCountryStatus = k.CountryMasterCountryStatus,
                CountryMasterCountryAmount = k.CountryMasterCountryAmount,
                CountryMasterCountryDelStatus= k.CountryMasterCountryDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllCountryById/{id}")]
        public List<CountryMasterViewModel> GetAllCountryById(int id)
        {
            return countryMasterService.GetAllCountryById(id).Select(k => new CountryMasterViewModel
            {

                CountryMasterCountryId = k.CountryMasterCountryId,
                CountryMasterCountryName = k.CountryMasterCountryName,
                CountryMasterCountryIsdCode = k.CountryMasterCountryIsdCode,
                CountryMasterCountryUserId = k.CountryMasterCountryUserId,
                CountryMasterCountryStatus = k.CountryMasterCountryStatus,
                CountryMasterCountryAmount = k.CountryMasterCountryAmount,
                CountryMasterCountryDelStatus = k.CountryMasterCountryDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertCountry")]
        public List<CountryMasterViewModel> InsertCountry([FromBody] CountryMasterViewModel countryMaster)
        {
            var result = _mapper.Map<CountryMaster>(countryMaster);
            return countryMasterService.InsertCountry(result).Select(k => new CountryMasterViewModel
            {
                CountryMasterCountryId = k.CountryMasterCountryId,
                CountryMasterCountryName = k.CountryMasterCountryName,
                CountryMasterCountryIsdCode = k.CountryMasterCountryIsdCode,
                CountryMasterCountryUserId = k.CountryMasterCountryUserId,
                CountryMasterCountryStatus = k.CountryMasterCountryStatus,
                CountryMasterCountryAmount = k.CountryMasterCountryAmount,
                CountryMasterCountryDelStatus = k.CountryMasterCountryDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateCountry")]
        public List<CountryMasterViewModel> UpdateCountry([FromBody] CountryMasterViewModel countryMaster)
        {
            var result = _mapper.Map<CountryMaster>(countryMaster);
            return countryMasterService.UpdateCountry(result).Select(k => new CountryMasterViewModel
            {
                CountryMasterCountryId = k.CountryMasterCountryId,
                CountryMasterCountryName = k.CountryMasterCountryName,
                CountryMasterCountryIsdCode = k.CountryMasterCountryIsdCode,
                CountryMasterCountryUserId = k.CountryMasterCountryUserId,
                CountryMasterCountryStatus = k.CountryMasterCountryStatus,
                CountryMasterCountryAmount = k.CountryMasterCountryAmount,
                CountryMasterCountryDelStatus = k.CountryMasterCountryDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteCountry")]
        public List<CountryMasterViewModel> DeleteCountry([FromBody] CountryMasterViewModel countryMaster)
        {
            var result = _mapper.Map<CountryMaster>(countryMaster);
            return countryMasterService.DeleteCountry(result).Select(k => new CountryMasterViewModel
            {
                CountryMasterCountryId = k.CountryMasterCountryId,
                CountryMasterCountryName = k.CountryMasterCountryName,
                CountryMasterCountryIsdCode = k.CountryMasterCountryIsdCode,
                CountryMasterCountryUserId = k.CountryMasterCountryUserId,
                CountryMasterCountryStatus = k.CountryMasterCountryStatus,
                CountryMasterCountryAmount = k.CountryMasterCountryAmount,
                CountryMasterCountryDelStatus = k.CountryMasterCountryDelStatus
            }).ToList();
        }
    }
}