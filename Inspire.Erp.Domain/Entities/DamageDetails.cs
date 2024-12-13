using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DamageDetails
    {
        public int? DamageDetailsId { get; set; }
        public int? DamageDetailsSno { get; set; }
        public int? DamageDetailsMaterialId { get; set; }
        public double? DamageDetailsQty { get; set; }
        public double? DamageDetailsPrice { get; set; }
        public double? DamageDetailsAmount { get; set; }
        public string DamageDetailsRemarks { get; set; }
        public int? DamageMasterId { get; set; }
        public string DamageDetailsVoucherNumber { get; set; }
        public int? DamageDetailsUnitId { get; set; }
        public bool? DamageDetailsDelStatus { get; set; }
    }
}
