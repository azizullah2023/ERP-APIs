


using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IWorkGroupMasterService
    {
        public IEnumerable<WorkGroupMaster> InsertWorkGroup(WorkGroupMaster workGroupMaster);
        public IEnumerable<WorkGroupMaster> UpdateWorkGroup(WorkGroupMaster workGroupMaster);
        public IEnumerable<WorkGroupMaster> DeleteWorkGroup(WorkGroupMaster workGroupMaster);
        public IEnumerable<WorkGroupMaster> GetAllWorkGroup();
        public IEnumerable<WorkGroupMaster> GetAllWorkGroupById(int id);
    }
}
