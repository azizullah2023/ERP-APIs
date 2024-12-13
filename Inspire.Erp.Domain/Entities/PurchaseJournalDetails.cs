using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseJournalDetails
    {
        public long PurchaseJournalDetailsId { get; set; }
        public long? PurchaseJournalId { get; set; }
        public string PurchaseJournalDetailsVno { get; set; }
        public double? PurchaseJournalDetailsVatableAmount { get; set; }
        public decimal? PurchaseJournalDetailsDrAmount { get; set; }
        public decimal? PurchaseJournalDetailsFcDrAmount { get; set; }
        public decimal? PurchaseJournalDetailsCrAmount { get; set; }
        public decimal? PurchaseJournalDetailsFcCrAmount { get; set; }
        public long? PurchaseJournalDetailsJobNo { get; set; }
        public int? PurchaseJournalDetailsCostCenterId { get; set; }
        public string PurchaseJournalDetailsNarration { get; set; }
        public string PurchaseJournalDetailsVatNo { get; set; }
        public string PurchaseJournalDetailsReferNo { get; set; }
        public string PurchaseJournalDetailsAccNo { get; set; }
        public bool? PurchaseJournalDetailsDelStatus { get; set; }

        [JsonIgnore]
        public virtual PurchaseJournal PurchaseJournal { get; set; }
    }
}
