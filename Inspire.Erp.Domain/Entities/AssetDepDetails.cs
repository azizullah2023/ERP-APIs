using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AssetDepDetails
    {
        public string AssetDepDetailsDjvno { get; set; }
        public string AssetDepDetailsPurchaseId { get; set; }
        public int? AssetDepDetailsId { get; set; }
        public DateTime? AssetDepDetailsPurchaseDate { get; set; }
        public double? AssetDepDetailsAmount { get; set; }
        public double? AssetDepDetailsAccDepAmount { get; set; }
        public DateTime? AssetDepDetailsDepFrom { get; set; }
        public DateTime? AssetDepDetailsDepTo { get; set; }
        public double? AssetDepDetailsDepPercentage { get; set; }
        public double? AssetDepDetailsDepAmount { get; set; }
        public double? AssetDepDetailsNetBookValue { get; set; }
        public bool? AssetDepDetailsDelStatus { get; set; }
    }
}
