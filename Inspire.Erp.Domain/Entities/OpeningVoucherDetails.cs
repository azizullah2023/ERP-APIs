using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OpeningVoucherDetails
    {
        public int OpeningVoucherDetailsId { get; set; }
        public string OpeningVoucherDetailsOpId { get; set; }
        public string OpeningVoucherDetailsVoucherNo { get; set; }
        public DateTime? OpeningVoucherDetailsVDate { get; set; }
        public double? OpeningVoucherDetailsDebit { get; set; }
        public double? OpeningVoucherDetailsCredit { get; set; }
        public string OpeningVoucherDetailsRefNo { get; set; }
        public string OpeningVoucherDetailsRemarks { get; set; }
        public int? OpeningVoucherDetailsFsno { get; set; }
        public int? OpeningVoucherDetailsUserId { get; set; }
        public DateTime? OpeningVoucherDetailsLastUpdateTime { get; set; }
        public string OpeningVoucherDetailsDrCr { get; set; }
        public bool? OpeningVoucherDetailsDelStatus { get; set; }

        public virtual VouchersNumbers OpeningVoucherDetailsOp { get; set; }
    }
}
