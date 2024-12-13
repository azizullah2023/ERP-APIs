using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ProjectDescriptionViewModel
    {
        public int? ProjectDescriptionMasterProjectDescriptionId { get; set; }
        public string ProjectDescriptionMasterProjectDescriptionName { get; set; }
        public int? ProjectDescriptionMasterProjectDescriptionSortOrder { get; set; }
        public bool? ProjectDescriptionMasterProjectDescriptionStatus { get; set; }
        public bool? ProjectDescriptionMasterProjectDescriptionDelStatus { get; set; }
    }
}