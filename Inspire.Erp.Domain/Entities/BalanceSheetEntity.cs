using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class BalanceSheetEntity
    {

        public string MA_RelativeNo { get; set; }
        public string MA_MainHead { get; set; }
        public string MA_SubHead { get; set; }
        public string MA_AccNo { get; set; }
        public string MA_AccName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
