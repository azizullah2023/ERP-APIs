using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BatchMaster
    {
        public int BatchMasterId { get; set; }
        public int? BatchMasterItemId { get; set; }
        public string BatchMasterBatchCode { get; set; }
        public DateTime? BatchMasterExpiryDate { get; set; }
        public bool? BatchMasterDelStatus { get; set; }
    }
}
