using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities.POS
{
    public partial class SalesHoldDetails
    {
        public long SDet_ID { get; set; }
        public string VoucherNo { get; set; }
        public decimal ItemId { get; set; }
        public string Description { get; set; }
        public string BatchCode { get; set; }
        public decimal? UnitId { get; set; }
        public decimal Sold_Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? GrossAmt { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal FSNO { get; set; }
        public decimal? SNO { get; set; }
        public long? DeliveryNote { get; set; }
        public int? LocationID { get; set; }
        public int? CompanyId { get; set; }
        public long? POD_ID { get; set; }
        public long? Del_DetId { get; set; }
        public long? Del_Id { get; set; }
        public long? Delv_id { get; set; }
        public string SerialNo_V { get; set; }
        public decimal? VatAmount { get; set; }
        public long? StationID { get; set; }
        public long? WorkPeriodID { get; set; }
        public float? VatableAmt { get; set; }
        public int? udid { get; set; }
        public virtual SalesHold SalesHold { get; set; }
    }




}
