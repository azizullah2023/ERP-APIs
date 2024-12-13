using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockReqDetails
    {
        public int? StockReqDetailsSrNo { get; set; }
        public int? StockReqDetailsSno { get; set; }
        public int? StockReqDetailsMaterialId { get; set; }
        public double? StockReqDetailsReqQty { get; set; }
        public double? StockReqDetailsRate { get; set; }
        public double? StockReqDetailsReqAmount { get; set; }
        public int? StockReqDetailsUnitId { get; set; }
        public string StockReqDetailsRemarks { get; set; }
        public string StockReqDetailsStatus { get; set; }
        public int? StockReqDetailsFsno { get; set; }
        public bool? StockReqDetailsIsEdit { get; set; }
        public int? StockReqDetailsSrdId { get; set; }
        public bool? StockReqDetailsIsApproved { get; set; }
        public string StockReqDetailsApprovedBy { get; set; }
        public bool? StockReqDetailsDelStatus { get; set; }
    }
}
