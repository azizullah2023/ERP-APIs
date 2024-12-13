using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IBudgetMasterService
    {
        public IEnumerable<BudgetMaster> InsertBudget(BudgetMaster budgetMaster);
        public IEnumerable<BudgetMaster> UpdateBudget(BudgetMaster budgetMaster);
        public IEnumerable<BudgetMaster> DeleteBudget(BudgetMaster budgetMaster);
        public IEnumerable<BudgetMaster> GetAllBudget();
        public IEnumerable<BudgetMaster> GetAllBudgetById(int id);
    }
}