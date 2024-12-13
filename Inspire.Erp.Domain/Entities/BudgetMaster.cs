using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BudgetMaster
    {
        public int BudgetMasterBudgetId { get; set; }
        public string BudgetMasterBudgetName { get; set; }
        public bool? BudgetMasterBudgetDelStatus { get; set; }
    }
}
