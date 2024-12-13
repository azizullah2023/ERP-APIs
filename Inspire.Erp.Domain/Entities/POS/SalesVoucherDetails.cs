using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class POS_SalesVoucherDetails
    {
        public long SDet_ID { get; set; }
        public long V_ID { get; set; }
        public string VoucherNo { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public string BatchCode { get; set; }
        public decimal? UnitId { get; set; }
        public decimal? Sold_Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? GrossAmt { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal FSNO { get; set; }
        public int? SNO { get; set; }
        public long? DeliveryNote { get; set; }
        public int? LocationID { get; set; }
        public int? CompanyId { get; set; }
        public long? POD_ID { get; set; }
        public long? Del_DetId { get; set; }
        public long? Del_Id { get; set; }
        public long? Delv_id { get; set; }
        public int? SerialNo_V { get; set; }
        public decimal? VatAmount { get; set; }
        public long? StationID { get; set; }
        public long? WorkPeriodID { get; set; }
        public decimal? VatableAmt { get; set; }
        public string SV_Remarks_V { get; set; }
        public string SV_Origin_V { get; set; }
        public string SV_VendorName_V { get; set; }
        public int? SVD_JCD_ID { get; set; }
        public decimal? SVD_Item_VAT { get; set; }
        public int? SerialNoRef_ID { get; set; }
        public int? EditBit { get; set; }
        public string SV_SERIAL_NO_ALL { get; set; }

        public int? UnitDetailsId { get; set; }

        [NotMapped]
        public virtual POS_SalesVoucher SalesVoucherDetailsNoNavigation { get; set; }
    }
}
