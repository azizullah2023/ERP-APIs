using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master.Implementations
{    public class RouteMasterService : IRouteMasterService
    {
        private IRepository<RouteMaster> routerepository;
        public RouteMasterService(IRepository<RouteMaster> _Routerepository)
        {
            routerepository = _Routerepository;
        }
        public IEnumerable<RouteMaster> InsertRouteMaster(RouteMaster RouteMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                ////TaxMaster.CityMasterCityId = Convert.ToInt32(cityrepository.GetAsQueryable()
                ////                                       .Where(x => x.CityMasterCityId > 0)
                ////                                       .DefaultIfEmpty()
                ////                                       .Max(o => o == null ? 0 : o.CityMasterCityId)) + 1;
                routerepository.Insert(RouteMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {

            }
            return this.GetAllRoute();
        }
        public IEnumerable<RouteMaster> UpdateRouteMaster(RouteMaster RouteMaster)
        {
            bool valid = false;
            try
            {
                routerepository.Update(RouteMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {

            }
            return this.GetAllRoute();
        }
        public IEnumerable<RouteMaster> DeleteRouteMaster(RouteMaster RouteMaster)
        {
            bool valid = false;
            try
            {
                routerepository.Delete(RouteMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {

            }
            return this.GetAllRoute();
        }

        public IEnumerable<RouteMaster> GetAllRoute()
        {
            IEnumerable<RouteMaster> routeMasters;
            try
            {
                routeMasters = routerepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return routeMasters;
        }

        public IEnumerable<RouteMaster> GetAllRouteById(int id)
        {
            IEnumerable<RouteMaster> routeMasters;
            try
            {
                routeMasters = routerepository.GetAsQueryable().Where(k => k.RmId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {

            }
            return routeMasters;
        }
    }
}
  