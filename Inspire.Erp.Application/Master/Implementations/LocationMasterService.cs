using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class LocationMasterService : ILocationMasterService
    {
        private IRepository<LocationMaster> locationrepository;
        private IRepository<FinancialPeriods> financialrepository;
        public LocationMasterService(IRepository<LocationMaster> _locationrepository, IRepository<FinancialPeriods> _financialrepository)
        {
            locationrepository = _locationrepository;
            financialrepository = _financialrepository;
        }
        public IEnumerable<LocationMaster> InsertLocation(LocationMaster LocationMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = (int)locationrepository.GetAsQueryable().Where(k => k.LocationMasterLocationId != null).Select(x => x.LocationMasterLocationId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                LocationMaster.LocationMasterLocationId = mxc;
                locationrepository.Insert(LocationMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //locationrepository.Dispose();
            }
            return this.GetAllLocation();
        }
        public IEnumerable<LocationMaster> UpdateLocation(LocationMaster LocationMaster)
        {
            bool valid = false;
            try
            {
                locationrepository.Update(LocationMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //locationrepository.Dispose();
            }
            return this.GetAllLocation();
        }
        public IEnumerable<LocationMaster> DeleteLocation(LocationMaster LocationMaster)
        {
            bool valid = false;
            try
            {
                locationrepository.Delete(LocationMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //locationrepository.Dispose();
            }
            return this.GetAllLocation();
        }

        public IEnumerable<LocationMaster> GetAllLocation()
        {
            IEnumerable<LocationMaster> LocationMaster;
            try
            {
                LocationMaster = locationrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //locationrepository.Dispose();
            }
            return LocationMaster;
        }

        public IEnumerable<LocationMaster> GetAllLocationById(int id)
        {
            IEnumerable<LocationMaster> LocationMaster;
            try
            {
                LocationMaster = locationrepository.GetAsQueryable().Where(k => k.LocationMasterLocationId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return LocationMaster;

        }

        public IEnumerable<FinancialPeriods> GetAllFinancialPeriod()
        {
            return financialrepository.GetAsQueryable().Where(x => x.FinancialPeriodsFsno != null).OrderByDescending(x => x.FinancialPeriodsFsno).Select(k => k);
        }

    }
}
