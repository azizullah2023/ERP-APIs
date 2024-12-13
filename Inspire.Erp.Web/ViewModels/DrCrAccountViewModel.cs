using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class DrCrAccountViewModel
    {
        public ChartofAccountsViewModel Debit { get; set; }
        public ChartofAccountsViewModel Credit { get; set; }
    }
}
