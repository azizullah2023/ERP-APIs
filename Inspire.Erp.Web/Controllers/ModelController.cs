using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ModelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IModelMasterService modelMasterService;
        public ModelController(IModelMasterService _modelMasterService, IMapper mapper)
        {

            modelMasterService = _modelMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllModel")]
        public List<ModelsViewModel> GetAllModel()
        {

            return modelMasterService.GetAllModel().Select(k => new ModelsViewModel
            {
                ModelMasterId = k.ModelMasterId,
                ModelMasterTypeId = k.ModelMasterTypeId,
                ModelMasterName = k.ModelMasterName,
                ModelMasterUserId = k.ModelMasterUserId,
                ModelMasterDeleted = k.ModelMasterDeleted,
                ModelMasterStatus =k.ModelMasterStatus,
                ModelMasterDelStatus = k.ModelMasterDelStatus

            }).ToList();
        }


        [HttpGet]
        [Route("GetAllModelById/{id}")]
        public List<ModelsViewModel> GetAllModelById(int id)
        {
            return modelMasterService.GetAllModelById(id).Select(k => new ModelsViewModel
            {

                ModelMasterId = k.ModelMasterId,
                ModelMasterTypeId = k.ModelMasterTypeId,
                ModelMasterName = k.ModelMasterName,
                ModelMasterUserId = k.ModelMasterUserId,
                ModelMasterDeleted = k.ModelMasterDeleted,
                ModelMasterStatus = k.ModelMasterStatus,
                ModelMasterDelStatus = k.ModelMasterDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertModel")]
        public List<ModelsViewModel> InsertModel([FromBody] ModelsViewModel modelMaster)
        {
            var result = _mapper.Map<ModelMaster>(modelMaster);
            return modelMasterService.InsertModel(result).Select(k => new ModelsViewModel
            {
                ModelMasterId = k.ModelMasterId,
                ModelMasterTypeId = k.ModelMasterTypeId,
                ModelMasterName = k.ModelMasterName,
                ModelMasterUserId = k.ModelMasterUserId,
                ModelMasterDeleted = k.ModelMasterDeleted,
                ModelMasterStatus = k.ModelMasterStatus,
                ModelMasterDelStatus = k.ModelMasterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateModel")]
        public List<ModelsViewModel> UpdateModel([FromBody] ModelsViewModel modelMaster)
        {
            var result = _mapper.Map<ModelMaster>(modelMaster);
            return modelMasterService.UpdateModel(result).Select(k => new ModelsViewModel
            {
                ModelMasterId = k.ModelMasterId,
                ModelMasterTypeId = k.ModelMasterTypeId,
                ModelMasterName = k.ModelMasterName,
                ModelMasterUserId = k.ModelMasterUserId,
                ModelMasterDeleted = k.ModelMasterDeleted,
                ModelMasterStatus = k.ModelMasterStatus,
                ModelMasterDelStatus = k.ModelMasterDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteModel")]
        public List<ModelsViewModel> DeleteModel([FromBody] ModelsViewModel modelMaster)
        {
            var result = _mapper.Map<ModelMaster>(modelMaster);
            return modelMasterService.DeleteModel(result).Select(k => new ModelsViewModel
            {
                ModelMasterId = k.ModelMasterId,
                ModelMasterTypeId = k.ModelMasterTypeId,
                ModelMasterName = k.ModelMasterName,
                ModelMasterUserId = k.ModelMasterUserId,
                ModelMasterDeleted = k.ModelMasterDeleted,
                ModelMasterStatus = k.ModelMasterStatus,
                ModelMasterDelStatus = k.ModelMasterDelStatus
            }).ToList();
        }
    }
}