using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PackageMaster
    {
        public int PackageMasterId { get; set; }
        public string PackageMasterDdcCode { get; set; }
        public string PackageMasterPackageName { get; set; }
        public string PackageMasterPackageDetails { get; set; }
        public string PackageMasterManufacturer { get; set; }
        public int? PackageMasterSupplierId { get; set; }
        public bool? PackageMasterDelStatus { get; set; }
    }
}
