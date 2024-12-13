using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CountryMasterViewModel

    {
        public int? CountryMasterCountryId { get; set; }
        public string CountryMasterCountryName { get; set; }
        public string CountryMasterCountryIsdCode { get; set; }
        public int? CountryMasterCountryUserId { get; set; }
        public bool? CountryMasterCountryStatus { get; set; }
        public double? CountryMasterCountryAmount { get; set; }
        public bool? CountryMasterCountryDelStatus { get; set; }
    }
}
