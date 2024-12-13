using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempCashBook
    {
        public int? TempCashBookSlNo { get; set; }
        public string TempCashBookVno { get; set; }
        public DateTime? TempCashBookVdate { get; set; }
        public string TempCashBookInvType { get; set; }
        public string TempCashBookDescription { get; set; }
        public string TempCashBookAccNo { get; set; }
        public double? TempCashBookDebit { get; set; }
        public double? TempCashBookCredit { get; set; }
        public bool? TempCashBookDelStatus { get; set; }
    }
}
