using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Modals;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class BankReconcilation : IBankReconcilation
    {
        public IRepository<MasterAccountsTable> _MasterAccountsTableRepo;
        public IRepository<AccountsTransactions> _AccountsTransactionsRepo;
        public IRepository<BankReceiptVoucherDetails> _BankReceiptVoucherDetailsRepo;
        public IRepository<ReconciliationVoucherDetails> _ReconciliationVoucherDetailsRepo;
        public IRepository<BankPaymentVoucherDetails> _BankPaymentVoucherDetailsRepo;
        public IRepository<BankLedger> _BankLedgerRepo;
        public IRepository<VouchersNumbers> _voucherNumbersRepository;
        public IRepository<ReconciliationVoucher> _ReconciliationVoucherRepo;


        public BankReconcilation(
            IRepository<MasterAccountsTable> MasterAccountsTable,
            IRepository<AccountsTransactions> AccountsTransactionsRepo,
            IRepository<BankReceiptVoucherDetails> BankReceiptVoucherDetailsRepo,
            IRepository<ReconciliationVoucherDetails> ReconciliationVoucherDetailsRepo,
            IRepository<BankPaymentVoucherDetails> BankPaymentVoucherDetailsRepo,
            IRepository<BankLedger> bank,
            IRepository<VouchersNumbers> voucherNumbersRepository,
            IRepository<ReconciliationVoucher> ReconciliationVoucherRepo

            )
        {
            _BankLedgerRepo = bank;
            _MasterAccountsTableRepo = MasterAccountsTable;
            _AccountsTransactionsRepo = AccountsTransactionsRepo;
            _BankReceiptVoucherDetailsRepo = BankReceiptVoucherDetailsRepo;
            _ReconciliationVoucherDetailsRepo = ReconciliationVoucherDetailsRepo;
            _BankPaymentVoucherDetailsRepo = BankPaymentVoucherDetailsRepo;
            _voucherNumbersRepository = voucherNumbersRepository;
            _ReconciliationVoucherRepo = ReconciliationVoucherRepo;


        }

        public async Task<IEnumerable<BankLedger>> BankLedger(string bankAccNo)
        {
            try
            {
               
                var excludedVouchers = _ReconciliationVoucherDetailsRepo
                                        .GetAsQueryable()
                                        .Where(rv => rv.ReconciliationVoucherDetailsVno != null && rv.ReconciliationVoucherDetailsMatched == "Y")
                                        .Select(rv => rv.ReconciliationVoucherDetailsVno);

                var list = await (from at in _AccountsTransactionsRepo.GetAsQueryable()
                                  join bpvd in _BankPaymentVoucherDetailsRepo.GetAsQueryable()
                                  on at.AccountsTransactionsVoucherNo equals bpvd.BankPaymentVoucherDetailsVoucherNo into bpvdGroup
                                  from bpvd in bpvdGroup.DefaultIfEmpty()
                                  join rvd in _BankReceiptVoucherDetailsRepo.GetAsQueryable()
                                  on at.AccountsTransactionsVoucherNo equals rvd.BankReceiptVoucherDetailsVoucherNo into rvdGroup
                                  from rvd in rvdGroup.DefaultIfEmpty()
                                  where at.AccountsTransactionsAccNo == bankAccNo
                                  where bpvd != null || rvd != null
                                  where !excludedVouchers.Contains(at.AccountsTransactionsVoucherNo)
                                  orderby at.AccountsTransactionsTransDate
                                  select new BankLedger
                                  {
                                      AccountsTransactions_VoucherNo = at.AccountsTransactionsVoucherNo,
                                      AccountsTransactions_Debit = at.AccountsTransactionsDebit,
                                      AccountsTransactions_Credit = at.AccountsTransactionsCredit,
                                      AccountsTransactions_TransSno = at.AccountsTransactionsTransSno,
                                      checkNo = at.AccountsTransactionsCheqNo != null ? (int?)Convert.ToInt32(at.AccountsTransactionsCheqNo) : null,
                                      AccountsTransactions_Description = at.AccountsTransactionsDescription,
                                      AccountsTransactions_TransDate = at.AccountsTransactionsTransDate
                                  }).ToListAsync();


                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ReconciliationVoucher SavebankReconciliation(ReconciliationVoucher reconciliation)
        {
            
                    string bankreconciliationVoucherNumber = this.GenerateVoucherNo(reconciliation.ReconciliationVoucherBankStDate.Value).VouchersNumbersVNo;
                    reconciliation.ReconciliationVoucherId = bankreconciliationVoucherNumber;
                    int maxcount = 0;
                    maxcount = Convert.ToInt32(
                        _ReconciliationVoucherRepo.GetAsQueryable()
                        .DefaultIfEmpty().Max(o => o == null ? 0 : o.ReconciliationVoucherSno) + 1);

                    reconciliation.ReconciliationVoucherSno = maxcount;

                    _ReconciliationVoucherRepo.Insert(reconciliation);
                    
                    reconciliation.ReconciliationVoucherDetails.ForEach((c) =>
                    {
                        c.ReconciliationVoucherDetailsId = bankreconciliationVoucherNumber;
                    });

                    _ReconciliationVoucherDetailsRepo.InsertList(reconciliation.ReconciliationVoucherDetails);
                    return this.getbankReconciliationById(bankreconciliationVoucherNumber);
                
        }
        public ReconciliationVoucher UpdatebankReconciliation(ReconciliationVoucher reconciliation)
        {

             _ReconciliationVoucherRepo.Update(reconciliation);
             _ReconciliationVoucherDetailsRepo.UpdateList(reconciliation.ReconciliationVoucherDetails);
   
            return this.getbankReconciliationById(reconciliation.ReconciliationVoucherId);
               
        }
        public async Task<List<MasterAccountsTable>> GetAccountListForDropdown()
        {
            return   _MasterAccountsTableRepo.GetAsQueryable().Where(m => m.MaRelativeNo == "01001002" && m.MaStatus == "R").ToList();
        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                //var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.BPV_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.ReconciliationVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = "BRE" + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.ReconciliationVoucher_TYPE,
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
        public async Task<Response<List<ReconciliationVoucher>>> getbankReconciliationList()
        {
            List<ReconciliationVoucher> result = new List<ReconciliationVoucher>();

            try
            {
                result = _ReconciliationVoucherRepo.GetAll().ToList();
                
                return Response<List<ReconciliationVoucher>>.Success(result,"Data found");
            }
            catch (Exception ex)
            {
                return Response<List<ReconciliationVoucher>>.Fail(result, ex.Message);
            }
        }
        public ReconciliationVoucher getbankReconciliationById(string Id)
        {
            ReconciliationVoucher obj = new ReconciliationVoucher();

            obj = _ReconciliationVoucherRepo.GetAsQueryable().Where(c => c.ReconciliationVoucherId == Id).SingleOrDefault();
            obj.ReconciliationVoucherDetails = _ReconciliationVoucherDetailsRepo.GetAsQueryable().Where(x => x.ReconciliationVoucherDetailsId == Id).ToList();

            return obj;
        }
    }
}
