using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AllocationVoucherDetails
    {
        public int AllocationVoucherDetailsSno { get; set; }
        public int? AllocationVoucherDetailsId { get; set; }
        public string AllocationVoucherDetailsVno { get; set; }
        public int? AllocationVoucherDetailsVtype { get; set; }
        public int? AllocationVoucherDetailsVFsno { get; set; }
        public int? AllocationVoucherDetailsVLocationId { get; set; }
        public int? AllocationVoucherDetailsTransSno { get; set; }
        public string AllocationVoucherDetailsAccNo { get; set; }
        public double? AllocationVoucherDetailsAllocDebit { get; set; }
        public double? AllocationVoucherDetailsAllocCredit { get; set; }
        public double? AllocationVoucherDetailsFcAllocDebit { get; set; }
        public double? AllocationVoucherDetailsFcAllocCredit { get; set; }
        public string AllocationVoucherDetailsRefVno { get; set; }
        public string AllocationVoucherDetailsRefVtype { get; set; }
        public int? AllocationVoucherDetailsRefLocationId { get; set; }
        public int? AllocationVoucherDetailsRefFsno { get; set; }
        public bool? AllocationVoucherDetailsDelStatus { get; set; }
        public string RefSPAccountNo { get; set; }
        public DateTime? RefSPDate { get; set; }    
        [NotMapped]
        public double AllocationVoucherDetailsNetAllocation { get; set; }
        [NotMapped]
        public double? AccountsTransactionsAllocBalance { get; set; }
        [NotMapped]
        public long AccountsTransactionsTransSno { get; set; }
        [NotMapped]
        public virtual AllocationVoucher AllocationVoucher { get; set; }
    }
}

