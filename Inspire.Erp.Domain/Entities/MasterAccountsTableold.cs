using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class MasterAccountsTableold
    {
        public int MasterAccountsTableSno { get; set; }
        public string MasterAccountsTableRelativeNo { get; set; }
        public string MasterAccountsTableAccNo { get; set; }
        public string MasterAccountsTableAccName { get; set; }
        public string MasterAccountsTableAccType { get; set; }
        public string MasterAccountsTableMainHead { get; set; }
        public string MasterAccountsTableSubHead { get; set; }
        public string MasterAccountsTableImageKey { get; set; }
        public string MasterAccountsTableSystemAcc { get; set; }
        public int? MasterAccountsTableFsno { get; set; }
        public string MasterAccountsTableStatus { get; set; }
        public int? MasterAccountsTableUserId { get; set; }
        public DateTime? MasterAccountsTableDateCreated { get; set; }
        public int? MasterAccountsTableCurrencyId { get; set; }
        public string MasterAccountsTableGpAcc { get; set; }
        public string MasterAccountsTableAcAcc { get; set; }
        public string MasterAccountsTableEdAcc { get; set; }
        public double? MasterAccountsTableOpenBalance { get; set; }
        public double? MasterAccountsTableTotalDebit { get; set; }
        public double? MasterAccountsTableTotalCredit { get; set; }
        public string MasterAccountsTableManualCode { get; set; }
        public bool? MasterAccountsTableIsAirAcc { get; set; }
        public bool? MasterAccountsTableIsSeaAcc { get; set; }
        public int? MasterAccountsTableCostCenterId { get; set; }
        public bool? MasterAccountsTableShowSuminTb { get; set; }
        public int? MasterAccountsTableCostCenterSub { get; set; }
        public int? MasterAccountsTableSortNo { get; set; }
        public double? MasterAccountsTableAssetValue { get; set; }
        public double? MasterAccountsTableAssetDepValue { get; set; }
        public double? MasterAccountsTableAssetQty { get; set; }
        public double? MasterAccountsTableLifeInYrs { get; set; }
        public string MasterAccountsTableAssetDepMode { get; set; }
        public DateTime? MasterAccountsTableAssetDate { get; set; }
        public bool? MasterAccountsTableIsAsset { get; set; }
        public bool? MasterAccountsTableDelStatus { get; set; }
    }
}
