using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GlobalAccountsTable
    {
        public int? GlobalAccountsTableSno { get; set; }
        public string GlobalAccountsTableRelativeNo { get; set; }
        public string GlobalAccountsTableAccNo { get; set; }
        public string GlobalAccountsTableAccName { get; set; }
        public string GlobalAccountsTableAccType { get; set; }
        public string GlobalAccountsTableMainHead { get; set; }
        public string GlobalAccountsTableSubHead { get; set; }
        public string GlobalAccountsTableImageKey { get; set; }
        public string GlobalAccountsTableSystemAcc { get; set; }
        public int? GlobalAccountsTableFsno { get; set; }
        public string GlobalAccountsTableStatus { get; set; }
        public int? GlobalAccountsTableUserId { get; set; }
        public DateTime? GlobalAccountsTableDtCreate { get; set; }
        public int? GlobalAccountsTableCurrencyId { get; set; }
        public string GlobalAccountsTableGpAcc { get; set; }
        public string GlobalAccountsTableAcAcc { get; set; }
        public string GlobalAccountsTableEdAcc { get; set; }
        public double? GlobalAccountsTableTotalDebit { get; set; }
        public double? GlobalAccountsTableTotalCredit { get; set; }
        public double? GlobalAccountsTableOpbal { get; set; }
        public bool? GlobalAccountsTableDelStatus { get; set; }
    }
}
