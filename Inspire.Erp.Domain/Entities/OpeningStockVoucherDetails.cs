using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OpeningStockVoucherDetails
    {

        public decimal? OpeningStockVoucherDetailsId { get; set; }
        public int? OpeningStockVoucherId { get; set; }
        public string OpeningStockVoucherDetailsNo { get; set; }
        public int? OpeningStockVoucherDetailsMatId { get; set; }
        public decimal? OpeningStockVoucherDetailsManualQty { get; set; }
        public decimal? OpeningStockVoucherDetailsAdjQty { get; set; }
        public string OpeningStockVoucherDetailsItemName { get; set; }
        public int? OpeningStockVoucherDetailsUnitId { get; set; }
        public decimal? OpeningStockVoucherDetailsAmount { get; set; }
        public decimal? OpeningStockVoucherDetailsCost { get; set; }
        public decimal? OpeningStockVoucherDetailsConversionType { get; set; }
        public bool? OpeningStockVoucherDetailsIsEdit { get; set; }
        public bool? OpeningStockVoucherDetailsDelStatus { get; set; }

        public long? OpeningStockVoucherDetailsSA_Id { get; set; }
        public long? OpeningStockVoucherDetailsJob_Id { get; set; }
     
        public string OpeningStockVoucherDetailsRemarks { get; set; }

      


        public virtual OpeningStockVoucher OpeningStockVoucherDetailsNoNavigation { get; set; }
    }
}
