using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ModelsViewModel
    {
    public int? ModelMasterId { get; set; }
    public int? ModelMasterTypeId { get; set; }
    public string ModelMasterName { get; set; }
    public int? ModelMasterUserId { get; set; }
    public bool? ModelMasterDeleted { get; set; }
    public bool? ModelMasterStatus { get; set; }
    public bool? ModelMasterDelStatus { get; set; }
}
}