using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.StoreWareHouse.Interface
{
    public interface IAlterItemsService
    {
        AltItemMaster save(AltItemMaster grn);
        IQueryable Delete(int id);
        AltItemMaster update(AltItemMaster grn, List<AltItemDetails> altItemDetails);               
        public AltItemMaster GetByID(int id);
        public IQueryable GetAll();

    }
}
