using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.MIS
{
   public class GetBalanceSheetDetailResponse
    {
        public string mainHead { get; set; }
        public string head { get; set; }
        public string accName { get; set; }
        public string subHead { get; set; }
        public string amount { get; set; }
        public List<HeadBalanceSheetDetail> __children { get; set; }
    }
    public class HeadBalanceSheetDetail
    {
        public string mainHead { get; set; }
        public string head { get; set; }
        public string accName { get; set; }
        public string subHead { get; set; }
        public string amount { get; set; }
        public List<SubHeadBalanceSheetDetail> __children { get; set; }
    }
    public class SubHeadBalanceSheetDetail
    {
        public string mainHead { get; set; }
        public string head { get; set; }
        public string accName { get; set; }
        public string subHead { get; set; }
        public string amount { get; set; }
        public List<AccNameBalanceSheetDetail> __children { get; set; }
    }
    public class AccNameBalanceSheetDetail
    {
        public string mainHead { get; set; }
        public string head { get; set; }
        public string accName { get; set; }
        public string subHead { get; set; }
        public string amount { get; set; }
    }

    public class DemoBalcanSheet
    {
        public string mainHead { get; set; }
        public string head { get; set; }
        public string accName { get; set; }
        public string subHead { get; set; }
        public string amount { get; set; }
        public List<DemoBalcanSheet> __children { get; set; }
    }
}
