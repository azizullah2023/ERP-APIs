using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Account
{
    public interface IChartofAccountsService
    {
        IEnumerable<MasterAccountsTable> InsertAccounts(MasterAccountsTable masterAccountsTable);
        IEnumerable<MasterAccountsTable> UpdateAccounts(MasterAccountsTable masterAccountsTable);

        IEnumerable<MasterAccountsTable> DeleteAccounts(MasterAccountsTable masterAccountsTable);

        IEnumerable<MasterAccountsTable> GetAllAccounts();
        IEnumerable<MasterAccountsTable> GetAllBankAccounts();

        IEnumerable<MasterAccountsTable> GetAllAccountsById(int id);

        DrCrAccounts GetDefaultCreditDebitAccounts();

    }
}
