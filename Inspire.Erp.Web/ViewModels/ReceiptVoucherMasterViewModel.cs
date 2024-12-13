using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ReceiptVoucherMasterViewModel
    {
        public int? ReceiptVoucherMasterSno { get; set; }
        public string ReceiptVoucherMasterVoucherNo { get; set; }
        public DateTime? ReceiptVoucherMasterVoucherDate { get; set; }
        public string ReceiptVoucherMasterVoucherType { get; set; }
        public string ReceiptVoucherMasterDrAcNo { get; set; }
        public double? ReceiptVoucherMasterDrAmount { get; set; }
        public double? ReceiptVoucherMasterFcDrAmount { get; set; }
        public double? ReceiptVoucherMasterCrAmount { get; set; }
        public double? ReceiptVoucherMasterFcCrAmount { get; set; }
        public string ReceiptVoucherMasterNarration { get; set; }
        public int? ReceiptVoucherMasterFsno { get; set; }
        public int? ReceiptVoucherMasterUserId { get; set; }
        public string ReceiptVoucherMasterRefNo { get; set; }
        public int? ReceiptVoucherMasterAllocId { get; set; }
        public int? ReceiptVoucherMasterCurrencyId { get; set; }
        public bool? ReceiptVoucherMasterDelStatus { get; set; }

        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<ReceiptVoucherDetailsViewModel> ReceiptVoucherDetails { get; set; }
    }
}
