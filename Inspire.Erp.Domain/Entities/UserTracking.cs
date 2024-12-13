using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UserTracking
    {
        public int UserTrackingUserNo { get; set; }
        public string UserTrackingUserVpNo { get; set; }
        public string UserTrackingUserVpType { get; set; }
        public int? UserTrackingUserUserId { get; set; }
        public string UserTrackingUserVpAction { get; set; }
        public int? UserTrackingUserFsno { get; set; }
        public DateTime? UserTrackingUserChangeDt { get; set; }
        public DateTime? UserTrackingUserChangeTime { get; set; }
        public bool? UserTrackingUserDelStatus { get; set; }
        public string UserTrackingPage { get; set; }
        public string UserTrackingSection { get; set; }
        public string UserTrackingOldValue { get; set; }
        public string UserTrackingNewValue { get; set; }
        public string UserTrackingField { get; set; }
        public string UserTrackingEntity { get; set; }
    }

    public class UserTrackingDisplay
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string VType { get; set; }
        public string Date { get; set; }
        public string   Time { get; set;}
        public string VNo { get; set; }
    }
}
