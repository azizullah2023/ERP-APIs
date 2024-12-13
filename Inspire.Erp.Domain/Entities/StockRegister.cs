using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockRegister
    {
        public long StockRegisterStoreID { get; set; }
        public string StockRegisterPurchaseID { get; set; }
        public string StockRegisterRefVoucherNo { get; set; }
        public DateTime? StockRegisterVoucherDate { get; set; }
        public int? StockRegisterSno { get; set; }
        public string StockRegisterBatchCode { get; set; }
        public DateTime? StockRegisterExpDate { get; set; }
        public int? StockRegisterMaterialID { get; set; }
        public decimal? StockRegisterQuantity { get; set; }
        public decimal? StockRegisterSIN { get; set; }
        public decimal? StockRegisterSout { get; set; }
        public decimal? StockRegisterRate { get; set; }
        public decimal? StockRegisterAmount { get; set; }
        public decimal? StockRegisterFCAmount { get; set; }
        public DateTime? StockRegisterAssignedDate { get; set; }
        public string StockRegisterDepCode { get; set; }
        public string StockRegisterStatus { get; set; }
        public string StockRegisterTransType { get; set; }
        public string StockRegisterRemarks { get; set; }
        public int? StockRegisterUnitID { get; set; }
        public int? StockRegisterLocationID { get; set; }
        public int? StockRegisterJobID { get; set; }
        public int? StockRegisterFSNO { get; set; }
        public decimal? StockRegisterNetStkBal { get; set; }
        public string StockRegisterFOC { get; set; }
        public decimal? StockRegisterUsedQTY { get; set; }
        public bool? StockRegisterQueryRun { get; set; }
        public bool? StockRegisterCalcDone { get; set; }
        public decimal? StockRegisterRateTmp { get; set; }
        public decimal? StockRegisterAmountTmp { get; set; }
        public int? StockRegisterDepID { get; set; }
        public decimal? StockRegisterLandingCost { get; set; }
        public decimal? StockRegisterFcRate { get; set; }
        public bool? StockRegisterDelStatus { get; set; }

    }
}

