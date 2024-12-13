using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UnitMaster
    {
        public UnitMaster()
        {
            UnitDetails = new HashSet<UnitDetails>();
        }

        public int UnitMasterUnitId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public string UnitMasterUnitFullName { get; set; }
        public string UnitMasterUnitDescription { get; set; }
        public bool? UnitMasterUnitStatus { get; set; }
        public bool? UnitMasterUnitDelStatus { get; set; }

        public virtual ICollection<UnitDetails> UnitDetails { get; set; }
    }
}
