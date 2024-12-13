using System;
using System.Collections.Generic;
using IInspire.Erp.Domain.Entities;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BankGuaranteeMaster:BaseEntity
    {
        public int? BankGuaranteeMasterBgid { get; set; }
        public string BankGuaranteeMasterBgname { get; set; }
        public int? BankGuaranteeMasterUserId { get; set; }
        public bool? BankGuaranteeMasterDeleted { get; set; }
        public bool? BankGuaranteeMasterStatus { get; set; }
    }
}
