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
    public class CostCenterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICostCenterMasterService costCenterMasterService;
        public CostCenterController(ICostCenterMasterService _businessMasterService, IMapper mapper)
        {

            costCenterMasterService = _businessMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllCostCenter")]
        public List<CostCenterViewModel> GetAllCostCenter()
        {

            return costCenterMasterService.GetAllCostCenter().Select(k => new CostCenterViewModel
            {
                CostCenterMasterCostCenterId = k.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName = k.CostCenterMasterCostCenterName,
                CostCenterMasterCostCenterStatus = k.CostCenterMasterCostCenterStatus,
                CostCenterMasterCostCenterIsSystem = k.CostCenterMasterCostCenterIsSystem,
                CostCenterMasterCostCenterSortOrder = k.CostCenterMasterCostCenterSortOrder,
                CostCenterMasterCostCenterDelStatus = k.CostCenterMasterCostCenterDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllCostCenterById/{id}")]
        public List<CostCenterViewModel> GetAllCostCenterById(int id)
        {
            return costCenterMasterService.GetAllCostCenterById(id).Select(k => new CostCenterViewModel
            {

                CostCenterMasterCostCenterId = k.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName = k.CostCenterMasterCostCenterName,
                CostCenterMasterCostCenterStatus = k.CostCenterMasterCostCenterStatus,
                CostCenterMasterCostCenterIsSystem = k.CostCenterMasterCostCenterIsSystem,
                CostCenterMasterCostCenterSortOrder = k.CostCenterMasterCostCenterSortOrder,
                CostCenterMasterCostCenterDelStatus = k.CostCenterMasterCostCenterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertCostCenter")]
        public List<CostCenterViewModel> InsertCostCenter([FromBody] CostCenterViewModel costCenterMaster)
        {
            var result = _mapper.Map<CostCenterMaster>(costCenterMaster);
            return costCenterMasterService.InsertCostCenter(result).Select(k => new CostCenterViewModel
            {
                CostCenterMasterCostCenterId = k.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName = k.CostCenterMasterCostCenterName,
                CostCenterMasterCostCenterStatus = k.CostCenterMasterCostCenterStatus,
                CostCenterMasterCostCenterIsSystem = k.CostCenterMasterCostCenterIsSystem,
                CostCenterMasterCostCenterSortOrder = k.CostCenterMasterCostCenterSortOrder,
                CostCenterMasterCostCenterDelStatus = k.CostCenterMasterCostCenterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateCostCenter")]
        public List<CostCenterViewModel> UpdateCostCenter([FromBody] CostCenterViewModel costCenterMaster)
        {
            var result = _mapper.Map<CostCenterMaster>(costCenterMaster);
            return costCenterMasterService.UpdateCostCenter(result).Select(k => new CostCenterViewModel
            {
                CostCenterMasterCostCenterId = k.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName = k.CostCenterMasterCostCenterName,
                CostCenterMasterCostCenterStatus = k.CostCenterMasterCostCenterStatus,
                CostCenterMasterCostCenterIsSystem = k.CostCenterMasterCostCenterIsSystem,
                CostCenterMasterCostCenterSortOrder = k.CostCenterMasterCostCenterSortOrder,
                CostCenterMasterCostCenterDelStatus = k.CostCenterMasterCostCenterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteCostCenter")]
        public List<CostCenterViewModel> DeleteCostCenter([FromBody] CostCenterViewModel costCenterMaster)
        {
            var result = _mapper.Map<CostCenterMaster>(costCenterMaster);
            return costCenterMasterService.DeleteCostCenter(result).Select(k => new CostCenterViewModel
            {
                CostCenterMasterCostCenterId = k.CostCenterMasterCostCenterId,
                CostCenterMasterCostCenterName = k.CostCenterMasterCostCenterName,
                CostCenterMasterCostCenterStatus = k.CostCenterMasterCostCenterStatus,
                CostCenterMasterCostCenterIsSystem = k.CostCenterMasterCostCenterIsSystem,
                CostCenterMasterCostCenterSortOrder = k.CostCenterMasterCostCenterSortOrder,
                CostCenterMasterCostCenterDelStatus = k.CostCenterMasterCostCenterDelStatus
            }).ToList();
        }
    }
}