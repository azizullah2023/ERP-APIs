using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class PurchaseVoucherCompositeView
    {
       public PurchaseVoucherViewModel PurchaseVoucher { get; set; }
        public IList<PurchaseVoucherDetailsViewModel> PurchaseVoucherDetails { get; set; }


        
    }
}
