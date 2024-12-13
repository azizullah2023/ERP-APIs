using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobMasterJobDetails
    {
        public int JobMasterJobDetailsJobDetId { get; set; }
        public int? JobMasterJobDetailsJobId { get; set; }
        public string JobMasterNo { get; set; }
        public string JobMasterJobDetailsDescription { get; set; }
        public string JobMasterJobDetailsStatus { get; set; }
        public string JobMasterJobDetailsRefNo { get; set; }
        public DateTime? JobMasterJobDetailsStatusDate { get; set; }
        public bool? JobMasterJobDetailsIsEdit { get; set; }
        public int? JobMasterJobDetailsUnitIdN { get; set; }
        public double? JobMasterJobDetailsQtyN { get; set; }
        public int? JobMasterJobDetailsDescriptionId { get; set; }
        public decimal? JobMasterJobDetailsJdAmt { get; set; }
        public bool? JobMasterJobDetailsDelStatus { get; set; }
        [JsonIgnore]
        public virtual JobMaster JobMasterJobDetailsJob { get; set; }
    }
}
