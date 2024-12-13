using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UserWebAccSettings
    {
        public int? UserWebAccSettingsId { get; set; }
        public string UserWebAccSettingsUserAccount { get; set; }
        public string UserWebAccSettingsUapwd { get; set; }
        public string UserWebAccSettingsServerName { get; set; }
        public string UserWebAccSettingsSmtp { get; set; }
        public int? UserWebAccSettingsPort { get; set; }
        public bool? UserWebAccSettingsEnableSsi { get; set; }
        public bool? UserWebAccSettingsDefaultMail { get; set; }
        public int? UserWebAccSettingsCreateUser { get; set; }
        public DateTime? UserWebAccSettingsCreateDateTime { get; set; }
        public int? UserWebAccSettingsLastUpdateUser { get; set; }
        public DateTime? UserWebAccSettingsLastUpdateDateTime { get; set; }
        public bool? UserWebAccSettingsIsBodyHtml { get; set; }
        public bool? UserWebAccSettingsDelStatus { get; set; }
    }
}
