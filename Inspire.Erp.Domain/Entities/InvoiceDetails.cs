using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceDetails
    {
        public int? InvoiceDetailsDetailsId { get; set; }
        public string InvoiceDetailsInvoiceNo { get; set; }
        public int? InvoiceDetailsSlno { get; set; }
        public int? InvoiceDetailsItemId { get; set; }
        public int? InvoiceDetailsUnitId { get; set; }
        public double? InvoiceDetailsQty { get; set; }
        public double? InvoiceDetailsUnitPrice { get; set; }
        public double? InvoiceDetailsAmount { get; set; }
        public string InvoiceDetailsDescription { get; set; }
        public int? InvoiceDetailsGddetailsId { get; set; }
        public bool? InvoiceDetailsIsEdited { get; set; }
        public bool? InvoiceDetailsDelStatus { get; set; }
    }
}
