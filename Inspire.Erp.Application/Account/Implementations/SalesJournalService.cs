using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
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
namespace Inspire.Erp.Application.Account.Implementations
{
    public class SalesJournalService : ISalesJournalService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<SalesJournal> _salesJournalRepository;
        private IRepository<SalesJournalDetails> _salesJournalDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;

        public SalesJournalService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<SalesJournalService> logger,
            IRepository<SalesJournal> salesJournalRepository, IRepository<SalesJournalDetails> salesJournalDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._salesJournalRepository = salesJournalRepository;
            this._salesJournalDetailsRepository = salesJournalDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;

        }
        //public SalesJournal InsertSalesJournal(SalesJournal salesJournal,
        //  List<SalesJournalDetails> salesJournalDetails)
        //{
        //    List<AccountsTransactions> accountsTransactions = new List<AccountsTransactions>();
        //    try {
        //        _accountTransactionRepository.BeginTransaction();

        //        _salesJournalRepository.Insert(salesJournal);
        //        _salesJournalDetailsRepository.InsertList(salesJournalDetails.Select(k => 
        //        {
        //            k.SalesJournalDetailsVno = salesJournal.SalesJournalVno;
        //            return k;
        //        }).ToList()) ;
        //        //int acctTransMaxVal = Convert.ToInt32(_accountTransactionRepository.GetAsQueryable()
        //        //                                        .Where(x => x.AccountsTransactionsTransSno > 0)
        //        //                                        .DefaultIfEmpty()
        //        //                                        .Max(o => o == null ? 0 : o.AccountsTransactionsTransSno))  + 1;

        //        AccountsTransactions creditData = new AccountsTransactions();
        //        creditData.AccountsTransactionsAccNo = salesJournal.SalesJournalCrAcNo;
        //        creditData.AccountsTransactionsTransDate = (DateTime) salesJournal.SalesJournalDate.Value.Date;
        //        creditData.AccountsTransactionsParticulars = salesJournal.SalesJournalNarration;
        //        creditData.AccountsTransactionsCredit = Convert.ToDecimal(salesJournal.SalesJournalDbAmount);
        //        creditData.AccountsTransactionsFcCredit = Convert.ToDecimal(salesJournal.SalesJournalDbAmount);
        //        creditData.AccountsTransactionsVoucherType = VoucherType.SalesJournal_TYPE;
        //        creditData.AccountsTransactionsTstamp = DateTime.Now;
        //        creditData.AccountsTransactionsVoucherNo = salesJournal.SalesJournalVno;
        //        creditData.AccountsTransactionsDescription = salesJournal.SalesJournalNarration;
        //        creditData.AccountsTransactionsStatus = AccountStatus.Approved;
        //        accountsTransactions.Add(creditData);
        //        //int transactionIntialCount = Convert.ToInt32(creditData.AccountsTransactionsTransSno + 1);
        //        foreach (var debit in salesJournalDetails)
        //        {
        //            //transactionIntialCount = transactionIntialCount + 1;
        //            AccountsTransactions debitData = new AccountsTransactions();
        //            debitData.AccountsTransactionsAccNo = debit.SalesJournalDetailsAcNo;
        //            debitData.AccountsTransactionsTransDate = (DateTime) creditData.AccountsTransactionsTransDate;
        //            debitData.AccountsTransactionsParticulars = debit.SalesJournalDetailsNarration;
        //            debitData.AccountsTransactionsDebit = Convert.ToDecimal(debit.SalesJournalDetailsDbAmount);
        //            debitData.AccountsTransactionsFcDebit = Convert.ToDecimal(debit.SalesJournalDetailsDbAmount);
        //            debitData.AccountsTransactionsVoucherType = VoucherType.SalesJournal_TYPE;
        //            debitData.AccountsTransactionsTstamp = DateTime.Now;
        //            debitData.AccountsTransactionsVoucherNo = salesJournal.SalesJournalVno;
        //            debitData.AccountsTransactionsDescription = debit.SalesJournalDetailsNarration;
        //            debitData.AccountsTransactionsStatus = AccountStatus.Approved;
        //            accountsTransactions.Add(debitData);

        //        }
        //        _accountTransactionRepository.InsertList(accountsTransactions);
        //        _accountTransactionRepository.TransactionCommit();
        //    }
        //    catch(Exception ex) {
        //        _accountTransactionRepository.TransactionRollback();
        //        throw ex;
        //    }

        //    return this.GetSavedSalesJournalDetails(salesJournal.SalesJournalVno);

        //}

        public SalesJournalModel UpdateSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails)
        {

            try
            {
                _salesJournalRepository.BeginTransaction();


                salesJournal.SalesJournalDetails = salesJournalDetails.Select((k) =>
                {
                    //if (k.SalesJournalDetailsId == 0)
                    //{
                    k.SalesJournalId = salesJournal.SalesJournalId;
                    k.SalesJournalDetailsVno = salesJournal.SalesJournalVno;
                    //k.SalesJournalDetailsId = 0;
                    //}

                    return k;
                }).ToList();

                _salesJournalRepository.Update(salesJournal);





                //_salesJournalRepository.Update(salesJournal);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.SalesJournal_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                //salesJournalDetails = salesJournalDetails.Select((k) =>
                //{
                //    if (k.SalesJournalId == 0)
                //    {
                //        k.SalesJournalId = salesJournal.SalesJournalId;
                //        k.SalesJournalDetailsVno = salesJournal.SalesJournalVno;
                //    }

                //    return k;
                //}).ToList();


                //_salesJournalDetailsRepository.UpdateList(salesJournalDetails);
                _salesJournalRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _salesJournalRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedSalesJournalDetails(salesJournal.SalesJournalVno);
        }

        public int DeleteSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails)
        {
            int i = 0;
            try
            {
                _salesJournalRepository.BeginTransaction();

                salesJournal.SalesJournalDelStatus = true;

                salesJournalDetails = salesJournalDetails.Select((k) =>
                {
                    k.SalesJournalDetailsDelStatus = true;
                    return k;
                }).ToList();

                _salesJournalDetailsRepository.UpdateList(salesJournalDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                _salesJournalRepository.Update(salesJournal);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == salesJournal.SalesJournalVno).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _salesJournalRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _salesJournalRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public SalesJournalModel InsertSalesJournal(SalesJournal salesJournal, List<AccountsTransactions> accountsTransactions, List<SalesJournalDetails> salesJournalDetails)
        {
            try
            {
                _salesJournalRepository.BeginTransaction();
                string salesJournalNumber = this.GenerateVoucherNo(salesJournal.SalesJournalDate.Value).VouchersNumbersVNo;
                salesJournal.SalesJournalVno = salesJournalNumber;
               

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _salesJournalRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.SalesJournalId) + 1);

                salesJournal.SalesJournalId = maxcount;
                salesJournal.SalesJournalDetails = salesJournal.SalesJournalDetails.AsEnumerable().Where(k => k != null).Select((x) => {
                
                     x.SalesJournalDetailsVno = salesJournalNumber;
                    return x;
                }).ToList();
                _salesJournalRepository.Insert(salesJournal);




                //salesJournalDetails = salesJournalDetails.Select((x) =>
                //{
                //    x.SalesJournalId = maxcount;
                //    x.SalesJournalDetailsVno = salesJournalNumber;
                //    return x;
                //}).ToList();

                //_salesJournalDetailsRepository.Update(salesJournalDetails);
                //_salesJournalDetailsRepository.InsertList(salesJournalDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = salesJournalNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.SalesJournal_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                _salesJournalRepository.TransactionCommit();
                return this.GetSavedSalesJournalDetails(salesJournal.SalesJournalVno);

            }
            catch (Exception ex)
            {
                _salesJournalRepository.TransactionRollback();
                throw ex;
            }



        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<SalesJournal> GetSalesJournal()
        {
            IEnumerable<SalesJournal> salesJournal_ALL = _salesJournalRepository.GetAll().Where(k => k.SalesJournalDelStatus == false || k.SalesJournalDelStatus == null);
            return salesJournal_ALL;
        }
        public SalesJournalModel GetSavedSalesJournalDetails(string pvno)
        {
            SalesJournalModel salesJournalModel = new SalesJournalModel();
            //salesJournalModel.salesJournal = _salesJournalRepository.GetAsQueryable().Where(k => k.SalesJournalVno == pvno && k.SalesJournalDelStatus == false).SingleOrDefault();

            salesJournalModel.salesJournal = _salesJournalRepository.GetAsQueryable().Where(k => k.SalesJournalVno == pvno).SingleOrDefault();



            salesJournalModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.SalesJournal_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            salesJournalModel.salesJournalDetails = _salesJournalDetailsRepository.GetAsQueryable().Where(x => x.SalesJournalDetailsVno == pvno && (x.SalesJournalDetailsDelStatus == false || x.SalesJournalDetailsDelStatus == null)).ToList();
            return salesJournalModel;


        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {


                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.SalesJournal_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.SalesJournal_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.SalesJournal_TYPE,
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
