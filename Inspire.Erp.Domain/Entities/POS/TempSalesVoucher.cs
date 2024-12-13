using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class POS_Temp_SalesVoucher
    {
        public POS_Temp_SalesVoucher()
        {
            SalesVoucherDetails = new HashSet<POS_Temp_SalesVoucherDetails>();
        }
        public long V_ID { get; set; }
        public string VoucherNo { get; set; }
        public long ShortNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string? Voucher_Type { get; set; }
        public string? CustomerName { get; set; }
        public int? Customer_ID { get; set; }
        public long? Location { get; set; }
        public long? Salesman { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remarks { get; set; }
        public long? UserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? FSNO { get; set; }
        public string? Description { get; set; }
        public string? CONO { get; set; }
        public string? DeliveryNote { get; set; }
        public string? Cust_PONo { get; set; }
        public decimal? Fc_Rate { get; set; }
        public int? CompanyId { get; set; }
        public string? Refrence { get; set; }
        public long? JobId { get; set; }
        public long? Currency_ID { get; set; }
        public DateTime? Cust_PODate { get; set; }
        public decimal? NetDiscount { get; set; }
        public string? CashCustName { get; set; }
        public decimal? DiscountPer { get; set; }
        public string? Shipping_Address { get; set; }
        public string? PaymentTerms_V { get; set; }
        public string? TermsAndConditions_V { get; set; }
        public string? InvoiceType { get; set; }
        public long? ContractId { get; set; }
        public string? InvoiceStatus { get; set; }
        public int? SV_QtnID_N { get; set; }
        public int? SV_EqpmentID_N { get; set; }
        public decimal? VatAmount { get; set; }
        public long? StationID { get; set; }
        public long? WorkPeriodID { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? Vat_AMT { get; set; }
        public decimal? Vat_Per { get; set; }
        public string? Vat_RoundSign { get; set; }
        public decimal? Vat_RountAmt { get; set; }
        public long? DeptID { get; set; }
        public double? Vatable_TotAmt { get; set; }
        public string? refno { get; set; }
        public decimal? Currency_Rate { get; set; }
        public int? SV_VesselId { get; set; }
        public bool? HOLD_B { get; set; }
        public int? Counter_ID_N { get; set; }
        public int? Shift_ID_N { get; set; }
        public bool? Retail_B { get; set; }
        public int? SV_DNID_N { get; set; }
        public int? Course_ID { get; set; }
        public string? SV_SM_VoucherNo { get; set; }
        public decimal? SV_SM_Bal { get; set; }
        public bool? SV_NoVat_B { get; set; }
        public decimal? Vat_TaxableAmt { get; set; }
        public bool? LOOP_UPDATE { get; set; }
        public int? SV_Job_ID { get; set; }
        public string? SV_Job_No { get; set; }
        public DateTime? SV_Job_Date { get; set; }
        public decimal? SV_Job_Adv_Amt { get; set; }
        public decimal? SV_Job_Balnce_Amt { get; set; }

        [NotMapped]
        public virtual ICollection<POS_Temp_SalesVoucherDetails> SalesVoucherDetails { get; set; }
    }
}
