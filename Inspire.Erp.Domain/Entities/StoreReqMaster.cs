using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StoreReqMaster
    {
        public int? StoreReqMasterSrno { get; set; }
        public DateTime? StoreReqMasterSrDate { get; set; }
        public int? StoreReqMasterJobId { get; set; }
        public int? StoreReqMasterFsno { get; set; }
        public int? StoreReqMasterUserId { get; set; }
        public int? StoreReqMasterLocationId { get; set; }
        public string StoreReqMasterNarration { get; set; }
        public int? StoreReqMasterBillofQtyId { get; set; }
        public bool? StoreReqMasterDelStatus { get; set; }
    }
}
