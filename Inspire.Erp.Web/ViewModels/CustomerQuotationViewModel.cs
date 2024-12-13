using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CustomerQuotationViewModel
    {
        public long CustomerQuotationQid { get; set; }
        public string CustomerQuotationVoucherNo { get; set; }
        public long? CustomerQuotationQuotationAddition { get; set; }
        public long? CustomerQuotationQuotationRoot { get; set; }
        public DateTime? CustomerQuotationQuotationDate { get; set; }
        public DateTime? CustomerQuotationQuotationValidDate { get; set; }
        public long? CustomerQuotationCustomerId { get; set; }
        public long? CustomerQuotationEnquiryId { get; set; }
        public string CustomerQuotationRemarks { get; set; }
        public long? CustomerQuotationLocationId { get; set; }
        public decimal? CustomerQuotationDiscountPercentage { get; set; }
        public decimal? CustomerQuotationDiscountAmount { get; set; }
        public decimal? CustomerQuotationGrossTotal { get; set; }
        public decimal? CustomerQuotationNetTotal { get; set; }
        public long? CustomerQuotationSalesManId { get; set; }
        public long? CustomerQuotationCurrencyId { get; set; }
        public long? CustomerQuotationJobId { get; set; }
        public string CustomerQuotationSubject { get; set; }
        public string CustomerQuotationNote { get; set; }
        public string CustomerQuotationWarranty { get; set; }
        public string CustomerQuotationTraining { get; set; }
        public string CustomerQuotationTechnicalDetails { get; set; }
        public string CustomerQuotationTerms { get; set; }
        public DateTime? CustomerQuotationDeliveryTimeDate { get; set; }
        public string CustomerQuotationPacking { get; set; }
        public string CustomerQuotationQuality { get; set; }
        public string CustomerQuotationHeader { get; set; }
        public string CustomerQuotationFooter { get; set; }
        public long? CustomerQuotationQuotationStatusId { get; set; }
        public long? CustomerQuotationFsno { get; set; }
        public bool? CustomerQuotationIsClose { get; set; }
        public string CustomerQuotationEnquiryNo { get; set; }
        public int? CustomerQuotationVoucherType { get; set; }
        public string CustomerQuotationCashCustomerName { get; set; }
        public string CustomerQuotationContactPersonV { get; set; }
        public decimal? CustomerQuotationDiscountAmountTotal { get; set; }
        public long? CustomerQuotationVendorId { get; set; }
        public long? CustomerQuotationTypeId { get; set; }
        public long? CustomerQuotationModelId { get; set; }
        public decimal? CustomerQuotationVatPercentage { get; set; }
        public decimal? CustomerQuotationVatAmount { get; set; }
        public int? CustomerQuotationQuotationEnquiryN { get; set; }
        public bool? CustomerQuotationDelStatus { get; set; }
        public string CustomerQuotationProjectName { get; set; }
        public string CustomerQuotationProjectRef { get; set; }
        public bool? CustomerQuotationReviseQuotation { get; set; }
        public decimal? CustomerQuotationGrossProfitTotal { get; set; }
        public List<CustomerQuotationDetailsViewModel> CustomerQuotationDetails { get; set; }

    }
}
