using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IBrandMasterService
    {
        public IEnumerable<VendorMaster> InsertBrand(VendorMaster vendorMaster);
        public IEnumerable<VendorMaster> UpdateBrand(VendorMaster vendorMaster);
        public IEnumerable<VendorMaster> DeleteBrand(VendorMaster vendorMaster);
        public IEnumerable<VendorMaster> GetAllBrand();
        public IEnumerable<VendorMaster> GetAllBrandById(int id);
    }
}