using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class PaymentVoucherCompositeView
    {
       public PaymentVoucherViewModel PaymentVoucher { get; set; }
        public IList<PaymentVoucherDetailsViewModel> PaymentVoucherDetails { get; set; }
    }
}
