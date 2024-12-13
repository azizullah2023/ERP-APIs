using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class SalesJournalCompositeView
    {
       public SalesJournalViewModel SalesJournal { get; set; }
        public IList<SalesJournalDetailsViewModel> SalesJournalDetails { get; set; }
    }
}
