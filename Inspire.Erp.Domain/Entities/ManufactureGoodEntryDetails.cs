using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ManufactureGoodEntryDetails
    {
        public int? ManufactureGoodEntryDetailsDetId { get; set; }
        public int? ManufactureGoodEntryDetailsId { get; set; }
        public int? ManufactureGoodEntryDetailsSno { get; set; }
        public int? ManufactureGoodEntryDetailsMaterialId { get; set; }
        public double? ManufactureGoodEntryDetailsQty { get; set; }
        public int? ManufactureGoodEntryDetailsUnitId { get; set; }
        public string ManufactureGoodEntryDetailsRemarks { get; set; }
        public int? ManufactureGoodEntryDetailsMaterialId2 { get; set; }
        public double? ManufactureGoodEntryDetailsKg { get; set; }
        public bool? ManufactureGoodEntryDetailsDelStatus { get; set; }
    }
}
