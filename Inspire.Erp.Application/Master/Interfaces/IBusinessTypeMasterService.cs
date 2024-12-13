using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IBusinessTypeMasterService
    {
        public IEnumerable<BusinessTypeMaster> InsertBusinessType(BusinessTypeMaster businessTypeMaster);
        public IEnumerable<BusinessTypeMaster> UpdateBusinessType(BusinessTypeMaster businessTypeMaster);
        public IEnumerable<BusinessTypeMaster> DeleteBusinessType(BusinessTypeMaster businessTypeMaster);
        public IEnumerable<BusinessTypeMaster> GetAllBusinessType();
        public IEnumerable<BusinessTypeMaster> GetAllBusinessTypeById(int id);
    }
}