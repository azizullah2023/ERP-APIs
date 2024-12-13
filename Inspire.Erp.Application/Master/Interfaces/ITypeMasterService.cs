using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ITypeMasterService
    {
        public IEnumerable<TypeMaster> InsertTypeMast(TypeMaster typeMaster);
        public IEnumerable<TypeMaster> UpdateTypeMast(TypeMaster typeMaster);
        public IEnumerable<TypeMaster> DeleteTypeMast(TypeMaster typeMaster);
        public IEnumerable<TypeMaster> GetAllTypeMast();
        public IEnumerable<TypeMaster> GetAllTypeMastById(int id);
    }
}