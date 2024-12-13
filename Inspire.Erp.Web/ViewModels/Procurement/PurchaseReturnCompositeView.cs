using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class PurchaseReturnCompositeView
    {
       public PurchaseReturnViewModel PurchaseReturn { get; set; }
        public IList<PurchaseReturnDetailsViewModel> PurchaseReturnDetails { get; set; }


        
    }
}
