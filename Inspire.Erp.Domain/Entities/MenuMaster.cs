using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class MenuMaster
    {
        public int? MenuMasterId { get; set; }
        public string MenuMasterLatin { get; set; }
        public string MenuMasterArabic { get; set; }
        public string MenuMasterHindi { get; set; }
        public bool? MenuMasterDelStatus { get; set; }
    }
}
