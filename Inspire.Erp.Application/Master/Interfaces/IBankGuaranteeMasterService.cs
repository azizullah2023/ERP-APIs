using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface IBankGuaranteeMasterService
    {
        public IEnumerable<BankGuaranteeMaster> InsertBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster);
        public IEnumerable<BankGuaranteeMaster> UpdateBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster);
        public IEnumerable<BankGuaranteeMaster> DeleteBankGuarantee(BankGuaranteeMaster bankGuaranteeMaster);
        public IEnumerable<BankGuaranteeMaster> GetAllBankGuarantee();
    }
}
