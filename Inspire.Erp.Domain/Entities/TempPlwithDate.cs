using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempPlwithDate
    {
        public string TempPlwithDateMainHead { get; set; }
        public string TempPlwithDateSubHead { get; set; }
        public string TempPlwithDateAccNo { get; set; }
        public string TempPlwithDateAccName { get; set; }
        public double? TempPlwithDateTotDebit { get; set; }
        public double? TempPlwithDateTotCredit { get; set; }
        public DateTime? TempPlwithDateTransDate { get; set; }
        public bool? TempPlwithDateDelStatus { get; set; }
    }
}
