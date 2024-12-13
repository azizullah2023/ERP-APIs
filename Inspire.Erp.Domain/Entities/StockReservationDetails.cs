using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class StockReservationDetails
    {
        public int? StockReservationDetailsRdId { get; set; }
        public int? StockReservationDetailsRId { get; set; }
        public int? StockReservationDetailsItemId { get; set; }
        public double? StockReservationDetailsSQty { get; set; }
        public string StockReservationDetailsRemarks { get; set; }
        public int? StockReservationDetailsUnitId { get; set; }
        public bool? StockReservationDetailsRelease { get; set; }
        public bool? StockReservationDetailsDelStatus { get; set; }
    }
}
