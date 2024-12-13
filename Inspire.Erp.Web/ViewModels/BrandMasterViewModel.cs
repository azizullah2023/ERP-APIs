using IInspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public  class BrandMasterViewModel: BaseEntity
    {
        public long? VendorMasterVendorId { get; set; }
        public string? VendorMasterVendorName { get; set; }
        public bool? VendorMasterVendorStatus { get; set; }
        public string? VendorMasterVendorAddress { get; set; }
        public string? VendorMasterVendorPhone { get; set; }
        public string? VendorMasterVendorFax { get; set; }
        public string? VendorMasterVendorEmail { get; set; }
        public bool? VendorMasterVendorDelStatus { get; set; }

    }
}