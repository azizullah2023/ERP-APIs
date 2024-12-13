using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public class IssueReturnCompositeView
    {
       public IssueReturnViewModel IssueReturn { get; set; }
        public IList<IssueReturnDetailsViewModel> IssueReturnDetails { get; set; }


        
    }
}
