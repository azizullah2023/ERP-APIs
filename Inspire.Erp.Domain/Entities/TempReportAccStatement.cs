using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TempReportAccStatement
    {
        public int AccSlNo { get; set; }
        public string AccAccNo { get; set; }
        public string AccName { get; set; }
        public string AccVchNo { get; set; }
        public DateTime? AccVchDate { get; set; }
        public string AccVchType { get; set; }
        public string AccDesc { get; set; }
        public decimal? AccDebit { get; set; }
        public decimal? AccCredit { get; set; }
        public decimal? AccTotDebit { get; set; }
        public decimal? AccTotCredit { get; set; }
        public decimal? AccRunning { get; set; }
        public decimal? AccDateDiff { get; set; }
        public string AccRefNo { get; set; }
        public string AccCostcenter { get; set; }
        public string AccVchAgainst { get; set; }
        public string AccLocation { get; set; }
        public string AccJob { get; set; }
        public string AccDepartment { get; set; }
        public string AccLpoNo { get; set; }
        public string AccChqNo { get; set; }
        public DateTime? AccChqDate { get; set; }
        public string AccVatNo { get; set; }
        public decimal? AccVatAmt { get; set; }
        public long? AccLocationId { get; set; }
        public long? AccJobId { get; set; }
        public long? AccCostcenterId { get; set; }
        public long? AccDepartmentId { get; set; }
    }
}
