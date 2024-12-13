using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PdcVoucherDetails
    {
        public int PdcVoucherDetailsDetId { get; set; }
        public int? PdcVoucherDetailsAccSno { get; set; }
        public string PdcVoucherDetailsAccno { get; set; }
        public string PdcVoucherDetailsDrCr { get; set; }
        public double? PdcVoucherDetailsAmount { get; set; }
        public double? PdcVoucherDetailsFcAmount { get; set; }
        public string PdcVoucherDetailsNarration { get; set; }
        public bool? PdcVoucherDetailsDelStatus { get; set; }
    }
}
