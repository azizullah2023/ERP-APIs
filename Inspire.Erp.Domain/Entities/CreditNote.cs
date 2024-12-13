using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Domain.Entities
{
    public class CreditNote
    {
        public long CreditNoteId { get; set; }
        public string CreditNoteVno { get; set; }
        public string CreditNoteRefNo { get; set; }
        public DateTime? CreditNoteDate { get; set; }
        public string CreditNoteAcNo { get; set; }
        public decimal? CreditNoteDrAmount { get; set; }
        public decimal? CreditNoteFcDrAmount { get; set; }
        public decimal? CreditNoteCrAmount { get; set; }
        public decimal? CreditNoteFcCrAmount { get; set; }
        public decimal? CreditNoteFcRate { get; set; }
        public string CreditNoteNarration { get; set; }
        public long? CreditNoteCurrencyId { get; set; }
        public decimal? CreditNoteFsno { get; set; }
        public long? CreditNoteUserId { get; set; }
        public string CreditNoteAllocId { get; set; }
        public long? CreditNoteLocationId { get; set; }
        public bool? CreditNoteDelStatus { get; set; }
        public string CreditNoteVoucherType { get; set; }
        [NotMapped]
        public List<CreditNoteDetails> CreditNoteDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
    }
}