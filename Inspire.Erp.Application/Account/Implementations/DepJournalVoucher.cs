using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Infrastructure.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class DepJournalVoucher: IDepJournalVoucher
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<JournalVoucher> _journalVoucherRepository;
        private IRepository<JournalVoucherDetails> _journalVoucherDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<UserTracking> _UserTracking;
        private readonly IRepository<UserFile> _UserFile;
        private readonly InspireErpDBContext context;

        public DepJournalVoucher(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger, IRepository<UserTracking> UserTracking, IRepository<UserFile> UserFile,
            IRepository<JournalVoucher> journalVoucherRepository, IRepository<JournalVoucherDetails> journalVoucherDetailsRepository,
            InspireErpDBContext _context)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._journalVoucherRepository = journalVoucherRepository;
            this._journalVoucherDetailsRepository = journalVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _UserTracking = UserTracking;
            _UserFile = UserFile;
            context = _context;

        }
        public JournalVoucher UpdateDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails)
        {

            try
            {
                _journalVoucherRepository.BeginTransaction();
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = "DEPJV";
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _journalVoucherDetailsRepository.UpdateList(journalVoucherDetails);
                _journalVoucherRepository.Update(journalVoucher);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Update";
                //trackingData.UserTrackingUserVpNo = journalVoucher.JournalVoucherVrefNo;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "DEPJV";
                //_UserTracking.Insert(trackingData);
                _journalVoucherRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _journalVoucherRepository.TransactionRollback();
                throw ex;
            }
            return this.GetSavedDepJournalVoucherDetails(journalVoucher.JournalVoucherVrefNo);
        }

        public int DeleteDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails)
        {
            int i = 0;
            try
            {
                _journalVoucherRepository.BeginTransaction();
                journalVoucher.JournalVoucherDelStatus = true;
                journalVoucherDetails = journalVoucherDetails.Select((k) =>
                {
                    k.JournalVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();
                _journalVoucherDetailsRepository.UpdateList(journalVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _journalVoucherRepository.Update(journalVoucher);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == journalVoucher.JournalVoucherVrefNo).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Delete";
                //trackingData.UserTrackingUserVpNo = journalVoucher.JournalVoucherVrefNo;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "DEPJV";
                //_UserTracking.Insert(trackingData);
                _journalVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _journalVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }
        public JournalVoucher InsertDepJournalVoucher(JournalVoucher journalVoucher, List<AccountsTransactions> accountsTransactions, List<JournalVoucherDetails> journalVoucherDetails)
        {
            try
            {
                _journalVoucherRepository.BeginTransaction();
                string jvno = this.GenerateVoucherNo(journalVoucher.JournalVoucherDate).VouchersNumbersVNo;
                journalVoucher.JournalVoucherVrefNo = jvno;
                journalVoucher.JournalVoucherType = "DEPJV";
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _journalVoucherRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.JournalVoucherId) + 1);

                //journalVoucher.JournalVoucherId = maxcount;
                _journalVoucherRepository.Insert(journalVoucher);
                journalVoucherDetails = journalVoucherDetails.Select((x) =>
                {
                    x.JournalVoucherID = maxcount;
                    x.JournalVoucherDetailVrefNo = journalVoucher.JournalVoucherVrefNo;
                    return x;
                }).ToList();
                _journalVoucherDetailsRepository.InsertList(journalVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = journalVoucher.JournalVoucherVrefNo;
                    k.AccountsTransactionsVoucherType = "DEPJV";
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Insert";
                //trackingData.UserTrackingUserVpNo = journalVoucher.JournalVoucherVrefNo;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "DEPJV";
                //_UserTracking.Insert(trackingData);
                _journalVoucherRepository.TransactionCommit();
                return this.GetSavedDepJournalVoucherDetails(journalVoucher.JournalVoucherVrefNo);

            }
            catch (Exception ex)
            {
                _journalVoucherRepository.TransactionRollback();
                throw ex;
            }
        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<JournalVoucher> GetDepJournalVoucher()
        {
            IEnumerable<JournalVoucher> journalVoucher_ALL = _journalVoucherRepository.GetAll().Where(k =>( k.JournalVoucherDelStatus == false || k.JournalVoucherDelStatus == null) && k.JournalVoucherType == "DEPJV");
            return journalVoucher_ALL;
        }
        public JournalVoucher GetSavedDepJournalVoucherDetails(string pvno)
        {
            JournalVoucher journalVoucherModel = new JournalVoucher();
            journalVoucherModel = _journalVoucherRepository.GetAsQueryable().Where(k => k.JournalVoucherVrefNo == pvno).SingleOrDefault();
            journalVoucherModel.JournalVoucherDetails = _journalVoucherDetailsRepository.GetAsQueryable().Where(x => x.JournalVoucherDetailVrefNo == pvno && x.JournalVoucherDetailsDelStatus == false).ToList();
            journalVoucherModel.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(x => x.AccountsTransactionsVoucherNo == pvno && x.AccountstransactionsDelStatus == false).ToList();
            return journalVoucherModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.Dep_JournalVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.Dep_JournalVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                Console.WriteLine("************************************* = " + prefix + vnoMaxVal);
                var voucherNo = prefix + vnoMaxVal;
                var voucherNumber = _voucherNumbersRepository.GetAll().FirstOrDefault(o => o.VouchersNumbersVNo == voucherNo);
                if (voucherNo != null)
                {
                    vnoMaxVal = vnoMaxVal + 1;
                    VouchersNumbers vouchersNumbers = new VouchersNumbers
                    {
                        VouchersNumbersVNo = prefix + vnoMaxVal,
                        VouchersNumbersVNoNu = vnoMaxVal,
                        VouchersNumbersVType = "DEPJV",
                        VouchersNumbersFsno = 1,
                        VouchersNumbersStatus = AccountStatus.Pending,
                        VouchersNumbersVoucherDate = DateTime.Now

                    };
                    _voucherNumbersRepository.Insert(vouchersNumbers);
                    return vouchersNumbers;
                }
                else
                {
                    VouchersNumbers vouchersNumbers = new VouchersNumbers
                    {
                        VouchersNumbersVNo = prefix + vnoMaxVal,
                        VouchersNumbersVNoNu = vnoMaxVal,
                        VouchersNumbersVType = VoucherType.Dep_JournalVoucher_TYPE,
                        VouchersNumbersFsno = 1,
                        VouchersNumbersStatus = AccountStatus.Pending,
                        VouchersNumbersVoucherDate = DateTime.Now

                    };
                    _voucherNumbersRepository.Insert(vouchersNumbers);
                    return vouchersNumbers;
                }
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

        public IQueryable GetJVTRacking(string JournalVoucher_VNO)
        {
            try
            {
                var userTrackngData = (from hdr in context.UserTracking
                                       join dtl in context.UserFile on (long)hdr.UserTrackingUserUserId equals dtl.UserId
                                       where (hdr.UserTrackingUserVpType == "DEPJV" && hdr.UserTrackingUserVpNo == JournalVoucher_VNO)
                                       select new
                                       {
                                           VPAction = hdr.UserTrackingUserVpAction,
                                           UserId = dtl.UserId,
                                           VPType = hdr.UserTrackingUserVpType,
                                           ChangeDt = hdr.UserTrackingUserChangeDt,
                                           ChangeTime = hdr.UserTrackingUserChangeTime,
                                           VPNo = hdr.UserTrackingUserVpNo,
                                       }).AsQueryable();
                return userTrackngData;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
