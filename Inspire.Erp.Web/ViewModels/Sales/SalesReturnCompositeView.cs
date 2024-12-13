using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public class SalesReturnCompositeView
    {
       public SalesReturnViewModel SalesReturn { get; set; }
        public IList<SalesReturnDetailsViewModel> SalesReturnDetails { get; set; }


        
    }
}
