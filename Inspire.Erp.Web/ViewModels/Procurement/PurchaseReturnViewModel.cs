using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public partial class PurchaseReturnViewModel
    {
        //public PurchaseReturnViewModel()
        //{
        //    PurchaseReturnDetails = new HashSet<PurchaseReturnDetailsViewModel>();
        //}
        //
        public decimal PurchaseReturnId { get; set; }
        public string PurchaseReturnNo { get; set; }
        public DateTime? PurchaseReturnDate { get; set; }
        public string PurchaseReturnType { get; set; }
        public decimal? PurchaseReturnPartyId { get; set; }
        public string PurchaseReturnPartyName { get; set; }
        public string PurchaseReturnPartyAddress { get; set; }
        public string PurchaseReturnPartyVatNo { get; set; }
        public string? PurchaseReturnRefNo { get; set; }
        public string? PurchaseReturnSupInvNo { get; set; }


        public string PurchaseReturnDescription { get; set; }

        public string PurchaseReturnGrno { get; set; }
        public DateTime? PurchaseReturnGrdate { get; set; }
        public string PurchaseReturnLpono { get; set; }
        public DateTime? PurchaseReturnLpodate { get; set; }
        public string PurchaseReturnQtnNo { get; set; }
        public DateTime? PurchaseReturnQtnDate { get; set; }
        
        public bool? PurchaseReturnExcludeVat { get; set; }
        public string PurchaseReturnPono { get; set; }
        public string PurchaseReturnBatchCode { get; set; }
        public string PurchaseReturnDayBookNo { get; set; }
        public int? PurchaseReturnLocationId { get; set; }
        public long? PurchaseReturnUserId { get; set; }
        public int? PurchaseReturnCurrencyId { get; set; }
        public int? PurchaseReturnCompanyId { get; set; }
        public int? PurchaseReturnJobId { get; set; }
        public decimal? PurchaseReturnFsno { get; set; }
        public decimal? PurchaseReturnFcRate { get; set; }
        public string PurchaseReturnStatus { get; set; }
        public decimal? PurchaseReturnTotalGrossAmount { get; set; }
        public decimal? PurchaseReturnTotalItemDisAmount { get; set; }
        public decimal? PurchaseReturnTotalActualAmount { get; set; }
        public decimal? PurchaseReturnTotalDisPer { get; set; }
        public decimal? PurchaseReturnTotalDisAmount { get; set; }
        public decimal? PurchaseReturnVatAmt { get; set; }
        public decimal? PurchaseReturnVatPer { get; set; }
        public string PurchaseReturnVatRoundSign { get; set; }
        public decimal? PurchaseReturnVatRountAmt { get; set; }
        public decimal? PurchaseReturnNetDisAmount { get; set; }
        public decimal? PurchaseReturnNetAmount { get; set; }
        public decimal? PurchaseReturnTransportCost { get; set; }
        public decimal? PurchaseReturnHandlingcharges { get; set; }
        public string PurchaseReturnIssueId { get; set; }
        public bool? PurchaseReturnJobDirectPur { get; set; }
        public bool? PurchaseReturnDelStatus { get; set; }


        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<PurchaseReturnDetailsViewModel> PurchaseReturnDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }



    }
}
