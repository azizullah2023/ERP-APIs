using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public class IssueVoucherCompositeView
    {
       public IssueVoucherViewModel IssueVoucher { get; set; }
        public IList<IssueVoucherDetailsViewModel> IssueVoucherDetails { get; set; }


        
    }
}
