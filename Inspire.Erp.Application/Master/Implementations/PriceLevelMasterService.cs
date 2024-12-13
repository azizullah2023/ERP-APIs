using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class PriceLevelMasterService : IPriceLevelMasterService
    {
        private IRepository<PriceLevelMaster> priceLevelrepository;
        public PriceLevelMasterService(IRepository<PriceLevelMaster> _priceLevelrepository)
        {
            priceLevelrepository = _priceLevelrepository;
        }
        public IEnumerable<PriceLevelMaster> InsertPriceLevel(PriceLevelMaster priceLevelMaster)
        {
            bool valid = false;
            try
            {
                valid = true;

                int mxc = Convert.ToInt32(priceLevelrepository.GetAsQueryable()
                                      .Where(x => x.PriceLevelMasterPriceLevelId > 0)
                                      .DefaultIfEmpty()
                                      .Max(o => o == null ? 0 : o.PriceLevelMasterPriceLevelId)) + 1;

                priceLevelMaster.PriceLevelMasterPriceLevelId = mxc;
                priceLevelrepository.Insert(priceLevelMaster);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //priceLevelrepository.Dispose();
            }
            return this.GetAllPriceLevel();
        }
        public IEnumerable<PriceLevelMaster> UpdatePriceLevel(PriceLevelMaster priceLevelMaster)
        {
            bool valid = false;
            try
            {
                priceLevelrepository.Update(priceLevelMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //priceLevelrepository.Dispose();
            }
            return this.GetAllPriceLevel();
        }
        public IEnumerable<PriceLevelMaster> DeletePriceLevel(PriceLevelMaster priceLevelMaster)
        {
            bool valid = false;
            try
            {
                priceLevelrepository.Delete(priceLevelMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //priceLevelrepository.Dispose();
            }
            return this.GetAllPriceLevel();
        }

        public IEnumerable<PriceLevelMaster> GetAllPriceLevel()
        {
            IEnumerable<PriceLevelMaster> priceLevelMasters;
            try
            {
                priceLevelMasters = priceLevelrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //priceLevelrepository.Dispose();
            }
            return priceLevelMasters;
        }
        public IEnumerable<PriceLevelMaster> GetAllPriceLevelById(int id)
        {
            IEnumerable<PriceLevelMaster> priceLevelMasters;
            try
            {
                priceLevelMasters = priceLevelrepository.GetAsQueryable().Where(k => k.PriceLevelMasterPriceLevelId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return priceLevelMasters;

        }

    }
}
