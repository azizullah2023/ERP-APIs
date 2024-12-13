using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesReturn
    {
        public SalesReturn()
        {
            // SalesReturnDetails = new HashSet<SalesReturnDetails>();
        }
        public long SalesReturnId { get; set; }
        public string? SalesReturnNo { get; set; }
        public DateTime? SalesReturnDate { get; set; }
        public string? SalesReturnType { get; set; }
        public decimal? SalesReturnPartyId { get; set; }
        public string? SalesReturnPartyName { get; set; }
        public string? SalesReturnPartyAddress { get; set; }
        public string? SalesReturnPartyVatNo { get; set; }
        public string? SalesReturnRefNo { get; set; }
        public string? SalesReturnContPerson { get; set; }
        public string? SalesReturnDlvNo { get; set; }
        public DateTime? SalesReturnDlvDate { get; set; }
        public string? SalesReturnSONo { get; set; }
        public DateTime? SalesReturnSODate { get; set; }
        public string? SalesReturnQtnNo { get; set; }
        public DateTime? SalesReturnQtnDate { get; set; }
        public int? SalesReturnSalesManID { get; set; }
        public int? SalesReturnDptID { get; set; }
        public string? SalesReturnEnqNo { get; set; }
        public DateTime? SalesReturnEnqDate { get; set; }
        public string? SalesReturnDescription { get; set; }
        public bool? SalesReturnExcludeVAT { get; set; }
        public int? SalesReturnLocationID { get; set; }
        public long? SalesReturnUserID { get; set; }
        public int? SalesReturnCurrencyId { get; set; }
        public int? SalesReturnCompanyId { get; set; }
        public int? SalesReturnJobId { get; set; }
        public decimal? SalesReturnFSNO { get; set; }
        public decimal? SalesReturnFc_Rate { get; set; }
        public string? SalesReturnStatus { get; set; }
        public decimal? SalesReturnTotalGrossAmount { get; set; }
        public decimal? SalesReturnTotalItemDisAmount { get; set; }
        public decimal? SalesReturnTotalActualAmount { get; set; }
        public decimal? SalesReturnTotalDisPer { get; set; }
        public decimal? SalesReturnTotalDisAmount { get; set; }
        public decimal? SalesReturnVat_AMT { get; set; }
        public decimal? SalesReturnVat_Per { get; set; }
        public string? SalesReturnVat_RoundSign { get; set; }
        public decimal? SalesReturnVat_RountAmt { get; set; }
        public decimal? SalesReturnNetDisAmount { get; set; }
        public decimal? SalesReturnNetAmount { get; set; }
        public string? SalesReturnHeader { get; set; }
        public string? SalesReturnDelivery { get; set; }
        public string? SalesReturnNotes { get; set; }
        public string? SalesReturnFooter { get; set; }
        public string? SalesReturnPaymentTerms { get; set; }
        public string? SalesReturnSubject { get; set; }
        public string? SalesReturnContent { get; set; }
        public string? SalesReturnRemarks1 { get; set; }
        public string? SalesReturnRemarks2 { get; set; }
        public string? SalesReturnTermsAndCondition { get; set; }
        public string? SalesReturnShippinAddress { get; set; }
        public string? SalesReturnSVNo { get; set; }
        public DateTime? SalesReturnSVDate { get; set; }
        public bool? SalesReturnDelStatus { get; set; }

        // [NotMapped]
        // public List<SalesReturnDetails> SalesReturnDetails { get; set; }
    }
}
