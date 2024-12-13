using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CodeMaster
    {
        public int? CodeMasterCode { get; set; }
        public string CodeMasterDescription { get; set; }
        public string CodeMasterType { get; set; }
        public string CodeMasterTypeDescription { get; set; }
        public bool? CodeMasterDelStatus { get; set; }
    }
}
