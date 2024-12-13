using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesReturnMaster
    {
        public string SalesReturnMasterSrNo { get; set; }
        public int? SalesReturnMasterSrNoNum { get; set; }
        public DateTime? SalesReturnMasterDate { get; set; }
        public string SalesReturnMasterReturnType { get; set; }
        public string SalesReturnMasterRefSvNo { get; set; }
        public DateTime? SalesReturnMasterRefSvDate { get; set; }
        public string SalesReturnMasterSvVoucherType { get; set; }
        public int? SalesReturnMasterCustNo { get; set; }
        public string SalesReturnMasterCustName { get; set; }
        public int? SalesReturnMasterLocationId { get; set; }
        public double? SalesReturnMasterGrossAmount { get; set; }
        public double? SalesReturnMasterDiscount { get; set; }
        public double? SalesReturnMasterNetAmount { get; set; }
        public double? SalesReturnMasterFcNetAmount { get; set; }
        public string SalesReturnMasterNarration { get; set; }
        public string SalesReturnMasterStatus { get; set; }
        public int? SalesReturnMasterFsno { get; set; }
        public int? SalesReturnMasterUserId { get; set; }
        public double? SalesReturnMasterFcRate { get; set; }
        public int? SalesReturnMasterCurrencyId { get; set; }
        public int? SalesReturnMasterWorkPeriodId { get; set; }
        public double? SalesReturnMasterVatAmt { get; set; }
        public double? SalesReturnMasterVatPercentage { get; set; }
        public string SalesReturnMasterVatRoungSign { get; set; }
        public double? SalesReturnMasterVatRoundAmt { get; set; }
        public int? SalesReturnMasterSalesManId { get; set; }
        public bool? SalesReturnMasterDelStatus { get; set; }
    }
}
