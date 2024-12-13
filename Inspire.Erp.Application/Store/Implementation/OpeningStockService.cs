////using System;
////using System.Collections.Generic;
////using System.Text;

////namespace Inspire.Erp.Application.Store.Implementation
////{
////    class OpeningStockService
////    {
////    }
////}
///
using Inspire.Erp.Application.Store.Interfaces;
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Inspire.Erp.Application.MODULE;

namespace Inspire.Erp.Application.Store.Implementation
{
    public class OpeningStockService : IOpeningStockService
    {
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<OpeningStock> _openingStockRepository;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private IRepository<OpeningStockVoucher> _openingstockVoucherRepository;
        private IRepository<OpeningStockVoucherDetails> _openingstockVoucherDetailsRepository;

        private readonly ILogger<OpeningStockService> _logger;
        private readonly IUtilityService _utilityService;

        public OpeningStockService(IRepository<AccountsTransactions> accountTransactionRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<OpeningStockService> logger,
            IRepository<OpeningStock> openingStockRepository, IRepository<StockRegister> stockRegisterRepository,
             IRepository<OpeningStockVoucher> openingstockVoucherRepository, IRepository<OpeningStockVoucherDetails> openingstockVoucherDetailsRepository,
            IUtilityService utilityService)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._openingStockRepository = openingStockRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _logger = logger;
            _utilityService = utilityService;
            _openingstockVoucherRepository = openingstockVoucherRepository;
            _openingstockVoucherDetailsRepository = openingstockVoucherDetailsRepository;

        }

        public OpeningStockModel UpdateOpeningStock(OpeningStock openingStock, List<AccountsTransactions> accountsTransactions,
                                                        List<StockRegister> stockregister)
        {

            try
            {
                _openingStockRepository.BeginTransaction();

                _openingStockRepository.Update(openingStock);
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
                _stockRegisterRepository.UpdateList(stockregister);
                _openingStockRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _openingStockRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedOPStockDetails(openingStock.OpeningStockPurchaseId);
        }

        public int DeleteOpeningStock(OpeningStock openingStock, List<AccountsTransactions> accountsTransactions, List<StockRegister> stockRegister)
        {
            int i = 0;
            try
            {
                _openingStockRepository.BeginTransaction();

                openingStock.OpeningStockDelStatus = true;

                stockRegister = stockRegister.Select((k) => {
                    k.StockRegisterDelStatus = true;
                    return k;
                }).ToList();

                _stockRegisterRepository.UpdateList(stockRegister);

                accountsTransactions = accountsTransactions.Select((k) => {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                _openingStockRepository.Update(openingStock);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == openingStock.OpeningStockPurchaseId).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _openingStockRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _openingStockRepository.TransactionRollback();
                i = 0;
                throw ex;
            }


            return i;

        }
        public OpeningStockModel InsertOpeningStock(OpeningStock openingStock, List<AccountsTransactions> accountsTransactions,
                                                        List<StockRegister> stockRegister)
        {
            try
            {
                _openingStockRepository.BeginTransaction();
                string openingSNumber = (openingStock.OpeningStockPurchaseId == null || openingStock.OpeningStockPurchaseId.Trim() == "") ?
                                              this.GenerateVoucherNo(openingStock.OpeningStockExpDate.Value).VouchersNumbersVNo : openingStock.OpeningStockPurchaseId;
                openingStock.OpeningStockPurchaseId = openingSNumber;

                StockRegister stockRegster = new StockRegister
                {

                    StockRegisterPurchaseID = openingSNumber,
                    StockRegisterRefVoucherNo = openingSNumber,
                    StockRegisterVoucherDate = DateTime.Now,
                    StockRegisterSno = 0,
                    StockRegisterBatchCode = openingStock.OpeningStockBatchCode,
                    StockRegisterMaterialID = openingStock.OpeningStockMaterialId,
                    StockRegisterQuantity = Convert.ToDecimal(openingStock.OpeningStockQty),
                    StockRegisterSIN = Convert.ToDecimal(openingStock.OpeningStockQty),
                    StockRegisterSout = 0,
                    StockRegisterRate = Convert.ToDecimal(openingStock.OpeningStockUnitRate),
                    StockRegisterAmount = Convert.ToDecimal(openingStock.OpeningStockAmount),
                    StockRegisterFCAmount = Convert.ToDecimal(openingStock.OpeningStockFcAmount),
                    StockRegisterRemarks = openingStock.OpeningStockRemakrs,
                    StockRegisterUnitID = openingStock.OpeningStockUnitId,
                    StockRegisterLocationID = openingStock.OpeningStockLocationId,
                    StockRegisterJobID = openingStock.OpeningStockJobId,
                    StockRegisterFSNO = openingStock.OpeningStockFsno

                };

                _stockRegisterRepository.InsertList(stockRegister);

                accountsTransactions = accountsTransactions.Select((k) => {
                    k.AccountsTransactionsVoucherNo = openingSNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.Payment;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _openingStockRepository.Insert(openingStock);
                _openingStockRepository.TransactionCommit();
                return this.GetSavedOPStockDetails(openingStock.OpeningStockPurchaseId);

            }
            catch (Exception ex)
            {
                _openingStockRepository.TransactionRollback();
                throw ex;
            }

        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<OpeningStock> GetOpeningStocks()
        {
            IEnumerable<OpeningStock> openingStocks = _openingStockRepository.GetAll().Where(k => k.OpeningStockDelStatus == false || k.OpeningStockDelStatus == null);
            return openingStocks;
        }


        public IEnumerable<OpeningStockVoucher> GetOpeningStockVouchers()
        {
            IEnumerable<OpeningStockVoucher> openingStocks = _openingstockVoucherRepository.GetAll().Where(k => k.OpeningStockVoucherDelStatus == false || k.OpeningStockVoucherDelStatus == null);
            return openingStocks;
        }



        public OpeningStockModel GetSavedOPStockDetails(string pvno)
        {
            if (_voucherNumbersRepository.GetAsQueryable().Any(x => x.VouchersNumbersVNo == pvno))
            {
                OpeningStockModel voucherdata = _voucherNumbersRepository.GetAsQueryable().Where(x => x.VouchersNumbersVNo == pvno)
                                                .Include(k => k.AccountsTransactions)
                                                .Include(k => k.PaymentVoucherDetails).AsEnumerable()
                                                .Select((k) => new OpeningStockModel
                                                {
                                                    AccountsTransactions = k.AccountsTransactions.Where(x => x.AccountstransactionsDelStatus == false).ToList(),
                                                    // stockRegister = k.StockRegisters.Where(x => x.StockRegisterDelStatus == false).ToList()
                                                })
                                                .SingleOrDefault();
                voucherdata.openingStock = _openingStockRepository.GetAsQueryable().Where(k => k.OpeningStockPurchaseId == pvno).FirstOrDefault();
                return voucherdata;

            }
            return null;

        }


        public VouchersNumbers GenerateOpeningStockVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {


                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.OpeningStockVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.OpeningStockVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;



                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.OpeningStockVoucher_TYPE,
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

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                ////var vno=  this._paymentVoucherRepository.GetAsQueryable().Where(k => k.PaymentVoucherVoucherNo == null).FirstOrDefault();

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
        private decimal getConverionTypebyUnitId(int? itemid, int? unitDetailsid)
        {
            try
            {
                return (decimal)_openingstockVoucherDetailsRepository.GetAsQueryable().FirstOrDefault(x => x.OpeningStockVoucherDetailsUnitId == itemid && x.OpeningStockVoucherDetailsUnitId == unitDetailsid).OpeningStockVoucherDetailsConversionType;
            }
            catch
            {
                return 1;
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
        public async Task<OpeningStockVoucherModel> InsertOpeningStockVoucher(OpeningStockVoucher openstockVoucher, List<AccountsTransactions> accountsTransactions, List<OpeningStockVoucherDetails> openstockVoucherDetails
        , List<StockRegister> stockRegister
         )
        {
            try
            {
                _openingstockVoucherRepository.BeginTransaction();
                string openingstockVoucherNumber = this.GenerateOpeningStockVoucherNo(openstockVoucher.OpeningStockVoucherDate.Date).VouchersNumbersVNo;
                openstockVoucher.OpeningStockVoucherNo = openingstockVoucherNumber;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _openingstockVoucherRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.OpeningStockVoucherId) + 1);



                openstockVoucher.OpeningStockVoucherId = maxcount;

                //x.OpeningStockVoucherDetailsConversionType = await _utilityService.GetBasicUnitConversion(x.OpeningStockVoucherDetailsMatId, x.OpeningStockVoucherDetailsUnitId);
                openstockVoucher.OpeningStockVoucherDetails = openstockVoucherDetails.Select((x) =>
                {
                    x.OpeningStockVoucherId = maxcount;
                    x.OpeningStockVoucherDetailsNo = openingstockVoucherNumber;
                    return x;
                }).ToList();


                openstockVoucher.OpeningStockVoucherDetails = await Task.WhenAll(openstockVoucher.OpeningStockVoucherDetails.Select(async (osvd) =>
                {
                    osvd.OpeningStockVoucherDetailsId = null;
                    osvd.OpeningStockVoucherDetailsConversionType = await _utilityService.GetBasicUnitConversion(osvd.OpeningStockVoucherDetailsMatId, osvd.OpeningStockVoucherDetailsUnitId);
                    return osvd;
                }));

                // openstockVoucher.OpeningStockVoucherDetails = osVDetails;


                //_openstockVoucherDetailsRepository.InsertList(issueVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    //k.AccountsTransactionsTransDate = issueVoucher.OpeningStockVoucherDate;
                    k.AccountsTransactionsVoucherNo = openingstockVoucherNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.OpeningStockVoucher_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                //stockRegister = stockRegister.Select((k) =>
                //{
                //    k.StockRegisterVoucherDate = openstockVoucher.OpeningStockVoucherDate;
                //    k.StockRegisterRefVoucherNo = openingstockVoucherNumber;
                //    k.StockRegisterTransType = VoucherType.OpeningStockVoucher_TYPE;
                //    k.StockRegisterStatus = AccountStatus.Approved;


                //    return k;
                //}).ToList();
                //_stockRegisterRepository.InsertList(stockRegister);

                var stockReg = new List<StockRegister>();
                foreach (var item in openstockVoucher.OpeningStockVoucherDetails)
                {
                    //var converontype = this.getConverionTypebyUnitId(item.OpeningStockVoucherDetailsMatId, item.OpeningStockVoucherDetailsUnitId);

                    var CostPriceBaseUnit = item.OpeningStockVoucherDetailsAmount; /// converontype;
                    stockReg.Add(new StockRegister()
                    {
                        StockRegisterPurchaseID = openstockVoucher.OpeningStockVoucherNo,
                        StockRegisterRefVoucherNo = openstockVoucher.OpeningStockVoucherNo,
                        StockRegisterVoucherDate = openstockVoucher.OpeningStockVoucherDate,
                        StockRegisterMaterialID = item.OpeningStockVoucherDetailsMatId,
                        StockRegisterQuantity = item.OpeningStockVoucherDetailsManualQty ?? 0,
                        StockRegisterSIN = item.OpeningStockVoucherDetailsManualQty ?? 0,
                        StockRegisterSout = 0,
                        StockRegisterRate = 0,
                        StockRegisterAmount = CostPriceBaseUnit ?? 0,
                        StockRegisterFCAmount = 0,
                        StockRegisterAssignedDate = openstockVoucher.OpeningStockVoucherDate,
                        StockRegisterStatus = AccountStatus.Approved,
                        StockRegisterTransType = VoucherType.OpeningStockVoucher_TYPE,
                        StockRegisterUnitID = item.OpeningStockVoucherDetailsUnitId,
                        StockRegisterLocationID = openstockVoucher.OpeningStockVoucherLocationId,
                        StockRegisterJobID = (int)item.OpeningStockVoucherDetailsJob_Id,
                        StockRegisterExpDate = null,
                        StockRegisterDepID = 1,
                        StockRegisterDelStatus = false
                    });
                }
                _stockRegisterRepository.InsertList(stockReg);

                _openingstockVoucherRepository.Insert(openstockVoucher);

                _openingstockVoucherRepository.TransactionCommit();
                return this.GetSavedOpeningStockVoucherDetails(openstockVoucher.OpeningStockVoucherNo);

            }
            catch (Exception ex)
            {
                _openingstockVoucherRepository.TransactionRollback();
                throw ex;
            }


        }

        public async Task<OpeningStockVoucherModel> UpdateOpeningStockVoucher(OpeningStockVoucher openingstockVoucher, List<AccountsTransactions> accountsTransactions,
    List<OpeningStockVoucherDetails> openingstockVoucherDetails
    , List<StockRegister> stockRegister
    )
        {

            try
            {
                _openingstockVoucherRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(openingstockVoucher.OpeningStockVoucherNo, VoucherType.OpeningStockVoucher_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(openingstockVoucher.OpeningStockVoucherNo, VoucherType.OpeningStockVoucher_TYPE, _accountTransactionRepository);
                //=================================
                openingstockVoucher.OpeningStockVoucherDetails = openingstockVoucherDetails.Select((k) =>
                {
                    //if (k.IssueVoucherDetailsId == 0)
                    //{
                    k.OpeningStockVoucherId = openingstockVoucher.OpeningStockVoucherId;
                    k.OpeningStockVoucherDetailsNo = openingstockVoucher.OpeningStockVoucherNo;
                    //k.IssueVoucherDetailsId = 0;
                    //}

                    return k;
                }).ToList();

                openingstockVoucher.OpeningStockVoucherDetails = await Task.WhenAll(openingstockVoucher.OpeningStockVoucherDetails.Select(async (osvd) =>
                {
                    osvd.OpeningStockVoucherDetailsConversionType = await _utilityService.GetBasicUnitConversion(osvd.OpeningStockVoucherDetailsMatId, osvd.OpeningStockVoucherDetailsUnitId);
                    return osvd;
                }));

                _openingstockVoucherRepository.Update(openingstockVoucher);
                //_issueVoucherRepository.Update(issueVoucher);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = openingstockVoucher.OpeningStockVoucherDate;
                        k.AccountsTransactionsVoucherNo = openingstockVoucher.OpeningStockVoucherNo;
                        k.AccountsTransactionsVoucherType = VoucherType.OpeningStockVoucher_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                stockRegister = stockRegister.Select((k) =>
                {
                    if (k.StockRegisterStoreID == 0)
                    {
                        k.StockRegisterVoucherDate = openingstockVoucher.OpeningStockVoucherDate;
                        k.StockRegisterRefVoucherNo = openingstockVoucher.OpeningStockVoucherNo;
                        k.StockRegisterTransType = VoucherType.OpeningStockVoucher_TYPE;
                        k.StockRegisterStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _stockRegisterRepository.UpdateList(stockRegister);

                _openingstockVoucherRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _openingstockVoucherRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedOpeningStockVoucherDetails(openingstockVoucher.OpeningStockVoucherNo);
        }



        //public OpeningStockVoucherModel GetSavedOpeningStockVoucherDetails(string pvno)
        //{
        //    OpeningStockVoucherModel openstockVoucherModel = new OpeningStockVoucherModel();
        //    //issueVoucherModel.issueVoucher = _issueVoucherRepository.GetAsQueryable().Where(k => k.IssueVoucherNo == pvno && k.IssueVoucherDelStatus == false).SingleOrDefault();

        //    openstockVoucherModel.openingstockVoucher = _openingstockVoucherRepository.GetAsQueryable().Where(k => k.OpeningStockVoucherNo == pvno).SingleOrDefault();



        //    openstockVoucherModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.IssueVoucher_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
        //    openstockVoucherModel.openingstockVoucherDetails = _openingstockVoucherDetailsRepository.GetAsQueryable().Where(x => x.OpeningStockVoucherDetailsNo == pvno && (x.OpeningStockVoucherDetailsDelStatus == false || x.OpeningStockVoucherDetailsDelStatus == null)).ToList();
        //    return openstockVoucherModel;


        //}
        public OpeningStockVoucherModel GetSavedOpeningStockVoucherDetails(string pvno)
        {
            OpeningStockVoucherModel openingstockVoucherModel = new OpeningStockVoucherModel();
            openingstockVoucherModel.openingstockVoucher = _openingstockVoucherRepository.GetAsQueryable().Where(k => k.OpeningStockVoucherNo == pvno).SingleOrDefault();
            openingstockVoucherModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.OpeningStockVoucher_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            openingstockVoucherModel.openingstockVoucherDetails = _openingstockVoucherDetailsRepository.GetAsQueryable().Where(x => x.OpeningStockVoucherDetailsNo == pvno && (x.OpeningStockVoucherDetailsDelStatus == false || x.OpeningStockVoucherDetailsDelStatus == null)).ToList();
            return openingstockVoucherModel;
        }




        public async Task<int> DeleteOpeningStockVoucher(OpeningStockVoucher openingstockVoucher, List<AccountsTransactions> accountsTransactions,
            List<OpeningStockVoucherDetails> openingstockVoucherDetails
            , List<StockRegister> stockRegister
            )
        {
            int i = 0;
            try
            {
                _openingstockVoucherRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(openingstockVoucher.OpeningStockVoucherNo, VoucherType.OpeningStockVoucher_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(openingstockVoucher.OpeningStockVoucherNo, VoucherType.OpeningStockVoucher_TYPE, _accountTransactionRepository);
                //=================================

                openingstockVoucher.OpeningStockVoucherDelStatus = true;

                openingstockVoucher.OpeningStockVoucherDetails = openingstockVoucher.OpeningStockVoucherDetails.Select((k) =>
                {
                    k.OpeningStockVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();
                _openingstockVoucherRepository.Update(openingstockVoucher);
                //_openingstockVoucherDetailsRepository.UpdateList(openingstockVoucherDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);




                stockRegister = stockRegister.Select((k) =>
                {
                    k.StockRegisterDelStatus = true;
                    return k;
                }).ToList();
                _stockRegisterRepository.UpdateList(stockRegister);




                //   openingstockVoucher.OpeningStockVoucherDetails = openingstockVoucherDetails;



                //var vchrnumer = await _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == openingstockVoucher.OpeningStockVoucherNo).FirstOrDefaultAsync();

                //_voucherNumbersRepository.Update(vchrnumer);

                _openingstockVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _openingstockVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
    }
}
