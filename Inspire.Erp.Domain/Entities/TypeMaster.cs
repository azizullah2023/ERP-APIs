using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TypeMaster
    {
        public int? TypeMasterTypeId { get; set; }
        public int? TypeMasterVendorId { get; set; }
        public string TypeMasterTypeName { get; set; }
        public int? TypeMasterUserId { get; set; }
        public int? TypeMasterDeleted { get; set; }
        public bool? TypeMasterStatus { get; set; }
        public bool? TypeMasterDelStatus { get; set; }
    }
}
