using Inspire.Erp.Domain.Entities.POS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public partial class SalesHoldViewModel
    {
        public decimal V_ID { get; set; }
        public string VoucherNo { get; set; }
        public long ShortNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string Voucher_Type { get; set; }
        public string CustomerName { get; set; }
        public decimal? Customer_ID { get; set; }
        public long? Location { get; set; }
        public long? Salesman { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        //public string Remarks { get; set; }
        public long? UserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? FSNO { get; set; }
        public string Description { get; set; }
        public string CONO { get; set; }
        public string DeliveryNote { get; set; }
        public string Cust_PONo { get; set; }
        //public decimal? Fc_Rate { get; set; }
        public int? CompanyId { get; set; }
        public string Refrence { get; set; }
        public long? JobId { get; set; }
        public long? Currency_ID { get; set; }
        public DateTime? Cust_PODate { get; set; }
        public decimal? NetDiscount { get; set; }
        public string CashCustName { get; set; }
        public decimal? DiscountPer { get; set; }
        //public string Shipping_Address { get; set; }
        //public string PaymentTerms_V { get; set; }
        //public string TermsAndConditions_V { get; set; }
        public string InvoiceType { get; set; }
        public long? ContractId { get; set; }
        public string InvoiceStatus { get; set; }
        //public int? SV_QtnID_N { get; set; }
        //public int? SV_EqpmentID_N { get; set; }
        public decimal? VatAmount { get; set; }
        public long? StationID { get; set; }
        public long? WorkPeriodID { get; set; }
        public string PaymentMode { get; set; }
        //public decimal? Vat_AMT { get; set; }
        //public decimal? Vat_Per { get; set; }
        //public string Vat_RoundSign { get; set; }
        //public decimal? Vat_RountAmt { get; set; }
        public long? DeptID { get; set; }
        public float? Vatable_TotAmt { get; set; }
        //public string refno { get; set; }
        //public decimal? Currency_Rate { get; set; }
        public List<SalesHoldDetailsViewModel> SalesHoldDetailsViewModel { get; set; }
    }


}
