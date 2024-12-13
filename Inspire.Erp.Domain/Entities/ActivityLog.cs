using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public int? ActivityLogTrackingId { get; set; }
        public int? ActivityLogPageId { get; set; }
        public string ActivityLogTableName { get; set; }
        public string ActivityLogColumnName { get; set; }
        public string ActivityLogOldValue { get; set; }
        public string ActivityLogNewValue { get; set; }
        public string ActivityLogUserIp { get; set; }
        public string ActivityLogBrowserInfo { get; set; }
        public int? ActivityLogModifiedUserId { get; set; }
        public DateTime? ActivityLogModifiedDate { get; set; }
        public bool? ActivityLogStatus { get; set; }
        public bool? ActivityLogDelStatus { get; set; }
    }
}
