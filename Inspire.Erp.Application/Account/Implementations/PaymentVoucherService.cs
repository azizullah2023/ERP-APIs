using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace Inspire.Erp.Application.Account.Implementations
{
    public class PaymentVoucherService : IPaymentVoucherService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PaymentVoucher> _paymentVoucherRepository;
        private IRepository<PaymentVoucherDetails> _paymentVoucherDetailsRepository;
        private IRepository<Domain.Entities.ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<UserTracking> UserTracking;
        private IRepository<UserFile> UserFile;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly InspireErpDBContext context;
        private readonly IRepository<AllocationVoucherDetails> _AllocationVoucherDetails;
        private readonly IRepository<AllocationVoucher> _voucherAllocation;
        private readonly IRepository<CurrencyMaster> _currencyMaster;

        public PaymentVoucherService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<Domain.Entities.ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger, IRepository<UserFile> _UserFile, IRepository<UserTracking> _UserTracking,
            IRepository<PaymentVoucher> paymentVoucherRepository, IRepository<PaymentVoucherDetails> paymentVoucherDetailsRepository,
            InspireErpDBContext _context, IRepository<AllocationVoucherDetails> AllocationVoucherDetail, IRepository<AllocationVoucher> voucherAllocation, IRepository<CurrencyMaster> currencyMaster)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._paymentVoucherRepository = paymentVoucherRepository;
            this._paymentVoucherDetailsRepository = paymentVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _logger = logger;
            UserTracking = _UserTracking;
            UserFile = _UserFile;
            context = _context;
            _AllocationVoucherDetails = AllocationVoucherDetail;
            _voucherAllocation = voucherAllocation;
            _currencyMaster = currencyMaster;
        }

        public PaymentVoucher UpdatePaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions,
                                                        List<PaymentVoucherDetails> paymentVoucherDetails, List<AllocationDetails> payAllo)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    //_paymentVoucherRepository.BeginTransaction();

                    _paymentVoucherRepository.Update(paymentVoucher);
                    accountsTransactions = accountsTransactions.Select((k) =>
                    {
                        if (k.AccountsTransactionsTransSno == 0)
                        {
                            k.AccountsTransactionsVoucherType = VoucherType.Payment;
                            k.AccountsTransactionsStatus = AccountStatus.Approved;
                        }

                        return k;
                    }).ToList();
                    _accountTransactionRepository.UpdateList(accountsTransactions);
                    _paymentVoucherDetailsRepository.UpdateList(paymentVoucherDetails);

                    //UserTracking trackingData = new UserTracking();
                    //trackingData.UserTrackingUserUserId = 1;
                    //trackingData.UserTrackingUserVpAction = "Update";
                    //trackingData.UserTrackingUserVpNo = paymentVoucher.PaymentVoucherVoucherNo;
                    //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                    //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                    //trackingData.UserTrackingUserVpType = "PAYMENTVOUCHER";
                    //UserTracking.Insert(trackingData);
                    ////voucher allocation details
                    //insertAllocations(paymentVoucher, payAllo);

                    scope.Complete();
                    //_paymentVoucherRepository.TransactionCommit();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    //_paymentVoucherRepository.TransactionRollback();
                    throw ex;
                }
            }
            return this.GetSavedPaymentDetails(paymentVoucher.PaymentVoucherVoucherNo);
        }
        private void insertAllocations(PaymentVoucher payVoucher, List<AllocationDetails> payAllo)
        {
            var avNo = $"AV {getMaxVoucherAllocation()}";
            var alloModel = new AllocationVoucher()
            {
                AllocationVoucherId = 0,
                AllocationVoucherType = 1,
                AllocationVoucherVoucherNo = avNo,
                AllocationVoucherVoucherDate = DateTime.Now.Date,
                AllocationVoucherDescription = payVoucher.PaymentVoucherNarration,
                AllocationVoucherVoucherAccNo = payVoucher.PaymentVoucherCrAcNo,
                AllocationVoucherVoucherAccType = "ALLOCATION",
                AllocationVoucherVoucherFsno = payVoucher.PaymentVoucherFsno,
                AllocationVoucherStatus = "A",
                AllocationVoucherUserId = payVoucher.PaymentVoucherUserId,
                AllocationVoucherAvStatus = "B",
                AllocationVoucherRefVno = payVoucher.PaymentVoucherVoucherNo,
                AllocationVoucherRefVtype = 1,//"RECEIPT",
                AllocationVoucherLocationId = 1,
                AllocationVoucherVcreation = "AccForm",
                AllocationVoucherDelStatus = false,


            };
            alloModel.VoucherAllocationDetails = new List<AllocationVoucherDetails>();
            foreach (var item in payAllo)
            {
                alloModel.VoucherAllocationDetails.Add(new AllocationVoucherDetails()
                {
                    //AllocationVoucherDetailsId =
                    AllocationVoucherDetailsVno = avNo,
                    AllocationVoucherDetailsVtype = 1,
                    AllocationVoucherDetailsVFsno = payVoucher.PaymentVoucherFsno,
                    AllocationVoucherDetailsVLocationId = 1,
                    AllocationVoucherDetailsTransSno = (int)item.TransNo,
                    AllocationVoucherDetailsAccNo = payVoucher.PaymentVoucherCrAcNo,
                    AllocationVoucherDetailsAllocDebit = item.Debit == null ?  0 : (double) item.Debit,
                    AllocationVoucherDetailsAllocCredit = item.Credit== null ? 0 : (double)item.Debit,
                    AllocationVoucherDetailsFcAllocDebit = item.Debit== null ? 0 : (double)item.Debit * 1, // currency rate
                    AllocationVoucherDetailsFcAllocCredit = item.Credit== null ? 0 : (double)item.Debit * 1, // currency rate
                    AllocationVoucherDetailsRefVno = payVoucher.PaymentVoucherVoucherNo,
                    AllocationVoucherDetailsRefVtype = "ALLOCATION",
                    AllocationVoucherDetailsRefLocationId = 1,
                    AllocationVoucherDetailsRefFsno = 1,
                    AllocationVoucherDetailsDelStatus = false,
                    AllocationVoucherDetailsNetAllocation = (double)item.NetAllocation

                });
            }
            AddEditVoucherAllocation(alloModel);
        }
        public void AddEditVoucherAllocation(AllocationVoucher model)
        {
            model.AllocationVoucherId = getMaxVoucherAllocation();
            _voucherAllocation.Insert(model);

            if (model.VoucherAllocationDetails != null && model.VoucherAllocationDetails.Count() > 0)
            {
                var vouchetAlloc = _voucherAllocation.GetAll().LastOrDefault();
                Console.WriteLine("************************************************** = " + vouchetAlloc.AllocationVoucherId);
                List<AllocationVoucherDetails> orderDetails = new List<AllocationVoucherDetails>();

                foreach (var x in model.VoucherAllocationDetails)
                {
                    orderDetails.Add(
                    new AllocationVoucherDetails
                    {
                        AllocationVoucherDetailsVno = x.AllocationVoucherDetailsVno,
                        AllocationVoucherDetailsAllocDebit = x.AllocationVoucherDetailsAllocDebit > 0 ? x.AllocationVoucherDetailsNetAllocation : 0.0,
                        AllocationVoucherDetailsAllocCredit = x.AllocationVoucherDetailsAllocCredit > 0 ? x.AllocationVoucherDetailsNetAllocation : 0.0,
                        AllocationVoucherDetailsVtype = 1,
                        AllocationVoucherDetailsDelStatus = false,
                        AllocationVoucherDetailsSno = Convert.ToInt32(vouchetAlloc.AllocationVoucherId),
                        AllocationVoucherDetailsVFsno = model.AllocationVoucherVoucherFsno,
                        AllocationVoucherDetailsVLocationId = model.AllocationVoucherLocationId,
                        AllocationVoucherDetailsTransSno = (int)x.AccountsTransactionsTransSno,
                        AllocationVoucherDetailsAccNo = model.AllocationVoucherVoucherAccNo,
                        AllocationVoucherDetailsFcAllocDebit = x.AllocationVoucherDetailsAllocDebit > 0 ? x.AllocationVoucherDetailsNetAllocation * 1 : 0.0, //==>currency rate
                        AllocationVoucherDetailsFcAllocCredit = x.AllocationVoucherDetailsAllocCredit > 0 ? x.AllocationVoucherDetailsNetAllocation * 1 : 0.0,
                        AllocationVoucherDetailsRefVno = model.AllocationVoucherRefVno,
                        AllocationVoucherDetailsRefVtype = x.AllocationVoucherDetailsRefVtype,
                        AllocationVoucherDetailsRefLocationId = x.AllocationVoucherDetailsRefLocationId,
                        AllocationVoucherDetailsRefFsno = x.AllocationVoucherDetailsRefFsno,
                        //AllocationVoucherDetailsId = getMaxVoucherAllocationDetails()
                    });

                    AccountsTransactions acc = _accountTransactionRepository.GetAsQueryable().Where(x => x.AccountsTransactionsTransSno == x.AccountsTransactionsTransSno).FirstOrDefault();
                    acc.AccountsTransactionsAllocCredit = acc.AccountsTransactionsAllocCredit + (decimal)x.AllocationVoucherDetailsNetAllocation;
                    acc.AccountsTransactionsFcAllocCredit = acc.AccountsTransactionsFcAllocCredit + (decimal)x.AllocationVoucherDetailsNetAllocation;
                    acc.AccountsTransactionsAllocBalance = acc.AccountsTransactionsCredit - (acc.AccountsTransactionsAllocCredit + (decimal)x.AllocationVoucherDetailsNetAllocation);
                    acc.AccountsTransactionsFcAllocBalance = acc.AccountsTransactionsFcCredit - (acc.AccountsTransactionsFcAllocCredit + (decimal)x.AllocationVoucherDetailsNetAllocation);
                    _accountTransactionRepository.Update(acc);



                }

                _AllocationVoucherDetails.InsertList(orderDetails);
                _AllocationVoucherDetails.SaveChangesAsync();

                //here need to update the accounts transaction table for allocation
                foreach (var item in model.VoucherAllocationDetails)
                {
                    if (item.AllocationVoucherDetailsTransSno == 0) continue;
                    AccountsTransactions acc = _accountTransactionRepository.GetAsQueryable().Where(x => x.AccountsTransactionsTransSno == item.AllocationVoucherDetailsTransSno).FirstOrDefault();
                    var currency = _currencyMaster.GetAsQueryable().Where(c => c.CurrencyMasterCurrencyId == acc.AccountsTransactionsCurrencyId).Distinct().FirstOrDefault();
                    decimal cRate = currency == null ? 1 : currency.CurrencyMasterCurrencyRate == null ? 1 : (decimal)currency.CurrencyMasterCurrencyRate;
                    if (acc != null)
                    {
                        if (acc.AccountsTransactionsDebit > 0)
                        {

                            decimal debit = acc.AccountsTransactionsDebit == null ? 0 : (decimal)acc.AccountsTransactionsDebit;
                            decimal alloDebit = acc.AccountsTransactionsAllocDebit == null ? 0 : (decimal)acc.AccountsTransactionsAllocDebit;
                            decimal fc_alloDebit = acc.AccountsTransactionsFcAllocDebit == null ? 0 : (decimal)acc.AccountsTransactionsFcAllocDebit;
                            decimal fcdebit = acc.AccountsTransactionsFcDebit == null ? 0 : (decimal)acc.AccountsTransactionsFcDebit;

                            acc.AccountsTransactionsAllocBalance = debit - (alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation);
                            acc.AccountsTransactionsAllocDebit = alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation;
                            acc.AccountsTransactionsFcAllocDebit = fc_alloDebit + (decimal)item.AllocationVoucherDetailsNetAllocation * cRate;
                            acc.AccountsTransactionsFcAllocBalance = fcdebit - (fc_alloDebit + ((decimal)item.AllocationVoucherDetailsNetAllocation) * cRate);

                        }
                        else if (acc.AccountsTransactionsCredit > 0)
                        {
                            decimal credit = acc.AccountsTransactionsCredit == null ? 0 : (decimal)acc.AccountsTransactionsCredit;
                            decimal alloCredit = acc.AccountsTransactionsAllocCredit == null ? 0 : (decimal)acc.AccountsTransactionsAllocCredit;
                            decimal fc_alloCredit = acc.AccountsTransactionsFcAllocCredit == null ? 0 : (decimal)acc.AccountsTransactionsFcAllocCredit;
                            decimal fcCredit = acc.AccountsTransactionsFcCredit == null ? 0 : (decimal)acc.AccountsTransactionsFcCredit;

                            acc.AccountsTransactionsAllocBalance = credit - (alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation);
                            acc.AccountsTransactionsAllocCredit = alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation;
                            acc.AccountsTransactionsFcAllocCredit = fc_alloCredit + (decimal)item.AllocationVoucherDetailsNetAllocation * cRate;
                            acc.AccountsTransactionsFcAllocBalance = fcCredit - (fc_alloCredit + ((decimal)item.AllocationVoucherDetailsNetAllocation) * cRate);
                        }
                        _accountTransactionRepository.Update(acc);

                    }
                }
            }
        }
        private int getMaxVoucherAllocation()
        {
            try
            {
                int? maxValue = _voucherAllocation.GetAll().Max(x => x.AllocationVoucherId);
                int? incrementedValue = maxValue.HasValue ? maxValue + 1 : 1;
                return (int)incrementedValue;
            }
            catch
            {
                return 1;
            }
        }

        public int DeletePaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions, List<PaymentVoucherDetails> paymentVoucherDetails)
        {
            int i = 0;
            try
            {
                _paymentVoucherRepository.BeginTransaction();
                paymentVoucher.PaymentVoucherDelStatus = true;
                paymentVoucherDetails = paymentVoucherDetails.Select((k) =>
                {
                    k.PaymentVocherDetailsDelStatus = true;
                    return k;
                }).ToList();
                _paymentVoucherDetailsRepository.UpdateList(paymentVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _paymentVoucherRepository.Update(paymentVoucher);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == paymentVoucher.PaymentVoucherVoucherNo).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Delete";
                //trackingData.UserTrackingUserVpNo = paymentVoucher.PaymentVoucherVoucherNo;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "PAYMENTVOUCHER";
                //UserTracking.Insert(trackingData);

                _paymentVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _paymentVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }
        public PaymentVoucher InsertPaymentVoucher(PaymentVoucher paymentVoucher, List<AccountsTransactions> accountsTransactions,
                                                        List<PaymentVoucherDetails> paymentVoucherDetails, List<AllocationDetails> paymentAllocation)
        {
            try
            {
                _paymentVoucherRepository.BeginTransaction();
                string paymentVoucherNumber = this.GenerateVoucherNo().VouchersNumbersVNo;
                paymentVoucher.PaymentVoucherVoucherNo = paymentVoucherNumber;
                paymentVoucher.PaymentVoucherFcCrAmount = paymentVoucher.PaymentVoucherFcCrAmount ?? 0;
                paymentVoucher.PaymentVoucherCrAmount = paymentVoucher.PaymentVoucherCrAmount?? 0;
                paymentVoucher.PaymentVoucherFcDbAmount = paymentVoucher.PaymentVoucherDbAmount ?? 0;
                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _paymentVoucherRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.PaymentVoucherSno) + 1);

                //paymentVoucher.PaymentVoucherVoucherNo = paymentVoucher.PaymentVoucherVoucherNo;
                paymentVoucherDetails = paymentVoucherDetails.Select((x) =>
                {
                    x.PaymentVoucherDetailsSno = maxcount;
                    x.PaymentVoucherDetailsVoucherNo = paymentVoucher.PaymentVoucherVoucherNo;
                    x.PaymentVoucherDetailsNarration = paymentVoucher.PaymentVoucherNarration;
                    x.PaymentVoucherDetailsCrAmount = x.PaymentVoucherDetailsCrAmount ?? 0;
                    x.PaymentVoucherDetailsCrFcAmount = x.PaymentVoucherDetailsCrFcAmount ?? 0;
                    return x;
                }).ToList();
                _paymentVoucherDetailsRepository.InsertList(paymentVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = paymentVoucher.PaymentVoucherVoucherNo;
                    k.AccountsTransactionsVoucherType = VoucherType.Payment;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _paymentVoucherRepository.Insert(paymentVoucher);
                //UserTracking trackingData = new UserTracking();
                //trackingData.UserTrackingUserUserId = 1;
                //trackingData.UserTrackingUserVpAction = "Insert";
                //trackingData.UserTrackingUserVpNo = paymentVoucher.PaymentVoucherVoucherNo;
                //trackingData.UserTrackingUserChangeDt = DateTime.Now;
                //trackingData.UserTrackingUserChangeTime = DateTime.Now;
                //trackingData.UserTrackingUserVpType = "PAYMENTVOUCHER";
                //UserTracking.Insert(trackingData);

                if(paymentAllocation.Count > 0)
                    insertAllocations(paymentVoucher, paymentAllocation);



                _paymentVoucherRepository.TransactionCommit();
                return this.GetSavedPaymentDetails(paymentVoucher.PaymentVoucherVoucherNo);
            }
            catch (Exception ex)
            {
                _paymentVoucherRepository.TransactionRollback();
                throw ex;
            }
        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<PaymentVoucher> GetPaymentVouchers()
        {
            IEnumerable<PaymentVoucher> paymentVouchers = _paymentVoucherRepository.GetAll().Where(k => k.PaymentVoucherDelStatus == false || k.PaymentVoucherDelStatus == null).ToList();
            return paymentVouchers;
        }
        public PaymentVoucher GetSavedPaymentDetails(string pvno)
        {
            if (_voucherNumbersRepository.GetAsQueryable().Any(x => x.VouchersNumbersVNo == pvno))
            {
                PaymentVoucher voucherdata = new PaymentVoucher();
                // _voucherNumbersRepository.GetAsQueryable().Where(x => x.VouchersNumbersVNo == pvno)
                //                                 .Include(k => k.AccountsTransactions)
                //                                 .Include(k => k.PaymentVoucherDetails).AsEnumerable()
                //                                 .Select((k) => new PaymentVoucher
                //                                 {
                //                                     AccountsTransactions = k.AccountsTransactions.Where(x => x.AccountstransactionsDelStatus == false).ToList(),
                //                                     PaymentVoucherDetails = k.PaymentVoucherDetails.Where(x => x.PaymentVocherDetailsDelStatus == false).ToList(),
                //                                 })
                //                                 .SingleOrDefault();
                voucherdata = _paymentVoucherRepository.GetAsQueryable().Where(k => k.PaymentVoucherVoucherNo == pvno).FirstOrDefault();
                voucherdata.AccountsTransactions = _accountTransactionRepository.GetAll().Where(x => x.AccountsTransactionsVoucherNo == pvno && x.AccountstransactionsDelStatus == false).ToList();
                voucherdata.PaymentVoucherDetails = _paymentVoucherDetailsRepository.GetAll().Where(x => x.PaymentVoucherDetailsVoucherNo == pvno && x.PaymentVocherDetailsDelStatus == false).ToList();
                voucherdata.allocationDetails = this.GetAllocationDetails(voucherdata.PaymentVoucherCrAcNo, voucherdata.PaymentVoucherVoucherNo).Result;
                return voucherdata;
            }
            return null;
        }

        public IQueryable GetUserTracking(string voucherNo)
        {
            try
            {
                var userTrackngData = (from hdr in context.UserTracking.Where(o => o.UserTrackingUserVpType == "PAYMENTVOUCHER" && o.UserTrackingUserVpNo == voucherNo)
                                       join dtl in context.UserFile on (long)hdr.UserTrackingUserUserId equals dtl.UserId
                                       select new
                                       {
                                           VPAction = hdr.UserTrackingUserVpAction,
                                           UserId = dtl.UserId,
                                           UserName = dtl.UserName,
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

        public VouchersNumbers GenerateVoucherNo()
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PV_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.Payment)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.Payment,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = DateTime.Now
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

        public ApiResponse<List<AllocationDetails>> GetAllocationDetails(string accountNo, string voucherNo)
        {
            try
            {
                var data = (from hdr in context.AllocationVoucherDetails
                            join dtl in context.AccountsTransactions on (long)hdr.AllocationVoucherDetailsTransSno equals dtl.AccountsTransactionsTransSno
                            where (dtl.RefNo == voucherNo &&
                            dtl.AccountsTransactionsAccNo == accountNo
                            )
                            select new Domain.Models.AllocationDetails()
                            {
                                TransDate = dtl.AccountsTransactionsTransDate,
                                VoucherNo = dtl.AccountsTransactionsVoucherNo,
                                Type = dtl.AccountsTransactionsVoucherType,
                                Debit = dtl.AccountsTransactionsAllocDebit,
                                Credit = dtl.AccountsTransactionsAllocCredit,
                                Balance = dtl.AccountsTransactionsAllocBalance,
                                AllocAmount = dtl.AccountsTransactionsFcRate,
                                NetAllocation = dtl.AccountsTransactionsVatableAmount,
                            }).ToList();
                return ApiResponse<List<AllocationDetails>>.Success(data, "Data Found");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
