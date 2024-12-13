using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AccountSettings
    {
        public int? AccountSettingsAccountId { get; set; }
        public string AccountSettingsAccountKeyValue { get; set; }
        public string AccountSettingsAccountCategory { get; set; }
        public string AccountSettingsAccountDescription { get; set; }
        public string AccountSettingsAccountValueType { get; set; }
        public string AccountSettingsAccountType { get; set; }
        public double? AccountSettingsAccountNumberValue { get; set; }
        public string AccountSettingsAccountTextValue { get; set; }
        public string AccountSettingsAccountTableName { get; set; }
        public int? AccountSettingsAccountFormId { get; set; }
        public bool? AccountSettingsAccountDelStatus { get; set; }
    }
}
