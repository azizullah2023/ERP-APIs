using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockTransferJobWise
    {
        public int? StockTransferJobWiseStid { get; set; }
        public string StockTransferJobWiseVno { get; set; }
        public DateTime? StockTransferJobWiseDate { get; set; }
        public int? StockTransferJobWiseJobIdFrom { get; set; }
        public int? StockTransferJobWiseJobIdTo { get; set; }
        public int? StockTransferJobWiseFsno { get; set; }
        public string StockTransferJobWiseStatus { get; set; }
        public int? StockTransferJobWiseUserId { get; set; }
        public string StockTransferJobWiseNarration { get; set; }
        public bool? StockTransferJobWiseDelStatus { get; set; }
    }
}
