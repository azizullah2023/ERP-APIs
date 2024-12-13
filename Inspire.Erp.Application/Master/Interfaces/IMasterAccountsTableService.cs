using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IMasterAccountsTableService
    {
        public IEnumerable<MasterAccountsTable> InsertAccount(MasterAccountsTable masterAccountsTable);
        public IEnumerable<MasterAccountsTable> InsertAccountGroup(MasterAccountsTable masterAccountsTable);
        public MasterAccountsTable NewAccount(MasterAccountsTable masterAccountsTable);
        public MasterAccountsTable AddNewAccount(MasterAccountsTable masterAccountsTable);
        public MasterAccountsTable UpdateNewAccount(MasterAccountsTable masterAccountsTable);
        public IEnumerable<MasterAccountsTable> UpdateAccount(MasterAccountsTable masterAccountsTable);
        public IEnumerable<MasterAccountsTable> DeleteAccount(MasterAccountsTable masterAccountsTable);
        public IEnumerable<MasterAccountsTable> GetAllAccount();
        public IEnumerable<MasterAccountsTable> GetAllGroup();
        public IEnumerable<MasterAccountsTable> GetAllAccountById(string id);
        public IEnumerable<MasterAccountsTable> GetAllAccountNotGroup();
        //public IEnumerable<AccountStockType> GetAllStockType();
        public IEnumerable<MasterAccountsTable> GetAccountMastersByName(string name);
        public MasterAccountsTable GetAllAccountMasterById(string id);
    }
}