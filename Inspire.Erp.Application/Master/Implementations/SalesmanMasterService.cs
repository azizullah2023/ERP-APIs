using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class SalesmanMasterService : ISalesmanMasterService
    {
        private IRepository<SalesManMaster> salesmanrepository;
        public SalesmanMasterService(IRepository<SalesManMaster> _countryrepository)
        {
            salesmanrepository = _countryrepository;
        }
        public IEnumerable<SalesManMaster> InsertSalesman(SalesManMaster SalesManMaster)
        {
            bool valid = false;
            try
            {
                int? mxc = 0;
                mxc =
                    salesmanrepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.SalesManMasterSalesManId) + 1;

                SalesManMaster.SalesManMasterSalesManId = mxc;
                salesmanrepository.Insert(SalesManMaster);
                GetAllSalesmanById((int)SalesManMaster.SalesManMasterSalesManId);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //salesmanrepository.Dispose();
            }
            return this.GetAllSalesman();
        }
        public IEnumerable<SalesManMaster> UpdateSalesman(SalesManMaster SalesManMaster)
        {
            bool valid = false;
            try
            {
                salesmanrepository.Update(SalesManMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //salesmanrepository.Dispose();
            }
            return this.GetAllSalesman();
        }
        public IEnumerable<SalesManMaster> DeleteSalesman(SalesManMaster SalesManMaster)
        {
            bool valid = false;
            try
            {
                salesmanrepository.Delete(SalesManMaster);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //salesmanrepository.Dispose();
            }
            return this.GetAllSalesman();
        }

        public IEnumerable<SalesManMaster> GetAllSalesman()
        {
            IEnumerable<SalesManMaster> SalesManMaster;
            try
            {
                SalesManMaster = salesmanrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //salesmanrepository.Dispose();
            }
            return SalesManMaster;
        }
        public IEnumerable<SalesManMaster> GetAllSalesmanById(int id)
        {
            IEnumerable<SalesManMaster> SalesManMaster;
            try
            {
                SalesManMaster = salesmanrepository.GetAsQueryable().Where(k => k.SalesManMasterSalesManId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return SalesManMaster;

        }

    }
}
