using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AllocationType
    {
        public int AllocationTypeId { get; set; }
        public string AllocationTypeName { get; set; }
        public int? AllocationTypeStatus { get; set; }
        public bool? AllocationTypeDelStatus { get; set; }
    }
}
