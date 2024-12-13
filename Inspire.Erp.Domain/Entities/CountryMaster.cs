using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CountryMaster
    {
        public int CountryMasterCountryId { get; set; }
        public string CountryMasterCountryName { get; set; }
        public string CountryMasterCountryIsdCode { get; set; }
        public int? CountryMasterCountryUserId { get; set; }
        public bool? CountryMasterCountryStatus { get; set; }
        public double? CountryMasterCountryAmount { get; set; }
        public bool? CountryMasterCountryDelStatus { get; set; }
    }
}
