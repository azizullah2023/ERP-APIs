using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ReportFilterViewModel
    {
    
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string Formate { get; set; }
        public int TotalCount { get; set; }
        public string Query { get; set; }
        public string Serach { get; set; }
        public string CostCenter { get; set; }
        public bool HideZeroTransactions { get; set; }
        public string PrintGroupWise { get; set; }

      
    }
}
