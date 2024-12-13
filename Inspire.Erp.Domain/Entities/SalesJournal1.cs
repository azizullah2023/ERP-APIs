﻿using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesJournal1
    {
        public SalesJournal1()
        {
            SalesJournalDetails1 = new HashSet<SalesJournalDetails1>();
        }

        public long SalesJournalId { get; set; }
        public string SalesJournalVno { get; set; }
        public string SalesJournalRefNo { get; set; }
        public DateTime? SalesJournalDate { get; set; }
        public decimal? SalesJournalDrAmount { get; set; }
        public decimal? SalesJournalFcDrAmount { get; set; }
        public decimal? SalesJournalCrAmount { get; set; }
        public decimal? SalesJournalFcCrAmount { get; set; }
        public decimal? SalesJournalFcRate { get; set; }
        public string SalesJournalNarration { get; set; }
        public long? SalesJournalCurrencyId { get; set; }
        public decimal? SalesJournalFsno { get; set; }
        public long? SalesJournalUserId { get; set; }
        public string SalesJournalAllocId { get; set; }
        public long? SalesJournalLocationId { get; set; }
        public bool? SalesJournalDelStatus { get; set; }
        public string SalesJournalVoucherType { get; set; }

        public virtual ICollection<SalesJournalDetails1> SalesJournalDetails1 { get; set; }
    }
}
