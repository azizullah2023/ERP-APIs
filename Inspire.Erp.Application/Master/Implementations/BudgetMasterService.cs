using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class BudgetMasterService : IBudgetMasterService
    {
        private IRepository<BudgetMaster> budgetrepository;
        public BudgetMasterService(IRepository<BudgetMaster> _budgetrepository)
        {
            budgetrepository = _budgetrepository;
        }
        public IEnumerable<BudgetMaster> InsertBudget(BudgetMaster budgetMaster)
        {
            bool valid = false;
            try
            {
                valid = true;
                //int mxc = 0;
                //mxc = (int)budgetrepository.GetAsQueryable().Where(k => k.BudgetMasterBudgetId != null).Select(x => x.BudgetMasterBudgetId).Max();
                //if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                //budgetMaster.BudgetMasterBudgetId = mxc;

                budgetMaster.BudgetMasterBudgetId   = Convert.ToInt32(budgetrepository.GetAsQueryable()
                                                       .Where(x => x.BudgetMasterBudgetId > 0)
                                                       .DefaultIfEmpty()
                                                       .Max(o => o == null ? 0 : o.BudgetMasterBudgetId)) + 1;


                budgetrepository.Insert(budgetMaster);
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
            return this.GetAllBudget();
        }

        //int MXid = countryrepository.GetAsQueryable().Where(k => k.CountryMasterCountryId != 0).Select(X => X.CountryMasterCountryId).Max();
        public IEnumerable<BudgetMaster> UpdateBudget(BudgetMaster budgetMaster)
        {
            bool valid = false;
            try
            {
                budgetrepository.Update(budgetMaster);
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
            return this.GetAllBudget();
        }
        public IEnumerable<BudgetMaster> DeleteBudget(BudgetMaster budgetMaster)
        {
            bool valid = false;
            try
            {
                budgetrepository.Delete(budgetMaster);
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
            return this.GetAllBudget();
        }

        public IEnumerable<BudgetMaster> GetAllBudget()
        {
            IEnumerable<BudgetMaster> budgetMasters;
            try
            {
                budgetMasters = budgetrepository.GetAll();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return budgetMasters;
        }

        public IEnumerable<BudgetMaster> GetAllBudgetById(int id)
        {
            IEnumerable<BudgetMaster> budgetMasters;
            try
            {
                budgetMasters = budgetrepository.GetAsQueryable().Where(k => k.BudgetMasterBudgetId == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return budgetMasters;

        }
    }
}
