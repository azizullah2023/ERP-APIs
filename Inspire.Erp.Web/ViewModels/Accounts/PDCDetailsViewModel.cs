using System;

namespace Inspire.Erp.Web.ViewModels.Accounts
{
    public class PDCDetailsViewModel
    {
        public int? PdcDetailsId { get; set; }
        public string PdcDetailsVNo { get; set; }
        public string PdcDetailsVType { get; set; }
        public string PdcDetailsTransType { get; set; }
        public DateTime? PdcDetailsTransDate { get; set; }
        public DateTime? PdcDetailsPDCDate { get; set; }
        public string PdcDetailsChequeNo { get; set; }
        public double? PdcDetailsChequeAmount { get; set; }
        public double? PdcDetailsFcChequeAmount { get; set; }
        public string PdcDetailsChequeBankName { get; set; }
        public string PdcDetailsBankAccountNo { get; set; }
        public string PdcDetailsChequeStatus { get; set; }
        public string PdcDetailsPdcVoucherId { get; set; }
        public DateTime? PdcDetailsPdcVoucherDate { get; set; }
        public string PdcDetailsPdcVoucherNarration { get; set; }
        public int? PdcDetailsFsno { get; set; }
        public int? PdcDetailsUserId { get; set; }
        public int? PdcDetailsFlatId { get; set; }
        public int? PdcDetailsBuildingId { get; set; }
        public string PdcDetailsContract { get; set; }
        public string PdcDetailsOldChequeStatus { get; set; }
        public string PdcDetailsPartyAccNo { get; set; }
        public bool? PdcDetailsDelStatus { get; set; }
    }
}
