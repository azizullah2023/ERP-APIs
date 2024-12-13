using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public partial class PurchaseQuotationViewModel
    {
        //public PurchaseQuotationViewModel()
        //{
        //    PurchaseQuotationDetails = new HashSet<PurchaseQuotationDetailsViewModel>();
        //}
        //
        public decimal PurchaseQuotationId { get; set; }
        public string PurchaseQuotationNo { get; set; }
        public DateTime PurchaseQuotationDate { get; set; }
        public string PurchaseQuotationType { get; set; }
        public decimal? PurchaseQuotationPartyId { get; set; }
        public string PurchaseQuotationPartyName { get; set; }
        public string PurchaseQuotationPartyAddress { get; set; }
        public string PurchaseQuotationPartyVatNo { get; set; }
        public string? PurchaseQuotationRefNo { get; set; }
        public string? PurchaseQuotationSupInvNo { get; set; }


        public string PurchaseQuotationDescription { get; set; }

        public string PurchaseQuotationGrno { get; set; }
        public DateTime? PurchaseQuotationGrdate { get; set; }
        public string PurchaseQuotationLpono { get; set; }
        public DateTime? PurchaseQuotationLpodate { get; set; }
        public string PurchaseQuotationQtnNo { get; set; }
        public DateTime? PurchaseQuotationQtnDate { get; set; }
        
        public bool? PurchaseQuotationExcludeVat { get; set; }
        public string PurchaseQuotationPono { get; set; }
        public string PurchaseQuotationBatchCode { get; set; }
        public string PurchaseQuotationDayBookNo { get; set; }
        public int? PurchaseQuotationLocationId { get; set; }
        public long? PurchaseQuotationUserId { get; set; }
        public int? PurchaseQuotationCurrencyId { get; set; }
        public int? PurchaseQuotationCompanyId { get; set; }
        public long? PurchaseQuotationJobId { get; set; }
        public decimal? PurchaseQuotationFsno { get; set; }
        public decimal? PurchaseQuotationFcRate { get; set; }
        public string PurchaseQuotationStatus { get; set; }
        public decimal? PurchaseQuotationTotalGrossAmount { get; set; }
        public decimal? PurchaseQuotationTotalItemDisAmount { get; set; }
        public decimal? PurchaseQuotationTotalActualAmount { get; set; }
        public decimal? PurchaseQuotationTotalDisPer { get; set; }
        public decimal? PurchaseQuotationTotalDisAmount { get; set; }
        public decimal? PurchaseQuotationVatAmt { get; set; }
        public decimal? PurchaseQuotationVatPer { get; set; }
        public string PurchaseQuotationVatRoundSign { get; set; }
        public decimal? PurchaseQuotationVatRountAmt { get; set; }
        public decimal? PurchaseQuotationNetDisAmount { get; set; }
        public decimal? PurchaseQuotationNetAmount { get; set; }
        public decimal? PurchaseQuotationTransportCost { get; set; }
        public decimal? PurchaseQuotationHandlingcharges { get; set; }
        public string PurchaseQuotationIssueId { get; set; }
        public bool? PurchaseQuotationJobDirectPur { get; set; }
        public bool? PurchaseQuotationDelStatus { get; set; }



        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<PurchaseQuotationDetailsViewModel> PurchaseQuotationDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }


    }
}
