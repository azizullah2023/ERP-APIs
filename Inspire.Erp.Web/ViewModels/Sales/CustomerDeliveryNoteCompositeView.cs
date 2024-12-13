using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Sales
{
    public class CustomerDeliveryNoteCompositeView
    {
        public CustomerDeliveryNoteViewModel CustomerDeliveryNote { get; set; }
        public IList<CustomerDeliveryNoteDetailsViewModel> CustomerDeliveryNoteDetails { get; set; }

    }
}

