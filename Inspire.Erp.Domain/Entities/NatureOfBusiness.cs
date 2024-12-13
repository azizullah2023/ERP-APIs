using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class NatureOfBusiness
    {
        public int? NatureOfBusinessId { get; set; }
        public string NatureOfBusinessNature { get; set; }
        public bool? NatureOfBusinessDelStatus { get; set; }
    }
}
