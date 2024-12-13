using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CreditNoteCompositeView
    {
       public CreditNoteViewModel CreditNote { get; set; }
        public IList<CreditNoteDetailsViewModel> CreditNoteDetails { get; set; }
    }
}
