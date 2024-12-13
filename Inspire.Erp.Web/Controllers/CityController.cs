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
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICityMasterService cityMasterService;
        public CityController(ICityMasterService _countryMasterService, IMapper mapper)
        {

            cityMasterService = _countryMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllCity")]
        public List<CityMasterViewModel> GetAllCity()
        {

            return cityMasterService.GetAllCity().Select(k => new CityMasterViewModel
            {

                CityMasterCityId = k.CityMasterCityId,
                CityMasterCityCountryId = k.CityMasterCityCountryId,
                CityMasterCityName = k.CityMasterCityName,
                CityMasterCityDeleted = k.CityMasterCityDeleted,
                CityMasterCityStatus = k.CityMasterCityStatus,
                CityMasterCityDelStatus= k.CityMasterCityDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllCityById/{id}")]
        public List<CityMasterViewModel> GetAllCityById(int id)
        {
            return cityMasterService.GetAllCityById(id).Select(k => new CityMasterViewModel
            {

                CityMasterCityId = k.CityMasterCityId,
                CityMasterCityCountryId = k.CityMasterCityCountryId,
                CityMasterCityName = k.CityMasterCityName,
                CityMasterCityDeleted = k.CityMasterCityDeleted,
                CityMasterCityStatus = k.CityMasterCityStatus,
                CityMasterCityDelStatus = k.CityMasterCityDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertCity")]
        public List<CityMasterViewModel> InsertCity([FromBody] CityMasterViewModel cityMaster)
        {
            
            var result = _mapper.Map<CityMaster>(cityMaster);
            return cityMasterService.InsertCity(result).Select(k => new CityMasterViewModel
            {
                CityMasterCityId = k.CityMasterCityId,
                CityMasterCityCountryId = k.CityMasterCityCountryId,
                CityMasterCityName = k.CityMasterCityName,
                CityMasterCityDeleted = k.CityMasterCityDeleted,
                CityMasterCityStatus = k.CityMasterCityStatus,
                CityMasterCityDelStatus = k.CityMasterCityDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("UpdateCity")]
        public List<CityMasterViewModel> UpdateCity([FromBody] CityMasterViewModel cityMaster)
        {
            var result = _mapper.Map<CityMaster>(cityMaster);
            return cityMasterService.UpdateCity(result).Select(k => new CityMasterViewModel
            {
                CityMasterCityId = k.CityMasterCityId,
                CityMasterCityCountryId = k.CityMasterCityCountryId,
                CityMasterCityName = k.CityMasterCityName,
                CityMasterCityDeleted = k.CityMasterCityDeleted,
                CityMasterCityStatus = k.CityMasterCityStatus,
                CityMasterCityDelStatus = k.CityMasterCityDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("DeleteCity")]
        public List<CityMasterViewModel> DeleteCity([FromBody] CityMasterViewModel cityMaster)
        {
            var result = _mapper.Map<CityMaster>(cityMaster);
            return cityMasterService.DeleteCity(result).Select(k => new CityMasterViewModel
            {
                CityMasterCityId = k.CityMasterCityId,
                CityMasterCityCountryId = k.CityMasterCityCountryId,
                CityMasterCityName = k.CityMasterCityName,
                CityMasterCityDeleted = k.CityMasterCityDeleted,
                CityMasterCityStatus = k.CityMasterCityStatus,
                CityMasterCityDelStatus = k.CityMasterCityDelStatus
            }).ToList();
        }
    }
}