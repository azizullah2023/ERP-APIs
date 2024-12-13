using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Application.MODULE;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Linq;
using AutoMapper.Configuration;


namespace Inspire.Erp.Application.Master.Implementations
{
    public class ProgressiveInoiceService : IProgressiveInvoiceService
    {
        private IRepository<ProgressiveInvoice> _progressiveInvoice;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<ProgressiveInvoiceDetails> _progressiveInvoiceDetails;
        public ProgressiveInoiceService(IRepository<ProgressiveInvoice> progressiveInvoice, IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository, IRepository<VouchersNumbers> voucherNumbersRepository)
        {
            _progressiveInvoice = progressiveInvoice;
            _accountTransactionRepository = accountTransactionRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbersRepository;
        }
        public ProgressiveInvoice InsertProgressInvoice(ProgressiveInvoice progressiveInvoice, List<AccountsTransactions> accountsTransactions)
        {
            try
            {
                _progressiveInvoice.BeginTransaction();
                string puchaseVoucherNumber = this.GenerateVoucherNo(progressiveInvoice.ProgressiveInvoiceInvDate).VouchersNumbersVNo;
                progressiveInvoice.ProgressiveInvoiceInvNo = puchaseVoucherNumber;


                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _progressiveInvoice.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.ProgressiveInvoiceId) + 1);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsTransDate = progressiveInvoice.ProgressiveInvoiceInvDate;
                    k.AccountsTransactionsVoucherNo = puchaseVoucherNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.ProgressiveInvoice_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    k.AccountsTransactionsAllocDebit = 0;
                    k.AccountsTransactionsAllocCredit = (decimal)progressiveInvoice.ProgressiveInvoiceTotAmount;
                    k.AccountsTransactionsFcDebit = 0;
                    k.AccountsTransactionsStatus = "A";
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                _progressiveInvoice.Insert(progressiveInvoice);

                _progressiveInvoice.TransactionCommit();
                return this.GetSavedProgressiveInoices(progressiveInvoice.ProgressiveInvoiceInvNo);

            }
            catch (Exception ex)
            {
                _progressiveInvoice.TransactionRollback();
                throw ex;
            }
        }
        public ProgressiveInvoice UpdateProgressInvoice(ProgressiveInvoice progressiveInvoice, List<AccountsTransactions> accountsTransactions)
        {
            try
            {

                _progressiveInvoice.BeginTransaction();
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(progressiveInvoice.ProgressiveInvoiceInvNo, progressiveInvoice.ProgressiveInvoiceInvoiceType, _accountTransactionRepository);
                _progressiveInvoice.Update(progressiveInvoice);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = progressiveInvoice.ProgressiveInvoiceInvDate;
                        k.AccountsTransactionsVoucherNo = progressiveInvoice.ProgressiveInvoiceInvNo;
                        k.AccountsTransactionsVoucherType = VoucherType.ProgressiveInvoice_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _progressiveInvoice.TransactionCommit();

            }
            catch (Exception ex)
            {
                _progressiveInvoice.TransactionRollback();
                throw ex;
            }

            return this.GetSavedProgressiveInoices(progressiveInvoice.ProgressiveInvoiceInvNo);
        }
        public int DeleteProgressInvoice(ProgressiveInvoice progressiveInvoice, List<AccountsTransactions> accountsTransactions)
        {
            int i = 0;
            try
            {
                _progressiveInvoice.BeginTransaction();

                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(progressiveInvoice.ProgressiveInvoiceInvNo, VoucherType.ProgressiveInvoice_TYPE, _accountTransactionRepository);

                progressiveInvoice.ProgressiveInvoiceDelStatus = true;
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _progressiveInvoice.Update(progressiveInvoice);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == progressiveInvoice.ProgressiveInvoiceInvNo).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                _progressiveInvoice.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _progressiveInvoice.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;
        }
        public ProgressiveInvoice GetSavedProgressiveInoices(string pvno)
        {
            ProgressiveInvoice voucherModel = new ProgressiveInvoice();
            voucherModel = _progressiveInvoice.GetAsQueryable().Where(k => k.ProgressiveInvoiceInvNo == pvno).SingleOrDefault();
            return voucherModel;
        }
        public IQueryable GetProgressInvoiceReport()
        {
            try
            {
                var detailData = _progressiveInvoice.GetAsQueryable().Where(a => a.ProgressiveInvoiceDelStatus != true);
                return detailData;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.ProgressiveInvoice_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.ProgressiveInvoice_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                //var prefix = "CN";
                //int vnoMaxVal = 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.ProgressiveInvoice_TYPE,
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

    }
}
