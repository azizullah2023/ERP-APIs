using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IProjectDescriptionService
    {
        public IEnumerable<ProjectDescriptionMaster> InsertProjectDesc(ProjectDescriptionMaster projectDescriptionMaster);
        public IEnumerable<ProjectDescriptionMaster> UpdateProjectDesc(ProjectDescriptionMaster projectDescriptionMaster);
        public IEnumerable<ProjectDescriptionMaster> DeleteProjectDesc(ProjectDescriptionMaster projectDescriptionMaster);
        public IEnumerable<ProjectDescriptionMaster> GetAllProjectDesc();
        public IEnumerable<ProjectDescriptionMaster> GetAllProjectDescById(int id);
    }
}