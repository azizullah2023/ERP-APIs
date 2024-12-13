using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class POS_SalesReturn
    {
        public POS_SalesReturn()
        {
            SalesVoucherDetails = new HashSet<POS_SalesVoucherDetails>();
        }
        public int Id { get; set; }
        public string SRNo { get; set; }
        public int? SRNoNumber { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnType { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public int? CustNo { get; set; }
        public string CustName { get; set; }
        public long? LocationId { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? FCNetAmount { get; set; }
        public string Narration { get; set; }

        public string Status { get; set; }

        public int? FSNO { get; set; }
        public int? UserId { get; set; }
        public long? CurrencyId { get; set; }
        public decimal? FCRate { get; set; }
        public decimal? ItemDiscount { get; set; }
        public decimal? NetDiscount { get; set; }

        public decimal? VatAmount { get; set; }
        public decimal? VatPercentage { get; set; }
        public string? VatRoungSign { get; set; }

        public decimal? VatRountAmount { get; set; }
        public int? SalesManId { get; set; }
    
        public long? StationID { get; set; }
        public long? WorkPeriodID { get; set; }
    
        public bool? DelStatus { get; set; }
       

        [NotMapped]
        public virtual ICollection<POS_SalesVoucherDetails> SalesVoucherDetails { get; set; }
    }
}
