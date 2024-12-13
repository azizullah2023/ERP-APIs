using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public  class FinancialPeriodsViewModel
    {
        public int? FinancialPeriodsFsno { get; set; }
        public DateTime? FinancialPeriodsStartDate { get; set; }
        public DateTime? FinancialPeriodsEndDate { get; set; }
        public string FinancialPeriodsStatus { get; set; }
        public string FinancialPeriodsYearEndFile { get; set; }
        public string FinancialPeriodsYearEndJv { get; set; }
        public bool? FinancialPeriodsDelStatus { get; set; }
    }
}
