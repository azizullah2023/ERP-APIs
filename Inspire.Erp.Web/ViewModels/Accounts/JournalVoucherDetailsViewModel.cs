using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public partial class JournalVoucherDetailsViewModel
    {
        public int? JournalVoucherDetailsId { get; set; }
        public int? JournalVoucherId { get; set; }
        public string JournalVoucherDetailsVno { get; set; }
        public string JournalVoucherDetailsAccNo { get; set; }
        public double? JournalVoucherDetailsVatableAmount { get; set; }
        public decimal? JournalVoucherDetailsDrAmount { get; set; }
        public decimal? JournalVoucherDetailsFcDrAmount { get; set; }
        public decimal? JournalVoucherDetailsCrAmount { get; set; }
        public decimal? JournalVoucherDetailsFcCrAmount { get; set; }
        public long? JournalVoucherDetailsJobNo { get; set; }
        public int? JournalVoucherDetailsCostCenterId { get; set; }
        public string JournalVoucherDetailsNarration { get; set; }
        public bool? JournalVoucherDetailsDelStatus { get; set; }
        public string JournalVoucherDetailsReferenceNo { get; set; }
    }
}
