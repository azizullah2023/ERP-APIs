using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class GeneralSettingsViewModel
    {
        public int GeneralSettingsId { get; set; }
        public string GeneralSettingsKeyValue { get; set; }
        public string GeneralSettingsCategory { get; set; }
        public string GeneralSettingsDescription { get; set; }
        public string GeneralSettingsValueType { get; set; }
        public bool? GeneralSettingsBoolValue { get; set; }
        public double? GeneralSettingsNumValue { get; set; }
        public string GeneralSettingsTextValue { get; set; }
        public string GeneralSettingsTableName { get; set; }
        public bool? GeneralSettingsDelStatus { get; set; }
    }
}
