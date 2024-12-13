using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class BankReceiptVoucherCompositeView
    {
       public BankReceiptVoucherViewModel BankReceiptVoucher { get; set; }
        public IList<BankReceiptVoucherDetailsViewModel> BankReceiptVoucherDetails { get; set; }
    }
}
