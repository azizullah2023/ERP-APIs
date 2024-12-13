using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesVoucher
    {
        // public SalesVoucher()
        // {
        //     // SalesVoucherDetails = new HashSet<SalesVoucherDetails>();
        // }
        public decimal? SalesVoucherId { get; set; }
        public string? SalesVoucherNo { get; set; }
        public DateTime? SalesVoucherDate { get; set; }
        public string? SalesVoucherType { get; set; }
        public decimal? SalesVoucherPartyId { get; set; }
        public string? SalesVoucherPartyName { get; set; }
        public string? SalesVoucherPartyAddress { get; set; }
        public string? SalesVoucherPartyVatNo { get; set; }
        public string? SalesVoucherRefNo { get; set; }
        public string? SalesVoucherContPerson { get; set; }
        public string? SalesVoucherDlvNo { get; set; }
        public DateTime? SalesVoucherDlvDate { get; set; }
        public string? SalesVoucherSONo { get; set; }
        public DateTime? SalesVoucherSODate { get; set; }
        public string? SalesVoucherQtnNo { get; set; }
        public DateTime? SalesVoucherQtnDate { get; set; }
        public int? SalesVoucherSalesManID { get; set; }
        public int? SalesVoucherDptID { get; set; }
        public string? SalesVoucherEnqNo { get; set; }
        public DateTime? SalesVoucherEnqDate { get; set; }
        public string? SalesVoucherDescription { get; set; }
        public bool? SalesVoucherExcludeVAT { get; set; }
        public int? SalesVoucherLocationID { get; set; }
        public long? SalesVoucherUserID { get; set; }
        public int? SalesVoucherCurrencyId { get; set; }
        public int? SalesVoucherCompanyId { get; set; }
        public int? SalesVoucherJobId { get; set; }
        public decimal? SalesVoucherFSNO { get; set; }
        public decimal? SalesVoucherFc_Rate { get; set; }
        public string? SalesVoucherStatus { get; set; }
        public decimal? SalesVoucherTotalGrossAmount { get; set; }
        public decimal? SalesVoucherTotalItemDisAmount { get; set; }
        public decimal? SalesVoucherTotalActualAmount { get; set; }
        public decimal? SalesVoucherTotalDisPer { get; set; }
        public decimal? SalesVoucherTotalDisAmount { get; set; }
        public decimal? SalesVoucherVat_AMT { get; set; }
        public decimal? SalesVoucherVat_Per { get; set; }
        public string? SalesVoucherVat_RoundSign { get; set; }
        public decimal? SalesVoucherVat_RountAmt { get; set; }
        public decimal? SalesVoucherNetDisAmount { get; set; }
        public decimal? SalesVoucherNetAmount { get; set; }
        public string? SalesVoucherHeader { get; set; }
        public string? SalesVoucherDelivery { get; set; }
        public string? SalesVoucherNotes { get; set; }
        public string? SalesVoucherFooter { get; set; }
        public string? SalesVoucherPaymentTerms { get; set; }
        public string? SalesVoucherSubject { get; set; }
        public string? SalesVoucherContent { get; set; }
        public string? SalesVoucherRemarks1 { get; set; }
        public string? SalesVoucherRemarks2 { get; set; }
        public string? SalesVoucherTermsAndCondition { get; set; }
        public string? SalesVoucherShippinAddress { get; set; }
        public bool? SalesVoucherDelStatus { get; set; }
        public long? SalesVoucherShortNo { get; set; }
        [NotMapped]
        public string? CurrencyMasterCurrencyName { get; set; }
        [NotMapped]
        public List<SalesVoucherDetails> SalesVoucherDetails { get; set; }
    }
}
