using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ISalesmanMasterService
    {
        public IEnumerable<SalesManMaster> InsertSalesman(SalesManMaster salesmanMaster);
        public IEnumerable<SalesManMaster> UpdateSalesman(SalesManMaster salesmanMaster);
        public IEnumerable<SalesManMaster> DeleteSalesman(SalesManMaster salesmanMaster);
        public IEnumerable<SalesManMaster> GetAllSalesman();
        public IEnumerable<SalesManMaster> GetAllSalesmanById (int id);
    }
}