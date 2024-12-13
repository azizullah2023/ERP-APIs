using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CityMaster
    {
        public int CityMasterCityId { get; set; }
        public int? CityMasterCityCountryId { get; set; }
        public string CityMasterCityName { get; set; }
        public bool? CityMasterCityDeleted { get; set; }
        public bool? CityMasterCityStatus { get; set; }
        public bool? CityMasterCityDelStatus { get; set; }
    }
}
