using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
   public class GetVoucherAllocationListResponse
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
        public List<GetAccountTransactionMutilAccountResponse> AccountsTransactions { get; set; }

    }
}


