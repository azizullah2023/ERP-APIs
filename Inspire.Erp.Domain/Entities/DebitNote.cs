using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DebitNote
    {
        public long DebitNoteId { get; set; }
        public string DebitNoteVno { get; set; }
        public string DebitNoteRefNo { get; set; }
        public DateTime? DebitNoteDate { get; set; }
        public string DebitNoteAcNo { get; set; }
        public decimal? DebitNoteDrAmount { get; set; }
        public decimal? DebitNoteFcDrAmount { get; set; }
        public decimal? DebitNoteCrAmount { get; set; }
        public decimal? DebitNoteFcCrAmount { get; set; }
        public decimal? DebitNoteFcRate { get; set; }
        public string DebitNoteNarration { get; set; }
        public long? DebitNoteCurrencyId { get; set; }
        public decimal? DebitNoteFsno { get; set; }
        public long? DebitNoteUserId { get; set; }
        public string DebitNoteAllocId { get; set; }
        public long? DebitNoteLocationId { get; set; }
        public bool? DebitNoteDelStatus { get; set; }
        public string DebitNoteVoucherType { get; set; }
        [NotMapped]
        public List<DebitNoteDetails> DebitNoteDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
    }
}