using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public partial class StockTransferViewModel
    {
        public int? StockTransferId { get; set; }
        public string StockTransferVoucherNo { get; set; }
        public DateTime? StockTransferStDate { get; set; }
        public int? StockTransferLocationIdFrom { get; set; }
        public int? StockTransferLocationIdTo { get; set; }
        public int? StockTransferFSNo { get; set; }
        public string StockTransferStatus { get; set; }
        public int? StockTransferUserId { get; set; }
        public string StockTransferNarration { get; set; }
        public int? StockTransferJobId { get; set; }
        public bool? StockTransferApproved { get; set; }
        public string StockTransferApprovedBy { get; set; }
        public DateTime? StockTransferApprovedDate { get; set; }
        public bool? StockTransferDelStatus { get; set; }



        public List<StockTransferDetailsViewModel> StockTransferDetails { get; set; }

        //public List<StockRegisterViewModel> StockRegister { get; set; }


    }
}
