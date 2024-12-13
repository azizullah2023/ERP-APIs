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
    public class ProjectDescriptionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IProjectDescriptionService projectDescriptionService;
        public ProjectDescriptionController(IProjectDescriptionService _projectDescriptionService, IMapper mapper)
        {

            projectDescriptionService = _projectDescriptionService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllProjectDesc")]
        public List<ProjectDescriptionViewModel> GetAllProjectDesc()
        {

            return projectDescriptionService.GetAllProjectDesc().Select(k => new ProjectDescriptionViewModel
            {
                ProjectDescriptionMasterProjectDescriptionId = k.ProjectDescriptionMasterProjectDescriptionId,
                ProjectDescriptionMasterProjectDescriptionName = k.ProjectDescriptionMasterProjectDescriptionName,
                ProjectDescriptionMasterProjectDescriptionSortOrder = k.ProjectDescriptionMasterProjectDescriptionSortOrder,
                ProjectDescriptionMasterProjectDescriptionStatus = k.ProjectDescriptionMasterProjectDescriptionStatus,
                ProjectDescriptionMasterProjectDescriptionDelStatus = k.ProjectDescriptionMasterProjectDescriptionDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllProjectDescById/{id}")]
        public List<ProjectDescriptionViewModel> GetAllProjectDescById(int id)
        {
            return projectDescriptionService.GetAllProjectDescById(id).Select(k => new ProjectDescriptionViewModel
            {

                ProjectDescriptionMasterProjectDescriptionId = k.ProjectDescriptionMasterProjectDescriptionId,
                ProjectDescriptionMasterProjectDescriptionName = k.ProjectDescriptionMasterProjectDescriptionName,
                ProjectDescriptionMasterProjectDescriptionSortOrder = k.ProjectDescriptionMasterProjectDescriptionSortOrder,
                ProjectDescriptionMasterProjectDescriptionStatus = k.ProjectDescriptionMasterProjectDescriptionStatus,
                ProjectDescriptionMasterProjectDescriptionDelStatus = k.ProjectDescriptionMasterProjectDescriptionDelStatus

            }).ToList();
        }


        [HttpPost]
        [Route("InsertProjectDesc")]
        public List<ProjectDescriptionViewModel> InsertProjectDesc([FromBody] ProjectDescriptionViewModel projectDescriptionMaster)
        {
            var result = _mapper.Map<ProjectDescriptionMaster>(projectDescriptionMaster);
            return projectDescriptionService.InsertProjectDesc(result).Select(k => new ProjectDescriptionViewModel
            {
                ProjectDescriptionMasterProjectDescriptionId = k.ProjectDescriptionMasterProjectDescriptionId,
                ProjectDescriptionMasterProjectDescriptionName = k.ProjectDescriptionMasterProjectDescriptionName,
                ProjectDescriptionMasterProjectDescriptionSortOrder = k.ProjectDescriptionMasterProjectDescriptionSortOrder,
                ProjectDescriptionMasterProjectDescriptionStatus = k.ProjectDescriptionMasterProjectDescriptionStatus,
                ProjectDescriptionMasterProjectDescriptionDelStatus = k.ProjectDescriptionMasterProjectDescriptionDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateProjectDesc")]
        public List<ProjectDescriptionViewModel> UpdateProjectDesc([FromBody] ProjectDescriptionViewModel projectDescriptionMaster)
        {
            var result = _mapper.Map<ProjectDescriptionMaster>(projectDescriptionMaster);
            return projectDescriptionService.UpdateProjectDesc(result).Select(k => new ProjectDescriptionViewModel
            {
                ProjectDescriptionMasterProjectDescriptionId = k.ProjectDescriptionMasterProjectDescriptionId,
                ProjectDescriptionMasterProjectDescriptionName = k.ProjectDescriptionMasterProjectDescriptionName,
                ProjectDescriptionMasterProjectDescriptionSortOrder = k.ProjectDescriptionMasterProjectDescriptionSortOrder,
                ProjectDescriptionMasterProjectDescriptionStatus = k.ProjectDescriptionMasterProjectDescriptionStatus,
                ProjectDescriptionMasterProjectDescriptionDelStatus = k.ProjectDescriptionMasterProjectDescriptionDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteProjectDesc")]
        public List<ProjectDescriptionViewModel> DeleteProjectDesc([FromBody] ProjectDescriptionViewModel projectDescriptionMaster)
        {
            var result = _mapper.Map<ProjectDescriptionMaster>(projectDescriptionMaster);
            return projectDescriptionService.DeleteProjectDesc(result).Select(k => new ProjectDescriptionViewModel
            {
                ProjectDescriptionMasterProjectDescriptionId = k.ProjectDescriptionMasterProjectDescriptionId,
                ProjectDescriptionMasterProjectDescriptionName = k.ProjectDescriptionMasterProjectDescriptionName,
                ProjectDescriptionMasterProjectDescriptionSortOrder = k.ProjectDescriptionMasterProjectDescriptionSortOrder,
                ProjectDescriptionMasterProjectDescriptionStatus = k.ProjectDescriptionMasterProjectDescriptionStatus,
                ProjectDescriptionMasterProjectDescriptionDelStatus = k.ProjectDescriptionMasterProjectDescriptionDelStatus
            }).ToList();
        }
    }
}