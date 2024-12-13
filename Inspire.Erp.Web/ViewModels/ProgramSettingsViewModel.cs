using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ProgramSettingsViewModel
    {
        public int? ProgramSettingsGenId { get; set; }
        public string? ProgramSettingsKeyValue { get; set; }
        public string? ProgramSettingsCategory { get; set; }
        public string? ProgramSettingsDescription { get; set; }
        public string? ProgramSettingsValueType { get; set; }
        public bool? ProgramSettingsBoolValue { get; set; }
        public double? ProgramSettingsNumValue { get; set; }
        public string ProgramSettingsTextValue { get; set; }
        public string ProgramSettingsTableName { get; set; }
        public string ProgramSettingsFormId { get; set; }
    }
}
