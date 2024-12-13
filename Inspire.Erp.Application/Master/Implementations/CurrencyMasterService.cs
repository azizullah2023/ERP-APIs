using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
  public class CurrencyMasterService: ICurrencyMasterService
    {
        private IRepository<CurrencyMaster> currencyrepository;
        public CurrencyMasterService(IRepository<CurrencyMaster> _currencyrepository)
                   {
            currencyrepository = _currencyrepository;
        }
        public IEnumerable<CurrencyMaster> InsertCurrency(CurrencyMaster currencyMaster) {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = currencyrepository.GetAsQueryable().Where(k => k.CurrencyMasterCurrencyId != null).Select(x => x.CurrencyMasterCurrencyId).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                currencyMaster.CurrencyMasterCurrencyId = mxc;

                currencyrepository.Insert(currencyMaster);
            }
            catch(Exception ex)
            {
                valid = false;
                throw ex;
                
            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllCurrency();
        }
        public IEnumerable<CurrencyMaster> UpdateCurrency(CurrencyMaster currencyMaster)
        {
            bool valid = false;
            try
            {
                currencyrepository.Update(currencyMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllCurrency();
        }
        public IEnumerable<CurrencyMaster> DeleteCurrency(CurrencyMaster currencyMaster)
        {
            bool valid = false;
            try
            {
                currencyrepository.Delete(currencyMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return this.GetAllCurrency();
        }

        public IEnumerable<CurrencyMaster> GetAllCurrency()
        {
            IEnumerable<CurrencyMaster> currencyMasters;
            try
            {
                currencyMasters  =  currencyrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //currencyrepository.Dispose();
            }
            return currencyMasters;
        }

        public IEnumerable<CurrencyMaster> GetAllCurrencyById(int id)
        {
            IEnumerable<CurrencyMaster> currencyMasters;
            try
            {
                currencyMasters = currencyrepository.GetAsQueryable().Where(k => k.CurrencyMasterCurrencyId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return currencyMasters;

        }

    }
}
