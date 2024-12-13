using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Store
{
    public partial class OpeningStockVoucherViewModel
    {

        public int? OpeningStockVoucherId { get; set; }
        public string OpeningStockVoucherNo { get; set; }
        public DateTime OpeningStockVoucherDate { get; set; }
        public int? OpeningStockVoucherLocationId { get; set; }

        public string OpeningStockVoucherRemarks { get; set; }

        public int? OpeningStockVoucherFSNO { get; set; }

        public string OpeningStockVoucherVNO { get; set; }

        public bool? OpeningStockVoucherDelStatus { get; set; }

        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<OpeningStockVoucherDetailsViewModel> OpeningStockVoucherDetails { get; set; }

        public List<StockRegisterViewModel> StockRegister { get; set; }


    }
}
