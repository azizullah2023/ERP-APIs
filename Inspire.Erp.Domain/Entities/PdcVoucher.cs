using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Inspire.Erp.Domain.Modals.PDCResponse;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PdcVoucher
    {
        public int PdcVoucherSno { get; set; }
        public string PdcVoucherVid { get; set; }
        public string PdcVoucherRef { get; set; }
        public DateTime? PdcVoucherPdcDate { get; set; }
        public double? PdcVoucherPdcAmount { get; set; }
        public double? PdcVoucherFcPdcAmount { get; set; }
        public string PdcVoucherNarration { get; set; }
        public int? PdcVoucherFsno { get; set; }
        public int? PdcVoucherUserId { get; set; }
        public bool? PdcVoucherDelStatus { get; set; }

        [NotMapped]
        public List<PDCGetList> PDCGetLists { get; set; }
        [NotMapped]
        public List<PdcDetails> PdcDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountTransaction { get; set; }
    }
}
