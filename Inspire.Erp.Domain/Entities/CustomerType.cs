using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerType
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public int? CustomerTypeUserId { get; set; }
        public bool? CustomerTypeDeleted { get; set; }
        public bool? CustomerTypeStatus { get; set; }
        public bool? CustomerTypeDelStatus { get; set; }
    }
}
