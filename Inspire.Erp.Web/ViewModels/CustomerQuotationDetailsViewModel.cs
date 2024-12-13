using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CustomerQuotationDetailsViewModel
    {
        public long CustomerQuotationDetailsQuotationId { get; set; }
        public long CustomerQuotationDetailsId { get; set; }
        public string CustomerQuotationDetailsVoucherNo { get; set; }
        public int? CustomerQuotationDetailsSlno { get; set; }
        public long? CustomerQuotationDetailsItemId { get; set; }
        public string CustomerQuotationDetailsDescription { get; set; }
        public long? CustomerQuotationDetailsUnitId { get; set; }
        public decimal? CustomerQuotationDetailsQty { get; set; }
        public decimal? CustomerQuotationDetailsUnitPrice { get; set; }
        public decimal? CustomerQuotationDetailsGrossAmount { get; set; }
        public decimal? CustomerQuotationDetailsDiscount { get; set; }
        public decimal? CustomerQuotationDetailsVat { get; set; }
        public decimal? CustomerQuotationDetailsNetAmount { get; set; }
        public int? CustomerQuotationDetailsFsno { get; set; }
        public bool? CustomerQuotationDetailsIsEdited { get; set; }
        public double? CustomerQuotationDetailsPurchasePrice { get; set; }
        public string CustomerQuotationDetailsPriceType { get; set; }
        public int? CustomerQuotationDetailsVoucherType { get; set; }
        public string CustomerQuotationDetailsCashCustomerName { get; set; }
        public long? CustomerQuotationDetailsEnquiryDetailsId { get; set; }
        public bool? CustomerQuotationDetailsDelStatus { get; set; }
        public decimal? CustomerQuotationDetailsGrossProfit { get; set; }
    }
}
