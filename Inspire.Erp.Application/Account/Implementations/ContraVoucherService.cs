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
using Inspire.Erp.Infrastructure.Database;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class ContraVoucherService : IContraVoucherService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<ContraVoucher> _contraVoucherRepository;
        private IRepository<ContraVoucherDetails> _contraVoucherDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<UserTracking> _UserTracking;
        private readonly IRepository<UserFile> _UserFile;
        private readonly InspireErpDBContext context;
        public ContraVoucherService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
            IRepository<ContraVoucher> contraVoucherRepository, IRepository<ContraVoucherDetails> contraVoucherDetailsRepository
            , IRepository<UserTracking> UserTracking, IRepository<UserFile> UserFile, InspireErpDBContext _context)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._contraVoucherRepository = contraVoucherRepository;
            this._contraVoucherDetailsRepository = contraVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _UserTracking = UserTracking;
            _UserFile = UserFile;
            context = _context;
        }

        public ContraVoucher UpdateContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails)
        {
            try
            {
                _contraVoucherRepository.BeginTransaction();
                _contraVoucherRepository.Update(contraVoucher);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.ContraVoucher_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _contraVoucherDetailsRepository.UpdateList(contraVoucherDetails);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Update";
                //trackingData.UserTrackingUserVpNo = contraVoucher.ContraVoucherVno;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "CONTRAVOUCHER";
                //_UserTracking.Insert(trackingData);
                _contraVoucherRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _contraVoucherRepository.TransactionRollback();
                throw ex;
            }
            return this.GetSavedContraVoucherDetails(contraVoucher.ContraVoucherVno);
        }

        public int DeleteContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails)
        {
            int i = 0;
            try
            {
                _contraVoucherRepository.BeginTransaction();
                contraVoucher.ContraVoucherDelStatus = true;
                contraVoucherDetails = contraVoucherDetails.Select((k) =>
                {
                    k.ContraVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();
                _contraVoucherDetailsRepository.UpdateList(contraVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _contraVoucherRepository.Update(contraVoucher);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == contraVoucher.ContraVoucherVno).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Delete";
                //trackingData.UserTrackingUserVpNo = contraVoucher.ContraVoucherVno;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "CONTRAVOUCHER";
                //_UserTracking.Insert(trackingData);
                _contraVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _contraVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }

        public ContraVoucher InsertContraVoucher(ContraVoucher contraVoucher, List<AccountsTransactions> accountsTransactions, List<ContraVoucherDetails> contraVoucherDetails)
        {
            try
            {
                _contraVoucherRepository.BeginTransaction();
               // contraVoucher.ContraVoucherVno = contraVoucher.ContraVoucherVno;
                string contraVoucherNumber = this.GenerateVoucherNo(contraVoucher.ContraVoucherDate).VouchersNumbersVNo;
                contraVoucher.ContraVoucherVno = contraVoucherNumber;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _contraVoucherRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.ContraVoucherId) + 1);

                contraVoucher.ContraVoucherId = maxcount;
               
                contraVoucherDetails = contraVoucherDetails.Select((x) =>
                {
                    x.ContraVoucherId = maxcount;
                    x.ContraVoucherDetailsVno = contraVoucher.ContraVoucherVno;
                    return x;
                }).ToList();
                _contraVoucherDetailsRepository.InsertList(contraVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = contraVoucher.ContraVoucherVno;
                    k.AccountsTransactionsVoucherType = VoucherType.ContraVoucher_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _contraVoucherRepository.Insert(contraVoucher);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Insert";
                //trackingData.UserTrackingUserVpNo = contraVoucher.ContraVoucherVno;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "CONTRAVOUCHER";
                //_UserTracking.Insert(trackingData);
                _contraVoucherRepository.TransactionCommit();
                return this.GetSavedContraVoucherDetails(contraVoucher.ContraVoucherVno);
            }
            catch (Exception ex)
            {
                _contraVoucherRepository.TransactionRollback();
                throw ex;
            }
        }

        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }

        public IEnumerable<ContraVoucher> GetContraVoucher()
        {
            IEnumerable<ContraVoucher> contraVoucher_ALL = _contraVoucherRepository.GetAll().Where(k => k.ContraVoucherDelStatus == false || k.ContraVoucherDelStatus == null);
            return contraVoucher_ALL;
        }

        public ContraVoucher GetSavedContraVoucherDetails(string pvno)
        {
            ContraVoucher contraVoucherModel = new ContraVoucher();
            var userTrackngData = (from hdr in context.UserTracking
                                   join dtl in context.UserFile on (long)hdr.UserTrackingUserUserId equals dtl.UserId
                                   where (hdr.UserTrackingUserVpType == "CONTRAVOUCHER" && hdr.UserTrackingUserVpNo == pvno)
                                   select new UserTrackingDisplay()
                                   {
                                       Action = hdr.UserTrackingUserVpAction,
                                       Id = dtl.UserId.ToString(),
                                       VType= hdr.UserTrackingUserVpType,
                                       Date= hdr.UserTrackingUserChangeDt.Value.ToString("dd/MM/yyyy"),
                                       Time = hdr.UserTrackingUserChangeTime.Value.ToString("hh:mm t"),
                                       VNo = hdr.UserTrackingUserVpNo,
                                   }).ToList();
            contraVoucherModel = _contraVoucherRepository.GetAsQueryable().Where(k => k.ContraVoucherVno == pvno).SingleOrDefault();
            contraVoucherModel.UserTrackingData = userTrackngData;
            contraVoucherModel.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno).ToList();
            contraVoucherModel.ContraVoucherDetails = _contraVoucherDetailsRepository.GetAsQueryable().Where(x => x.ContraVoucherDetailsVno == pvno).ToList();
            return contraVoucherModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.ContraVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.ContraVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.ContraVoucher_TYPE,
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
