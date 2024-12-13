using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
   public class AddEditVoucherAllocationResponse
    {
        public int? AllocationVoucherId { get; set; }
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
        public List<VoucherAllocationDetailsResponse> VoucherAllocationDetails { get; set; }
    }
    public class VoucherAllocationDetailsResponse
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
    }
}
