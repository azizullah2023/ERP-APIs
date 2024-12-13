using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class VWStockBaseUnitWithValue
    {   
        public long ItemId { get; set; }
        public decimal Stock { get; set; }
        public string? ItemName { get; set; }
        public long RelativeNO { get; set; }
        public int? UnitId { get; set; }
        public string? UnitShortName { get; set; }
        public decimal AVGRATE { get; set; }
        public decimal AMOUNT{ get; set; }
        public decimal AVGPURCHASEValue { get; set;}
        [NotMapped]
        public string ItemNo { get; set; }

    }
}

 