using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class Qtemplates
    {
        public int QtemplatesId { get; set; }
        public string QtemplatesDescription { get; set; }
        public string QtemplatesType { get; set; }
        public bool? QtemplatesDelStatus { get; set; } = false;
    }
}
