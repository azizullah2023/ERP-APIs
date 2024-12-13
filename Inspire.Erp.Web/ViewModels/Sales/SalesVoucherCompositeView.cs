using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public class SalesVoucherCompositeView
    {
       public SalesVoucherViewModel SalesVoucher { get; set; }
        public IList<SalesVoucherDetailsViewModel> SalesVoucherDetails { get; set; }


        
    }
}
