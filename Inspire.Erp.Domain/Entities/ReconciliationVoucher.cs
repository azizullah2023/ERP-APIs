using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
   
    public partial class ReconciliationVoucher
    {
        public int ReconciliationVoucherSno { get; set; }
        public string ReconciliationVoucherId { get; set; }
        public string ReconciliationVoucherNarration { get; set; }
        public string ReconciliationVoucherBankAcNo { get; set; }
        public double? ReconciliationVoucherClosingBalance { get; set; }
        public double? ReconciliationVoucherBankBalance { get; set; }
        public double? ReconciliationVoucherUnClearedTotal { get; set; }
        public DateTime? ReconciliationVoucherBankStDate { get; set; }
        public bool? ReconciliationVoucherComplete { get; set; }
        public int? ReconciliationVoucherFsno { get; set; }
        public int? ReconciliationVoucherUserId { get; set; }
        public int? ReconciliationVoucherLocationId { get; set; }
        public bool? ReconciliationVoucherDelStatus { get; set; }
        [NotMapped]
        public List<ReconciliationVoucherDetails> ReconciliationVoucherDetails { get; set; }
    }
}
