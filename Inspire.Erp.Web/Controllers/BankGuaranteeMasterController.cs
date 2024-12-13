using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class BankGuaranteeMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBankGuaranteeMasterService bankGuaranteeMasterService;
        public BankGuaranteeMasterController(IBankGuaranteeMasterService _bankGuaranteeMasterService, IMapper mapper)
        {

            bankGuaranteeMasterService = _bankGuaranteeMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllBankGuarantee")]
        public List<BankGuaranteeMasterViewModel> GetAllBankGuarantee()
        {

            return bankGuaranteeMasterService.GetAllBankGuarantee().Select(k => new BankGuaranteeMasterViewModel
            {
                BankGuaranteeMasterBgid = k.BankGuaranteeMasterBgid,
                BankGuaranteeMasterBgname = k.BankGuaranteeMasterBgname,
                BankGuaranteeMasterUserId = k.BankGuaranteeMasterUserId,
                BankGuaranteeMasterDeleted = k.BankGuaranteeMasterDeleted,
                BankGuaranteeMasterStatus = k.BankGuaranteeMasterStatus
            }).ToList();
        }

        [HttpPost]
        [Route("InsertBankGuarantee")]
        public List<BankGuaranteeMasterViewModel> InsertBankGuarantee([FromBody] BankGuaranteeMasterViewModel bankGuaranteeMaster)
        {
            var result = _mapper.Map<BankGuaranteeMaster>(bankGuaranteeMaster);
            return bankGuaranteeMasterService.InsertBankGuarantee(result).Select(k => new BankGuaranteeMasterViewModel
            {
                BankGuaranteeMasterBgid = k.BankGuaranteeMasterBgid,
                BankGuaranteeMasterBgname = k.BankGuaranteeMasterBgname,
                BankGuaranteeMasterUserId = k.BankGuaranteeMasterUserId,
                BankGuaranteeMasterDeleted = k.BankGuaranteeMasterDeleted,
                BankGuaranteeMasterStatus = k.BankGuaranteeMasterStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateBankGuarantee")]
        public List<BankGuaranteeMasterViewModel> UpdateBankGuarantee([FromBody] BankGuaranteeMasterViewModel bankGuaranteeMaster)
        {
            var result = _mapper.Map<BankGuaranteeMaster>(bankGuaranteeMaster);
            return bankGuaranteeMasterService.UpdateBankGuarantee(result).Select(k => new BankGuaranteeMasterViewModel
            {
                BankGuaranteeMasterBgid = k.BankGuaranteeMasterBgid,
                BankGuaranteeMasterBgname = k.BankGuaranteeMasterBgname,
                BankGuaranteeMasterUserId = k.BankGuaranteeMasterUserId,
                BankGuaranteeMasterDeleted = k.BankGuaranteeMasterDeleted,
                BankGuaranteeMasterStatus = k.BankGuaranteeMasterStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteBankGuarantee")]
        public List<BankGuaranteeMasterViewModel> DeleteBankGuarantee([FromBody] BankGuaranteeMasterViewModel bankGuaranteeMaster)
        {
            var result = _mapper.Map<BankGuaranteeMaster>(bankGuaranteeMaster);
            return bankGuaranteeMasterService.DeleteBankGuarantee(result).Select(k => new BankGuaranteeMasterViewModel
            {
                BankGuaranteeMasterBgid = k.BankGuaranteeMasterBgid,
                BankGuaranteeMasterBgname = k.BankGuaranteeMasterBgname,
                BankGuaranteeMasterUserId = k.BankGuaranteeMasterUserId,
                BankGuaranteeMasterDeleted = k.BankGuaranteeMasterDeleted,
                BankGuaranteeMasterStatus = k.BankGuaranteeMasterStatus
            }).ToList();
        }
    }
}