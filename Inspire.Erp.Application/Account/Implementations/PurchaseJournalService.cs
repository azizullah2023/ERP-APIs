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
using Inspire.Erp.Application.MODULE;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class PurchaseJournalService : IPurchaseJournalService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PurchaseJournal> _purchaseJournalRepository;
        private IRepository<PurchaseJournalDetails> _purchaseJournalDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<UserTracking> _UserTracking;



        public PurchaseJournalService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
            IRepository<PurchaseJournal> purchaseJournalRepository, IRepository<PurchaseJournalDetails> purchaseJournalDetailsRepository
            , IRepository<UserTracking> UserTracking)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._purchaseJournalRepository = purchaseJournalRepository;
            this._purchaseJournalDetailsRepository = purchaseJournalDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _UserTracking = UserTracking;

        }

        public PurchaseJournal UpdatePurchaseJournal(PurchaseJournal purchaseJournal, List<AccountsTransactions> accountsTransactions, List<PurchaseJournalDetails> purchaseJournalDetails)
        {

            try
            {

                //=================================               
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(purchaseJournal.PurchaseJournalVno, VoucherType.PurchaseJournal_TYPE, _accountTransactionRepository);
                //=================================

                _purchaseJournalRepository.BeginTransaction();

                var existingjournalList = _purchaseJournalDetailsRepository.GetAll().Where(o => o.PurchaseJournalDetailsVno == purchaseJournal.PurchaseJournalVno).ToList();
                _purchaseJournalDetailsRepository.DeleteList(existingjournalList);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsTransSno = 0;
                    k.AccountsTransactionsVoucherNo = purchaseJournal.PurchaseJournalVno;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseJournal_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;

                    return k;
                }).ToList();

                _accountTransactionRepository.InsertList(accountsTransactions);
                foreach (var x in purchaseJournal.PurchaseJournalDetails)
                {
                    x.PurchaseJournalDetailsId = 0;
                    x.PurchaseJournalId = purchaseJournal.PurchaseJournalId;
                    x.PurchaseJournalDetailsVno = purchaseJournal.PurchaseJournalVno;
                };
                _purchaseJournalDetailsRepository.InsertList(purchaseJournalDetails);
                _purchaseJournalRepository.Update(purchaseJournal);

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = purchaseJournal.PurchaseJournalVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "PURCHASEVOUCHER";
                _UserTracking.Insert(trackingData);
                _purchaseJournalRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _purchaseJournalRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedPurchaseJournalDetails(purchaseJournal.PurchaseJournalVno);
        }

        public int DeletePurchaseJournal(PurchaseJournal purchaseJournal, List<AccountsTransactions> accountsTransactions, List<PurchaseJournalDetails> purchaseJournalDetails)
        {
            int i = 0;
            try
            {
                _purchaseJournalRepository.BeginTransaction();

                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(purchaseJournal.PurchaseJournalVno, VoucherType.PurchaseJournal_TYPE, _accountTransactionRepository);

                purchaseJournal.PurchaseJournalDelStatus = true;

                purchaseJournalDetails = purchaseJournalDetails.Select((k) =>
                {
                    k.PurchaseJournalDetailsDelStatus = true;
                    return k;
                }).ToList();

                _purchaseJournalDetailsRepository.UpdateList(purchaseJournalDetails);                              

                _purchaseJournalRepository.Update(purchaseJournal);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == purchaseJournal.PurchaseJournalVno).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                trackingData.UserTrackingUserVpNo = purchaseJournal.PurchaseJournalVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "PURCHASEVOUCHER";
                _UserTracking.Insert(trackingData);
                _purchaseJournalRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _purchaseJournalRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public PurchaseJournal InsertPurchaseJournal(PurchaseJournal purchaseJournal, List<AccountsTransactions> accountsTransactions, List<PurchaseJournalDetails> purchaseJournalDetails)
        {
            try
            {
                _purchaseJournalRepository.BeginTransaction();
                string purchaseJournalNumber = this.GenerateVoucherNo(purchaseJournal.PurchaseJournalDate.Value).VouchersNumbersVNo;
                purchaseJournal.PurchaseJournalVno = purchaseJournalNumber;


                long maxcount = _purchaseJournalRepository.GetAsQueryable().Select(o => (long?)o.PurchaseJournalId).Max() ?? 0;

                maxcount += 1;

                purchaseJournal.PurchaseJournalId = maxcount;

                _purchaseJournalRepository.Insert(purchaseJournal);
                //purchaseJournal.PurchaseJournalDetails = purchaseJournal.PurchaseJournalDetails.AsEnumerable().Where(k => k != null).Select(x =>
                foreach (var x in purchaseJournal.PurchaseJournalDetails)
                {
                    x.PurchaseJournalDetailsId = 0;
                    x.PurchaseJournalId = maxcount;
                    x.PurchaseJournalDetailsVno = purchaseJournalNumber;
                };
                _purchaseJournalDetailsRepository.InsertList(purchaseJournalDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = purchaseJournalNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseJournal_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);


                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = purchaseJournal.PurchaseJournalVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "PURCHASEVOUCHER";
                _UserTracking.Insert(trackingData);

                _purchaseJournalRepository.TransactionCommit();
                return this.GetSavedPurchaseJournalDetails(purchaseJournal.PurchaseJournalVno);

            }
            catch (Exception ex)
            {
                _purchaseJournalRepository.TransactionRollback();
                throw ex;
            }

        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<PurchaseJournal> GetPurchaseJournal()
        {
            IEnumerable<PurchaseJournal> purchaseJournal_ALL = _purchaseJournalRepository.GetAll().Where(k => k.PurchaseJournalDelStatus == false || k.PurchaseJournalDelStatus == null);
            return purchaseJournal_ALL;
        }
        public PurchaseJournal GetSavedPurchaseJournalDetails(string pvno)
        {
            PurchaseJournal purchaseJournal = new PurchaseJournal();

            purchaseJournal = _purchaseJournalRepository.GetAsQueryable().Where(k => k.PurchaseJournalVno == pvno).SingleOrDefault();
            purchaseJournal.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.PurchaseJournal_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            purchaseJournal.PurchaseJournalDetails = _purchaseJournalDetailsRepository.GetAsQueryable().Where(x => x.PurchaseJournalDetailsVno == pvno && (x.PurchaseJournalDetailsDelStatus == false || x.PurchaseJournalDetailsDelStatus == null)).ToList();
            return purchaseJournal;


        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseJournal_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseJournal_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                //var prefix = "CN";
                //int vnoMaxVal = 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseJournal_TYPE,
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
