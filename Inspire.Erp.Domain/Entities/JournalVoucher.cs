using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JournalVoucher
    {
        ////public int JournalVoucherId { get; set; }
        ////public string JournalVoucherVno { get; set; }
        ////public string JournalVoucherRefNo { get; set; }
        ////public DateTime? JournalVoucherDate { get; set; }
        ////public string JournalVoucherAcNo { get; set; }
        ////public decimal? JournalVoucherDrAmount { get; set; }
        ////public decimal? JournalVoucherFcDrAmount { get; set; }
        ////public decimal? JournalVoucherCrAmount { get; set; }
        ////public decimal? JournalVoucherFcCrAmount { get; set; }
        ////public decimal? JournalVoucherFcRate { get; set; }
        ////public string JournalVoucherNarration { get; set; }
        ////public long? JournalVoucherCurrencyId { get; set; }
        ////public decimal? JournalVoucherFsno { get; set; }
        ////public long? JournalVoucherUserId { get; set; }
        ////public string JournalVoucherAllocId { get; set; }
        ////public long? JournalVoucherLocationId { get; set; }
        ////public bool? JournalVoucherDelStatus { get; set; }
        ////public string JournalVoucherStatus { get; set; }
        ////public string JournalVoucherVoucherType { get; set; }
        ///
        //public long JournalVoucher_ID { get; set; }//
        //public string? JournalVoucher_VNO { get; set; }//
        //public string? JournalVoucher_Ac_No { get; set; }//
        //public string? JournalVoucher_Ref_No { get; set; }//
        //public DateTime? JournalVoucher_Date { get; set; }//
        //public decimal? JournalVoucher_Dr_Amount { get; set; }//
        //public decimal? JournalVoucher_Cr_Amount { get; set; }//
        //public decimal? JournalVoucher_FC_Cr_Amount { get; set; }//
        //public decimal? JournalVoucher_FC_Rate { get; set; }//
        //public decimal? JournalVoucher_FC_Dr_Amount { get; set; }//
        //public string? JournalVoucher_Narration { get; set; }//
        //public string? JournalVoucher_AllocID { get; set; }
        //public decimal? JournalVoucher_FSNO { get; set; }//
        //public string? JournalVoucher_VoucherType { get; set; }
        //public long? JournalVoucher_LocationId { get; set; }
        //public long? JournalVoucher_CurrencyId { get; set; }//
        //public bool? JournalVoucher_DelStatus { get; set; }
        //public long? JournalVoucher_User_ID { get; set; }
        [NotMapped]
        public List<JournalVoucherDetails> JournalVoucherDetails { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
        public int JournalVoucherSno { get; set; }
        public int JournalVoucherId { get; set; }
        public string JournalVoucherVrefNo { get; set; }
        public DateTime? JournalVoucherDate { get; set; }
        public double? JournalVoucherAmount { get; set; }
        public decimal? JournalVoucherDrAmount { get; set; }
        public decimal? JournalVoucherFcDrAmount { get; set; }
        public decimal? JournalVoucherCrAmount { get; set; }
        public decimal? JournalVoucherFcCrAmount { get; set; }
        public string JournalVoucherNarration { get; set; }
        public int? JournalVoucherFsno { get; set; }
        public string JournalVoucherStatus { get; set; }
        public int? JournalVoucherUserId { get; set; }
        public string JournalVoucherType { get; set; }
        public int? JournalVoucherLocationId { get; set; }
        public string JournalVoucherRefNo { get; set; }
        public int? JournalVoucherJobNo { get; set; }
        public int? JournalVoucherCostCenterId { get; set; }
        public int? JournalVoucherCurrencyId { get; set; }
        public DateTime? JournalVoucherDepreciationFrom { get; set; }
        public DateTime? JournalVoucherDepreciationTo { get; set; }
        public bool? JournalVoucherDelStatus { get; set; }
    }
}
