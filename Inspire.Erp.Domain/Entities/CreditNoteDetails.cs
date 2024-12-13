using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CreditNoteDetails
    {
        public long CreditNoteDetailsId { get; set; }
        public long CreditNoteId { get; set; }
        public string CreditNoteDetailsVno { get; set; }
        public string CreditNoteDetailsAccNo { get; set; }
        public double? CreditNoteDetailsVatableAmount { get; set; }
        public decimal? CreditNoteDetailsDrAmount { get; set; }
        public decimal? CreditNoteDetailsFcDrAmount { get; set; }
        public decimal? CreditNoteDetailsCrAmount { get; set; }
        public decimal? CreditNoteDetailsFcCrAmount { get; set; }
        public long? CreditNoteDetailsJobNo { get; set; }
        public int? CreditNoteDetailsCostCenterId { get; set; }
        public string CreditNoteDetailsNarration { get; set; }
        public bool? CreditNoteDetailsDelStatus { get; set; }
    }
}
