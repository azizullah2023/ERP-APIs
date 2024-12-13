using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvCreditNoteDetails
    {
        public int? AvCreditNoteDetailsId { get; set; }
        public int? AvCreditNoteDetailsAccSno { get; set; }
        public string AvCreditNoteDetailsAccNo { get; set; }
        public double? AvCreditNoteDetailsFcAmount { get; set; }
        public int? AvCreditNoteDetailsJobNo { get; set; }
        public string AvCreditNoteDetailsNarration { get; set; }
        public int? AvCreditNoteDetailsCostCenterId { get; set; }
        public bool? AvCreditNoteDetailsDelStatus { get; set; }
    }
}
