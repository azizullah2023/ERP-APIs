using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class SalesJournalDetailsViewModel
    {
        public long SalesJournalDetailsId { get; set; }
        public long? SalesJournalId { get; set; }
        public string SalesJournalDetailsVno { get; set; }
        public double? SalesJournalDetailsVatableAmount { get; set; }
        public decimal? SalesJournalDetailsDrAmount { get; set; }
        public decimal? SalesJournalDetailsFcDrAmount { get; set; }
        public decimal? SalesJournalDetailsCrAmount { get; set; }
        public decimal? SalesJournalDetailsFcCrAmount { get; set; }
        public int? SalesJournalDetailsJobNo { get; set; }
        public int? SalesJournalDetailsCostCenterId { get; set; }
        public string SalesJournalDetailsNarration { get; set; }
        public string SalesJournalDetailsVatNo { get; set; }
        public string SalesJournalDetailsReferNo { get; set; }
        public string SalesJournalDetailsAccNo { get; set; }
        public bool? SalesJournalDetailsDelStatus { get; set; }
    }
}
