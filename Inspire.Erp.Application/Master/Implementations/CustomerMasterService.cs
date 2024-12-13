using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Inspire.Erp.Application.Master
{
    public class CustomerMasterService : ICustomerMasterService
    {
        private IRepository<CustomerMaster> customerrepository;
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        private readonly IUtilityService _utilityService;
        private string DeptorsRelativeNo = "";

        public CustomerMasterService(IRepository<CustomerMaster> _customerrepository, IConfiguration config,
            IRepository<MasterAccountsTable> masterAccountTable, IUtilityService utilityService)
        {
            customerrepository = _customerrepository;
            _masterAccountTable = masterAccountTable;
            _utilityService = utilityService;
            DeptorsRelativeNo = config["ApplicationSettings:DebtorRelativeNo"];
        }
        public async Task<CustomerMaster> InsertCustomer(CustomerMaster customerMaster)
        {
            bool valid = false;

            try
            {
                //_masterAccountTable.BeginTransaction();
                var fs = await _utilityService.GetFinancialPeriods();
                valid = true;
                int mxc = 0;
                var maxId = _masterAccountTable.GetAsQueryable().AsNoTracking().Max(x => x.MaSno);
                var masterAccount = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaAccNo == DeptorsRelativeNo).FirstOrDefault();
                var cusAccNo = string.Empty;
                MasterAccountsTable masterAccountsTable = null;
                if (customerMaster.IsCreateAccount)
                {
                    var count = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaRelativeNo == DeptorsRelativeNo).ToList().Count() + 1;
                    //cusAccNo = count.ToString().Length == 1 ? "00" + count : count.ToString().Length == 2 ? "0" + count : count.ToString();
                    cusAccNo = count.ToString("D3");
                    cusAccNo = masterAccount.MaAccNo + cusAccNo;

                    var isunique = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaAccNo == cusAccNo).FirstOrDefault();
                    while (isunique != null)
                    {
                        // Increment the count and regenerate the cusAccNo
                        count++;
                        cusAccNo = count.ToString("D3");
                        cusAccNo = masterAccount.MaAccNo + cusAccNo;

                        // Check if the new cusAccNo is unique
                        isunique = _masterAccountTable.GetAsQueryable().AsNoTracking()
                                                      .Where(x => x.MaAccNo == cusAccNo)
                                                      .FirstOrDefault();
                    }
                    masterAccountsTable = new MasterAccountsTable()
                    {
                        MaAccName = customerMaster.CustomerMasterCustomerName,
                        MaAccNo = cusAccNo,
                        MaAccType = "A",
                        MaImageKey = masterAccount.MaImageKey,
                        MaAssetDate = DateTime.Now,
                        MaFsno = fs.Result.FinancialPeriodsFsno,
                        MaMainHead = masterAccount.MaMainHead,
                        MaRelativeNo = masterAccount.MaAccNo,
                        MaSubHead = masterAccount.MaSubHead,
                        MaUserId = 0,
                        MaSno = maxId + 1,
                        MaDateCreated = DateTime.Now,
                        MaStatus = "R",
                    };
                }
                if (masterAccountsTable != null)
                {
                    await _utilityService.SaveMasterAccountTable(masterAccountsTable);
                }
                var query = customerrepository.GetAsQueryable().AsNoTracking().Where(k => k.CustomerMasterCustomerNo != null)
                    .Select(x => x.CustomerMasterCustomerNo);
                if (query.Any())
                {
                    mxc = query.Max();
                }
                mxc = mxc == null ? 1 : mxc + 1;
                customerMaster.CustomerMasterCustomerNo = mxc;
                customerMaster.CustomerMasterCustomerReffAcNo = cusAccNo;
                customerrepository.Insert(customerMaster);


                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Customer Master",
                    Section = "Add Customer Master",
                    Entity = "Customer Master",
                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion
                //customerrepository.SaveChangesAsync();
                _masterAccountTable.SaveChangesAsync();
                //_masterAccountTable.TransactionCommit();

            }
            catch (Exception ex)
            {
                _masterAccountTable.TransactionRollback();
                valid = false;
                throw ex;

            }
            finally
            {
                //customerrepository.Dispose();
            }


            return this.GetAllCustomerById(customerMaster.CustomerMasterCustomerNo);
        }
        public CustomerMaster UpdateCustomer(CustomerMaster customerMaster)
        {
            bool valid = false;
            try
            {
                //_masterAccountTable.BeginTransaction();
                //var fs = await _utilityService.GetFinancialPeriods();
                //valid = true;
                //int mxc = 0;
                //var maxId = _masterAccountTable.GetAsQueryable().AsNoTracking().Max(x => x.MaSno);
                //var masterAccount = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaAccNo == DeptorsRelativeNo).FirstOrDefault();
                //var isAlreadyAccount = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaAccName == customerMaster.CustomerMasterCustomerName).FirstOrDefault();
                //var cusAccNo = string.Empty;
                //MasterAccountsTable masterAccountsTable = null;
                //if (customerMaster.IsCreateAccount && isAlreadyAccount == null)
                //{
                //    var count = _masterAccountTable.GetAsQueryable().AsNoTracking().Where(x => x.MaRelativeNo == DeptorsRelativeNo).ToList().Count() + 1;
                //    cusAccNo = count.ToString().Length == 1 ? "00" + count : count.ToString().Length == 2 ? "0" + count : count.ToString();
                //    cusAccNo = masterAccount.MaAccNo + cusAccNo;
                //    masterAccountsTable = new MasterAccountsTable()
                //    {
                //        MaAccName = customerMaster.CustomerMasterCustomerName,
                //        MaAccNo = cusAccNo,
                //        MaAccType = "A",
                //        MaImageKey = masterAccount.MaImageKey,
                //        MaAssetDate = DateTime.Now,
                //        MaFsno = fs.Result.FinancialPeriodsFsno,
                //        MaMainHead = masterAccount.MaMainHead,
                //        MaRelativeNo = masterAccount.MaAccNo,
                //        MaSubHead = masterAccount.MaSubHead,
                //        MaUserId = 0,
                //        MaSno = maxId + 1,
                //        MaDateCreated = DateTime.Now,
                //        MaStatus = "R",
                //    };
                //}

                //if (masterAccountsTable != null)
                //    await _utilityService.SaveMasterAccountTable(masterAccountsTable);

                customerrepository.Update(customerMaster);

                //_masterAccountTable.TransactionCommit();
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //customerrepository.Dispose();
            }
            return this.GetAllCustomerById(customerMaster.CustomerMasterCustomerNo);
        }
        public CustomerMaster DeleteCustomer(CustomerMaster customerMaster)
        {
            bool valid = false;
            try
            {
                _masterAccountTable.BeginTransaction();
                customerMaster.CustomerMasterCustomerDeleteStatus = true;
                customerrepository.Update(customerMaster);
                var account = _masterAccountTable.GetAsQueryable().Where(a => a.MaRelativeNo == customerMaster.CustomerMasterCustomerReffAcNo).FirstOrDefault();

                _masterAccountTable.Delete(account);
                _masterAccountTable.TransactionCommit();
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //customerrepository.Dispose();
            }
            return customerMaster;
        }
        public IEnumerable<CustomerMaster> GetAllCustomer()
        {
            IEnumerable<CustomerMaster> customerMasters;
            try
            {
                customerMasters = customerrepository.GetAll().Where(a => a.CustomerMasterCustomerDelStatus != true).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //customerrepository.Dispose();
            }
            return customerMasters;
        }
        public CustomerMaster GetAllCustomerById(int id)
        {
            CustomerMaster customerMasters;
            try
            {
                customerMasters = customerrepository.GetAsQueryable().Where(k => k.CustomerMasterCustomerNo == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return customerMasters;

        }
    }
}
