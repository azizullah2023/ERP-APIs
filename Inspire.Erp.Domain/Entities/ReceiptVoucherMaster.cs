using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ReceiptVoucherMaster
    {
        public int ReceiptVoucherMasterSno { get; set; }
        public string ReceiptVoucherMasterVoucherNo { get; set; }
        public DateTime? ReceiptVoucherMasterVoucherDate { get; set; }
        public string ReceiptVoucherMasterVoucherType { get; set; }
        public string ReceiptVoucherMasterDrAcNo { get; set; }
        public double? ReceiptVoucherMasterDrAmount { get; set; }
        public double? ReceiptVoucherMasterFcDrAmount { get; set; }
        public double? ReceiptVoucherMasterCrAmount { get; set; }
        public double? ReceiptVoucherMasterFcCrAmount { get; set; }
        public string ReceiptVoucherMasterNarration { get; set; }
        public int? ReceiptVoucherMasterFsno { get; set; }
        public int? ReceiptVoucherMasterUserId { get; set; }
        public string ReceiptVoucherMasterRefNo { get; set; }
        public int? ReceiptVoucherMasterAllocId { get; set; }
        public int? ReceiptVoucherMasterCurrencyId { get; set; }
        public bool? ReceiptVoucherMasterDelStatus { get; set; }
        [NotMapped]
        public List<ReceiptVoucherDetails> ReceiptVoucherDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
        [NotMapped]
        public List<Domain.Models.TrackingData> TrackingData { get; set; }
        [NotMapped]
        public List<Domain.Models.AllocationDetails> rvAllocationDetails { get; set; }
    }
}
