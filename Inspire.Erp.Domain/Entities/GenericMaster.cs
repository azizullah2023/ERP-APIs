using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GenericMaster
    {
        public int? GenericMasterId { get; set; }
        public string GenericMasterName { get; set; }
        public int? GenericMasterUserId { get; set; }
        public bool? GenericMasterDeleted { get; set; }
        public bool? GenericMasterStatus { get; set; }
        public bool? GenericMasterDelStatus { get; set; }
    }
}
