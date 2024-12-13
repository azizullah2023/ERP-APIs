using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvCreditNote
    {
        public int? AvCreditNoteSno { get; set; }
        public int? AvCreditNoteId { get; set; }
        public string AvCreditNoteRefNo { get; set; }
        public DateTime? AvCreditNoteDate { get; set; }
        public string AvCreditNoteCrAcNo { get; set; }
        public double? AvCreditNoteCrAmount { get; set; }
        public double? AvCreditNoteFcCrAmount { get; set; }
        public double? AvCreditNoteFcRate { get; set; }
        public string AvCreditNoteNarration { get; set; }
        public string AvCreditNoteStatus { get; set; }
        public int? AvCreditNoteFsno { get; set; }
        public int? AvCreditNoteUserId { get; set; }
        public int? AvCreditNoteAllocId { get; set; }
        public bool? AvCreditNoteDelStatus { get; set; }
    }
}
