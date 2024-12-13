using Inspire.Erp.Domain.Modals.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public partial class StockRegisterResponse
    {
        public long? Item_Id { get; set; }
        public string? Stock_Register_Unit { get; set; }
        public decimal? OpenQty { get; set; }
        public decimal? OpenQtyAmount { get; set; }
        public decimal? StockIn { get; set; }
        public decimal? StockInAmount { get; set; }
        public decimal? StockOut { get; set; }
        public decimal? StockOutAmount { get; set; }
        public decimal? TotalBal { get; set; }
        public decimal? TotalBalAmount { get; set; }
        public string? Item_Name { get; set; }

        //[NotMapped]
        //public string profileUrl { get; set; }
    }
    public class StockMovementResponse
    {
        public List<StockRegisterResponse> StockRegisterResponse { get; set; }

        public List<StockMovementInOut> StockMovementIn { get; set; }
        public List<StockMovementInOut> StockMovementOut { get; set; }
    }

    public class StockMovementInOut
    {
        public int? Sr { get;set; }
        public string? Ref_Vocher_No { get; set; }
        public string? Voucher_Date { get; set; }
        public string? TransType { get; set; }
        public string? Location { get; set; }
        public string? Unit { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Qty { get; set; }
        public decimal? QtyAmount { get; set; }

    }
}
