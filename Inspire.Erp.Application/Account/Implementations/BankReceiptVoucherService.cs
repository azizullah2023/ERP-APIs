using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class BankReceiptVoucherService : IBankReceiptVoucherService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<BankReceiptVoucher> _bankReceiptVoucherRepository;
        private IRepository<BankReceiptVoucherDetails> _bankReceiptVoucherDetailsRepository;
        private IRepository<Domain.Entities.ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<PdcDetails> _pdcDetailsRepository;
        private readonly IRepository<UserTracking> _UserTracking;
        private readonly IRepository<UserFile> _UserFile;
        private readonly InspireErpDBContext context;
        private readonly IPaymentVoucherService _IPaymentVoucherService;

        private readonly IRepository<AllocationVoucherDetails> _alloDetails;
        private readonly IRepository<AllocationVoucher> _alloVoucher;
        private readonly IRepository<CurrencyMaster> _currencyMaster;

        public BankReceiptVoucherService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<Domain.Entities.ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers,
            IRepository<BankReceiptVoucher> bankReceiptVoucherRepository, IRepository<BankReceiptVoucherDetails> bankReceiptVoucherDetailsRepository,
             IRepository<PdcDetails> pdcDetailsRepository
             , IRepository<UserTracking> UserTracking, IRepository<UserFile> UserFile, InspireErpDBContext _context,
              IPaymentVoucherService IPaymentVoucherService, IRepository<AllocationVoucherDetails> alloDetails, IRepository<AllocationVoucher> alloVoucher, IRepository<CurrencyMaster> currencyMaster)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._bankReceiptVoucherRepository = bankReceiptVoucherRepository;
            this._bankReceiptVoucherDetailsRepository = bankReceiptVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _pdcDetailsRepository = pdcDetailsRepository;
            _UserTracking = UserTracking;
            _UserFile = UserFile;
            context = _context;
            _IPaymentVoucherService = IPaymentVoucherService;
            _alloDetails = alloDetails;
            _alloVoucher = alloVoucher;
            _currencyMaster = currencyMaster;
        }

        public BankReceiptVoucher UpdateBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher, List<AccountsTransactions> accountsTransactions, List<BankReceiptVoucherDetails> bankReceiptVoucherDetails, List<Domain.Models.AllocationDetails> allocationDetails)
        {

            try
            {
                _bankReceiptVoucherRepository.BeginTransaction();
                _bankReceiptVoucherRepository.Update(bankReceiptVoucher);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsVoucherType = VoucherType.BankPayment;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }
                    k.AccountsTransactionsAllocCredit = k.AccountsTransactionsCredit;
                    k.AccountsTransactionsAllocDebit = k.AccountsTransactionsDebit;
                    k.AccountsTransactionsAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    k.AccountsTransactionsFcAllocDebit = 0;
                    k.AccountsTransactionsFcAllocCredit = 0;
                    k.AccountsTransactionsFcAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                List<PdcDetails> _pdcDetails = new List<PdcDetails>();
                foreach (var item in bankReceiptVoucherDetails)
                {
                    if (item.BankReceiptVoucherDetailsPdc == true)
                    {
                        var pdetails = _pdcDetailsRepository.GetAsQueryable().Where(pd => pd.PDC_Details_V_No == bankReceiptVoucher.BankReceiptVoucherVoucherNo).ToList();
                        if (pdetails.Count > 0)
                            _pdcDetailsRepository.UpdateList(pdetails);
                        else
                        {
                            _pdcDetails.Add(new PdcDetails()
                            {
                                PDC_Details_V_No = bankReceiptVoucher.BankReceiptVoucherVoucherNo,
                                PDC_Details_V_Type = "BANK RECEIPT",
                                PDC_Details_Trans_Type = "RECEIVED",
                                PDC_Details_Trans_Date = DateTime.Now,
                                PDC_Details_PDC_Date = DateTime.Now,
                                PDC_Details_Cheque_No = item.BankReceiptVoucherDetailsChequeNo == null ? "" : item.BankReceiptVoucherDetailsChequeNo.ToString(),
                                PDC_Details_Cheque_Amount = item.BankReceiptVoucherDetailsDrAmount,
                                PDC_Details_FC_Cheque_Amount = item.BankReceiptVoucherDetailsDrFcAmount,
                                PDC_Details_Bank_Account_No = item.BankReceiptVoucherDetailsAcNo,
                                PDC_Details_Cheque_Bank_Name = "",
                                PDC_Details_Cheque_Status = "NC",
                                PDC_Details_PDC_Voucher_ID = null,
                                PDC_Details_PDC_Voucher_Date = DateTime.Now,
                                PDC_Details_PDC_Voucher_Narration = bankReceiptVoucher.BankReceiptVoucherNarration,
                                PDC_Details_FSNO = item.BankReceiptVoucherDetailsFsno,
                                PDC_Details_User_ID = bankReceiptVoucher.BankReceiptVoucherUserId,
                                PDC_Details_Flat_ID = null,
                                PDC_Details_Building_ID = null,
                                PDC_Details_Contract = null,
                                PDC_Details_Old_Cheque_Status = null,
                                PDC_Details_PartyAccNo = item.BankReceiptVoucherDetailsAcNo,
                                PDC_Details_DelStatus = false,
                            });
                        }

                    }

                    else
                    {
                        var pdetails = _pdcDetailsRepository.GetAsQueryable().Where(pd => pd.PDC_Details_V_No == bankReceiptVoucher.BankReceiptVoucherVoucherNo).ToList();
                        if (pdetails.Count > 0)
                            _pdcDetailsRepository.DeleteList(pdetails);
                    }
                }
                if (_pdcDetails != null && _pdcDetails.Count() > 0)
                {
                     _pdcDetailsRepository.InsertList(_pdcDetails);
                }
                _bankReceiptVoucherDetailsRepository.UpdateList(bankReceiptVoucherDetails);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "BANKRECEIPTVOUCHER";
                _UserTracking.Insert(trackingData);

                //here allocation details 
                if (allocationDetails.Count() > 0) { insertAllocations(bankReceiptVoucher, allocationDetails); }


                _bankReceiptVoucherRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _bankReceiptVoucherRepository.TransactionRollback();
                throw ex;
            }
            return this.GetSavedBankReceiptDetails(bankReceiptVoucher.BankReceiptVoucherVoucherNo);
        }

        public int DeleteBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher, List<AccountsTransactions> accountsTransactions, List<BankReceiptVoucherDetails> bankReceiptVoucherDetails)
        {
            int i = 0;
            try
            {
                _bankReceiptVoucherRepository.BeginTransaction();
                bankReceiptVoucher.BankReceiptVoucherDelStatus = true;
                bankReceiptVoucherDetails = bankReceiptVoucherDetails.Select((k) =>
                {
                    k.BankReceiptVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();
                _bankReceiptVoucherDetailsRepository.UpdateList(bankReceiptVoucherDetails);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                _bankReceiptVoucherRepository.Update(bankReceiptVoucher);
                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == bankReceiptVoucher.BankReceiptVoucherVoucherNo).FirstOrDefault();
                _voucherNumbersRepository.Update(vchrnumer);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                trackingData.UserTrackingUserVpNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "BANKRECEIPTVOUCHER";
                _UserTracking.Insert(trackingData);
                _bankReceiptVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _bankReceiptVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }
            return i;
        }

        public BankReceiptVoucher InsertBankReceiptVoucher(BankReceiptVoucher bankReceiptVoucher, List<AccountsTransactions> accountsTransactions, List<BankReceiptVoucherDetails> bankReceiptVoucherDetails, List<Domain.Models.AllocationDetails> allocationDetails)
        {
            try
            {
                _bankReceiptVoucherRepository.BeginTransaction();
                //bankReceiptVoucher.BankReceiptVoucherVoucherNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                string bankReceiptVoucherNumber = this.GenerateVoucherNo(bankReceiptVoucher.BankReceiptVoucherVDate).VouchersNumbersVNo;
                
                bankReceiptVoucher.BankReceiptVoucherVoucherNo = bankReceiptVoucherNumber;
               
                bankReceiptVoucherDetails = bankReceiptVoucherDetails.Select((x) =>
                {
                    x.BankReceiptVoucherDetailsSno = null;
                    x.BankReceiptVoucherDetailsVoucherNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                    return x;
                }).ToList();
                _bankReceiptVoucherDetailsRepository.InsertList(bankReceiptVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                    k.AccountsTransactionsVoucherType = VoucherType.BankReceipt;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    k.AccountsTransactionsAllocCredit = 0;
                    k.AccountsTransactionsAllocDebit = 0;
                    k.AccountsTransactionsAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    k.AccountsTransactionsFcAllocDebit = 0;
                    k.AccountsTransactionsFcAllocCredit = 0;
                    k.AccountsTransactionsFcAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    return k;
                }).ToList();
                List<PdcDetails> _pdcDetails = new List<PdcDetails>();
                foreach (var item in bankReceiptVoucherDetails)
                {
                    if (item.BankReceiptVoucherDetailsPdc == true)
                    {
                        _pdcDetails.Add(new PdcDetails()
                        {
                            PDC_Details_V_No = bankReceiptVoucher.BankReceiptVoucherVoucherNo,
                            PDC_Details_V_Type = "BANK RECEIPT",
                            PDC_Details_Trans_Type = "RECEIVED",
                            PDC_Details_Trans_Date = DateTime.Now,
                            PDC_Details_PDC_Date = DateTime.Now,
                            PDC_Details_Cheque_No = item.BankReceiptVoucherDetailsChequeNo == null ? "" : item.BankReceiptVoucherDetailsChequeNo.ToString(),
                            PDC_Details_Cheque_Amount = item.BankReceiptVoucherDetailsDrAmount,
                            PDC_Details_FC_Cheque_Amount = item.BankReceiptVoucherDetailsDrFcAmount,
                            PDC_Details_Bank_Account_No = item.BankReceiptVoucherDetailsAcNo,
                            PDC_Details_Cheque_Bank_Name = "",
                            PDC_Details_Cheque_Status = "NC",
                            PDC_Details_PDC_Voucher_ID = null,
                            PDC_Details_PDC_Voucher_Date = DateTime.Now,
                            PDC_Details_PDC_Voucher_Narration = bankReceiptVoucher.BankReceiptVoucherNarration,
                            PDC_Details_FSNO = item.BankReceiptVoucherDetailsFsno,
                            PDC_Details_User_ID = bankReceiptVoucher.BankReceiptVoucherUserId,
                            PDC_Details_Flat_ID = null,
                            PDC_Details_Building_ID = null,
                            PDC_Details_Contract = null,
                            PDC_Details_Old_Cheque_Status = null,
                            PDC_Details_PartyAccNo = item.BankReceiptVoucherDetailsAcNo,
                            PDC_Details_DelStatus = false,
                        });
                    }
                }
                if (_pdcDetails != null && _pdcDetails.Count() > 0)
                {
                    _pdcDetailsRepository.InsertList(_pdcDetails);
                }
                _accountTransactionRepository.InsertList(accountsTransactions);
                _bankReceiptVoucherRepository.Insert(bankReceiptVoucher);
                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = bankReceiptVoucher.BankReceiptVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "BANKRECEIPTVOUCHER";
                _UserTracking.Insert(trackingData);


                //here allocation details 
                if(allocationDetails.Count() > 0) { insertAllocations(bankReceiptVoucher, allocationDetails); }


                _bankReceiptVoucherRepository.TransactionCommit();
                return this.GetSavedBankReceiptDetails(bankReceiptVoucher.BankReceiptVoucherVoucherNo);

            }
            catch (Exception ex)
            {
                _bankReceiptVoucherRepository.TransactionRollback();
                throw ex;
            }
        }

        public void InsertPDCDetails(List<PdcDetails> pdcDetails)
        {
            _pdcDetailsRepository.InsertList(pdcDetails);
        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<BankReceiptVoucher> GetBankReceiptVouchers()
        {
            IEnumerable<BankReceiptVoucher> bankReceiptVouchers = _bankReceiptVoucherRepository.GetAll().Where(k => k.BankReceiptVoucherDelStatus == false || k.BankReceiptVoucherDelStatus == null);
            return bankReceiptVouchers;
        }


        private void insertAllocations(BankReceiptVoucher bankReceiptVoucher, List<AllocationDetails> payAllo)
        {
            var avNo = $"AV {getMaxVoucherAllocation()}";
            var alloModel = new AllocationVoucher()
            {
                AllocationVoucherId = 0,
                AllocationVoucherType = 1,
                AllocationVoucherVoucherNo = avNo,
                AllocationVoucherVoucherDate = DateTime.Now.Date,
                AllocationVoucherDescription = bankReceiptVoucher.BankReceiptVoucherNarration,
                AllocationVoucherVoucherAccNo = bankReceiptVoucher.BankReceiptVoucherDrAcNo,
                AllocationVoucherVoucherAccType = "ALLOCATION",
                AllocationVoucherVoucherFsno = bankReceiptVoucher.BankReceiptVoucherFsno,
                AllocationVoucherStatus = "A",
                AllocationVoucherUserId = bankReceiptVoucher.BankReceiptVoucherUserId,
                AllocationVoucherAvStatus = "B",
                AllocationVoucherRefVno = bankReceiptVoucher.BankReceiptVoucherVoucherNo,
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
                    AllocationVoucherDetailsVFsno = bankReceiptVoucher.BankReceiptVoucherFsno,
                    AllocationVoucherDetailsVLocationId = 1,
                    AllocationVoucherDetailsTransSno = (int)item.TransNo,
                    AllocationVoucherDetailsAccNo = bankReceiptVoucher.BankReceiptVoucherDrAcNo,
                    AllocationVoucherDetailsAllocDebit = item.Debit == null ? 0 : (double)item.Debit,
                    AllocationVoucherDetailsAllocCredit = item.Credit == null ? 0 : (double)item.Debit,
                    AllocationVoucherDetailsFcAllocDebit = item.Debit == null ? 0 : (double)item.Debit * 1, // currency rate
                    AllocationVoucherDetailsFcAllocCredit = item.Credit == null ? 0 : (double)item.Debit * 1, // currency rate
                    AllocationVoucherDetailsRefVno = bankReceiptVoucher.BankReceiptVoucherRefNo,
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
            _alloVoucher.Insert(model);

            if (model.VoucherAllocationDetails != null && model.VoucherAllocationDetails.Count() > 0)
            {
                var vouchetAlloc = _alloVoucher.GetAll().LastOrDefault();
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

                _alloDetails.InsertList(orderDetails);
                _alloDetails.SaveChangesAsync();
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
                int? maxValue = _alloVoucher.GetAll().Max(x => x.AllocationVoucherId);
                int? incrementedValue = maxValue.HasValue ? maxValue + 1 : 1;
                return (int)incrementedValue;
            }
            catch
            {
                return 1;
            }
        }
        public BankReceiptVoucher GetSavedBankReceiptDetails(string pvno)
        {
            BankReceiptVoucher voucherdata = new BankReceiptVoucher();
            var userTrackngData = (from hdr in context.UserTracking
                                   join dtl in context.UserFile on (long)hdr.UserTrackingUserUserId equals dtl.UserId
                                   where (hdr.UserTrackingUserVpType == "BANKRECEIPTVOUCHER" && hdr.UserTrackingUserVpNo == pvno)
                                   select new UserTrackingDisplay
                                   {
                                       //VPAction = hdr.UserTrackingUserVpAction,
                                       //UserId = dtl.UserId,
                                       //VPType = hdr.UserTrackingUserVpType,
                                       //ChangeDt = hdr.UserTrackingUserChangeDt,
                                       //ChangeTime = hdr.UserTrackingUserChangeTime,
                                       //VPNo = hdr.UserTrackingUserVpNo,

                                       Action = hdr.UserTrackingUserVpAction,
                                       Id = dtl.UserId.ToString(),
                                       VType = hdr.UserTrackingUserVpType,
                                       Date = hdr.UserTrackingUserChangeDt.Value.ToString("dd/MM/yyyy"),
                                       Time = hdr.UserTrackingUserChangeTime.Value.ToString("hh:mm t"),
                                       VNo = hdr.UserTrackingUserVpNo,
                                   }).ToList();
            voucherdata = _bankReceiptVoucherRepository.GetAsQueryable().Where(k => k.BankReceiptVoucherVoucherNo == pvno).SingleOrDefault();
            voucherdata.UserTrackingData = userTrackngData;
            voucherdata.AccountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno).ToList();
            voucherdata.BankReceiptVoucherDetails = _bankReceiptVoucherDetailsRepository.GetAsQueryable().Where(x => x.BankReceiptVoucherDetailsVoucherNo == pvno).ToList();
            voucherdata.AllocationDetails = this._IPaymentVoucherService.GetAllocationDetails(voucherdata.BankReceiptVoucherDrAcNo, voucherdata.BankReceiptVoucherVoucherNo).Result;
            return voucherdata;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.BRV_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.BankReceipt)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
                var voucherNo = "BRV" + vnoMaxVal;
                var voucherNumber = _voucherNumbersRepository.GetAll().FirstOrDefault(o => o.VouchersNumbersVNo == voucherNo);
                if (voucherNo != null)
                {

                    var num = _voucherNumbersRepository.GetAll().Where(o => o.VouchersNumbersVNo.Contains("BRV")).OrderByDescending(o => o.VouchersNumbersVNoNu).FirstOrDefault().VouchersNumbersVNoNu + 1;
                    VouchersNumbers vouchersNumbers = new VouchersNumbers
                    {
                        VouchersNumbersVNo = "BRV" + num,
                        VouchersNumbersVNoNu = (decimal)num,
                        VouchersNumbersVType = VoucherType.BankReceipt,
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
                        VouchersNumbersVNo = "BRV" + vnoMaxVal,
                        VouchersNumbersVNoNu = vnoMaxVal,
                        VouchersNumbersVType = VoucherType.BankReceipt,
                        VouchersNumbersFsno = 1,
                        VouchersNumbersStatus = AccountStatus.Pending,
                        VouchersNumbersVoucherDate = VoucherGenDate
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
    }
}
