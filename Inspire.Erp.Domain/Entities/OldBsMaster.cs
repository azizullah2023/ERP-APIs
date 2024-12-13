using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OldBsMaster
    {
        public int? OldBsMasterId { get; set; }
        public string OldBsMasterDescription { get; set; }
        public string OldBsMasterPost { get; set; }
        public int? OldBsMasterFsno { get; set; }
        public string OldBsMasterRefJv { get; set; }
        public bool? OldBsMasterDelStatus { get; set; }
    }
}
