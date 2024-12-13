using Inspire.Erp.Application.Account.Interfaces;
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
    public class OBVoucherService : IOBVoucherService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<OpeningVoucherMaster> _obVoucherRepository;
        private IRepository<OpeningVoucherDetails> _obVoucherDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<OBVoucherService> _logger;

        public OBVoucherService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<OBVoucherService> logger,
            IRepository<OpeningVoucherMaster> obVoucherRepository, IRepository<OpeningVoucherDetails> obVoucherDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._obVoucherRepository = obVoucherRepository;
            this._obVoucherDetailsRepository = obVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _logger = logger;

        }

        public OBVoucherModel UpdateOBVoucher(OpeningVoucherMaster openingBalanceVoucher, List<AccountsTransactions> accountsTransactions,
                                                        List<OpeningVoucherDetails> openingVoucherDetails)
        {

            try
            {
                _obVoucherRepository.BeginTransaction();

                _obVoucherRepository.Update(openingBalanceVoucher);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.OBVoucher;
                        k.AccountsTransactionsVoucherNo = "OB"+ openingBalanceVoucher.OpeningVoucherMasterId;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _obVoucherDetailsRepository.UpdateList(openingVoucherDetails);
                _obVoucherRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _obVoucherRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedOBVoucherDetails(Convert.ToString(openingBalanceVoucher.OpeningVoucherMasterId));
        }

        public int DeleteOBVoucher(OpeningVoucherMaster openingVoucher, List<AccountsTransactions> accountsTransactions, List<OpeningVoucherDetails> openingVoucherDetails)
        {
            int i = 0;
            try
            {
                _obVoucherRepository.BeginTransaction();

                openingVoucher.OpeningVoucherMasterDelStatus = true;

                openingVoucherDetails = openingVoucherDetails.Select((k) => {
                    k.OpeningVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();

                _obVoucherDetailsRepository.UpdateList(openingVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) => {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                _obVoucherRepository.Update(openingVoucher);

                ////var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == openingVoucher.OpeningVoucherMasterId).FirstOrDefault();

                ////_voucherNumbersRepository.Update(vchrnumer);

                _obVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _obVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public OBVoucherModel InsertOBVoucher(OpeningVoucherMaster openingVoucher, List<AccountsTransactions> accountsTransactions,
                                                        List<OpeningVoucherDetails> openingVoucherDetails)
        {
            try
            {
                _obVoucherRepository.BeginTransaction();

                ////int OBtVoucherNumber = (openingVoucher.OpeningVoucherMasterId == null || openingVoucher.OpeningVoucherMasterId == 0) ?
                ////              this.GenerateVoucherNo(openingVoucher.OpeningVoucherMasterOpBDate.Value).VouchersNumbersVNo :  openingVoucher.OpeningVoucherMasterId;
                ////openingVoucher.OpeningVoucherMasterId = OBtVoucherNumber;

                             
                int mxc=0;
                mxc = (int)_obVoucherRepository.GetAsQueryable().Where(k => k.OpeningVoucherMasterId != null)
                            .DefaultIfEmpty()
                            .Max(o => o == null ? 0 : o.OpeningVoucherMasterId) + 1;

             


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = "OB" + Convert.ToString( mxc),
                    VouchersNumbersVNoNu = mxc,
                    VouchersNumbersVType = VoucherType.OBVoucher,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = openingVoucher.OpeningVoucherMasterOpBDate.Value

                };
                _voucherNumbersRepository.Insert(vouchersNumbers);


                openingVoucher.OpeningVoucherMasterId = mxc;

                ////string vNo = "";
                openingVoucherDetails = openingVoucherDetails.Select((x) => {
                    x.OpeningVoucherDetailsId = 0;
                    x.OpeningVoucherDetailsOpId = "OB" + Convert.ToString( mxc);
                    ////vNo = x.OpeningVoucherDetailsVoucherNo;
                    return x;
                }).ToList();
                _obVoucherDetailsRepository.InsertList(openingVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) => {
                    k.AccountsTransactionsLpoNo = Convert.ToString(mxc);
                    k.AccountsTransactionsVoucherType = VoucherType.OBVoucher;
                    k.AccountsTransactionsVoucherNo = vouchersNumbers.VouchersNumbersVNo; 
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _obVoucherRepository.Insert(openingVoucher);
                _obVoucherRepository.TransactionCommit();
               
                return this.GetSavedOBVoucherDetails(Convert.ToString(openingVoucher.OpeningVoucherMasterId));

            }
            catch (Exception ex)
            {
                _obVoucherRepository.TransactionRollback();
                throw ex;
            }
        
        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<OpeningVoucherMaster> GetOBVouchers()
        {
            IEnumerable<OpeningVoucherMaster> openingVouchers = _obVoucherRepository.GetAll().Where(k => k.OpeningVoucherMasterDelStatus == false || k.OpeningVoucherMasterDelStatus == null);
            return openingVouchers;
        }
        public OBVoucherModel GetSavedOBVoucherDetails(string pvno)
        {
            if (_voucherNumbersRepository.GetAsQueryable().Any(x => x.VouchersNumbersVNo == "OB" + pvno))
            {
                OBVoucherModel voucherdata = _voucherNumbersRepository.GetAsQueryable().Where(x => x.VouchersNumbersVNo == "OB" + pvno)
                                                .Include(k => k.AccountsTransactions)
                                                .Include(k => k.OpeningVoucherDetails).AsEnumerable()
                                                .Select((k) => new OBVoucherModel
                                                {
                                                    accountsTransactions = k.AccountsTransactions.Where(x => x.AccountstransactionsDelStatus == false).ToList(),
                                                    openingVoucherDetails = k.OpeningVoucherDetails.Where(x => x.OpeningVoucherDetailsDelStatus == false).ToList()
                                                })
                                                .SingleOrDefault();
                voucherdata.openingBalanceVoucher = _obVoucherRepository.GetAsQueryable().Where(k => k.OpeningVoucherMasterId == Convert.ToInt32(pvno)).FirstOrDefault();
                return voucherdata;

            }
            return null;

        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                ////var vno=  this._paymentVoucherRepository.GetAsQueryable().Where(k => k.PaymentVoucherVoucherNo == null).FirstOrDefault();

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PV_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.OBVoucher)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.OBVoucher,
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
