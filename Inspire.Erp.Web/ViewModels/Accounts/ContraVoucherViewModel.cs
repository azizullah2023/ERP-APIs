using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public partial class ContraVoucherViewModel
    {
        public int? ContraVoucherId { get; set; }
        public string ContraVoucherVno { get; set; }
        public string ContraVoucherRefNo { get; set; }
        public DateTime? ContraVoucherDate { get; set; }
        public string ContraVoucherAcNo { get; set; }
        public decimal? ContraVoucherDrAmount { get; set; }
        public decimal? ContraVoucherFcDrAmount { get; set; }
        public decimal? ContraVoucherCrAmount { get; set; }
        public decimal? ContraVoucherFcCrAmount { get; set; }
        public decimal? ContraVoucherFcRate { get; set; }
        public string ContraVoucherNarration { get; set; }
        public long? ContraVoucherCurrencyId { get; set; }
        public decimal? ContraVoucherFsno { get; set; }
        public long? ContraVoucherUserId { get; set; }
        public string ContraVoucherAllocId { get; set; }
        public long? ContraVoucherLocationId { get; set; }
        public bool? ContraVoucherDelStatus { get; set; }
        public string ContraVoucherVoucherType { get; set; }


        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<ContraVoucherDetailsViewModel> ContraVoucherDetails { get; set; }
    }
}
