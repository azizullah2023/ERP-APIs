using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class PurchaseRequisitionCompositeView
    {
        public PurchaseRequisitionViewModel PurchaseRequisition { get; set; }
        public IList<PurchaseRequisitionDetailsViewModel> PurchaseRequisitionDetails { get; set; }



    }
}
