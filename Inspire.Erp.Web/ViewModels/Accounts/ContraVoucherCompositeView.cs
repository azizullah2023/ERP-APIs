using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ContraVoucherCompositeView
    {
       public ContraVoucherViewModel ContraVoucher { get; set; }
        public IList<ContraVoucherDetailsViewModel> ContraVoucherDetails { get; set; }
    }
}
