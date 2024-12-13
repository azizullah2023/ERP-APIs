using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BusinessTypeMaster
    {
        public int? BusinessTypeMasterBusinessTypeId { get; set; }
        public string BusinessTypeMasterBusinessTypeName { get; set; }
        public bool? BusinessTypeMasterBusinessTypeStatus { get; set; }
        public bool? BusinessTypeMasterBusinessTypeDelStatus { get; set; }
    }
}
