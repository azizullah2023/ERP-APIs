using Inspire.Erp.Domain.Modals.AccountStatement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Stock
{
   public class StockReportBaseUnitResponse
    {
        public long? ItemId { get; set; }

        public string PartNo { get; set; }
        public string ItemName { get; set; }
        public int? Stock { get; set; }
        public int? Unit { get; set; }
        public decimal? AverageRate { get; set; }
        public decimal? AveragePurchaseValue { get; set; }
        public decimal? Amount { get; set; }

    }

    public class StockMovementReportResponse
    {
        public List<StockMovementSInOutGroupWiseResponse> StockMovementInOutGroupWise { get; set; }
        public List<StockMovementInOutResponse> StockMovementInOut { get; set; }
    }

    public class StockMovementSInOutGroupWiseResponse
    {
        public long? ItemId { get; set; }
        public string ItemName { get; set; }
        public string GroupName { get; set; }
        public long? ItemMasterAccountNo { get; set; }
        public decimal? OpenQuantity { get; set; }
        public decimal? OpenQuantityAmount { get; set; }
        public decimal? BalanceTotal { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? PurchaseInQty { get; set; }
        public decimal? PurchaseInAmount { get; set; }
        public decimal? PurchaseOutQty { get; set; }
        public decimal? PurchaseOutAmount { get; set; }
        public decimal? SaleInQty { get; set; }
        public decimal? SaleInAmount { get; set; }
        public decimal? SaleOutQty { get; set; }
        public decimal?  SaleOutAmount { get; set; }

    }



    public class StockMovementInOutResponse
    {
        public long? Mat_ID { get; set; }
        public long UnitId { get; set; }
        public string ShortName { get; set; }
        public string? Ref_Vocher_No { get; set; }
        public string? AssingedDate { get; set; }
        public string? TransType { get; set; }
        public string? DisplayTranstype { get; set; }
        public long? RelativeNo { get; set; }
        public string? GroupName { get; set; }
        public long? JobId { get; set; }
        public string? jobname { get; set; }
        public long? LocationId { get; set; }
        public string? Loca_Name { get; set; }
     
        public decimal? Rate { get; set; }
        public decimal? QtyIn { get; set; }
        public decimal? QtyInAmount { get; set; }

        public decimal? QtyOut { get; set; }
        public decimal? QtyOutAmount { get; set; }
        public decimal? LPO { get; set; }
    }


}
