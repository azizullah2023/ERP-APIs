using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ICostCenterMasterService
    {
        public IEnumerable<CostCenterMaster> InsertCostCenter(CostCenterMaster costCenterMaster);
        public IEnumerable<CostCenterMaster> UpdateCostCenter(CostCenterMaster costCenterMaster);
        public IEnumerable<CostCenterMaster> DeleteCostCenter(CostCenterMaster costCenterMaster);
        public IEnumerable<CostCenterMaster> GetAllCostCenter();
        public IEnumerable<CostCenterMaster> GetAllCostCenterById(int id);
    }
}