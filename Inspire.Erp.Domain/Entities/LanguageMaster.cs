using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class LanguageMaster
    {
        public int? LanguageMasterId { get; set; }
        public string LanguageMasterDescription { get; set; }
        public bool? LanguageMasterDelStatus { get; set; }
    }
}
