using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockAdjustmentVoucher
    {
        public int? StockAdjustmentVoucherSaId { get; set; }
        public string StockAdjustmentVoucherSaNo { get; set; }
        public DateTime? StockAdjustmentVoucherSaDate { get; set; }
        public string StockAdjustmentVoucherSaRemarks { get; set; }
        public int? StockAdjustmentVoucherFsno { get; set; }
        public string? StockAdjustmentVoucherStVno { get; set; }
        public int? StockAdjustmentVoucherLocationId { get; set; }
        public bool? StockAdjustmentVoucherDelStatus { get; set; }

        [NotMapped]
        public List<StockAdjustmentVoucherDetails> stockAdjustmentVoucherDetails { get; set; }
    }
}
