using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GeneralSettings1
    {
        public string GeneralSettingsAppName { get; set; }
        public string GeneralSettingsSettingName { get; set; }
        public string GeneralSettingsKeyName { get; set; }
        public string GeneralSettingsSettingValue { get; set; }
        public bool? GeneralSettingsDelStatus { get; set; }
    }
}
