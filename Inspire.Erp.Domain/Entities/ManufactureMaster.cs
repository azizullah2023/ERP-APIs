using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ManufactureMaster
    {
        public int? ManufactureMasterId { get; set; }
        public string ManufactureMasterName { get; set; }
        public int? ManufactureMasterUserId { get; set; }
        public bool? ManufactureMasterDeleted { get; set; }
        public bool? ManufactureMasterStatus { get; set; }
        public bool? ManufactureMasterDelStatus { get; set; }
    }
}
