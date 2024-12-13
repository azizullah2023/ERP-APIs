using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class TypeMasterViewModel
    {
        public int? TypeMasterTypeId { get; set; }
        public int? TypeMasterVendorId { get; set; }
        public string TypeMasterTypeName { get; set; }
        public int? TypeMasterUserId { get; set; }
        public int? TypeMasterDeleted { get; set; }
        public bool? TypeMasterStatus { get; set; }
        public bool? TypeMasterDelStatus { get; set; }
    }
}
