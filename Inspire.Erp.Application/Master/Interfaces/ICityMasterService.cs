using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ICityMasterService
    {
        public IEnumerable<CityMaster> InsertCity(CityMaster cityMaster);
        public IEnumerable<CityMaster> UpdateCity(CityMaster cityMaster);
        public IEnumerable<CityMaster> DeleteCity(CityMaster cityMaster);
        public IEnumerable<CityMaster> GetAllCity();
        public IEnumerable<CityMaster> GetAllCityById(int id);
    }
}