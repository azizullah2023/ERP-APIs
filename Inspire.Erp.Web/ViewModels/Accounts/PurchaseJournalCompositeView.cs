using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class PurchaseJournalCompositeView
    {
       public PurchaseJournalViewModel PurchaseJournal { get; set; }
        public IList<PurchaseJournalDetailsViewModel> PurchaseJournalDetails { get; set; }
    }
}
