using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.MIS
{
  public  class GetFinancialYearResponse
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
