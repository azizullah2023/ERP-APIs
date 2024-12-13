using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.StoreWareHouse.Interface
{
    public interface IDamageEntry
    {
        public DamageMaster GetDamageById(int? id);
        public DamageMaster InsertDamageEntry(DamageMaster JobDamageMasterViewModel);
        public IEnumerable<DamageMaster> GetDamageEntry();
        public IEnumerable<DamageMaster> DeleteDamageEntry(string Id);
        public DamageMaster UpdateDamageEntry(DamageMaster JobDamageMasterViewModel);
        public DamageMaster GetDamageEntryVoucherNo(string? id);    
    }
}