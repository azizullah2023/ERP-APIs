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
    public class CreditNoteService : ICreditNoteService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<CreditNote> _creditNoteRepository;
        private IRepository<CreditNoteDetails> _creditNoteDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private IRepository<UserTracking> _userTracking;
        public CreditNoteService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
            IRepository<CreditNote> creditNoteRepository, IRepository<CreditNoteDetails> creditNoteDetailsRepository,
            IRepository<UserTracking> userTracking)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._creditNoteRepository = creditNoteRepository;
            this._creditNoteDetailsRepository = creditNoteDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            this._userTracking = userTracking;
        }

        public CreditNote UpdateCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails)
        {
            try
            {
                _creditNoteRepository.BeginTransaction();
                creditNote.CreditNoteDetails = creditNoteDetails.Select((k) =>
                {
                    k.CreditNoteId = creditNote.CreditNoteId;
                    k.CreditNoteDetailsVno = creditNote.CreditNoteVno;
                    return k;
                }).ToList();
                _creditNoteRepository.Update(creditNote);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.CreditNote_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = creditNote.CreditNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "CREDITNOTEVOUCHER";
                _userTracking.Insert(trackingData);
                _creditNoteRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _creditNoteRepository.TransactionRollback();
                throw ex;
            }
            return this.GetSavedCreditNoteDetails(creditNote.CreditNoteVno);
        }

        public int DeleteCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails)
        {
            int i = 0;
            try
            {
                _creditNoteRepository.BeginTransaction();
                creditNote.CreditNoteDelStatus = true;
                creditNoteDetails = creditNoteDetails.Select((k) =>
                {
                    k.CreditNoteDetailsDelStatus = true;
                    return k;
                }).ToList();
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                creditNote.CreditNoteDetails = creditNoteDetails;
                _creditNoteRepository.Update(creditNote);
                _accountTransactionRepository.UpdateList(accountsTransactions);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                trackingData.UserTrackingUserVpNo = creditNote.CreditNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "CREDITNOTEVOUCHER";
                _userTracking.Insert(trackingData);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == creditNote.CreditNoteVno).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                _creditNoteRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _creditNoteRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }

        public CreditNote InsertCreditNote(CreditNote creditNote, List<AccountsTransactions> accountsTransactions, List<CreditNoteDetails> creditNoteDetails)
        {
            try
            {
                _creditNoteRepository.BeginTransaction();
                creditNote.CreditNoteVno = creditNote.CreditNoteVno;
                string creditVno = this.GenerateVoucherNo(creditNote.CreditNoteDate).VouchersNumbersVNo;
                creditNote.CreditNoteVno = creditVno;
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _creditNoteRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.CreditNoteId) + 1);

                creditNote.CreditNoteId = maxcount;
                creditNoteDetails = creditNoteDetails.Select((x) =>
                {
                    x.CreditNoteId = maxcount;
                    x.CreditNoteDetailsVno = creditNote.CreditNoteVno;
                    return x;
                }).ToList();
                _creditNoteDetailsRepository.InsertList(creditNoteDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = creditNote.CreditNoteVno;
                    k.AccountsTransactionsVoucherType = VoucherType.CreditNote_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _creditNoteRepository.Insert(creditNote);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = creditNote.CreditNoteVno;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "CREDITNOTEVOUCHER";
                _userTracking.Insert(trackingData);
                _creditNoteRepository.TransactionCommit();
                return this.GetSavedCreditNoteDetails(creditNote.CreditNoteVno);
            }
            catch (Exception ex)
            {
                _creditNoteRepository.TransactionRollback();
                throw ex;
            }
        }

        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }

        public IEnumerable<CreditNote> GetCreditNote()
        {
            IEnumerable<CreditNote> creditNote_ALL = _creditNoteRepository.GetAll().Where(k => k.CreditNoteDelStatus == false || k.CreditNoteDelStatus == null);
            return creditNote_ALL;
        }

        public CreditNote GetSavedCreditNoteDetails(string pvno)
        {
            CreditNote creditNoteModel = new CreditNote();
            creditNoteModel = _creditNoteRepository.GetAsQueryable().Where(k => k.CreditNoteVno == pvno).SingleOrDefault();
            creditNoteModel.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.CreditNote_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            creditNoteModel.CreditNoteDetails = _creditNoteDetailsRepository.GetAsQueryable().Where(x => x.CreditNoteDetailsVno == pvno && (x.CreditNoteDetailsDelStatus == false || x.CreditNoteDetailsDelStatus == null)).ToList();
            return creditNoteModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.CreditNote_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.CreditNote_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.CreditNote_TYPE,
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
