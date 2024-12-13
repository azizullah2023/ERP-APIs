using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ItemPriceLevelDetails
    {
        public int ItemPriceLevelId { get; set; }
        public long ItemId { get; set; }
        public long LevelId { get; set; }
        public double? LevelRate { get; set; }
        public double? LevelAmt { get; set; }
        public int? UnitId { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual PriceLevelMaster Level { get; set; }
    }
}
