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
    public class TermsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ITermsAndConditionService termsAndConditionService;
        public TermsController(ITermsAndConditionService _termsAndConditionService, IMapper mapper)
        {
            termsAndConditionService = _termsAndConditionService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllTerms")]
        public List<TermsAndConditionViewModel> GetAllTerms()
        {
            return termsAndConditionService.GetAllTerms().Select(k => new TermsAndConditionViewModel
            {
              TermsAndConditionTermsId = k.TermsAndConditionTermsId,
              TermsAndConditionTermsConditions = k.TermsAndConditionTermsConditions,
              TermsAndConditionTermsDeleted = k.TermsAndConditionTermsDeleted,
                TermsAndConditionsTermsDelStatus=k.TermsAndConditionsTermsDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllTermsById/{id}")]
        public List<TermsAndConditionViewModel> GetAllTermsById(int id)
        {
            return termsAndConditionService.GetAllTermsById(id).Select(k => new TermsAndConditionViewModel
            {
                TermsAndConditionTermsId = k.TermsAndConditionTermsId,
                TermsAndConditionTermsConditions = k.TermsAndConditionTermsConditions,
                TermsAndConditionTermsDeleted = k.TermsAndConditionTermsDeleted,
                TermsAndConditionsTermsDelStatus = k.TermsAndConditionsTermsDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertTerms")]
        public List<TermsAndConditionViewModel> InsertTerms([FromBody] TermsAndConditionViewModel termsAndCondition)
        {
            var result = _mapper.Map<TermsAndCondition>(termsAndCondition);
            return termsAndConditionService.InsertTerms(result).Select(k => new TermsAndConditionViewModel
            {
                TermsAndConditionTermsId = k.TermsAndConditionTermsId,
                TermsAndConditionTermsConditions = k.TermsAndConditionTermsConditions,
                TermsAndConditionTermsDeleted = k.TermsAndConditionTermsDeleted,
                TermsAndConditionsTermsDelStatus = k.TermsAndConditionsTermsDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateTerms")]
        public List<TermsAndConditionViewModel> UpdateTerms([FromBody] TermsAndConditionViewModel termsAndCondition)
        {
            var result = _mapper.Map<TermsAndCondition>(termsAndCondition);
            return termsAndConditionService.UpdateTerms(result).Select(k => new TermsAndConditionViewModel
            {
                TermsAndConditionTermsId = k.TermsAndConditionTermsId,
                TermsAndConditionTermsConditions = k.TermsAndConditionTermsConditions,
                TermsAndConditionTermsDeleted = k.TermsAndConditionTermsDeleted,
                TermsAndConditionsTermsDelStatus = k.TermsAndConditionsTermsDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteTerms")]
        public List<TermsAndConditionViewModel> DeleteTerms([FromBody] TermsAndConditionViewModel termsAndCondition)
        {
            var result = _mapper.Map<TermsAndCondition>(termsAndCondition);
            return termsAndConditionService.DeleteTerms(result).Select(k => new TermsAndConditionViewModel
            {
                TermsAndConditionTermsId = k.TermsAndConditionTermsId,
                TermsAndConditionTermsConditions = k.TermsAndConditionTermsConditions,
                TermsAndConditionTermsDeleted = k.TermsAndConditionTermsDeleted,
                TermsAndConditionsTermsDelStatus = k.TermsAndConditionsTermsDelStatus
            }).ToList();
        }
    }
}
