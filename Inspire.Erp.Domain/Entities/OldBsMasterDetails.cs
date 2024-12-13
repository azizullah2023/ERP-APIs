using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OldBsMasterDetails
    {
        public int? OldBsMasterDetailsId { get; set; }
        public int? OldBsMasterDetailsSno { get; set; }
        public string OldBsMasterDetailsAccNo { get; set; }
        public double? OldBsMasterDetailsCrate { get; set; }
        public double? OldBsMasterDetailsBcAmount { get; set; }
        public double? OldBsMasterDetailsFcAmount { get; set; }
        public string OldBsMasterDetailsNarration { get; set; }
        public string OldBsMasterDetailsGroupName { get; set; }
        public string OldBsMasterDetailsDrCrType { get; set; }
        public int? OldBsMasterDetailsFsno { get; set; }
        public bool? OldBsMasterDetailsDelStatus { get; set; }
    }
}
