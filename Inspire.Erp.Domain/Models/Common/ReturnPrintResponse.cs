using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Common
{
   public class ReturnPrintResponse
    {
        public System.IO.MemoryStream stream { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
    }
}
