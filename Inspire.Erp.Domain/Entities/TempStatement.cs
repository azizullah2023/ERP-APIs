using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStatement
    {
        public int? TempStatementSno { get; set; }
        public string TempStatementVno { get; set; }
        public DateTime? TempStatementVdate { get; set; }
        public string TempStatementVtype { get; set; }
        public string TempStatementDesc { get; set; }
        public double? TempStatementDebit { get; set; }
        public double? TempStatementCredit { get; set; }
        public double? TempStatementBalance { get; set; }
        public string TempStatementCostCenter { get; set; }
        public string TempStatementAccNo { get; set; }
        public string TempStatementRemarks { get; set; }
        public bool? TempStatementDelStatus { get; set; }
    }
}
