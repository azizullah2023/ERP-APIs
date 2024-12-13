using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReportStockRegister
    {


        ////public string DepartmentMasterDepartmentName { get; set; }
        ////public string LocationMasterLocationName { get; set; }
        ////public string JobMasterJobName { get; set; }
        ////public string ItemMasterItemName { get; set; }
        ////public string ItemMasterPartNo { get; set; }
        ////public string UnitMasterUnitShortName { get; set; }
        ////public decimal StockRegisterId { get; set; }
        ////public string StockRegisterTransType { get; set; }
        ////public string StockRegisterVoucherNo { get; set; }
        ////public DateTime? StockRegisterVoucherDate { get; set; }
        ////public string StockRegisterRemarks { get; set; }
        ////public decimal? StockRegisterSno { get; set; }
        ////public string StockRegisterBatchCode { get; set; }
        ////public DateTime? StockRegisterExpDate { get; set; }
        ////public long? StockRegisterMatId { get; set; }
        ////public decimal? StockRegisterQuantity { get; set; }
        ////public int? StockRegisterUnitId { get; set; }
        ////public decimal? StockRegisterSin { get; set; }
        ////public decimal? StockRegisterSout { get; set; }
        ////public decimal? StockRegisterRate { get; set; }
        ////public decimal? StockRegisterAmount { get; set; }
        ////public decimal? StockRegisterFCAmount { get; set; }
        ////public string StockRegisterStatus { get; set; }
        ////public int? StockRegisterDepId { get; set; }
        ////public decimal? StockRegisterFcRate { get; set; }
        ////public int? StockRegisterLocationID { get; set; }
        ////public int? StockRegisterJobId { get; set; }
        ////public int? StockRegisterFsno { get; set; }
        ////public double? StockRegisterNetStkBal { get; set; }
        ////public double? StockRegisterLandingCost { get; set; }
        ////public bool? StockRegisterDelStatus { get; set; }
        ///

        public string DepartmentMasterDepartmentName { get; set; }
        public string LocationMasterLocationName { get; set; }
        public string JobMasterJobName { get; set; }
        public string ItemMasterItemName { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public decimal StockRegisterStoreID { get; set; }
        public string StockRegisterPurchaseId { get; set; }
        public string StockRegisterRefVoucherNo { get; set; }
        public DateTime? StockRegisterVoucherDate { get; set; }
        public int? StockRegisterSno { get; set; }
        public string StockRegisterBatchCode { get; set; }
        public DateTime? StockRegisterExpDate { get; set; }
        public int? StockRegisterMaterialId { get; set; }
        public decimal? StockRegisterQuantity { get; set; }
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
        public int? StockRegisterUnitId { get; set; }
        public int? StockRegisterLocationID { get; set; }
        public int? StockRegisterJobId { get; set; }
        public int? StockRegisterFsno { get; set; }
        public decimal? StockRegisterNetStkBal { get; set; }
        public string StockRegisterFoc { get; set; }
        public decimal? StockRegisterUsedQty { get; set; }
        public bool? StockRegisterQueryRun { get; set; }
        public bool? StockRegisterCalcDone { get; set; }
        public decimal? StockRegisterRateTmp { get; set; }
        public decimal? StockRegisterAmountTmp { get; set; }
        public int? StockRegisterDepId { get; set; }
        public decimal? StockRegisterLandingCost { get; set; }
        public decimal? StockRegisterFcRate { get; set; }
        public bool? StockRegisterDelStatus { get; set; }

    }
}
