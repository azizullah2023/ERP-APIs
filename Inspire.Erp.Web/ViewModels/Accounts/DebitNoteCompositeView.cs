using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class DebitNoteCompositeView
    {
       public DebitNoteViewModel DebitNote { get; set; }
        public IList<DebitNoteDetailsViewModel> DebitNoteDetails { get; set; }
    }
}
