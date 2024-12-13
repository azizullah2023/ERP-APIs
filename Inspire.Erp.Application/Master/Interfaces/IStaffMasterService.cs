using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IStaffMasterService
    {
        public IEnumerable<StaffMaster> InsertStaff(StaffMaster staffMaster);
        public IEnumerable<StaffMaster> UpdateStaff(StaffMaster staffMaster);
        public IEnumerable<StaffMaster> DeleteStaff(StaffMaster staffMaster);
        public IEnumerable<StaffMaster> GetAllStaff();
        public IEnumerable<StaffMaster> GetAllStaffById(int id);
    }
}