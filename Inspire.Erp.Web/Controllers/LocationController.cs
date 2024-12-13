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
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ILocationMasterService locationMasterService;
        public LocationController(ILocationMasterService _locationMasterService, IMapper mapper)
        {

            locationMasterService = _locationMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllLocation")]
        public List<LocationMasterViewModel> GetAllLocation()
        {

           return locationMasterService.GetAllLocation().Select(k => new LocationMasterViewModel
            {

                LocationMasterLocationId = k.LocationMasterLocationId,
                LocationMasterLocationName = k.LocationMasterLocationName,
                LocationMasterLocationAddress = k.LocationMasterLocationAddress,
                LocationMasterLocationDeleted = k.LocationMasterLocationDeleted,
                LocationMasterLocationStatus = k.LocationMasterLocationStatus,
                LocationMasterLocationTelephone = k.LocationMasterLocationTelephone,
                LocationMasterLocationFax = k.LocationMasterLocationFax,
                LocationMasterLocationEmail = k.LocationMasterLocationEmail,
                LocationMasterLocationCashAccount = k.LocationMasterLocationCashAccount,
                LocationMasterLocationCostCenter = k.LocationMasterLocationCostCenter,
                LocationMasterLocationDelStatus= k.LocationMasterLocationDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllFinancialPeriod")]
        public List<FinancialPeriods> GetAllFinancialPeriod()
        {

            return locationMasterService.GetAllFinancialPeriod().ToList();
        }


        [HttpGet]
        [Route("GetAllLocationById/{id}")]
        public List<LocationMasterViewModel> GetAllLocationById(int id)
        {
            return locationMasterService.GetAllLocationById(id).Select(k => new LocationMasterViewModel
            {

                LocationMasterLocationId = k.LocationMasterLocationId,
                LocationMasterLocationName = k.LocationMasterLocationName,
                LocationMasterLocationAddress = k.LocationMasterLocationAddress,
                LocationMasterLocationDeleted = k.LocationMasterLocationDeleted,
                LocationMasterLocationStatus = k.LocationMasterLocationStatus,
                LocationMasterLocationTelephone = k.LocationMasterLocationTelephone,
                LocationMasterLocationFax = k.LocationMasterLocationFax,
                LocationMasterLocationEmail = k.LocationMasterLocationEmail,
                LocationMasterLocationCashAccount = k.LocationMasterLocationCashAccount,
                LocationMasterLocationCostCenter = k.LocationMasterLocationCostCenter,
                LocationMasterLocationDelStatus = k.LocationMasterLocationDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertLocation")]
        public List<LocationMasterViewModel> InsertLocation([FromBody] LocationMasterViewModel locationMaster)
        {
            var result = _mapper.Map<LocationMaster>(locationMaster);
            return locationMasterService.InsertLocation(result).Select(k => new LocationMasterViewModel
            {
                LocationMasterLocationId = k.LocationMasterLocationId,
                LocationMasterLocationName = k.LocationMasterLocationName,
                LocationMasterLocationAddress = k.LocationMasterLocationAddress,
                LocationMasterLocationDeleted = k.LocationMasterLocationDeleted,
                LocationMasterLocationStatus = k.LocationMasterLocationStatus,
                LocationMasterLocationTelephone = k.LocationMasterLocationTelephone,
                LocationMasterLocationFax = k.LocationMasterLocationFax,
                LocationMasterLocationEmail = k.LocationMasterLocationEmail,
                LocationMasterLocationCashAccount = k.LocationMasterLocationCashAccount,
                LocationMasterLocationCostCenter = k.LocationMasterLocationCostCenter,
                LocationMasterLocationDelStatus = k.LocationMasterLocationDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateLocation")]
        public List<LocationMasterViewModel> UpdateLocation([FromBody] LocationMasterViewModel locationMaster)
        {
            var result = _mapper.Map<LocationMaster>(locationMaster);
            return locationMasterService.UpdateLocation(result).Select(k => new LocationMasterViewModel
            {
                LocationMasterLocationId = k.LocationMasterLocationId,
                LocationMasterLocationName = k.LocationMasterLocationName,
                LocationMasterLocationAddress = k.LocationMasterLocationAddress,
                LocationMasterLocationDeleted = k.LocationMasterLocationDeleted,
                LocationMasterLocationStatus = k.LocationMasterLocationStatus,
                LocationMasterLocationTelephone = k.LocationMasterLocationTelephone,
                LocationMasterLocationFax = k.LocationMasterLocationFax,
                LocationMasterLocationEmail = k.LocationMasterLocationEmail,
                LocationMasterLocationCashAccount = k.LocationMasterLocationCashAccount,
                LocationMasterLocationCostCenter = k.LocationMasterLocationCostCenter,
                LocationMasterLocationDelStatus = k.LocationMasterLocationDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteLocation")]
        public List<LocationMasterViewModel> DeleteLocation([FromBody] LocationMasterViewModel locationMaster)
        {
            var result = _mapper.Map<LocationMaster>(locationMaster);
            return locationMasterService.DeleteLocation(result).Select(k => new LocationMasterViewModel
            {
                LocationMasterLocationId = k.LocationMasterLocationId,
                LocationMasterLocationName = k.LocationMasterLocationName,
                LocationMasterLocationAddress = k.LocationMasterLocationAddress,
                LocationMasterLocationDeleted = k.LocationMasterLocationDeleted,
                LocationMasterLocationStatus = k.LocationMasterLocationStatus,
                LocationMasterLocationTelephone = k.LocationMasterLocationTelephone,
                LocationMasterLocationFax = k.LocationMasterLocationFax,
                LocationMasterLocationEmail = k.LocationMasterLocationEmail,
                LocationMasterLocationCashAccount = k.LocationMasterLocationCashAccount,
                LocationMasterLocationCostCenter = k.LocationMasterLocationCostCenter,
                LocationMasterLocationDelStatus = k.LocationMasterLocationDelStatus
            }).ToList();
        }
    }
}