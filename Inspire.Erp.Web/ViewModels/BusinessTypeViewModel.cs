using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class BusinessMasterViewModel

    {
        public int? BusinessTypeMasterBusinessTypeId { get; set; }
        public string BusinessTypeMasterBusinessTypeName { get; set; }
        public bool? BusinessTypeMasterBusinessTypeStatus { get; set; }
        public bool? BusinessTypeMasterBusinessTypeDelStatus { get; set; }

    }
}
