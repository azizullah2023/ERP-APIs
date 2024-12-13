////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;

////namespace Inspire.Erp.Web.ViewModels.Accounts
////{
////    public class OpeningBalanceVoucherCompositeView
////    {
////    }
////}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public class OpeningBalanceVoucherCompositeView
    {
        public OpeningBalanceVoucherViewModel OpeningBalanceVoucher { get; set; }
        public IList<OpeningBalanceVoucherDetailsViewModel> OpeningBalanceVoucherDetails { get; set; }
    }
}
