using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockReservation
    {
        public int? StockReservationId { get; set; }
        public DateTime? StockReservationRDate { get; set; }
        public int? StockReservationJobId { get; set; }
        public string StockReservationRemarks { get; set; }
        public bool? StockReservationRelease { get; set; }
        public bool? StockReservationDelStatus { get; set; }
    }
}
