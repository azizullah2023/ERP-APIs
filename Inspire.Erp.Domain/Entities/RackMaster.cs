using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class RackMaster
    {
        public int? RackMasterId { get; set; }
        public string RackMasterName { get; set; }
        public bool? RackMasterStatus { get; set; }
        public bool? RackMasterDelStatus { get; set; }
    }
}
