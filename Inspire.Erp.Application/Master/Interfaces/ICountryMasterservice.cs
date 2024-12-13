using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ICountryMasterService
    { 
        public IEnumerable<CountryMaster> InsertCountry(CountryMaster countryMaster);
        public IEnumerable<CountryMaster> UpdateCountry(CountryMaster countryMaster);
        public IEnumerable<CountryMaster> DeleteCountry(CountryMaster countryMaster);
        public IEnumerable<CountryMaster> GetAllCountry();
        public IEnumerable<CountryMaster> GetAllCountryById(int id);
        
    }
}