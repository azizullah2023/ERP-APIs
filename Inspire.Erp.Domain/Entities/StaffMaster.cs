using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StaffMaster
    {
        public int StaffMasterStaffId { get; set; }
        public string StaffMasterStaffCode { get; set; }
        public string StaffMasterStaffName { get; set; }
        public bool? StaffMasterStaffDelStatus { get; set; }
    }
}
