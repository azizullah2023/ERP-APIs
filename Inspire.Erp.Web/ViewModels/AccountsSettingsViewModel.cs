﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class AccountsSettingsViewModel
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
