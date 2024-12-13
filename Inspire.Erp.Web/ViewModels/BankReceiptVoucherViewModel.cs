using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class BankReceiptVoucherViewModel
    {
       	public string BankReceiptVoucherVoucherNo { get; set; }
        public DateTime? BankReceiptVoucherVDate { get; set; }
        public string BankReceiptVoucherDrAcNo { get; set; }
        public double? BankReceiptVoucherDrAmount { get; set; }
        public double? BankReceiptVoucherFrDrAmount { get; set; }
        public double? BankReceiptVoucherCrAmount { get; set; }
        public double? BankReceiptVoucherFrCrAmount { get; set; }
        public string BankReceiptVoucherNarration { get; set; }
        public string BankReceiptVoucherStatus { get; set; }
        public string BankReceiptVoucherRefNo { get; set; }
        public int? BankReceiptVoucherUserId { get; set; }
        public int? BankReceiptVoucherCurrencyId { get; set; }
        public double? BankReceiptVoucherFcRate { get; set; }
        public int? BankReceiptVoucherFsno { get; set; }
        public int? BankReceiptVoucherAllocId { get; set; }
        public bool? BankReceiptVoucherDelStatus { get; set; }
        public int? BankReceiptVoucherLocationId { get; set; }

        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<BankReceiptVoucherDetailsViewModel> BankReceiptVoucherDetails { get; set; }
    }
}
