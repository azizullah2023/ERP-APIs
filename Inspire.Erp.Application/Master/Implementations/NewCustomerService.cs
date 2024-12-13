////using System;
////using System.Collections.Generic;
////using System.Text;

////namespace Inspire.Erp.Application.Master.Implementations
////{
////    class NewCustomerService
////    {
////    }
////}

using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class NewCustomerService : INewCustomerService
    {
        private IRepository<CustomerMaster> _customerMasterRepository;
        private IRepository<CustomerContacts> _customerContactsRepository;
        private IRepository<CustomerDepartments> _customerDepartmentsRepository;
        private readonly IRepository<MasterAccountsTable> _masterAccountTable;
        ////private IRepository<ProgramSettings> _programsettingsRepository;
        ////private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<NewCustomerService> _logger;

        public NewCustomerService(IRepository<CustomerMaster> customerMasterRepository, IRepository<CustomerContacts> customerContactsRepository,
            IRepository<CustomerDepartments> customerDepartmentsRepository, IRepository<MasterAccountsTable> masterAccountTable, ILogger<NewCustomerService> logger)
        {
            this._customerMasterRepository = customerMasterRepository; _masterAccountTable = masterAccountTable;
            this._customerContactsRepository = customerContactsRepository;
            this._customerDepartmentsRepository = customerDepartmentsRepository;
            _logger = logger;

        }

        public CustomerViewModel UpdateNewCustomer(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments)
        {

            try
            {
                _customerMasterRepository.BeginTransaction();

                _customerMasterRepository.Update(customerMaster);
                _customerContactsRepository.UpdateList(customerContacts);
                _customerDepartmentsRepository.UpdateList(customerDepartments);
                _customerMasterRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _customerMasterRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedCustomers(customerMaster.CustomerMasterCustomerNo);
        }

        public int DeleteNewCustomers(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments)
        {
            int i = 0;
            try
            {
                _customerMasterRepository.BeginTransaction();

                customerMaster.CustomerMasterCustomerDeleteStatus = true;
                customerMaster.CustomerMasterCustomerDelStatus = true;

                customerContacts = customerContacts.Select((k) =>
                {
                    k.CustomerContactsDelStatus = true;
                    return k;
                }).ToList();

                _customerContactsRepository.UpdateList(customerContacts);

                customerDepartments = customerDepartments.Select((k) =>
                {
                    k.CustomerDepartmentsDelStatus = true;
                    return k;
                }).ToList();
                _customerDepartmentsRepository.UpdateList(customerDepartments);

                var account = _masterAccountTable.GetAsQueryable().Where(a => a.MaAccNo == customerMaster.CustomerMasterCustomerReffAcNo).FirstOrDefault();
                if (account != null)
                {
                    _masterAccountTable.Delete(account);
                }

                _customerMasterRepository.Update(customerMaster);

                _customerMasterRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _customerMasterRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public CustomerViewModel InsertNewCustomer(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments)
        {
            try
            {
                _customerMasterRepository.BeginTransaction();

                ////int OBtVoucherNumber = (openingVoucher.OpeningVoucherMasterId == null || openingVoucher.OpeningVoucherMasterId == 0) ?
                ////              this.GenerateVoucherNo(openingVoucher.OpeningVoucherMasterOpBDate.Value).VouchersNumbersVNo :  openingVoucher.OpeningVoucherMasterId;
                ////openingVoucher.OpeningVoucherMasterId = OBtVoucherNumber;


                int mxc = 0;
                mxc = _customerMasterRepository.GetAsQueryable().Where(k => k.CustomerMasterCustomerNo != null)
                            .DefaultIfEmpty()
                            .Max(o => o == null ? 0 : o.CustomerMasterCustomerNo) + 1;

                ////VouchersNumbers vouchersNumbers = new VouchersNumbers
                ////{
                ////    VouchersNumbersVNo = "OB" + Convert.ToString(mxc),
                ////    VouchersNumbersVNoNu = mxc,
                ////    VouchersNumbersVType = VoucherType.OBVoucher,
                ////    VouchersNumbersFsno = 1,
                ////    VouchersNumbersStatus = AccountStatus.Pending,
                ////    VouchersNumbersVoucherDate = openingVoucher.OpeningVoucherMasterOpBDate.Value

                ////};
                ////_voucherNumbersRepository.Insert(vouchersNumbers);


                customerMaster.CustomerMasterCustomerNo = mxc;

                ////string vNo = "";
                ///
                if (customerContacts != null)
                {
                    customerContacts = customerContacts.Select((x) =>
                    {
                        x.CustomerContactsCustomerId = mxc;
                        return x;
                    }).ToList();
                    _customerContactsRepository.InsertList(customerContacts);

                };

                if (customerDepartments != null)
                {
                    customerDepartments = customerDepartments.Select((x) =>
                    {
                        x.CustomerDepartmentsCustomerId = mxc;
                        return x;
                    }).ToList();

                    _customerDepartmentsRepository.InsertList(customerDepartments);

                }
                _customerMasterRepository.Insert(customerMaster);
                _customerMasterRepository.TransactionCommit();

                return this.GetSavedCustomers(customerMaster.CustomerMasterCustomerNo);

            }
            catch (Exception ex)
            {
                _customerMasterRepository.TransactionRollback();
                throw ex;
            }

        }

        public IEnumerable<CustomerMaster> GetAllCustomers()
        {
            IEnumerable<CustomerMaster> customerMasters = _customerMasterRepository.GetAll().Where(k => k.CustomerMasterCustomerDelStatus == false || k.CustomerMasterCustomerDelStatus == null);
            return customerMasters;
        }
        public CustomerViewModel GetSavedCustomers(int pvno)
        {

            CustomerViewModel customerView = new CustomerViewModel();

            customerView.customerMaster = _customerMasterRepository.GetAsQueryable().Where(K => K.CustomerMasterCustomerNo == pvno).SingleOrDefault();
            customerView.customercontacts = _customerContactsRepository.GetAsQueryable().Where(c => c.CustomerContactsCustomerId == pvno && (c.CustomerContactsDelStatus == false || c.CustomerContactsDelStatus == null)).ToList();
            customerView.customerdepartments = _customerDepartmentsRepository.GetAsQueryable().Where(x => x.CustomerDepartmentsCustomerId == pvno && (x.CustomerDepartmentsDelStatus == false || x.CustomerDepartmentsDelStatus == null)).ToList();


            return customerView;

        }
    }
}
