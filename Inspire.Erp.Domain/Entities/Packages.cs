using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class Packages
    {
        public int? PackagesId { get; set; }
        public string PackagesName { get; set; }
        public int? PackagesUserId { get; set; }
        public bool? PackagesDeleted { get; set; }
        public bool? PackagesStatus { get; set; }
        public bool? PackagesDelStatus { get; set; }
    }
}
