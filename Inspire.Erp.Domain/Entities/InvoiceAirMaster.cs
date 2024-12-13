using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceAirMaster
    {
        public int? InvoiceAirMasterInvoiceNo { get; set; }
        public DateTime? InvoiceAirMasterInvoiceDate { get; set; }
        public int? InvoiceAirMasterCustomerId { get; set; }
        public int? InvoiceAirMasterJobId { get; set; }
        public int? InvoiceAirMasterDebitId { get; set; }
        public string InvoiceAirMasterRemarks { get; set; }
        public bool? InvoiceAirMasterDelStatus { get; set; }
    }
}
