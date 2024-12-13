using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesVoucherBatchDetials
    {
        public int SalesVoucherBatchDetialsDetId { get; set; }
        public string SalesVoucherBatchDetialsVoucherNo { get; set; }
        public int? SalesVoucherBatchDetialsItemId { get; set; }
        public string SalesVoucherBatchDetialsDescription { get; set; }
        public string SalesVoucherBatchDetialsBatchCode { get; set; }
        public int? SalesVoucherBatchDetialsUnitId { get; set; }
        public double? SalesVoucherBatchDetialsSoldQty { get; set; }
        public double? SalesVoucherBatchDetialsUnitPrice { get; set; }
        public double? SalesVoucherBatchDetialsCostPrice { get; set; }
        public double? SalesVoucherBatchDetialsGrossAmt { get; set; }
        public double? SalesVoucherBatchDetialsDiscount { get; set; }
        public double? SalesVoucherBatchDetialsNetAmount { get; set; }
        public int? SalesVoucherBatchDetialsFsno { get; set; }
        public int? SalesVoucherBatchDetialsSno { get; set; }
        public int? SalesVoucherBatchDetialsDeliveryNote { get; set; }
        public int? SalesVoucherBatchDetialsLocationId { get; set; }
        public int? SalesVoucherBatchDetialsCompanyId { get; set; }
        public int? SalesVoucherBatchDetialsPodId { get; set; }
        public int? SalesVoucherBatchDetialsDeliveryId { get; set; }
        public int? SalesVoucherBatchDetialsDelDetailsId { get; set; }
        public DateTime? SalesVoucherBatchDetialsExpDate { get; set; }
        public double? SalesVoucherBatchDetialsFocQty { get; set; }
        public bool? SalesVoucherBatchDetailsDelStatus { get; set; }
    }
}
