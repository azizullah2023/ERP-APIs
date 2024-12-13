using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DepartmentMaster
    {
        public int DepartmentMasterDepartmentId { get; set; }
        public string DepartmentMasterDepartmentName { get; set; }
        public string DepartmentMasterDepartmentCode { get; set; }
        public bool? DepartmentMasterDepartmentStatus { get; set; }
        public bool? DepartmentMasterDepartmentDelStatus { get; set; }
    }
}
