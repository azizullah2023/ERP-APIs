using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DeliveryOrderVoucherBatchDetails
    {
        public string DovVoucherNo { get; set; }
        public int? DovItemId { get; set; }
        public string DovDescription { get; set; }
        public string DovBatchCode { get; set; }
        public int? DovUnitId { get; set; }
        public double? DovSoldQty { get; set; }
        public double? DovUnitPrice { get; set; }
        public double? DovCostPrice { get; set; }
        public double? DovGrossAmount { get; set; }
        public double? DovDiscount { get; set; }
        public double? DovNetAmount { get; set; }
        public int? DovFsno { get; set; }
        public int? DovSno { get; set; }
        public int? DovDeliveryNoteId { get; set; }
        public int? DovLocationId { get; set; }
        public int? DovCompanyId { get; set; }
        public int? DovPodId { get; set; }
        public int? DovDeliveryId { get; set; }
        public int? DovDeliveryDetialsId { get; set; }
        public DateTime? DovExpDate { get; set; }
        public double? DovFocqty { get; set; }
        public double? DovVat { get; set; }
        public bool? DovDelStatus { get; set; }
    }
}
