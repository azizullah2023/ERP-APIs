using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobBudgetDetails
    {
        public int? JobBudgetDetailsId { get; set; }
        public int? JobBudgetDetailsSno { get; set; }
        public int? JobBudgetDetailsBudgetId { get; set; }
        public double? JobBudgetDetailsBudgetAmount { get; set; }
        public bool? JobBudgetDetailsDelStatus { get; set; }
    }
}
