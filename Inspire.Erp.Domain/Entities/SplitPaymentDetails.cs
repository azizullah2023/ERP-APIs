using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SplitPaymentDetails
    {
        public int SplitPaymentDetailsId { get; set; }
        public string SplitPaymentDetailsInvoiceNo { get; set; }
        public double? SplitPaymentDetailsCardAmount { get; set; }
        public double? SplitPaymentDetailsCashAmount { get; set; }
        public int? SplitPaymentDetailsWorkPeriodId { get; set; }
        public bool? SplitPaymentDetailsDelStatus { get; set; }
    }
}
