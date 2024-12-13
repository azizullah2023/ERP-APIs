using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempAssetDepCalc
    {
        public string TempAssetDepCalcPurchaseId { get; set; }
        public int? TempAssetDepCalcId { get; set; }
        public DateTime? TempAssetDepCalcPurchaseDate { get; set; }
        public double? TempAssetDepCalcAmount { get; set; }
        public double? TempAssetDepCalcAccDepAmt { get; set; }
        public double? TempAssetDepCalcBookValue { get; set; }
        public DateTime? TempAssetDepCalcDepFrom { get; set; }
        public DateTime? TempAssetDepCalcDepTo { get; set; }
        public double? TempAssetDepCalcDepPercentage { get; set; }
        public double? TempAssetDepCalcDepAmount { get; set; }
        public double? TempAssetDepCalcNetBookValue { get; set; }
        public bool? TempAssetDepCalcDelStatus { get; set; }
    }
}
