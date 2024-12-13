using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class StaffMasterViewModel
    {
        public int? StaffMasterStaffId { get; set; }
        public string StaffMasterStaffCode { get; set; }
        public string StaffMasterStaffName { get; set; }
        public bool? StaffMasterStaffDelStatus { get; set; }
    }
}
