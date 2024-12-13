using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class AllocationDetails
    {
        public long TransNo { get; set; }
        public DateTime? TransDate { get; set; }
       public string VoucherNo { get; set; }
        public string Type { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Balance { get; set; }
        public decimal? AllocAmount { get; set; }
        public decimal? NetAllocation { get; set; }
        public string Status { get; set; }
        public string RefLocation { get; set; }
    }
}
