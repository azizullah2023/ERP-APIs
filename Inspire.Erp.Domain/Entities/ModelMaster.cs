using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ModelMaster
    {
        public int? ModelMasterId { get; set; }
        public int? ModelMasterTypeId { get; set; }
        public string ModelMasterName { get; set; }
        public int? ModelMasterUserId { get; set; }
        public bool? ModelMasterDeleted { get; set; }
        public bool? ModelMasterStatus { get; set; }
        public bool? ModelMasterDelStatus { get; set; }
    }
}
