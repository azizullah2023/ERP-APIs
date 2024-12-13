using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IDepartmentMasterService
    {
        public IEnumerable<DepartmentMaster> InsertDepartment(DepartmentMaster departmentMaster);
        public IEnumerable<DepartmentMaster> UpdateDepartment(DepartmentMaster departmentMaster);
        public IEnumerable<DepartmentMaster> DeleteDepartment(DepartmentMaster departmentMaster);
        public IEnumerable<DepartmentMaster> GetAllDepartment();
        public IEnumerable<DepartmentMaster> GetAllDepartmentById(int id);
    }
}