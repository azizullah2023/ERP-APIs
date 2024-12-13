using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UserPermission
    {
        public int UserPermissionSno { get; set; }
        public int? UserPermissionFormId { get; set; }
        public int? UserPermissionWorkGroupId { get; set; }
        public bool? UserPermissionUallow { get; set; }
        public bool? UserPermissionUadd { get; set; }
        public bool? UserPermissionUedit { get; set; }
        public bool? UserPermissionUdelete { get; set; }
        public bool? UserPermissionUview { get; set; }
        public bool? UserPermissionUprint { get; set; }
        public bool? UserPermissionDelStatus { get; set; }
    }
}
