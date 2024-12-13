using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankReceiptVoucherDetails
    {
        public int BankReceiptVoucherDetailsId { get; set; }
        public string BankReceiptVoucherDetailsVoucherNo { get; set; }
        public int? BankReceiptVoucherDetailsSno { get; set; }
        public string BankReceiptVoucherDetailsAcNo { get; set; }
        public double? BankReceiptVoucherDetailsDrAmount { get; set; }
        public double? BankReceiptVoucherDetailsDrFcAmount { get; set; }
        public double? BankReceiptVoucherDetailsCrAmount { get; set; }
        public double? BankReceiptVoucherDetailsCrFcAmount { get; set; }
        public int? BankReceiptVoucherDetailsChequeNo { get; set; }
        public DateTime? BankReceiptVoucherDetailsChequeDate { get; set; }
        public string BankReceiptVoucherDetailsNarration { get; set; }
        public int? BankReceiptVoucherDetailsFsno { get; set; }
        public int? BankReceiptVoucherDetailsJobNo { get; set; }
        public int? BankReceiptVoucherDetailsCostCenterId { get; set; }
        public bool? BankReceiptVoucherDetailsPdc { get; set; }
        public bool? BankReceiptVoucherDetailsDelStatus { get; set; }
        public int? BankReceiptVoucherDetailsPdcId { get; set; }

        [JsonIgnore]
        public virtual VouchersNumbers BankReceiptVoucherDetailsVoucherNoNavigation { get; set; }
    }
}
