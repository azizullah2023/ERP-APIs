using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReportType
    {
        public int ReportTypeId { get; set; }
        public string ReportTypeVoucherName { get; set; }
        public string ReportTypeFileName { get; set; }
        public bool? ReportTypeIsDefault { get; set; }
        public string ReportTypeDescription { get; set; }
        public bool? ReportTypeIsImage { get; set; }
        public bool? ReportTypeIsVisible { get; set; }
        public bool? ReportTypeDelStatus { get; set; }
    }
}
