using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobCordinator
    {
        public int? JobCordinatorCode { get; set; }
        public string JobCordinatorDescription { get; set; }
        public bool? JobCordinatorActive { get; set; }
        public bool? JobCordinatorDelStatus { get; set; }
    }
}
