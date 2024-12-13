using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobCostEntry
    {
        public int? JobCostEntryJcId { get; set; }
        public string JobCostEntryAccNo { get; set; }
        public DateTime? JobCostEntryTransDate { get; set; }
        public double? JobCostEntryDrAmount { get; set; }
        public double? JobCostEntryCrAmount { get; set; }
        public string JobCostEntryVoucherType { get; set; }
        public string JobCostEntryVoucherNo { get; set; }
        public string JobCostEntryDescription { get; set; }
        public int? JobCostEntryUserId { get; set; }
        public int? JobCostEntryFsno { get; set; }
        public int? JobCostEntryLocationId { get; set; }
        public int? JobCostEntryJobId { get; set; }
        public int? JobCostEntryCostCenterId { get; set; }
        public int? JobCostEntryDepartmentId { get; set; }
        public int? JobCostEntryCompanyId { get; set; }
        public int? JobCostEntryCurrencyId { get; set; }
        public bool? JobCostEntryDelStatus { get; set; }
    }
}
