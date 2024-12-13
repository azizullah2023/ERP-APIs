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
    public class BudgetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBudgetMasterService BudgetMasterService;
        public BudgetController(IBudgetMasterService _BudgetMasterService, IMapper mapper)
        {

            BudgetMasterService = _BudgetMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllBudget")]
        public List<BudgetMasterViewModel> GetAllBudget()
        {

            return BudgetMasterService.GetAllBudget().Select(k => new BudgetMasterViewModel
            {
                BudgetMasterBudgetId = k.BudgetMasterBudgetId,
                BudgetMasterBudgetName = k.BudgetMasterBudgetName,
                BudgetMasterBudgetDelStatus = k.BudgetMasterBudgetDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllBudgetById/{id}")]
        public List<BudgetMasterViewModel> GetAllBudgetById(int id)
        {
            return BudgetMasterService.GetAllBudgetById(id).Select(k => new BudgetMasterViewModel
            {

                BudgetMasterBudgetId = k.BudgetMasterBudgetId,
                BudgetMasterBudgetName = k.BudgetMasterBudgetName,
                BudgetMasterBudgetDelStatus = k.BudgetMasterBudgetDelStatus

            }).ToList();
        }


        [HttpPost]
        [Route("InsertBudget")]
        public List<BudgetMasterViewModel> InsertBudget([FromBody] BudgetMasterViewModel budgetMaster)
        {
            var result = _mapper.Map<BudgetMaster>(budgetMaster);
            return BudgetMasterService.InsertBudget(result).Select(k => new BudgetMasterViewModel
            {
                BudgetMasterBudgetId = k.BudgetMasterBudgetId,
                BudgetMasterBudgetName = k.BudgetMasterBudgetName,
                BudgetMasterBudgetDelStatus = k.BudgetMasterBudgetDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateBudget")]
        public List<BudgetMasterViewModel> UpdateBudget([FromBody] BudgetMasterViewModel budgetMaster)
        {
            var result = _mapper.Map<BudgetMaster>(budgetMaster);
            return BudgetMasterService.UpdateBudget(result).Select(k => new BudgetMasterViewModel
            {
                BudgetMasterBudgetId = k.BudgetMasterBudgetId,
                BudgetMasterBudgetName = k.BudgetMasterBudgetName,
                BudgetMasterBudgetDelStatus = k.BudgetMasterBudgetDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteBudget")]
        public List<BudgetMasterViewModel> DeleteBudget([FromBody] BudgetMasterViewModel budgetMaster)
        {
            var result = _mapper.Map<BudgetMaster>(budgetMaster);
            return BudgetMasterService.DeleteBudget(result).Select(k => new BudgetMasterViewModel
            {
                BudgetMasterBudgetId = k.BudgetMasterBudgetId,
                BudgetMasterBudgetName = k.BudgetMasterBudgetName,
                BudgetMasterBudgetDelStatus = k.BudgetMasterBudgetDelStatus
            }).ToList();
        }
    }
}