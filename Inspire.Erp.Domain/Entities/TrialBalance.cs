using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TrialBalance
    {
        public int TrialBalanceID { get; set; }
        public string TrialBalanceMainHead { get; set; }
        public string TrialBalanceSubHead { get; set; }
        public string TrialBalanceRelativeNo { get; set; }
        public string TrialBalanceRelativeName { get; set; }
        public string TrialBalanceAccNo { get; set; }
        public string TrialBalanceAccName { get; set; }
        public double? TrialBalanceOpenBalance { get; set; }
        public double? TrialBalanceTotalDebit { get; set; }
        public double? TrialBalanceTotalCredit { get; set; }
        public double? TrialBalanceDebitBalance { get; set; }
        public double? TrialBalanceCreditBalance { get; set; }
        public int? TrialBalanceNumericAccNo { get; set; }
        public bool? TrialBalanceDelStatus { get; set; }
    }
}
