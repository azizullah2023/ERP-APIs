using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Store
{
   

    public class OpeningStockDetails
    {
        public int? openingStockStockId { get; set; }
        public int? openingStockPurchaseId { get; set; }
        public int? openingStockSno { get; set; }
        public string openingStockBatchCode { get; set; }
        public int? openingStockMaterialId { get; set; }
        public int? openingStockQty { get; set; }
        public int? openingStockCurrencyId { get; set; }
        public int? openingStockCRate { get; set; }
        public int? openingStockUnitRate { get; set; }
        public int? openingStockAmount { get; set; }
        public int? openingStockFcAmount { get; set; }
        public string openingStockRemakrs { get; set; }
        public int? openingStockFsno { get; set; }
        public bool? openingStockPosted { get; set; }
        public int? openingStockUnitId { get; set; }
        public int? openingStockLocationId { get; set; }
        public int? openingStockJobId { get; set; }
        public bool? openingStockIsEdit { get; set; }
        public DateTime? openingStockExpDate { get; set; }
        public bool? openingStockDelStatus { get; set; }
    }

    public class OpeningStockDtViewModel
    {
        public DateTime? openingStockDate { get; set; }
        public int? openingStockLocationId { get; set; }    
        public string openingStockCreditAcc { get; set; }
        public List<OpeningStockDetails>? openingStockDetails { get; set; }
        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<StockRegisterViewModel> StockRegister { get; set; }
    }


}
