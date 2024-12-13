using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ProformaInvoiceDetails
    {
        public int ProformaInvoiceDetailsId { get; set; }
        public int? ProformaInvoiceDetailsSlNo { get; set; }
        public int? ProformaInvoiceDetailsItemId { get; set; }
        public string ProformaInvoiceDetailsDescription { get; set; }
        public int? ProformaInvoiceDetailsUnitId { get; set; }
        public double? ProformaInvoiceDetailsQty { get; set; }
        public double? ProformaInvoiceDetailsUnitPrice { get; set; }
        public double? ProformaInvoiceDetailsAmount { get; set; }
        public double? ProformaInvoiceDetailsFcAmount { get; set; }
        public bool? ProformaInvoiceDetailsIsEdited { get; set; }
        public int? ProformaInvoiceDetailsFsno { get; set; }
        public double? ProformaInvoiceDetailsDeliveryQty { get; set; }
        public int? ProformaInvoiceDetailsPodId { get; set; }
        public int? ProformaInvoiceDetailsQuotationDetId { get; set; }
        public string ProformaInvoiceDetailsRemarks { get; set; }
        public bool? ProformaInvoiceDetailsDelStatus { get; set; }
    }
}
