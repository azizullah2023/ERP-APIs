using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class MasterAccountsTableDelete
    {
        public int? MasterAccountsTableDeleteSno { get; set; }
        public string MasterAccountsTableDeleteRelativeNo { get; set; }
        public string MasterAccountsTableDeleteAccNo { get; set; }
        public string MasterAccountsTableDeleteAccName { get; set; }
        public string MasterAccountsTableDeleteAccType { get; set; }
        public string MasterAccountsTableDeleteMainHead { get; set; }
        public string MasterAccountsTableDeleteSubHead { get; set; }
        public string MasterAccountsTableDeleteImageKey { get; set; }
        public string MasterAccountsTableDeleteSystemAcc { get; set; }
        public int? MasterAccountsTableDeleteFsno { get; set; }
        public string MasterAccountsTableDeleteStatus { get; set; }
        public int? MasterAccountsTableDeleteUserId { get; set; }
        public DateTime? MasterAccountsTableDeleteDateCreated { get; set; }
        public int? MasterAccountsTableDeleteCurrencyId { get; set; }
        public string MasterAccountsTableDeleteGpAcc { get; set; }
        public string MasterAccountsTableDeleteAcAcc { get; set; }
        public string MasterAccountsTableDeleteEdAcc { get; set; }
        public double? MasterAccountsTableDeleteOpenBalance { get; set; }
        public double? MasterAccountsTableDeleteTotalDebit { get; set; }
        public double? MasterAccountsTableDeleteTotalCredit { get; set; }
        public string MasterAccountsTableDeleteManualCode { get; set; }
        public bool? MasterAccountsTableDeleteIsSeaAcc { get; set; }
        public bool? MasterAccountsTableDeleteIsAirAcc { get; set; }
        public bool? MasterAccountsTableDeleteDelStatus { get; set; }
    }
}
