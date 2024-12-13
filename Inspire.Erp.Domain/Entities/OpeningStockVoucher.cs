using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OpeningStockVoucher
    {
        public OpeningStockVoucher()
        {
            OpeningStockVoucherDetails = new HashSet<OpeningStockVoucherDetails>();
        }

        public int? OpeningStockVoucherId { get; set; }
        public string OpeningStockVoucherNo { get; set; }
        public DateTime OpeningStockVoucherDate { get; set; }
        public int? OpeningStockVoucherLocationId { get; set; }
        public string OpeningStockVoucherRemarks { get; set; }
        public int? OpeningStockVoucherFSNO { get; set; }
        public string OpeningStockVoucherVNO { get; set; }
        public bool? OpeningStockVoucherDelStatus { get; set; }
        public virtual ICollection<OpeningStockVoucherDetails> OpeningStockVoucherDetails { get; set; }
        
    }
}
