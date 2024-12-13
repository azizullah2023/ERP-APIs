using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Interfaces
{
    public interface IBankReconcilation
    {
        public Task<List<MasterAccountsTable>> GetAccountListForDropdown();
        public Task<IEnumerable<BankLedger>> BankLedger(string bankAccNo);
        public ReconciliationVoucher SavebankReconciliation(ReconciliationVoucher reconciliation);
        public ReconciliationVoucher UpdatebankReconciliation(ReconciliationVoucher reconciliation);
        public Task<Response<List<ReconciliationVoucher>>> getbankReconciliationList();
        public ReconciliationVoucher getbankReconciliationById(string Id);

    }
}
