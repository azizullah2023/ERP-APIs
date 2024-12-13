using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class VendorMaster
    {
        public long? VendorMasterVendorId { get; set; }
        public string VendorMasterVendorName { get; set; }
        public bool? VendorMasterVendorStatus { get; set; }
        public string VendorMasterVendorAddress { get; set; }
        public string VendorMasterVendorPhone { get; set; }
        public string VendorMasterVendorFax { get; set; }
        public string VendorMasterVendorEmail { get; set; }
        public bool? VendorMasterVendorDelStatus { get; set; }
    }
}
