using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AllocationVoucher
    {
        public int AllocationVoucherId { get; set; }
        public int? AllocationVoucherType { get; set; }
        public string AllocationVoucherVoucherNo { get; set; }
        public DateTime? AllocationVoucherVoucherDate { get; set; }
        public string AllocationVoucherDescription { get; set; }
        public string AllocationVoucherVoucherAccNo { get; set; }
        public string AllocationVoucherVoucherAccType { get; set; }
        public int? AllocationVoucherVoucherFsno { get; set; }
        public string AllocationVoucherStatus { get; set; }
        public int? AllocationVoucherUserId { get; set; }
        public string AllocationVoucherAvStatus { get; set; }
        public string AllocationVoucherRefVno { get; set; }
        public int? AllocationVoucherRefVtype { get; set; }
        public int? AllocationVoucherLocationId { get; set; }
        public string AllocationVoucherVcreation { get; set; }
        public bool? AllocationVoucherDelStatus { get; set; }
        [NotMapped]
        public virtual ICollection<AllocationVoucherDetails> VoucherAllocationDetails { get; set; }
    }
}

