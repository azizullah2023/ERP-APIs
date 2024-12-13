using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public partial class SalesVoucherViewModel
    {
        //public SalesVoucherViewModel()
        //{
        //    SalesVoucherDetails = new HashSet<SalesVoucherDetailsViewModel>();
        //}
        //
        public decimal? SalesVoucherId { get; set; }
        public string SalesVoucherNo { get; set; }
        public DateTime? SalesVoucherDate { get; set; }
        public string SalesVoucherType { get; set; }
        public decimal? SalesVoucherPartyId { get; set; }
        public string SalesVoucherPartyName { get; set; }
        public string SalesVoucherPartyAddress { get; set; }
        public string SalesVoucherPartyVatNo { get; set; }
        public string SalesVoucherRefNo { get; set; }
        public string SalesVoucherContPerson { get; set; }
        public string SalesVoucherDlvNo { get; set; }
        public DateTime? SalesVoucherDlvDate { get; set; }
        public string SalesVoucherSono { get; set; }
        public DateTime? SalesVoucherSodate { get; set; }
        public string SalesVoucherQtnNo { get; set; }
        public DateTime? SalesVoucherQtnDate { get; set; }
        public int? SalesVoucherSalesManId { get; set; }
        public int? SalesVoucherDptId { get; set; }
        public string SalesVoucherEnqNo { get; set; }
        public DateTime? SalesVoucherEnqDate { get; set; }
        public string SalesVoucherDescription { get; set; }
        public bool? SalesVoucherExcludeVat { get; set; }
        public int? SalesVoucherLocationId { get; set; }
        public long? SalesVoucherUserId { get; set; }
        public int? SalesVoucherCurrencyId { get; set; }
        public int? SalesVoucherCompanyId { get; set; }
        public int? SalesVoucherJobId { get; set; }
        public decimal? SalesVoucherFsno { get; set; }
        public decimal? SalesVoucherFcRate { get; set; }
        public string SalesVoucherStatus { get; set; }
        public decimal? SalesVoucherTotalGrossAmount { get; set; }
        public decimal? SalesVoucherTotalItemDisAmount { get; set; }
        public decimal? SalesVoucherTotalActualAmount { get; set; }
        public decimal? SalesVoucherTotalDisPer { get; set; }
        public decimal? SalesVoucherTotalDisAmount { get; set; }
        public decimal? SalesVoucherVatAmt { get; set; }
        public decimal? SalesVoucherVatPer { get; set; }
        public string SalesVoucherVatRoundSign { get; set; }
        public decimal? SalesVoucherVatRountAmt { get; set; }
        public decimal? SalesVoucherNetDisAmount { get; set; }
        public decimal? SalesVoucherNetAmount { get; set; }
        public string SalesVoucherHeader { get; set; }
        public string SalesVoucherDelivery { get; set; }
        public string SalesVoucherNotes { get; set; }
        public string SalesVoucherFooter { get; set; }
        public string SalesVoucherPaymentTerms { get; set; }
        public string SalesVoucherSubject { get; set; }
        public string SalesVoucherContent { get; set; }
        public string SalesVoucherRemarks1 { get; set; }
        public string SalesVoucherRemarks2 { get; set; }
        public string SalesVoucherTermsAndCondition { get; set; }
        public string SalesVoucherShippinAddress { get; set; }

        public bool? SalesVoucherDelStatus { get; set; }
        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<SalesVoucherDetailsViewModel> SalesVoucherDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }


    }
}
