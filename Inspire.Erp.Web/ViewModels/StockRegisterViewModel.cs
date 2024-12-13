using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class StockRegisterViewModel
    {

        public int StockRegisterStoreID { get; set; }
        public string StockRegisterPurchaseId { get; set; }
        public string StockRegisterRefVoucherNo { get; set; }
        public DateTime StockRegisterVoucherDate { get; set; }
        public int? StockRegisterSno { get; set; }
        public string StockRegisterBatchCode { get; set; }
        public DateTime? StockRegisterExpDate { get; set; }
        public int? StockRegisterMaterialId { get; set; }
        public double? StockRegisterQuantity { get; set; }
        public decimal? StockRegisterSin { get; set; }
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
        public int? StockRegisterJobId { get; set; }
        public int? StockRegisterFsno { get; set; }
        public decimal? StockRegisterNetStkBal { get; set; }
        public string StockRegisterFoc { get; set; }
        public double? StockRegisterUsedQty { get; set; }
        public bool? StockRegisterQueryRun { get; set; }
        public bool? StockRegisterCalcDone { get; set; }
        public double? StockRegisterRateTmp { get; set; }
        public double? StockRegisterAmountTmp { get; set; }
        public decimal? StockRegisterFcRate { get; set; }
        public int? StockRegisterDepID { get; set; }
        public decimal? StockRegisterLandingCost { get; set; }
        public bool? StockRegisterDelStatus { get; set; }
    }
}
