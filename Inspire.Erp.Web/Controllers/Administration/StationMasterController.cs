using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels.Administration;
using Inspire.Erp.Application.Administration.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers.Administration
{
    [Route("api/master")]
    [Produces("application/json")]
    [ApiController]
    public class StationMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IStationMasterServices stationMasterService;
        private IPosStationSettingsServices PosStationSettingsServices;
        public StationMasterController(IStationMasterServices _stationMasterService, IMapper mapper, IPosStationSettingsServices posStationSettingsServices)
        {

            stationMasterService = _stationMasterService;
            _mapper = mapper;
            PosStationSettingsServices = posStationSettingsServices;
        }
        [HttpGet]
        [Route("GetAllStation")]
        public List<StationMasterViewModel> GetAllStation()
        {

            return stationMasterService.GetAllStation().Select(k => new StationMasterViewModel
            {
                StationMasterCode = k.StationMasterCode,
                StationMasterStationName = k.StationMasterStationName,
                StationMasterAddress = k.StationMasterAddress,
                StationMasterCity = k.StationMasterCity,
                StationMasterPostOffice = k.StationMasterPostOffice,
                StationMasterTele1 = k.StationMasterTele1,
                StationMasterTele2 = k.StationMasterTele2,
                StationMasterFax = k.StationMasterFax,
                StationMasterEmail = k.StationMasterEmail,
                StationMasterWebSite = k.StationMasterWebSite,
                StationMasterCountry = k.StationMasterCountry,
                StationMasterLogoPath = k.StationMasterLogoPath,
                StationMasterSignPath = k.StationMasterSignPath,
                StationMasterLogoImg = k.StationMasterLogoImg,
                StationMasterSignImg = k.StationMasterSignImg,
                StationMasterSealImg = k.StationMasterSealImg,
                StationMasterVatNo = k.StationMasterVatNo,
                StationMasterDelStatus = k.StationMasterDelStatus,
                BankName= k.BankName,
                AccountName=k.AccountName,
                AccountNo=k.AccountNo,
                IBAN=k.IBAN,
                SwiftCode=k.SwiftCode,
                BankBranch=k.BankBranch,
                BankCurrency=k.BankCurrency
                 }).ToList();
        }

        [HttpPost]
        [Route("DeleteStationMaster")]
        public async Task<IActionResult> DeleteStationMaster(StationMaster stationMaster)
        {
            return Ok(await stationMasterService.DeleteStationMaster(stationMaster));
        }

        [HttpGet]
        [Route("GetStationMaster/{Id}")]
        public async Task<IActionResult> GetStationMaster(int Id)
        {
            return Ok(await stationMasterService.GetStationMaster(Id));
        }

        [HttpPost]
        [Route("InsterStationMaster")]
        public async Task<IActionResult> InsterStationMaster(StationMaster stationMaster)
        {
            return Ok(await stationMasterService.InsertStationMaster(stationMaster));
        }
        [HttpPut]
        [Route("UpdateStationMaster")]
        public async Task<IActionResult> UpdateStationMaster(StationMaster stationMaster)
        {
            return Ok(await stationMasterService.UpdateStationMaster(stationMaster));
        }


        //Pos Station Settings  

        [HttpGet]
        [Route("GetAllCounter")]
        public List<PosStationSettings> GetAllCounter()
        {

            return PosStationSettingsServices.GetAllCounter().ToList();
        }

        [HttpPost]
        [Route("InsterCounter")]
        public async Task<IActionResult> InsterCounter(PosStationSettings stationMaster)
        {
            return Ok( PosStationSettingsServices.InsterCounter(stationMaster));
        }

        [HttpPut]
        [Route("UpdateCounter")]
        public async Task<IActionResult> UpdateCounter(PosStationSettings stationMaster)
        {
            return Ok( PosStationSettingsServices.UpdateCounter(stationMaster));
        }

        [HttpGet]
        [Route("GetCounter/{Id}")]
        public async Task<IActionResult> GetCounter(int Id)
        {
            return Ok(await PosStationSettingsServices.GetCounter(Id));
        }

        [HttpDelete]
        [Route("DeleteCounter/{Id}")]
        public async Task<IActionResult> DeleteCounter(int Id)
        {
            return Ok(await PosStationSettingsServices.DeleteCounter(Id));
        }
    }
}