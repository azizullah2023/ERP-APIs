using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.DTO.Job_Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inspire.Erp.Domain.DTO.Supplier;

namespace Inspire.Erp.Application.Master
{
    public interface ISupplierMasterService
    {
        public Task<SuppliersMaster> InsertSupplier(SuppliersMaster SuppliersMaster);
        public SuppliersMaster UpdateSupplier(SuppliersMaster SuppliersMaster);
        public SuppliersMaster DeleteSupplier(SuppliersMaster SuppliersMaster);
        public IEnumerable<SuppliersMaster> GetAllSupplier();
        public SuppliersMaster GetAllSupplierById(int id);
        public IEnumerable<ItemMasterSupplierDetais> GetUpdatedSupplierDetailsByItem(int itemId);
        public Task<Response<List<SupplierSearchListDto>>> GetSupplierFilteredList(string supplierName);

    }
}
