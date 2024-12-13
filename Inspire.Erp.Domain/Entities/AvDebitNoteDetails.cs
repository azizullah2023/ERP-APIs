using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class AvDebitNoteDetails
    {
        public int? AvDebitNoteDetailsId { get; set; }
        public int? AvDebitNoteDetailsAccSno { get; set; }
        public string AvDebitNoteDetailsAccNo { get; set; }
        public double? AvDebitNoteDetailsAmount { get; set; }
        public double? AvDebitNoteDetailsFcAmount { get; set; }
        public int? AvDebitNoteDetailsJobNo { get; set; }
        public string AvDebitNoteDetailsNarration { get; set; }
        public int? AvDebitNoteDetailsCostCenterId { get; set; }
        public bool? AvDebitNoteDelStatus { get; set; }
    }
}
