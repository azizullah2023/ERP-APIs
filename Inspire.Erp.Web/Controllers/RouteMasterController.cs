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
using Inspire.Erp.Application.Master.Interfaces;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class RouteMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IRouteMasterService routeMasterService;
        public RouteMasterController(IRouteMasterService _routeMasterService, IMapper mapper)
        {

            routeMasterService = _routeMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllRoute")]
        public List<RouteMasterViewModel> GetAllRoute()
        {
            return routeMasterService.GetAllRoute().Select(k => new RouteMasterViewModel
            {
                RmId = k.RmId,
                RmName = k.RmName,
                RmVanId = k.RmVanId,
                RmSalesmanId = k.RmSalesmanId,
                RmStatus = k.RmStatus,
                RmDelStatus = k.RmDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllRouteById/{id}")]
        public List<RouteMasterViewModel> GetAllRouteById(int id)
        {
            return routeMasterService.GetAllRouteById(id).Select(k => new RouteMasterViewModel
            {
                RmId = k.RmId,
                RmName = k.RmName,
                RmVanId = k.RmVanId,
                RmSalesmanId = k.RmSalesmanId,
                RmStatus = k.RmStatus,
                RmDelStatus = k.RmDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertRouteMaster")]
        public List<RouteMasterViewModel> InsertRouteMaster([FromBody] RouteMasterViewModel routeMaster)
        {
            var result = _mapper.Map<RouteMaster>(routeMaster);
            return routeMasterService.InsertRouteMaster(result).Select(k => new RouteMasterViewModel
            {
                RmId = k.RmId,
                RmName = k.RmName,
                RmVanId = k.RmVanId,
                RmSalesmanId = k.RmSalesmanId,
                RmStatus = k.RmStatus,
                RmDelStatus = k.RmDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateRouteMaster")]
        public List<RouteMasterViewModel> UpdateRouteMaster([FromBody] RouteMasterViewModel routeMaster)
        {
            var result = _mapper.Map<RouteMaster>(routeMaster);
            return routeMasterService.UpdateRouteMaster(result).Select(k => new RouteMasterViewModel
            {
                RmId = k.RmId,
                RmName = k.RmName,
                RmVanId = k.RmVanId,
                RmSalesmanId = k.RmSalesmanId,
                RmStatus = k.RmStatus,
                RmDelStatus = k.RmDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteRouteMaster")]
        public List<RouteMasterViewModel> DeleteRouteMaster([FromBody] RouteMasterViewModel routeMaster)
        {
            var result = _mapper.Map<RouteMaster>(routeMaster);
            return routeMasterService.DeleteRouteMaster(result).Select(k => new RouteMasterViewModel
            {
                RmId = k.RmId,
                RmName = k.RmName,
                RmVanId = k.RmVanId,
                RmSalesmanId = k.RmSalesmanId,
                RmStatus = k.RmStatus,
                RmDelStatus = k.RmDelStatus
            }).ToList();
        }
    }
}