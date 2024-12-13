using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ILocationMasterService
    {
        public IEnumerable<LocationMaster> InsertLocation(LocationMaster locationMaster);
        public IEnumerable<LocationMaster> UpdateLocation(LocationMaster locationMaster);
        public IEnumerable<LocationMaster> DeleteLocation(LocationMaster locationMaster);
        public IEnumerable<LocationMaster> GetAllLocation();
        public IEnumerable<LocationMaster> GetAllLocationById(int id);
        public IEnumerable<FinancialPeriods> GetAllFinancialPeriod();
    }
}