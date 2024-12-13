using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class UnitDetails
    {
        public int UnitDetailsId { get; set; }
        public long UnitDetailsItemId { get; set; }
        public int UnitDetailsUnitId { get; set; }
        public double? UnitDetailsConversionType { get; set; }
        public double? UnitDetailsRate { get; set; }
        public double? UnitDetailsWrate { get; set; }
        public bool? UnitDetailsDefUnit { get; set; }
        public bool? UnitDetailsDefWunit { get; set; }
        public string UnitDetailsPacking { get; set; }
        public string UnitDetailsBarcode { get; set; }
        public string UnitDetailsDescrption { get; set; }
        public double? UnitDetailsUnitCost { get; set; }
        public double? UnitDetailsUnitPrice { get; set; }
        public decimal? UnitDetailsProfitPer { get; set; }
        public decimal? UnitDetailsOverhdPer { get; set; }
        public decimal? UnitDetailsHeight { get; set; }
        public decimal? UnitDetailsWidth { get; set; }
        public decimal? UnitDetailsReorder { get; set; }
        public bool? UnitDetailsDelStatus { get; set; }

        public virtual ItemMaster UnitDetailsItem { get; set; }
        public virtual UnitMaster UnitDetailsUnit { get; set; }
    }
}
