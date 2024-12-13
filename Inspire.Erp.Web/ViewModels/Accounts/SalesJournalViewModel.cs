using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class SalesJournalViewModel
    {
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
        public int? SalesJournalCurrencyId { get; set; }
        public decimal? SalesJournalFsno { get; set; }
        public int? SalesJournalUserId { get; set; }
        public string SalesJournalAllocId { get; set; }
        public int? SalesJournalLocationId { get; set; }
        public bool? SalesJournalDelStatus { get; set; }
        public string SalesJournalVoucherType { get; set; }
        public  List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public  List<SalesJournalDetailsViewModel> SalesJournalDetails { get; set; }
    }
}
