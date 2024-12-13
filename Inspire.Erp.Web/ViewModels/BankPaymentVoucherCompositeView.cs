////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;

////namespace Inspire.Erp.Web.ViewModels
////{
////    public class BankPaymentVoucherCompositeView
////    {
////       public BankPaymentVoucherViewModel BankPaymentVoucher { get; set; }
////        public IList<BankPaymentVoucherDetailsViewModel> BankPaymentVoucherDetails { get; set; }
////    }
////}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class BankPaymentVoucherCompositeView
    {
        public BankPaymentVoucherViewModel BankPaymentVoucher { get; set; }
        public IList<BankPaymentVoucherDetailsViewModel> BankPaymentVoucherDetails { get; set; }
    }
}
