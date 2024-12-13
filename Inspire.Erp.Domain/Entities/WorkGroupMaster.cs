using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class WorkGroupMaster
    {
        public WorkGroupMaster()
        {
            WorkGroupPermissions = new HashSet<WorkGroupPermissions>();

        }
        public int WorkGroupMasterWorkGroupId { get; set; }
        public string WorkGroupMasterWorkGroupName { get; set; }
        public bool? WorkGroupMasterWorkGroupDelStatus { get; set; }
        //public ICollection<WorkGroupMenuDetail> MenusWorkgroupDetails { get; set; }

        public ICollection<WorkGroupPermissions> WorkGroupPermissions { get; set; }
    }
}
