using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ServiceMaster
    {
        public int? ServiceMasterId { get; set; }
        public string ServiceMasterCode { get; set; }
        public bool? ServiceMasterDelStatus { get; set; }
    }
}
