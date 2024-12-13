using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankMaster
    {
        public int? BankMasterDi { get; set; }
        public string BankMasterName { get; set; }
        public int? BankMasterStatus { get; set; }
        public bool? BankMasterDelStatus { get; set; }
    }
}
