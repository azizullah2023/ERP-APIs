using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SiteMaster
    {
        public int? SiteMasterSiteId { get; set; }
        public string SiteMasterSiteName { get; set; }
        public string SiteMasterShortName { get; set; }
        public bool? SiteMasterDelStatus { get; set; }
    }
}
