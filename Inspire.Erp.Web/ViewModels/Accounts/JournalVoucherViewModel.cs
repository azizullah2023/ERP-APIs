using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public partial class JournalVoucherViewModel
    {
        public int? JournalVoucherId { get; set; }
        public string JournalVoucherVno { get; set; }
        public string JournalVoucherRefNo { get; set; }
        public DateTime? JournalVoucherDate { get; set; }
        public string JournalVoucherAcNo { get; set; }
        public decimal? JournalVoucherDrAmount { get; set; }
        public decimal? JournalVoucherFcDrAmount { get; set; }
        public decimal? JournalVoucherCrAmount { get; set; }
        public decimal? JournalVoucherFcCrAmount { get; set; }
        public decimal? JournalVoucherFcRate { get; set; }
        public string JournalVoucherNarration { get; set; }
        public long? JournalVoucherCurrencyId { get; set; }
        public decimal? JournalVoucherFsno { get; set; }
        public long? JournalVoucherUserId { get; set; }
        public string JournalVoucherAllocId { get; set; }
        public long? JournalVoucherLocationId { get; set; }
        public bool? JournalVoucherDelStatus { get; set; }
        public string JournalVoucherVoucherType { get; set; }



        public List<AccountTransactionsViewModel> AccountsTransactions { get; set; }
        public List<JournalVoucherDetailsViewModel> JournalVoucherDetails { get; set; }
    }
}
