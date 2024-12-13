using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PurchaseJournal
    {
        //public PurchaseJournal()
        //{
        //    PurchaseJournalDetails = new HashSet<PurchaseJournalDetails>();
        //}

        public long PurchaseJournalId { get; set; }
        public string PurchaseJournalVno { get; set; }
        public string PurchaseJournalRefNo { get; set; }
        public DateTime? PurchaseJournalDate { get; set; }
        public decimal? PurchaseJournalDrAmount { get; set; }
        public decimal? PurchaseJournalFcDrAmount { get; set; }
        public decimal? PurchaseJournalCrAmount { get; set; }
        public decimal? PurchaseJournalFcCrAmount { get; set; }
        public decimal? PurchaseJournalFcRate { get; set; }
        public string PurchaseJournalNarration { get; set; }
        public long? PurchaseJournalCurrencyId { get; set; }
        public decimal? PurchaseJournalFsno { get; set; }
        public long? PurchaseJournalUserId { get; set; }
        public string PurchaseJournalAllocId { get; set; }
        public long? PurchaseJournalLocationId { get; set; }
        public bool? PurchaseJournalDelStatus { get; set; }
        public string PurchaseJournalVoucherType { get; set; }

        [NotMapped]
        public List<PurchaseJournalDetails> PurchaseJournalDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }

    }
}
