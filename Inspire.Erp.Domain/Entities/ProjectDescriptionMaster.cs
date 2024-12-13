using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ProjectDescriptionMaster
    {
        public int ProjectDescriptionMasterProjectDescriptionId { get; set; }
        public string ProjectDescriptionMasterProjectDescriptionName { get; set; }
        public int? ProjectDescriptionMasterProjectDescriptionSortOrder { get; set; }
        public bool? ProjectDescriptionMasterProjectDescriptionStatus { get; set; }
        public bool? ProjectDescriptionMasterProjectDescriptionDelStatus { get; set; }
    }
}
