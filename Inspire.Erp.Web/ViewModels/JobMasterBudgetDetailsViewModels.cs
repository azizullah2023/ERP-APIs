using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JobMasterBudgetDetailsViewModels
    {
        public int JobMasterBudgetDetailsDtlId { get; set; } 
        public int? JobMasterBudgetDetailsJobId { get; set; }
        public string JobMasterNo { get; set; }
       // public int? JobMasterBudgetDetailsSno { get; set; }
        public int? JobMasterBudgetDetailsBudId { get; set; }
        public decimal? JobMasterBudgetDetailsBudAmount { get; set; }
        public bool? JobMasterBudgetDetailsDelStatus { get; set; }
        public decimal? JobMasterBudgetDetailsActual { get; set; }
        public decimal? JobMasterBudgetDetailsVariance { get; set; }

    }
}
