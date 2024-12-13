using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CostCenterViewModel

    {
        public int? CostCenterMasterCostCenterId { get; set; }
        public string CostCenterMasterCostCenterName { get; set; }
        public bool? CostCenterMasterCostCenterStatus { get; set; }
        public bool? CostCenterMasterCostCenterIsSystem { get; set; }
        public int? CostCenterMasterCostCenterSortOrder { get; set; }
        public bool? CostCenterMasterCostCenterDelStatus { get; set; }
    }
}