using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Application.Master.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master.Implementations

{
    public class TaxMasterService : ITaxMasterService
    {
        private IRepository<TaxMaster> taxrepository;
        public TaxMasterService(IRepository<TaxMaster> _Taxrepository)
        {
            taxrepository = _Taxrepository;
        }
        public IEnumerable<TaxMaster> InsertTaxMaster(TaxMaster TaxMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                ////TaxMaster.CityMasterCityId = Convert.ToInt32(cityrepository.GetAsQueryable()
                ////                                       .Where(x => x.CityMasterCityId > 0)
                ////                                       .DefaultIfEmpty()
                ////                                       .Max(o => o == null ? 0 : o.CityMasterCityId)) + 1;
                ///
                //int? mxc = 0;
                //mxc =
                //    cityrepository.GetAsQueryable()
                //    .DefaultIfEmpty().Max(o => o == null ? 0 : o.CityMasterCityId) + 1;

                taxrepository.Insert(TaxMaster);
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
            return this.GetAllTax();
        }
        public IEnumerable<TaxMaster> UpdateTaxMaster(TaxMaster TaxMaster)
        {
            bool valid = false;
            try
            {
                taxrepository.Update(TaxMaster);
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
            return this.GetAllTax();
        }
        public IEnumerable<TaxMaster> DeleteTaxMaster(TaxMaster TaxMaster)
        {
            bool valid = false;
            try
            {
                taxrepository.Delete(TaxMaster);
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
            return this.GetAllTax();
        }

        public IEnumerable<TaxMaster> GetAllTax()
        {
            IEnumerable<TaxMaster> taxMasters;
            try
            {
                taxMasters = taxrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return taxMasters;
        }

        public IEnumerable<TaxMaster> GetAllTaxById(int id)
        {
            IEnumerable<TaxMaster> taxMasters;
            try
            {
                taxMasters = taxrepository.GetAsQueryable().Where(k => k.TmId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return taxMasters;

        }

    }
}
