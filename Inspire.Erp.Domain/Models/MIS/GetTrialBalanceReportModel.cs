using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.MIS.TrialBalance
{
    public class TrialBalanceGroupModel
    {
        public string MainHead { get; set; }
        public string SubHead { get; set; }
        public string RelativeName { get; set; }
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public double? OpenBalance { get; set; }
        public double? TotalCredit { get; set; }
        public double? TotalDebit { get; set; }
    }


    public class GetTrialBalanceReportModel
    {
        // public string mainHead { get; set; }
        public string head { get; set; }
        public string subHead { get; set; }
        public string accName { get; set; }
        public string totalDebit { get; set; }
        public string totalCredit { get; set; }
        public string openBalance { get; set; }
        //public string closingBalance { get; set; }
        public List<SubHeadTrialBalance> __children { get; set; }
    }
    public class SubHeadTrialBalance
    {
        public string subHead { get; set; }
        public string accName { get; set; }
        public string totalDebit { get; set; }
        public string totalCredit { get; set; }
        public string openBalance { get; set; }
        public string closingBalance { get; set; }
        public List<AccNameTrialBalance> __children { get; set; }
    }
    public class AccNameTrialBalance
    {
        public string subHead { get; set; }
        public string accName { get; set; }
        public string totalDebit { get; set; }
        public string totalCredit { get; set; }
        public string openBalance { get; set; }
        public string closingBalance { get; set; }
    }



}
