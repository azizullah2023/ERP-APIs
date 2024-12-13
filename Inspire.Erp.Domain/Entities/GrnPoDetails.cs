using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GrnPoDetails
    {
        public int? GrnPoDetailsSno { get; set; }
        public int? GrnPoDetailsPoId { get; set; }
        public DateTime? GrnPoDetailsPoDate { get; set; }
        public string GrnPoDetailsGrnNo { get; set; }
        public bool? GrnPoDetailsDelStatus { get; set; }
    }
}
