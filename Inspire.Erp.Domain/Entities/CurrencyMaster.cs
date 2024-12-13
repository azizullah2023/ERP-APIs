using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CurrencyMaster
    {
        public int CurrencyMasterCurrencyId { get; set; }
        public string CurrencyMasterCurrencyName { get; set; }
        public string CurrencyMasterCurrencySymbol { get; set; }
        public double? CurrencyMasterCurrencyRate { get; set; }
        public string CurrencyMasterCurrencyRemarks { get; set; }
        public string CurrencyMasterCurrencyType { get; set; }
        public int? CurrencyMasterCurrencyUserId { get; set; }
        public bool? CurrencyMasterCurrencyStatus { get; set; }
        public string CurrencyMasterCurrencyShortName { get; set; }
        public string CurrencyMasterCurrencyDenomination { get; set; }
        public bool? CurrencyMasterCurrencyDelStatus { get; set; }
    }
}
