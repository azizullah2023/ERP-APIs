using System;
using System.Collections.Generic;
using Inspire.Erp.Domain.Entities;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public partial class SalesReturnViewModel
    {
        //public SalesReturnViewModel()
        //{
        //    SalesReturnDetails = new HashSet<SalesReturnDetailsViewModel>();
        //}
        //
        public decimal SalesReturnId { get; set; }
        public string SalesReturnNo { get; set; }
        public DateTime SalesReturnDate { get; set; }
        public string SalesReturnType { get; set; }
        public decimal? SalesReturnPartyId { get; set; }
        public string SalesReturnPartyName { get; set; }
        public string SalesReturnPartyAddress { get; set; }
        public string SalesReturnPartyVatNo { get; set; }
        public string SalesReturnRefNo { get; set; }
        public string SalesReturnContPerson { get; set; }
        public string SalesReturnDlvNo { get; set; }
        public DateTime? SalesReturnDlvDate { get; set; }
        public string SalesReturnSono { get; set; }
        public DateTime? SalesReturnSodate { get; set; }
        public string SalesReturnQtnNo { get; set; }
        public DateTime? SalesReturnQtnDate { get; set; }
        public int? SalesReturnSalesManId { get; set; }
        public int? SalesReturnDptId { get; set; }
        public string SalesReturnEnqNo { get; set; }
        public DateTime? SalesReturnEnqDate { get; set; }
        public string SalesReturnDescription { get; set; }
        public bool? SalesReturnExcludeVat { get; set; }
        public int? SalesReturnLocationId { get; set; }
        public long? SalesReturnUserId { get; set; }
        public int? SalesReturnCurrencyId { get; set; }
        public int? SalesReturnCompanyId { get; set; }
        public int? SalesReturnJobId { get; set; }
        public decimal? SalesReturnFsno { get; set; }
        public decimal? SalesReturnFcRate { get; set; }
        public string SalesReturnStatus { get; set; }
        public decimal? SalesReturnTotalGrossAmount { get; set; }
        public decimal? SalesReturnTotalItemDisAmount { get; set; }
        public decimal? SalesReturnTotalActualAmount { get; set; }
        public decimal? SalesReturnTotalDisPer { get; set; }
        public decimal? SalesReturnTotalDisAmount { get; set; }
        public decimal? SalesReturnVatAmt { get; set; }
        public decimal? SalesReturnVatPer { get; set; }
        public string SalesReturnVatRoundSign { get; set; }
        public decimal? SalesReturnVatRountAmt { get; set; }
        public decimal? SalesReturnNetDisAmount { get; set; }
        public decimal? SalesReturnNetAmount { get; set; }
        public string SalesReturnHeader { get; set; }
        public string SalesReturnDelivery { get; set; }
        public string SalesReturnNotes { get; set; }
        public string SalesReturnFooter { get; set; }
        public string SalesReturnPaymentTerms { get; set; }
        public string SalesReturnSubject { get; set; }
        public string SalesReturnContent { get; set; }
        public string SalesReturnRemarks1 { get; set; }
        public string SalesReturnRemarks2 { get; set; }
        public string SalesReturnTermsAndCondition { get; set; }
        public string SalesReturnShippinAddress { get; set; }
        public string SalesReturnSvno { get; set; }
        public DateTime? SalesReturnSvdate { get; set; }

        public bool? SalesReturnDelStatus { get; set; }
        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<SalesReturnDetailsViewModel> SalesReturnDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }


    }

    public class DTOSalesReturnSave
    {
        public SalesReturn salesReturn { get; set; }
        public List<SalesReturnDetails> salesReturnDetails { get; set; }
        public List<AccountsTransactions> accountsTransactions { get; set; }
    }
}
