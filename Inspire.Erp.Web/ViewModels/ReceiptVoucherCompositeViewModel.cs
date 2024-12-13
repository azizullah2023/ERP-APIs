using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ReceiptVoucherCompositeViewModel
    {
        public ReceiptVoucherMasterViewModel ReceiptVoucherMasterViewModel { get; set; }
        public IList<ReceiptVoucherDetailsViewModel> ReceiptVoucherDetails { get; set; }
    }
}
