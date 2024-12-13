using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ViewQuotation
    {
        public string Description { get; set; }
        public decimal? Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? GrossAmt { get; set; }
        public decimal? Discount { get; set; }
        public string UnitName { get; set; }
        public DateTime? QuotationDate { get; set; }
        public decimal? GrossTotal { get; set; }
        public decimal? NetTotal { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string CurrencyName { get; set; }
        public string Terms { get; set; }
        public string Subject { get; set; }
        public string QuotationId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string CashCustomer { get; set; }
        public string CustomerLocation { get; set; }
        public string Telephone { get; set; }
        public string Note { get; set; }
        public string Warranty { get; set; }
        public string SalesMan { get; set; }
        public string SalesManContactNo { get; set; }
        public string CustomerContactPerson { get; set; }
        public string QuotationContactPer { get; set; }
        public decimal? VatPercent { get; set; }
        public decimal? VatAmt { get; set; }
        public decimal? DiscAmt { get; set; }
        public string Quality { get; set; }
        public string ItemName { get; set; }
        public decimal? ItemVat { get; set; }
        public decimal? ItemNetAmt { get; set; }
    }
}
