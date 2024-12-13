using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Extensions.Logging;

namespace Inspire.Erp.Application.StoreWareHouse.Implementation
{
    public class DebitNoteService : IDebitNoteService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<DebitNote> _debitNoteRepository;
        private IRepository<DebitNoteDetails> _debitNoteDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<UserTracking> _UserTracking;


        public DebitNoteService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<DebitNote> debitNoteRepository, IRepository<DebitNoteDetails> debitNoteDetailsRepository,
            IRepository<UserTracking> UserTracking)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._debitNoteRepository = debitNoteRepository;
            this._debitNoteDetailsRepository = debitNoteDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _UserTracking = UserTracking;

        }

        public DebitNote UpdateDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails)
        {
            try
            {
                _debitNoteRepository.BeginTransaction();
                debitNote.DebitNoteDetails = debitNoteDetails.Select((k) =>
                {
                    k.DebitNoteId = debitNote.DebitNoteId;
                    k.DebitNoteDetailsVno = debitNote.DebitNoteVno;
                    return k;
                }).ToList();
                _debitNoteRepository.Update(debitNote);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.DebitNote_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = debitNote.DebitNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "DEBITNOTEVOUCHER";
                _UserTracking.Insert(trackingData);
                _debitNoteRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _debitNoteRepository.TransactionRollback();
                throw ex;
            }
            return this.GetSavedDebitNoteDetails(debitNote.DebitNoteVno);
        }

        public int DeleteDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails)
        {
            int i = 0;
            try
            {
                _debitNoteRepository.BeginTransaction();
                debitNote.DebitNoteDelStatus = true;
                debitNoteDetails = debitNoteDetails.Select((k) =>
                {
                    k.DebitNoteDetailsDelStatus = true;
                    return k;
                }).ToList();
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                debitNote.DebitNoteDetails = debitNoteDetails;
                _debitNoteRepository.Update(debitNote);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                trackingData.UserTrackingUserVpNo = debitNote.DebitNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "DEBITNOTEVOUCHER";
                _UserTracking.Insert(trackingData);
                 var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == debitNote.DebitNoteVno).FirstOrDefault();
                  _voucherNumbersRepository.Update(vchrnumer);
                _debitNoteRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _debitNoteRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }
        public DebitNote InsertDebitNote(DebitNote debitNote, List<AccountsTransactions> accountsTransactions, List<DebitNoteDetails> debitNoteDetails)
        {
            try
            {
                _debitNoteRepository.BeginTransaction();
                //debitNote.DebitNoteVno = debitNote.DebitNoteVno;
                string DebitNoteName = this.GenerateVoucherNo(debitNote.DebitNoteDate).VouchersNumbersVNo;
                debitNote.DebitNoteVno = DebitNoteName;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _debitNoteRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.DebitNoteId) + 1);

                debitNote.DebitNoteId = maxcount;
                debitNoteDetails = debitNoteDetails.Select((x) =>
                {
                    x.DebitNoteId = maxcount;
                    x.DebitNoteDetailsVno = debitNote.DebitNoteVno;
                    return x;
                }).ToList();
                _debitNoteDetailsRepository.InsertList(debitNoteDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = debitNote.DebitNoteVno;
                    k.AccountsTransactionsVoucherType = VoucherType.DebitNote_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _debitNoteRepository.Insert(debitNote);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = debitNote.DebitNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "DEBITNOTEVOUCHER";
                _UserTracking.Insert(trackingData);
                _debitNoteRepository.TransactionCommit();
                return this.GetSavedDebitNoteDetails(debitNote.DebitNoteVno);
            }
            catch (Exception ex)
            {
                _debitNoteRepository.TransactionRollback();
                throw ex;
            }
        }

        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }

        public IEnumerable<DebitNote> GetDebitNote()
        {
            IEnumerable<DebitNote> debitNote_ALL = _debitNoteRepository.GetAll().Where(k => k.DebitNoteDelStatus == false || k.DebitNoteDelStatus == null);
            return debitNote_ALL;
        }

        public DebitNote GetSavedDebitNoteDetails(string pvno)
        {
            DebitNote debitNoteModel = new DebitNote();
            debitNoteModel = _debitNoteRepository.GetAsQueryable().Where(k => k.DebitNoteVno == pvno).SingleOrDefault();
            debitNoteModel.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.DebitNote_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            debitNoteModel.DebitNoteDetails = _debitNoteDetailsRepository.GetAsQueryable().Where(x => x.DebitNoteDetailsVno == pvno && (x.DebitNoteDetailsDelStatus == false || x.DebitNoteDetailsDelStatus == null)).ToList();
            return debitNoteModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.DebitNote_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.DebitNote_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.DebitNote_TYPE,
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