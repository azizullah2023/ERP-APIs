using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public  class DepartmentMasterViewModel
    {
        public int? DepartmentMasterDepartmentId { get; set; }
        public string? DepartmentMasterDepartmentName { get; set; }
        public string? DepartmentMasterDepartmentCode { get; set; }
        public bool? DepartmentMasterDepartmentStatus { get; set; }
        public bool? DepartmentMasterDepartmentDelStatus { get; set; }
    }

}
