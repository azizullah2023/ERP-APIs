////using System;
////using System.Collections.Generic;

////namespace Inspire.Erp.Web.ViewModels
////{
////    public class BankPaymentVoucherViewModel
////    {
////        public string BankPaymentVoucherVoucherNo { get; set; }
////        public DateTime? BankPaymentVoucherVDt { get; set; }
////        public string BankPaymentVoucherOptType { get; set; }
////        public double? BankPaymentVoucherCrAmount { get; set; }
////        public double? BankPaymentVoucherFrCrAmount { get; set; }
////        public double? BankPaymentVoucherDrAmount { get; set; }
////        public double? BankPaymentVoucherFrDrAmount { get; set; }
////        public string BankPaymentVoucherNarration { get; set; }
////        public string BankPaymentVoucherStatus { get; set; }
////        public int? BankPaymentVoucherUserId { get; set; }
////        public int? BankPaymentVoucherCurrencyId { get; set; }
////        public double? BankPaymentVoucherFcRate { get; set; }
////        public string BankPaymentVoucherCrAcNo { get; set; }
////        public int? BankPaymentVoucherFsno { get; set; }
////        public string BankPaymentVoucherRefNo { get; set; }
////        public int? BankPaymentVoucherAllocId { get; set; }
////        public int? BankPaymentVoucherLocationId { get; set; }
////        public bool? BankPaymentVoucherDelStatus { get; set; }

////        public List<AccountTransactionsViewModel> AccountsTransactions { get; set; }
////        public List<BankPaymentVoucherDetailsViewModel> BankPaymentVoucherDetails { get; set; }
////    }
////}
///

using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class BankPaymentVoucherViewModel
    {
        public string BankPaymentVoucherVoucherNo { get; set; }
        public DateTime? BankPaymentVoucherVDt { get; set; }
        public string BankPaymentVoucherOptType { get; set; }
        public double? BankPaymentVoucherCrAmount { get; set; }
        public double? BankPaymentVoucherFrCrAmount { get; set; }
        public double? BankPaymentVoucherDrAmount { get; set; }
        public double? BankPaymentVoucherFrDrAmount { get; set; }
        public string BankPaymentVoucherNarration { get; set; }
        public string BankPaymentVoucherStatus { get; set; }
        public int? BankPaymentVoucherUserId { get; set; }
        public int? BankPaymentVoucherCurrencyId { get; set; }
        public double? BankPaymentVoucherFcRate { get; set; }
        public string BankPaymentVoucherCrAcNo { get; set; }
        public int? BankPaymentVoucherFsno { get; set; }
        public string BankPaymentVoucherRefNo { get; set; }
        public int? BankPaymentVoucherAllocId { get; set; }
        public int? BankPaymentVoucherLocationId { get; set; }
        public bool? BankPaymentVoucherDelStatus { get; set; }

        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<BankPaymentVoucherDetailsViewModel> BankPaymentVoucherDetails { get; set; }
    }
}

