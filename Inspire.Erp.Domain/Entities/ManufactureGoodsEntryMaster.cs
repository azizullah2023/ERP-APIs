using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ManufactureGoodsEntryMaster
    {
        public int? ManufactureGoodsEntryMasterId { get; set; }
        public string ManufactureGoodsEntryMasterRefNo { get; set; }
        public string ManufactureGoodsEntryMasterRemarks { get; set; }
        public DateTime? ManufactureGoodsEntryMasterEntryDate { get; set; }
        public int? ManufactureGoodsEntryMasterLocationId { get; set; }
        public bool? ManufactureGoodsEntryMasterDelStatus { get; set; }
    }
}
