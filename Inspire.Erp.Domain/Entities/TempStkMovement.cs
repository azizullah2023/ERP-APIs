using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStkMovement
    {
        public string TempStkMovementVno { get; set; }
        public DateTime? TempStkMovementVdate { get; set; }
        public string TempStkMovementVtype { get; set; }
        public double? TempStkMovementQtyIn { get; set; }
        public double? TempStkMovementQtyOut { get; set; }
        public double? TempStkMovementAmount { get; set; }
        public double? TempStkMovementTotAmt { get; set; }
        public bool? TempStkMovementDelStatus { get; set; }
    }
}
