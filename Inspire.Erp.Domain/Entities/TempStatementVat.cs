using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempStatementVat
    {
        public int? TempStatementVatSlno { get; set; }
        public string TempStatementVatVno { get; set; }
        public DateTime? TempStatementVatVdate { get; set; }
        public string TempStatementVatVtype { get; set; }
        public string TempStatementVatDesc { get; set; }
        public double? TempStatementVatDebit { get; set; }
        public double? TempStatementVatCredit { get; set; }
        public double? TempStatementVatBalance { get; set; }
        public string TempStatementVatPartyName { get; set; }
        public string TempStatementVatVatno { get; set; }
        public string TempStatementVatCostCenter { get; set; }
        public string TempStatementVatAccNo { get; set; }
        public double? TempStatementVatVatableAmt { get; set; }
        public string TempStatementVatRemarks { get; set; }
        public string TempStatementVatDepartment { get; set; }
        public bool? TempStatementVatDelStatus { get; set; }
    }
}
