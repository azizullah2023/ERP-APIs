using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public class StockLedgerParametersViewModel
    {

        public bool? rptSummary { get; set; }
        public bool? rptDetails { get; set; }
        public bool? rptDateRange { get; set; }
        public bool? rptAsOnDate { get; set; }
        public bool? rptPrintWithRef { get; set; }
        public bool? rptPrintWithCheq { get; set; }
        public bool? rptPrintWithAlloc { get; set; }
        public bool? rptPrintWithAgainstAcc { get; set; }
        public bool? rptPrintWithRelatedAccTrans { get; set; }
        public bool? rptHideZero { get; set; }


        public int? rptItemGroup { get; set; }

        public int? rptJob { get; set; }
        public int? rptItem { get; set; }
        public int? rptLocation { get; set; }





        public DateTime? rptDtpAson { get; set; }

        public DateTime? rptDtpFrom { get; set; }
        public DateTime? rptDtpTo { get; set; }
        public Double? rptTotDebit { get; set; }
        public Double? rptTotCredit { get; set; }
        public string? rptTotRunningBalanceCreditDebit { get; set; }
        public string? rptPartNo { get; set; }
        public string? rptNarration { get; set; }

        public List<ViewStockTransferTypeViewModel>? viewStockTransferType { get; set; }

    }
}
