using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Master
{
    public interface IUnitMasterService
    {
        public IEnumerable<UnitMaster> InsertUnit(UnitMaster unitMaster);
        public IEnumerable<UnitMaster> UpdateUnit(UnitMaster unitMaster);
        public IEnumerable<UnitMaster> DeleteUnit(UnitMaster unitMaster);
        public IEnumerable<UnitMaster> GetAllUnit();
        public Task<Response<List<GetUnitDetailsMasterList>>> GetUnitDetailsByItemId(long itemId);
        public IEnumerable<UnitMaster> GetAllUnitById(int id);
    }
}
