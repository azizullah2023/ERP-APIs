using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ItemPriceLevelDetailsViewModel
    {
        public int? ItemPriceLevelId { get; set; }
        public long? ItemId { get; set; }
        public long? LevelId { get; set; }
        public double? LevelRate { get; set; }
        public double? LevelAmt { get; set; }
        public int? UnitId { get; set; }
    }
}
