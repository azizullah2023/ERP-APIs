using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Printer
{
    public partial class PrinterConnectionViewModel
    {
        public int? Id { get; set; }
        public string SystemUser { get; set; }
        public string SytemConnectionId { get; set; }
        public string AppUserId { get; set; }
        public string AppConnectionId { get; set; }
        public string PrinterName { get; set; }
        public bool? IsActive { get; set; }
        public bool? Status { get; set; }
    }
}
