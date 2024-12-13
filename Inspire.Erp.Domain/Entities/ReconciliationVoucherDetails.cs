using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReconciliationVoucherDetails
    {
        public string ReconciliationVoucherDetailsId { get; set; }
        public int ReconciliationVoucherDetailsSno { get; set; }
        public string ReconciliationVoucherDetailsVno { get; set; }
        public int? ReconciliationVoucherDetailsTransSno { get; set; }
        public double? ReconciliationVoucherDetailsDebit { get; set; }
        public double? ReconciliationVoucherDetailsCredit { get; set; }
        public string ReconciliationVoucherDetailsMatched { get; set; }
        public string ReconciliationVoucherDetailsChequeNo { get; set; }
        public string ReconciliationVoucherDetailsDescription { get; set; }
        public int? ReconciliationVoucherDetailsLocationId { get; set; }
        public DateTime? ReconciliationVoucherDetailsTransDate { get; set; }
        public bool? ReconciliationVoucherDetailsDelStatus { get; set; }
    }
}
