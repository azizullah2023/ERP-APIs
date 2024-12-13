using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockAdjustmentVoucherDetails
    {
        public int StockAdjustmentVoucherDetailsDetId { get; set; }
        public string StockAdjustmentVoucherDetailsSaNo { get; set; }
        public int? StockAdjustmentVoucherDetailsMaterialId { get; set; }
        public double? StockAdjustmentVoucherDetailsManualQty { get; set; }
        public double? StockAdjustmentVoucherDetailsAdjQty { get; set; }
        public int? StockAdjustmentVoucherDetailsLocationId { get; set; }
        public int? StockAdjustmentVoucherDetailsJobId { get; set; }
        public int? StockAdjustmentVoucherDetailsSaId { get; set; }
        public double? StockAdjustmentVoucherDetailsCosePrice { get; set; }
        public bool? StockAdjustmentVoucherDetailsIsEdit { get; set; }
        public int? StockAdjustmentVoucherDetailsUnitId { get; set; }
        public string StockAdjustmentVoucherDetailsBatchCode { get; set; }
        public DateTime? StockAdjustmentVoucherDetailsExpDate { get; set; }
        public bool? StockAdjustmentVoucherDetailsDelStatus { get; set; }
    }
}
