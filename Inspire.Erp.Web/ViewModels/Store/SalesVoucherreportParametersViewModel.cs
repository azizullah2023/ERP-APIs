using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public class SalesvoucherreportParametersViewModel
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
        public int? rptCustomer { get; set; }

        public string? rptCustomerName { get; set; }

        public string? rptNarration { get; set; }

        public bool? rptZeroVat { get; set; }



        public List<ViewStockTransferTypeViewModel>? viewStockTransferType { get; set; }




    }
}
