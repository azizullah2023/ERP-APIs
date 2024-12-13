using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class RfqEntryDetails
    {
        public int? RfqEntryDetailsDetId { get; set; }
        public int? RfqEntryDetailsItemId { get; set; }
        public string RfqEntryDetailsDescription { get; set; }
        public int? RfqEntryDetailsUnitId { get; set; }
        public double? RfqEntryDetailsQuantity { get; set; }
        public double? RfqEntryDetailsPrice { get; set; }
        public double? RfqEntryDetailsDelqty { get; set; }
        public double? RfqEntryDetailsRfqamt { get; set; }
        public string RfqEntryDetailsRemarks { get; set; }
        public bool? RfqEntryDetailsDelStatus { get; set; }
    }
}
