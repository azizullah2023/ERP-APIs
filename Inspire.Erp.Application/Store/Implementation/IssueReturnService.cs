using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.Logging;
using Inspire.Erp.Application.MODULE;
namespace Inspire.Erp.Application.Store.Implementation
{
    public class IssueReturnService : IIssueReturnService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<IssueReturn> _issueReturnRepository;
        private IRepository<IssueReturnDetails> _issueReturnDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;

        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;

        //private IRepository<ReportIssueReturn> _reportIssueReturnRepository;
        public IssueReturnService(
            //IRepository<ReportIssueReturn> reportIssueReturnRepository,
            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,

            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<IssueReturn> issueReturnRepository, IRepository<IssueReturnDetails> issueReturnDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._issueReturnRepository = issueReturnRepository;
            this._issueReturnDetailsRepository = issueReturnDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;


            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;

            //_reportIssueReturnRepository = reportIssueReturnRepository;
        }

        //public IEnumerable<ReportIssueReturn> IssueReturn_GetReportIssueReturn()
        //{
        //    return _reportIssueReturnRepository.GetAll();
        //}




        public IssueReturnModel UpdateIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions,
            List<IssueReturnDetails> issueReturnDetails
            , List<StockRegister> stockRegister
            )
        {

            try
            {
                _issueReturnRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(issueReturn.IssueReturnNo, VoucherType.IssueReturn_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(issueReturn.IssueReturnNo, VoucherType.IssueReturn_TYPE, _accountTransactionRepository);
                //=================================
                issueReturn.IssueReturnDetails = issueReturnDetails.Select((k) =>
                {
                    //if (k.IssueReturnDetailsId == 0)
                    //{
                    k.IssueReturnId = issueReturn.IssueReturnId;
                    k.IssueReturnDetailsNo = issueReturn.IssueReturnNo;
                    //k.IssueReturnDetailsId = 0;
                    //}

                    return k;
                }).ToList();

                _issueReturnRepository.Update(issueReturn);





                //_issueReturnRepository.Update(issueReturn);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = issueReturn.IssueReturnDate;
                        k.AccountsTransactionsVoucherNo = issueReturn.IssueReturnNo;
                        k.AccountsTransactionsVoucherType = VoucherType.IssueReturn_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                stockRegister = stockRegister.Select((k) =>
                {
                    if (k.StockRegisterStoreID == 0)
                    {
                        k.StockRegisterVoucherDate = issueReturn.IssueReturnDate;
                        k.StockRegisterRefVoucherNo = issueReturn.IssueReturnNo;
                        k.StockRegisterTransType = VoucherType.IssueReturn_TYPE;

                        k.StockRegisterStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _stockRegisterRepository.UpdateList(stockRegister);






                _issueReturnRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _issueReturnRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedIssueReturnDetails(issueReturn.IssueReturnNo);
        }

        public int DeleteIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions,
            List<IssueReturnDetails> issueReturnDetails
            , List<StockRegister> stockRegister
            )
        {
            int i = 0;
            try
            {
                _issueReturnRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(issueReturn.IssueReturnNo, VoucherType.IssueReturn_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(issueReturn.IssueReturnNo, VoucherType.IssueReturn_TYPE, _accountTransactionRepository);
                //=================================


                issueReturn.IssueReturnDelStatus = true;

                issueReturnDetails = issueReturnDetails.Select((k) =>
                {
                    k.IssueReturnDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_issueReturnDetailsRepository.UpdateList(issueReturnDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);




                stockRegister = stockRegister.Select((k) =>
                {
                    k.StockRegisterDelStatus = true;
                    return k;
                }).ToList();
                _stockRegisterRepository.UpdateList(stockRegister);




                issueReturn.IssueReturnDetails = issueReturnDetails;

                _issueReturnRepository.Update(issueReturn);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == issueReturn.IssueReturnNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _issueReturnRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _issueReturnRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
        public IssueReturnModel InsertIssueReturn(IssueReturn issueReturn, List<AccountsTransactions> accountsTransactions,
            List<IssueReturnDetails> issueReturnDetails
            , List<StockRegister> stockRegister
            )
        {
            try
            {
                _issueReturnRepository.BeginTransaction();
                string issueReturnNumber = this.GenerateVoucherNo(issueReturn.IssueReturnDate.Date).VouchersNumbersVNo;
                issueReturn.IssueReturnNo = issueReturnNumber;


                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _issueReturnRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.IssueReturnId) + 1);

                issueReturn.IssueReturnId = maxcount;






                issueReturnDetails = issueReturnDetails.Select((x) =>
                {
                    x.IssueReturnId = maxcount;
                    x.IssueReturnDetailsNo = issueReturnNumber;
                    return x;
                }).ToList();
                //_issueReturnDetailsRepository.InsertList(issueReturnDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    //k.AccountsTransactionsTransDate = issueReturn.IssueReturnDate;
                    k.AccountsTransactionsVoucherNo = issueReturnNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.IssueReturn_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                foreach (var item in issueReturnDetails)
                {
                    stockRegister.Add(new StockRegister
                    {
                        StockRegisterMaterialID = item.IssueReturnDetailsMatId,
                        StockRegisterPurchaseID = issueReturnNumber,
                        StockRegisterRefVoucherNo = issueReturnNumber,
                        StockRegisterQuantity = item.IssueReturnDetailsQuantity,
                        StockRegisterRate = item.IssueReturnDetailsRate,
                        StockRegisterSIN = item.IssueReturnDetailsQuantity,
                        StockRegisterSout = 0,
                        StockRegisterAmount = item.IssueReturnDetailsActualAmount,
                        StockRegisterVoucherDate = issueReturn.IssueReturnDate,
                        StockRegisterTransType = VoucherType.IssueReturn_TYPE,
                        StockRegisterStatus = AccountStatus.Approved,
                        StockRegisterAssignedDate = issueReturn.IssueReturnDate,
                        StockRegisterExpDate = null,
                        StockRegisterDepID = 1,
                        StockRegisterDelStatus = false,
                        StockRegisterUnitID = item.IssueReturnDetailsUnitId,
                        StockRegisterLocationID = issueReturn.IssueReturnLocationId
                    });
                }
                _stockRegisterRepository.InsertList(stockRegister);

                _issueReturnRepository.Insert(issueReturn);
                _issueReturnRepository.TransactionCommit();
                return this.GetSavedIssueReturnDetails(issueReturn.IssueReturnNo);

            }
            catch (Exception ex)
            {
                _issueReturnRepository.TransactionRollback();
                throw ex;
            }



        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<IssueReturn> GetIssueReturn()
        {
            IEnumerable<IssueReturn> issueReturn_ALL = _issueReturnRepository.GetAll().Where(k => k.IssueReturnDelStatus == false || k.IssueReturnDelStatus == null);
            return issueReturn_ALL;
        }
        public IssueReturnModel GetSavedIssueReturnDetails(string pvno)
        {
            IssueReturnModel issueReturnModel = new IssueReturnModel();
            //issueReturnModel.issueReturn = _issueReturnRepository.GetAsQueryable().Where(k => k.IssueReturnNo == pvno && k.IssueReturnDelStatus == false).SingleOrDefault();

            issueReturnModel.issueReturn = _issueReturnRepository.GetAsQueryable().Where(k => k.IssueReturnNo == pvno).SingleOrDefault();



            issueReturnModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.IssueReturn_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            issueReturnModel.issueReturnDetails = _issueReturnDetailsRepository.GetAsQueryable().Where(x => x.IssueReturnDetailsNo == pvno && (x.IssueReturnDetailsDelStatus == false || x.IssueReturnDetailsDelStatus == null)).ToList();
            return issueReturnModel;

        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {



                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.IssueReturn_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.IssueReturn_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.IssueReturn_TYPE,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = VoucherGenDate

                };
                _voucherNumbersRepository.Insert(vouchersNumbers);
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        //using Microsoft.Extensions.Logging;
        //private readonly ILogger<PaymentVoucherService> _logger;
        //IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
        public VouchersNumbers GetVouchersNumbers(string pvno)
        {
            try
            {
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
    }
}

