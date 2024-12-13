using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.File
{
   public class GetFileFromExtensionResponse
    {
        public string Path { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
    }
}
