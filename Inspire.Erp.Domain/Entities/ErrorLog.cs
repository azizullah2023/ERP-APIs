using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ErrorLog
    {
        public int ErrorLogId { get; set; }
        public string ErrorLogDetails { get; set; }
        public string ErrorLogFormName { get; set; }
        public string ErrorLogFuntionName { get; set; }
        public int? ErrorLogLineNo { get; set; }
        public DateTime? ErrorLogTime { get; set; }
        public DateTime? ErrorLogDate { get; set; }
        public string ErrorLogTraceDetails { get; set; }
        public bool? ErrorLogDelStatus { get; set; }
    }
}
