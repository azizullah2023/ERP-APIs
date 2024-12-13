using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CityMasterViewModel

    {
        public int? CityMasterCityId { get; set; }
        public int? CityMasterCityCountryId { get; set; }
        public string CityMasterCityName { get; set; }
        public bool? CityMasterCityDeleted { get; set; }
        public bool? CityMasterCityStatus { get; set; }
        public bool? CityMasterCityDelStatus { get; set; }

    }
}
