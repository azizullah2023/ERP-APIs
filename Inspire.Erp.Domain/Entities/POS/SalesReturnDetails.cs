using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class POS_SalesReturnDetails
    {
        public int Id { get; set; }
        public string SRNO { get; set; }
        public string BatchCode { get; set; }
        public string VoucherNo { get; set; }
        public long? MaterialId { get; set; }
        public long? UnitId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? GrossAmt { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? FCAmount { get; set; }
        public long? FSNO { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? VAT { get; set; }
        public decimal? FOCQuantity { get; set; }


        [NotMapped]
        public virtual POS_SalesReturn SalesReturnDetailsNoNavigation { get; set; }
    }
}
