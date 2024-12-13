using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class LocationMaster
    {
        public int? LocationMasterLocationId { get; set; }
        public string LocationMasterLocationName { get; set; }
        public string LocationMasterLocationAddress { get; set; }
        public bool? LocationMasterLocationDeleted { get; set; }
        public bool? LocationMasterLocationStatus { get; set; }
        public string LocationMasterLocationTelephone { get; set; }
        public string LocationMasterLocationFax { get; set; }
        public string LocationMasterLocationEmail { get; set; }
        public string LocationMasterLocationCashAccount { get; set; }
        public string LocationMasterLocationCostCenter { get; set; }
        public bool? LocationMasterLocationDelStatus { get; set; }
    }
}
