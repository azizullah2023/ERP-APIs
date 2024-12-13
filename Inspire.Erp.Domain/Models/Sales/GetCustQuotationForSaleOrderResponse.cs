using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
    public class GetCustQuotationForSaleOrderResponse
    {
        public int ItemId { get; set; }
        public long QuotationId { get; set; }
        public int? QuotationSerial { get; set; }
        public long QuotationDetailsId { get; set; }
        public string VoucherNo { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string Description { get; set; }
        public decimal BalanceQuantity { get; set; }
    }

    public class GetCustomerQuotationDetail
    {
        public long QuotationId { get; set; }
        public string VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public int? VoucherType { get; set; }
        public decimal? NetAmount { get; set; }
        public long? CustomerId { get; set; }
        public bool? Status { get; set; }
        public DateTime? QuotationDate { get; set; }

    }

}
