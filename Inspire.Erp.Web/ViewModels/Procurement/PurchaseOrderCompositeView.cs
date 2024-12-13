using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class PurchaseOrderCompositeView
    {
       public PurchaseOrderViewModel PurchaseOrder { get; set; }
        public IList<PurchaseOrderDetailsViewModel> PurchaseOrderDetails { get; set; }


        
    }
}
