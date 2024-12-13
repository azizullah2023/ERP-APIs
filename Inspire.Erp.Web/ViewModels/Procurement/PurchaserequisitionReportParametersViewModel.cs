using System;
using System.Collections.Generic;
using Inspire.Erp.Web.ViewModels.Store;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class PurchaserequisitionReportParametersViewModel
    {

        public bool? rptSummary { get; set; }
        public bool? rptDetails { get; set; }
        public bool? rptDateRange { get; set; }

        public DateTime? rptDtpFrom { get; set; }
        public DateTime? rptDtpTo { get; set; }

        public int? rptBrand { get; set; }
        public int? rptItemGroup { get; set; }
        public int? rptItem { get; set; }
        public string? rptPartNo { get; set; }
        public int? rptLocation { get; set; }
        public int? rptJob { get; set; }

        public int? rptSalesMan { get; set; }
        public int? rptDpt { get; set; }

        public int? rptCurrency { get; set; }

        public string? rptInvoiceType { get; set; }
        public int? rptSupplier { get; set; }

        public string? rptSupplierName { get; set; }

        public string? rptSuppInvNo { get; set; }

        public bool? rptPrintGroupByItem { get; set; }

        public List<ViewStockTransferTypeViewModel>? viewStockTransferType { get; set; }

    }
}
