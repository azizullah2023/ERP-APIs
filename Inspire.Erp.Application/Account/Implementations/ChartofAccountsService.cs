using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Account
////{
////    public class ChartofAccountsService: IChartofAccountsService
////    {
////        private IRepository<MasterAccountsTable> chartofAccountsrepository;
////        private IRepository<AccountSettings> accountSettingsrepository;
////        public ChartofAccountsService(IRepository<MasterAccountsTable> _chartofAccountsrepository, IRepository<AccountSettings> _accountSettingsrepository)
////        {
////            chartofAccountsrepository = _chartofAccountsrepository;
////            accountSettingsrepository = _accountSettingsrepository;
////        }
////        public IEnumerable<MasterAccountsTable> InsertAccounts(MasterAccountsTable masterAccountsTable)
////        {
////            bool valid = false;
////            try
////            {
////                valid = true;
////                int mxc = 0;
////                mxc = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaSno != null).Select(x => x.MaSno).Max();
////                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

////                masterAccountsTable.MaSno = mxc;
////                chartofAccountsrepository.Insert(masterAccountsTable);
////            }
////            catch (Exception ex)
////            {
////                valid = false;
////                throw ex;

////            }
////            finally
////            {
////                //cityrepository.Dispose();
////            }
////            return this.GetAllAccounts();
////        }
////        public IEnumerable<MasterAccountsTable> UpdateAccounts(MasterAccountsTable masterAccountsTable)
////        {
////            bool valid = false;
////            try
////            {
////                chartofAccountsrepository.Update(masterAccountsTable);
////                valid = true;

////            }
////            catch (Exception ex)
////            {
////                valid = false;
////                throw ex;

////            }
////            finally
////            {
////                //cityrepository.Dispose();
////            }
////            return this.GetAllAccounts();
////        }
////        public IEnumerable<MasterAccountsTable> DeleteAccounts(MasterAccountsTable masterAccountsTable)
////        {
////            bool valid = false;
////            try
////            {
////                chartofAccountsrepository.Delete(masterAccountsTable);
////                valid = true;

////            }
////            catch (Exception ex)
////            {
////                valid = false;
////                throw ex;

////            }
////            finally
////            {
////                //cityrepository.Dispose();
////            }
////            return this.GetAllAccounts();
////        }

////        public IEnumerable<MasterAccountsTable> GetAllAccounts()
////        {
////            IEnumerable<MasterAccountsTable> masterAccountsTable;
////            try
////            {
////                masterAccountsTable = chartofAccountsrepository.GetAll();

////            }
////            catch (Exception ex)
////            {
////                throw ex;

////            }
////            finally
////            {
////                //cityrepository.Dispose();
////            }
////            return masterAccountsTable;
////        }
////        public IEnumerable<MasterAccountsTable> GetAllAccountsById(int id)
////        {
////            IEnumerable<MasterAccountsTable> masterAccountsTable;
////            try
////            {
////                masterAccountsTable = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaSno == id).Select(k => k);

////            }
////            catch (Exception ex)
////            {
////                throw ex;

////            }
////            finally
////            {
////                //cityrepository.Dispose();
////            }
////            return masterAccountsTable;

////        }

////        public DrCrAccounts GetDefaultCreditDebitAccounts()
////        {
////            DrCrAccounts drCrAccounts = new DrCrAccounts();
////            string[] delimiters = { "::" };
////            drCrAccounts.Debit = accountSettingsrepository.GetAsQueryable()
////                                                               .Where(c =>c.AccountSettingsAccountKeyValue == AccountStatus.ExpenseAccountNo)
////                                                               .Select(x => new MasterAccountsTable {
////                                                                    MaAccName = x.AccountSettingsAccountTextValue
////                                                                                             .Split(delimiters,StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
////                                                                    MaAccNo = x.AccountSettingsAccountTextValue
////                                                                                             .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
////                                                               }).SingleOrDefault() ;
////            drCrAccounts.Credit = accountSettingsrepository.GetAsQueryable()
////                                                            .Where(c => c.AccountSettingsAccountKeyValue == AccountStatus.StockAccountNumber)
////                                                            .Select(x => new MasterAccountsTable
////                                                            {
////                                                                MaAccName = x.AccountSettingsAccountTextValue
////                                                                                          .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
////                                                                MaAccNo = x.AccountSettingsAccountTextValue
////                                                                                          .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
////                                                            }).SingleOrDefault();
////            return drCrAccounts;
////        }



////    }
////}

{
    public class ChartofAccountsService : IChartofAccountsService
    {
        private IRepository<MasterAccountsTable> chartofAccountsrepository;
        private IRepository<AccountSettings> accountSettingsrepository;
        public ChartofAccountsService(IRepository<MasterAccountsTable> _chartofAccountsrepository, IRepository<AccountSettings> _accountSettingsrepository)
        {
            chartofAccountsrepository = _chartofAccountsrepository;
            accountSettingsrepository = _accountSettingsrepository;
        }
        public IEnumerable<MasterAccountsTable> InsertAccounts(MasterAccountsTable masterAccountsTable)
        {
            bool valid = false;
            try
            {
                valid = true;
                int mxc = 0;
                mxc = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaSno != null).Select(x => x.MaSno).Max();
                if (mxc == null) { mxc = 1; } else { mxc = mxc + 1; }

                masterAccountsTable.MaSno = mxc;
                chartofAccountsrepository.Insert(masterAccountsTable);
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllAccounts();
        }
        public IEnumerable<MasterAccountsTable> UpdateAccounts(MasterAccountsTable masterAccountsTable)
        {
            bool valid = false;
            try
            {
                chartofAccountsrepository.Update(masterAccountsTable);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllAccounts();
        }
        public IEnumerable<MasterAccountsTable> DeleteAccounts(MasterAccountsTable masterAccountsTable)
        {
            bool valid = false;
            try
            {
                chartofAccountsrepository.Delete(masterAccountsTable);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllAccounts();
        }

        public IEnumerable<MasterAccountsTable> GetAllAccounts()
        {
            IEnumerable<MasterAccountsTable> masterAccountsTable;
            try
            {
                masterAccountsTable = chartofAccountsrepository.GetAll();
                //// masterAccountsTable = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaAccNo != "" && (k.MaDelStatus == false || k.MaDelStatus == null)
                ////&& k.MaAccType == AccountMasterStatus.Account ).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return masterAccountsTable;
        }
        public IEnumerable<MasterAccountsTable> GetAllAccountsById(int id)
        {
            IEnumerable<MasterAccountsTable> masterAccountsTable;
            try
            {
                masterAccountsTable = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaSno == id).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return masterAccountsTable;

        }

        public DrCrAccounts GetDefaultCreditDebitAccounts()
        {
            DrCrAccounts drCrAccounts = new DrCrAccounts();
            string[] delimiters = { "::" };
            drCrAccounts.Debit = accountSettingsrepository.GetAsQueryable()
                                                               .Where(c => c.AccountSettingsAccountKeyValue == AccountStatus.ExpenseAccountNo)
                                                               .Select(x => new MasterAccountsTable
                                                               {
                                                                   MaAccName = x.AccountSettingsAccountTextValue
                                                                                             .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
                                                                   MaAccNo = x.AccountSettingsAccountTextValue
                                                                                             .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
                                                               }).SingleOrDefault();
            drCrAccounts.Credit = accountSettingsrepository.GetAsQueryable()
                                                            .Where(c => c.AccountSettingsAccountKeyValue == AccountStatus.StockAccountNumber)
                                                            .Select(x => new MasterAccountsTable
                                                            {
                                                                MaAccName = x.AccountSettingsAccountTextValue
                                                                                          .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
                                                                MaAccNo = x.AccountSettingsAccountTextValue
                                                                                          .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[1].Trim()
                                                            }).SingleOrDefault();
            return drCrAccounts;
        }

        public IEnumerable<MasterAccountsTable> GetAllBankAccounts()
        {
            var masterAccountsTable = chartofAccountsrepository.GetAsQueryable().Where(k => k.MaRelativeNo == "01001002" && (k.MaDelStatus == false || k.MaDelStatus == null)
             ).Select(k => k);

            return masterAccountsTable;
        }
    }
}


