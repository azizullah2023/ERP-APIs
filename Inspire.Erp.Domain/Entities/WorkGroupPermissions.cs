using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class WorkGroupPermissions
    {

      
        public int WorkGroupPermissionsSno { get; set; }
        public int? WorkGroupPermissionsMenuId { get; set; }
        public int? WorkGroupPermissionsWorkGroupId { get; set; }
        public bool? WorkGroupPermissionsUallow { get; set; }
        public bool? WorkGroupPermissionsUadd { get; set; }
        public bool? WorkGroupPermissionsUedit { get; set; }
        public bool? WorkGroupPermissionsUdelete { get; set; }
        public bool? WorkGroupPermissionsUview { get; set; }
        public bool? WorkGroupPermissionsUprint { get; set; }
        public bool? WorkGroupPermissionsDelStatus { get; set; }
        public string WorkGroupPermissionsFormType { get; set; }

        public virtual WorkGroupMaster WorkGroupMaster { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
