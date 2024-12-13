using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public class PaymentVoucherViewModel
    {
        public int? PaymentVoucherSno { get; set; }
        public string PaymentVoucherVoucherNo { get; set; }
        public DateTime? PaymentVoucherDate { get; set; }
        public string PaymentVoucherVoucherRef { get; set; }
        public string PaymentVoucherCrAcNo { get; set; }
        public double? PaymentVoucherCrAmount { get; set; }
        public double? PaymentVoucherFcCrAmount { get; set; }
        public double? PaymentVoucherDbAmount { get; set; }
        public double? PaymentVoucherFcDbAmount { get; set; }
        public string PaymentVoucherNarration { get; set; }
        public int? PaymentVoucherFsno { get; set; }
        public int? PaymentVoucherUserId { get; set; }
        public int? PaymentVoucherCurrencyId { get; set; }
        public string PaymentVoucherCurrencyName { get; set; }
        public int? PaymentVoucherLocationId { get; set; }
        public int? PaymentVoucherAllocationId { get; set; }
        public bool? PaymentVoucherDelStatus { get; set; }
        public  List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public  List<PaymentVoucherDetailsViewModel> PaymentVoucherDetails { get; set; }
    }
}
