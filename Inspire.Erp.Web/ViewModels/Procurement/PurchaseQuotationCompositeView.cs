using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class PurchaseQuotationCompositeView
    {
       public PurchaseQuotationViewModel PurchaseQuotation { get; set; }
        public IList<PurchaseQuotationDetailsViewModel> PurchaseQuotationDetails { get; set; }


        
    }
}
