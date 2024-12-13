using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
  public  class AddOldBalanceSheetResponse
    {
        public int? OldBsMasterId { get; set; }
        public string OldBsMasterDescription { get; set; }
        public string OldBsMasterPost { get; set; }
        public int? OldBsMasterFsno { get; set; }
        public string OldBsMasterRefJv { get; set; }
        public bool? OldBsMasterDelStatus { get; set; }
        public bool IsBsExisting { get; set; } = false;
        public double? TotalAmount { get; set; }
        public DateTime? FinancialPeriodsStartDate { get; set; }
        public string JvNO { get; set; }
        public List<AddOldBalanceSheetDetailResponse> OldBsMasterDetails { get; set; }
    }
    public class AddOldBalanceSheetDetailResponse {
        public int? OldBsMasterDetailsId { get; set; }
        public int? OldBsMasterDetailsSno { get; set; }
        public string OldBsMasterDetailsAccNo { get; set; }
        public string OldBsMasterDetailsAccName { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public double? OldBsMasterDetailsCrate { get; set; }
        public double? OldBsMasterDetailsBcAmount { get; set; }
        public double? OldBsMasterDetailsFcAmount { get; set; }
        public string OldBsMasterDetailsNarration { get; set; }
        public string OldBsMasterDetailsGroupName { get; set; }
        public string OldBsMasterDetailsDrCrType { get; set; }
        public int? OldBsMasterDetailsFsno { get; set; }
        public bool? OldBsMasterDetailsDelStatus { get; set; }
    }

}
