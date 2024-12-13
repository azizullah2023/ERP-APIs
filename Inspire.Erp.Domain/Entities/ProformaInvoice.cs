using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ProformaInvoice
    {
        public int? ProformaInvoiceId { get; set; }
        public string? ContectPerson { get; set; }
        public DateTime? ProformaInvoiceDate { get; set; }
        public int? ProformaInvoiceLocationId { get; set; }
        public int? ProformaInvoiceCustCode { get; set; }
        public string ProformaInvoiceCustName { get; set; }
        public int? ProformaInvoiceSalesManId { get; set; }
        public string ProformaInvoiceLpoNo { get; set; }
        public DateTime? ProformaInvoiceLpoDate { get; set; }
        public int? ProformaInvoiceCurrencyId { get; set; }
        public string ProformaInvoiceRemarks { get; set; }
        public double? ProformaInvoiceDiscountPercentage { get; set; }
        public double? ProformaInvoiceDiscountAmount { get; set; }
        public double? ProformaInvoiceTotal { get; set; }
        public double? ProformaInvoiceGrandTotal { get; set; }
        public int? ProformaInvoiceFsno { get; set; }
        public int? ProformaInvoiceUserId { get; set; }
        public string ProformaInvoiceSubject { get; set; }
        public string ProformaInvoiceNote { get; set; }
        public string ProformaInvoiceWarranty { get; set; }
        public string ProformaInvoiceTraining { get; set; }
        public string ProformaInvoiceTechnicalDetails { get; set; }
        public string ProformaInvoiceTerms { get; set; }
        public int? ProformaInvoiceCpoAboutIt { get; set; }
        public DateTime? ProformaInvoiceCpoDeliveryTime { get; set; }
        public string ProformaInvoicePacking { get; set; }
        public string ProformaInvoiceQuatationId { get; set; }
        public string ProformaInvoiceQuality { get; set; }
        public bool? ProformaInvoiceIsLocalPurchase { get; set; }
        public int? ProformaInvoiceJobId { get; set; }
        public bool? ProformaInvoiceDelStatus { get; set; }

        [NotMapped]
        public List<ProformaInvoiceDetails> ProformaInvoiceDetails { get; set; }
    }
}
