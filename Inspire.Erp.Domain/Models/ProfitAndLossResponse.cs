using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class ProfitAndLossResponse
    {
        public string MA_MainHead { get; set; }
        public string MA_SubHead { get; set; }
        public string MA_AccNo { get; set; }
        public string MA_AccName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
