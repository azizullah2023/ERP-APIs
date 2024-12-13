using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class CityMasterService : ICityMasterService
    {
        private IRepository<CityMaster> cityrepository;
        public CityMasterService(IRepository<CityMaster> _Cityrepository)
        {
            cityrepository = _Cityrepository;
        }
        public IEnumerable<CityMaster> InsertCity(CityMaster CityMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                CityMaster.CityMasterCityId = Convert.ToInt32(cityrepository.GetAsQueryable()
                                                       .Where(x => x.CityMasterCityId > 0)
                                                       .DefaultIfEmpty()
                                                       .Max(o => o == null ? 0 : o.CityMasterCityId)) + 1;
                cityrepository.Insert(CityMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCity();
        }
        public IEnumerable<CityMaster> UpdateCity(CityMaster CityMaster)
        {
            bool valid = false;
            try
            {
                cityrepository.Update(CityMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCity();
        }
        public IEnumerable<CityMaster> DeleteCity(CityMaster CityMaster)
        {
            bool valid = false;
            try
            {
                cityrepository.Delete(CityMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllCity();
        }

        public IEnumerable<CityMaster> GetAllCity()
        {
            IEnumerable<CityMaster> cityMasters;
            try
            {
                cityMasters = cityrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return cityMasters;
        }

        public IEnumerable<CityMaster> GetAllCityById(int id)
        {
            IEnumerable<CityMaster> cityMasters;
            try
            {
                cityMasters = cityrepository.GetAsQueryable().Where(k => k.CityMasterCityId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return cityMasters;

        }

    }
}
