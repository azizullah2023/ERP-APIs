using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AutoCodes
    {
        public int? AutoCodeId { get; set; }
        public string AutoCodeColDescription { get; set; }
        public int? AutoCodeColValue { get; set; }
        public string AutoCodeColValueV { get; set; }
        public bool? AutoCodeDelStatus { get; set; }
    }
}
