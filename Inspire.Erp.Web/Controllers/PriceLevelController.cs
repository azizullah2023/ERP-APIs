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
    public class PriceLevelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IPriceLevelMasterService priceLevelMasterService;
        public PriceLevelController(IPriceLevelMasterService _priceLevelMasterService, IMapper mapper)
        {

            priceLevelMasterService = _priceLevelMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllPriceLevel")]
        public List<PriceLevelMasterViewModel> GetAllPriceLevel()
        {

            return priceLevelMasterService.GetAllPriceLevel().Select(k => new PriceLevelMasterViewModel
            {
                PriceLevelMasterPriceLevelId = (int)k.PriceLevelMasterPriceLevelId,
                 
                PriceLevelMasterPriceLevelName = k.PriceLevelMasterPriceLevelName,

                PriveLevelMasterPriceLevelDelStatus = k.PriveLevelMasterPriceLevelDelStatus

            }).ToList();
        }

        [HttpGet]
        [Route("GetAllPriceLevelById/{id}")]
        public List<PriceLevelMasterViewModel> GetAllPriceLevelById(int id)
        {
            return priceLevelMasterService.GetAllPriceLevelById(id).Select(k => new PriceLevelMasterViewModel
            {

                PriceLevelMasterPriceLevelId = (int)k.PriceLevelMasterPriceLevelId,
                PriceLevelMasterPriceLevelName = k.PriceLevelMasterPriceLevelName,
                 PriveLevelMasterPriceLevelDelStatus = k.PriveLevelMasterPriceLevelDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertPriceLevel")]
        public List<PriceLevelMasterViewModel> InsertPriceLevel([FromBody] PriceLevelMasterViewModel priceLevelMaster)
        {
            var result = _mapper.Map<PriceLevelMaster>(priceLevelMaster);
            return priceLevelMasterService.InsertPriceLevel(result).Select(k => new PriceLevelMasterViewModel
            {
                PriceLevelMasterPriceLevelId = (int)k.PriceLevelMasterPriceLevelId,
                
                PriceLevelMasterPriceLevelName = k.PriceLevelMasterPriceLevelName,

                 PriveLevelMasterPriceLevelDelStatus = k.PriveLevelMasterPriceLevelDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdatePriceLevel")]
        public List<PriceLevelMasterViewModel> UpdatePriceLevel([FromBody] PriceLevelMasterViewModel priceLevelMaster)
        {
            var result = _mapper.Map<PriceLevelMaster>(priceLevelMaster);
            return priceLevelMasterService.UpdatePriceLevel(result).Select(k => new PriceLevelMasterViewModel
            {
                PriceLevelMasterPriceLevelId = (int)k.PriceLevelMasterPriceLevelId,
                
                PriceLevelMasterPriceLevelName = k.PriceLevelMasterPriceLevelName,
                PriveLevelMasterPriceLevelDelStatus = k.PriveLevelMasterPriceLevelDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeletePriceLevel")]
        public List<PriceLevelMasterViewModel> DeletePriceLevel([FromBody] PriceLevelMasterViewModel priceLevelMaster)
        {
            var result = _mapper.Map<PriceLevelMaster>(priceLevelMaster);
            return priceLevelMasterService.DeletePriceLevel(result).Select(k => new PriceLevelMasterViewModel
            {
                PriceLevelMasterPriceLevelId = (int)k.PriceLevelMasterPriceLevelId,
                
                PriceLevelMasterPriceLevelName = k.PriceLevelMasterPriceLevelName,
                 PriveLevelMasterPriceLevelDelStatus = k.PriveLevelMasterPriceLevelDelStatus
            }).ToList();
        }
    }
}