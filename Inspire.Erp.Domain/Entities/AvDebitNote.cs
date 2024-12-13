using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvDebitNote
    {
        public int? AvDebitNoteSno { get; set; }
        public int? AvDebitNoteId { get; set; }
        public string AvDebitNoteRefNo { get; set; }
        public DateTime? AvDebitNoteDate { get; set; }
        public string AvDebitNoteDrAcNo { get; set; }
        public double? AvDebitNoteDrAmount { get; set; }
        public double? AvDebitNoteFcDrAmount { get; set; }
        public double? AvDebitNoteFcRate { get; set; }
        public string AvDebitNoteNarration { get; set; }
        public string AvDebitNoteStatus { get; set; }
        public int? AvDebitNoteFsno { get; set; }
        public int? AvDebitNoteUserId { get; set; }
        public int? AvDebitNoteAllocId { get; set; }
        public bool? AvDebitNoteDelStatus { get; set; }
    }
}
