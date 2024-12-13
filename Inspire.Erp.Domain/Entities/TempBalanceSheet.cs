using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempBalanceSheet
    {
        public string TempBalanceSheetMainHead { get; set; }
        public string TempBalanceSheetSubHead { get; set; }
        public string TempBalanceSheetRelativeNo { get; set; }
        public string TempBalanceSheetRelativeName { get; set; }
        public string TempBalanceSheetAccNo { get; set; }
        public string TempBalanceSheetAccName { get; set; }
        public double? TempBalanceSheetTotalDebit { get; set; }
        public double? TempBalanceSheetTotalCredit { get; set; }
        public double? TempBalanceSheetDebitBalance { get; set; }
        public double? TempBalanceSheetCreditBalance { get; set; }
        public bool? TempBalanceSheetDelStatus { get; set; }
    }
}
