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
    public class TaxMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ITaxMasterService taxMasterService;
        public TaxMasterController(ITaxMasterService _taxMasterService, IMapper mapper)
        {

            taxMasterService = _taxMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllTax")]
        public List<TaxMasterViewModel> GetAllTax()
        {

            return taxMasterService.GetAllTax().Select(k => new TaxMasterViewModel
            {
                TmId = k.TmId,
                TmType = k.TmType,
                TmName = k.TmName,
                TmPercentage = k.TmPercentage,
                TmCgst = k.TmCgst,
                TmSgst = k.TmSgst,
                TmDelStatus= k.TmDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllTaxById/{id}")]
        public List<TaxMasterViewModel> GetAllTaxById(int id)
        {
            return taxMasterService.GetAllTaxById(id).Select(k => new TaxMasterViewModel
            {

                TmId = k.TmId,
                TmType = k.TmType,
                TmName = k.TmName,
                TmPercentage = k.TmPercentage,
                TmCgst = k.TmCgst,
                TmSgst = k.TmSgst,
                TmDelStatus = k.TmDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertTaxMaster")]
        public List<TaxMasterViewModel> InsertTaxMaster([FromBody] TaxMasterViewModel taxMaster)
        {

            var result = _mapper.Map<TaxMaster>(taxMaster);
            return taxMasterService.InsertTaxMaster(result).Select(k => new TaxMasterViewModel
            {
                TmId = k.TmId,
                TmType = k.TmType,
                TmName = k.TmName,
                TmPercentage = k.TmPercentage,
                TmCgst = k.TmCgst,
                TmSgst = k.TmSgst,
                TmDelStatus = k.TmDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("UpdateTaxMaster")]
        public List<TaxMasterViewModel> UpdateTaxMaster([FromBody] TaxMasterViewModel taxMaster)
        {
            var result = _mapper.Map<TaxMaster>(taxMaster);
            return taxMasterService.UpdateTaxMaster(result).Select(k => new TaxMasterViewModel
            {
                TmId = k.TmId,
                TmType = k.TmType,
                TmName = k.TmName,
                TmPercentage = k.TmPercentage,
                TmCgst = k.TmCgst,
                TmSgst = k.TmSgst,
                TmDelStatus = k.TmDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("DeleteTaxMaster")]
        public List<TaxMasterViewModel> DeleteTaxMaster([FromBody] TaxMasterViewModel taxMaster)
        {
            var result = _mapper.Map<TaxMaster>(taxMaster);
            return taxMasterService.DeleteTaxMaster(result).Select(k => new TaxMasterViewModel
            {
                TmId = k.TmId,
                TmType = k.TmType,
                TmName = k.TmName,
                TmPercentage = k.TmPercentage,
                TmCgst = k.TmCgst,
                TmSgst = k.TmSgst,
                TmDelStatus = k.TmDelStatus
            }).ToList();
        }
    }
}