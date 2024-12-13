using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AgeingTable
    {
        public string AgeingAccNo { get; set; }
        public string AgeingCsname { get; set; }
        public double? Ageing030 { get; set; }
        public double? Ageing3160 { get; set; }
        public double? Ageing6190 { get; set; }
        public double? Ageing91180 { get; set; }
        public double? Ageing181270 { get; set; }
        public double? Ageing271360 { get; set; }
        public double? AgeingOver360 { get; set; }
        public double? AgeingCreditLimit { get; set; }
        public double? AgeingCreditBalance { get; set; }
        public double? AgeingAllBalance { get; set; }
        public double? AgeingOutstandingBal { get; set; }
        public double? AgeingCreditDays { get; set; }
        public int? AgeingSalesMan { get; set; }
        public string AgeingSalesManName { get; set; }
        public bool? AgeingDelStatus { get; set; }
    }
}
