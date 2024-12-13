using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempMisStatement
    {
        public int? TempMisStatementSlNo { get; set; }
        public string TempMisStatementVno { get; set; }
        public DateTime? TempMisStatementVdate { get; set; }
        public string TempMisStatementVtype { get; set; }
        public string TempMisStatementDesc { get; set; }
        public double? TempMisStatementAmount { get; set; }
        public bool? TempMisStatementDelStatus { get; set; }
    }
}
