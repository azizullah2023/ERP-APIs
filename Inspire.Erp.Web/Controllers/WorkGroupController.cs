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
    public class WorkGroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IWorkGroupMasterService workGroupMasterService;
        public WorkGroupController(IWorkGroupMasterService _workGroupMasterService, IMapper mapper)
        {

            workGroupMasterService = _workGroupMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllWorkGroup")]
        public List<WorkGroupMasterViewModel> GetAllWorkGroup()
        {

            return workGroupMasterService.GetAllWorkGroup().Select(k => new WorkGroupMasterViewModel
            {
                WorkGroupMasterWorkGroupId = k.WorkGroupMasterWorkGroupId,
                WorkGroupMasterWorkGroupName = k.WorkGroupMasterWorkGroupName,
                WorkGroupMasterWorkGroupDelStatus=k.WorkGroupMasterWorkGroupDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllWorkGroupById/{id}")]
        public List<WorkGroupMasterViewModel> GetAllWorkGroupById(int id)
        {
            return workGroupMasterService.GetAllWorkGroupById(id).Select(k => new WorkGroupMasterViewModel
            {
                WorkGroupMasterWorkGroupId = k.WorkGroupMasterWorkGroupId,
                WorkGroupMasterWorkGroupName = k.WorkGroupMasterWorkGroupName,
                WorkGroupMasterWorkGroupDelStatus = k.WorkGroupMasterWorkGroupDelStatus
            }).ToList();
        }


        [HttpPost]
        [Route("InsertWorkGroup")]
        public List<WorkGroupMasterViewModel> InsertWorkGroup([FromBody] WorkGroupMasterViewModel workGroupMaster)
        {
            return workGroupMasterService.InsertWorkGroup(_mapper.Map<WorkGroupMaster>(workGroupMaster)).Select(k => new WorkGroupMasterViewModel
            {
                WorkGroupMasterWorkGroupId = k.WorkGroupMasterWorkGroupId,
                WorkGroupMasterWorkGroupName = k.WorkGroupMasterWorkGroupName,
                WorkGroupMasterWorkGroupDelStatus = k.WorkGroupMasterWorkGroupDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateWorkGroup")]
        public List<WorkGroupMasterViewModel> UpdateWorkGroup([FromBody] WorkGroupMasterViewModel workGroupMaster)
        {
            var result = _mapper.Map<WorkGroupMaster>(workGroupMaster);
            return workGroupMasterService.UpdateWorkGroup(result).Select(k => new WorkGroupMasterViewModel
            {
                WorkGroupMasterWorkGroupId = k.WorkGroupMasterWorkGroupId,
                WorkGroupMasterWorkGroupName = k.WorkGroupMasterWorkGroupName,
                WorkGroupMasterWorkGroupDelStatus = k.WorkGroupMasterWorkGroupDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteWorkGroup")]
        public List<WorkGroupMasterViewModel> DeleteWorkGroup([FromBody] WorkGroupMasterViewModel workGroupMaster)
        {
            var result = _mapper.Map<WorkGroupMaster>(workGroupMaster);
            return workGroupMasterService.DeleteWorkGroup(result).Select(k => new WorkGroupMasterViewModel
            {
                WorkGroupMasterWorkGroupId = k.WorkGroupMasterWorkGroupId,
                WorkGroupMasterWorkGroupName = k.WorkGroupMasterWorkGroupName,
                WorkGroupMasterWorkGroupDelStatus = k.WorkGroupMasterWorkGroupDelStatus
            }).ToList();
        }
    }
}





















