using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class CountryMasterService : ICountryMasterService
    {
        private IRepository<CountryMaster> countryrepository;
        public CountryMasterService(IRepository<CountryMaster> _countryrepository)
        {
            countryrepository = _countryrepository;
        }
        public IEnumerable<CountryMaster> InsertCountry(CountryMaster CountryMaster)
        {
            bool valid = false;
            try
            { 
                valid = true;
              

                //CountryMaster.CountryMasterCountryId = Convert.ToInt32(countryrepository.GetAsQueryable()
                //                                        .Where(x => x.CountryMasterCountryId > 0)
                //                                        .DefaultIfEmpty()
                //                                        .Max(o => o == null ? 0 : o.CountryMasterCountryId)) + 1;

                countryrepository.Insert(CountryMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //countryrepository.Dispose();
            }
            return this.GetAllCountry();
        }
        public IEnumerable<CountryMaster> UpdateCountry(CountryMaster CountryMaster)
        {
            bool valid = false;
            try
            {
                countryrepository.Update(CountryMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //countryrepository.Dispose();
            }
            return this.GetAllCountry();
        }
        public IEnumerable<CountryMaster> DeleteCountry(CountryMaster CountryMaster)
        {
            bool valid = false;
            try
            {
                countryrepository.Delete(CountryMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //countryrepository.Dispose();
            }
            return this.GetAllCountry();
        }

        public IEnumerable<CountryMaster> GetAllCountry()
        {
            IEnumerable<CountryMaster> countryMasters;
            try
            {
                countryMasters = countryrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //countryrepository.Dispose();
            }
            return countryMasters;
        }

        public IEnumerable<CountryMaster> GetAllCountryById(int id)
        {
            IEnumerable<CountryMaster> countryMasters;
            try
            {
                countryMasters = countryrepository.GetAsQueryable().Where(k => k.CountryMasterCountryId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return countryMasters;

        }

    }
}
