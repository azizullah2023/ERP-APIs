using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DebitNoteDetails
    {
        public long DebitNoteDetailsId { get; set; }
        public long DebitNoteId { get; set; }
        public string DebitNoteDetailsVno { get; set; }
        public string DebitNoteDetailsAccNo { get; set; }
        public double? DebitNoteDetailsVatableAmount { get; set; }
        public decimal? DebitNoteDetailsDrAmount { get; set; }
        public decimal? DebitNoteDetailsFcDrAmount { get; set; }
        public decimal? DebitNoteDetailsCrAmount { get; set; }
        public decimal? DebitNoteDetailsFcCrAmount { get; set; }
        public long? DebitNoteDetailsJobNo { get; set; }
        public int? DebitNoteDetailsCostCenterId { get; set; }
        public string DebitNoteDetailsNarration { get; set; }
        public bool? DebitNoteDetailsDelStatus { get; set; }
    }
}
