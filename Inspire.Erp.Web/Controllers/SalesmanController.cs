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
    public class SalesmanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ISalesmanMasterService salesmanMasterService;
        public SalesmanController(ISalesmanMasterService _salesmanMasterService, IMapper mapper)
        {

            salesmanMasterService = _salesmanMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllSalesman")]
        public List<SalesmanMasterViewModel> GetAllSalesman()
        {

            return salesmanMasterService.GetAllSalesman().Select(k => new SalesmanMasterViewModel
            {

                SalesManMasterSalesManId = k.SalesManMasterSalesManId,
                SalesManMasterSalesManName = k.SalesManMasterSalesManName,
                SalesManMasterSalesManDeleted = k.SalesManMasterSalesManDeleted,
                SalesManMasterSalesManContactNo = k.SalesManMasterSalesManContactNo,
                SalesManMasterSalesManLocationId = k.SalesManMasterSalesManLocationId,
                SalesManMasterSalesManDelStatus=k.SalesManMasterSalesManDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllSalesmanById/{id}")]
        public List<SalesmanMasterViewModel> GetAllSalesmanById(int id)
        {
            return salesmanMasterService.GetAllSalesmanById(id).Select(k => new SalesmanMasterViewModel
            {


                SalesManMasterSalesManId = k.SalesManMasterSalesManId,
                SalesManMasterSalesManName = k.SalesManMasterSalesManName,
                SalesManMasterSalesManDeleted = k.SalesManMasterSalesManDeleted,
                SalesManMasterSalesManContactNo = k.SalesManMasterSalesManContactNo,
                SalesManMasterSalesManLocationId = k.SalesManMasterSalesManLocationId,
                SalesManMasterSalesManDelStatus = k.SalesManMasterSalesManDelStatus
            }).ToList();
        }



        [HttpPost]
        [Route("InsertSalesman")]
        public List<SalesmanMasterViewModel> InsertSalesman([FromBody] SalesmanMasterViewModel salesmanMaster)
        {
            var result = _mapper.Map<SalesManMaster>(salesmanMaster);
            return salesmanMasterService.InsertSalesman(result).Select(k => new SalesmanMasterViewModel
            {
                SalesManMasterSalesManId = k.SalesManMasterSalesManId,
                SalesManMasterSalesManName = k.SalesManMasterSalesManName,
                SalesManMasterSalesManDeleted = k.SalesManMasterSalesManDeleted,
                SalesManMasterSalesManContactNo = k.SalesManMasterSalesManContactNo,
                SalesManMasterSalesManLocationId = k.SalesManMasterSalesManLocationId,
                SalesManMasterSalesManDelStatus = k.SalesManMasterSalesManDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateSalesman")]
        public List<SalesmanMasterViewModel> UpdateSalesman([FromBody] SalesmanMasterViewModel salesmanMaster)
        {
            var result = _mapper.Map<SalesManMaster>(salesmanMaster);
            return salesmanMasterService.UpdateSalesman(result).Select(k => new SalesmanMasterViewModel
            {
                SalesManMasterSalesManId = k.SalesManMasterSalesManId,
                SalesManMasterSalesManName = k.SalesManMasterSalesManName,
                SalesManMasterSalesManDeleted = k.SalesManMasterSalesManDeleted,
                SalesManMasterSalesManContactNo = k.SalesManMasterSalesManContactNo,
                SalesManMasterSalesManLocationId = k.SalesManMasterSalesManLocationId,
                SalesManMasterSalesManDelStatus = k.SalesManMasterSalesManDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteSalesman")]
        public List<SalesmanMasterViewModel> DeleteSalesman([FromBody] SalesmanMasterViewModel salesmanMaster)
        {
            var result = _mapper.Map<SalesManMaster>(salesmanMaster);
            return salesmanMasterService.DeleteSalesman(result).Select(k => new SalesmanMasterViewModel
            {
                SalesManMasterSalesManId = k.SalesManMasterSalesManId,
                SalesManMasterSalesManName = k.SalesManMasterSalesManName,
                SalesManMasterSalesManDeleted = k.SalesManMasterSalesManDeleted,
                SalesManMasterSalesManContactNo = k.SalesManMasterSalesManContactNo,
                SalesManMasterSalesManLocationId = k.SalesManMasterSalesManLocationId,
                SalesManMasterSalesManDelStatus = k.SalesManMasterSalesManDelStatus
            }).ToList();
        }
    }
}