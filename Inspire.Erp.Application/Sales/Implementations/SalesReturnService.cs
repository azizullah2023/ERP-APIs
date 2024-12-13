using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Sales.Interfaces;
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
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Infrastructure.Database;
namespace Inspire.Erp.Application.Account.Implementations
{
    public class SalesReturnService : ISalesReturnService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<SalesReturn> _salesReturnRepository; private IRepository<CurrencyMaster> _currencyMasterRepository;
        private IRepository<SalesReturnDetails> _salesReturnDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;
        private IRepository<UnitDetails> _UnitDetailsRepository;
        private IRepository<CustomerMaster> _customerMasterRepository;
        private IRepository<CustomerMaster> _salesManMasterRepository;
        private IRepository<DepartmentMaster> _departmentMasterRepository;
        public readonly InspireErpDBContext _Context;


        //private IRepository<ReportSalesReturn> _reportSalesReturnRepository;
        public SalesReturnService(
            //IRepository<ReportSalesReturn> reportSalesReturnRepository,
            IRepository<CustomerMaster> customerMasterRepository,
            IRepository<CustomerMaster> salesManMasterRepository,
            IRepository<DepartmentMaster> departmentMasterRepository,
            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,
            IRepository<UnitDetails> unitDetailsRepository, IRepository<CurrencyMaster> currencyMasterRepository,
            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<SalesReturn> salesReturnRepository, IRepository<SalesReturnDetails> salesReturnDetailsRepository, InspireErpDBContext Context)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._salesReturnRepository = salesReturnRepository;
            this._salesReturnDetailsRepository = salesReturnDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _currencyMasterRepository = currencyMasterRepository;
            _UnitDetailsRepository = unitDetailsRepository;
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _Context = Context;
            _locationMasterRepository = locationMasterRepository;


            _customerMasterRepository = customerMasterRepository;

            _salesManMasterRepository = salesManMasterRepository;

            _departmentMasterRepository = departmentMasterRepository;

            //_reportSalesReturnRepository = reportSalesReturnRepository;
        }

        //public IEnumerable<ReportSalesReturn> SalesReturn_GetReportSalesReturn()
        //{
        //    return _reportSalesReturnRepository.GetAll();
        //}



        ////public IEnumerable<LocationMaster> SalesReturn_GetAllLocationMaster()
        ////{
        ////    return _locationMasterRepository.GetAll();
        ////}



        ////public IEnumerable<SuppliersMaster> SalesReturn_GetAllSuppliersMaster()
        ////{
        ////    return _suppliersMasterRepository.GetAll();
        ////}


        ////public IEnumerable<DepartmentMaster> SalesReturn_GetAllDepartmentMaster()
        ////{
        ////    return _departmentMasterRepository.GetAll();
        ////}




        ////public IEnumerable<CustomerMaster> SalesReturn_GetAllCustomerMaster()
        ////{
        ////    return _customerMasterRepository.GetAll();
        ////}







        ////public IEnumerable<CustomerMaster> SalesReturn_GetAllSalesManMaster()
        ////{
        ////    return _salesManMasterRepository.GetAll();
        ////}


        ////public IEnumerable<UnitMaster> SalesReturn_GetAllUnitMaster()
        ////{
        ////    return _unitMasterRepository.GetAll();
        ////}
        ////public IEnumerable<ItemMaster> SalesReturn_GetAllItemMaster()
        ////{
        ////    return _itemMasterRepository.GetAll();
        ////}

        public SalesReturnModel UpdateSalesReturn(SalesReturn salesReturn, List<AccountsTransactions> accountsTransactions,
            List<SalesReturnDetails> salesReturnDetails
            , List<StockRegister> stockRegister
            )
        {

            try
            {
                List<SalesReturnDetails> addedSalesReturnist = new List<SalesReturnDetails>();
                _salesReturnRepository.BeginTransaction();
                salesReturn.SalesReturnDate = DateTime.Now;
                salesReturn.SalesReturnDlvDate = DateTime.Now;
                salesReturn.SalesReturnSODate = DateTime.Now;
                salesReturn.SalesReturnQtnDate = DateTime.Now;
                salesReturn.SalesReturnEnqDate = DateTime.Now;
                salesReturn.SalesReturnSVDate = DateTime.Now;
                //=================================
                //clsCommonFunctions.Delete_OldEntry_StockRegister(salesReturn.SalesReturnNo, VoucherType.SalesReturn_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntryOf_StockRegister(salesReturn.SalesReturnNo, VoucherType.SalesReturn_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(salesReturn.SalesReturnNo, VoucherType.SalesReturn_TYPE, _accountTransactionRepository);
                _salesReturnRepository.Update(salesReturn);
                Console.WriteLine("STEP ONE");
                //=================================
                salesReturnDetails = salesReturnDetails.Select((k) =>
                {
                    k.SalesReturnDetailsId = 0;
                    k.SalesReturnId = salesReturn.SalesReturnId;
                    k.SalesReturnDetailsNo = salesReturn.SalesReturnNo;
                    return k;
                }).ToList();

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsUserId = salesReturn.SalesReturnUserID == null ? -1 : salesReturn.SalesReturnUserID;
                    k.AccountsTransactionsVoucherNo = salesReturn.SalesReturnNo;
                    k.AccountsTransactionsVoucherType = VoucherType.SalesReturn_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    k.AccountsTransactionsAllocCredit = k.AccountsTransactionsCredit;
                    k.AccountsTransactionsAllocDebit = k.AccountsTransactionsDebit;
                    k.AccountsTransactionsAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    k.AccountsTransactionsFcAllocDebit = 0;
                    k.AccountsTransactionsFcAllocCredit = 0;
                    k.AccountsTransactionsFcAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);
                var removedList = _Context.SalesReturnDetails.Where(o => o.SalesReturnId == salesReturn.SalesReturnId).ToList();
                _Context.SalesReturnDetails.RemoveRange(removedList);
                _Context.SaveChanges();

                _Context.SalesReturnDetails.AddRange(salesReturnDetails);
                _Context.SaveChanges();


                Console.WriteLine("STEP TWO");



                //stockRegister = stockRegister.Select((k) =>
                //{
                //    if (k.StockRegisterStoreID == 0)
                //    {
                //        k.StockRegisterVoucherDate = salesReturn.SalesReturnDate;
                //        k.StockRegisterRefVoucherNo = salesReturn.SalesReturnNo;
                //        k.StockRegisterTransType = VoucherType.SalesReturn_TYPE;

                //        k.StockRegisterStatus = AccountStatus.Approved;
                //    }

                //    return k;
                //}).ToList();
                //_stockRegisterRepository.UpdateList(stockRegister);

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(salesReturn.SalesReturnCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;

                List<StockRegister> sr = new List<StockRegister>();

                foreach (var item in salesReturnDetails)
                {
                    var converontype = this.getConverionTypebyUnitId(item.SalesReturnDetailsMatId, item.SalesReturnDetailsUnitId);
                    sr.Add(new StockRegister()
                    {
                        StockRegisterRefVoucherNo = salesReturn.SalesReturnSVNo,
                        StockRegisterPurchaseID = salesReturn.SalesReturnNo,
                        StockRegisterAssignedDate = DateTime.Now,
                        StockRegisterTransType = VoucherType.SalesReturn_TYPE,
                        StockRegisterFCAmount = item.SalesReturnDetailsRate * item.SalesReturnDetailsQuantity*rate,
                        StockRegisterFcRate = salesReturn.SalesReturnFc_Rate,
                        StockRegisterStatus = "A",
                        StockRegisterJobID = salesReturn.SalesReturnJobId,
                        StockRegisterLocationID = salesReturn.SalesReturnLocationID,
                        StockRegisterFSNO = null,
                        StockRegisterVoucherDate = salesReturn.SalesReturnDate,
                        StockRegisterSIN = item.SalesReturnDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterRate = item.SalesReturnDetailsRate,
                        StockRegisterAmount = item.SalesReturnDetailsRate * item.SalesReturnDetailsQuantity,
                        StockRegisterMaterialID = item.SalesReturnDetailsMatId,
                        StockRegisterQuantity = item.SalesReturnDetailsQuantity * (decimal)(converontype) ?? 0,

                    });
                }
                _Context.StockRegister.AddRange(sr);
                _Context.SaveChanges();

                Console.WriteLine("STEP THREE");



                _salesReturnRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _salesReturnRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedSalesReturnDetails(salesReturn.SalesReturnNo);
        }

        private decimal getConverionTypebyUnitId(int? itemid, int? unitDetailsid)
        {
            try
            {
                return (decimal)_UnitDetailsRepository.GetAsQueryable().FirstOrDefault(x => x.UnitDetailsItemId == itemid && x.UnitDetailsUnitId == unitDetailsid).UnitDetailsConversionType;
            }
            catch
            {
                return 1;
            }
        }
        public int DeleteSalesReturn(SalesReturn salesReturn, List<AccountsTransactions> accountsTransactions,
            List<SalesReturnDetails> salesReturnDetails
            , List<StockRegister> stockRegister
            )
        {
            int i = 0;
            try
            {
                _salesReturnRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(salesReturn.SalesReturnNo, VoucherType.SalesReturn_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(salesReturn.SalesReturnNo, VoucherType.SalesReturn_TYPE, _accountTransactionRepository);
                //=================================

                salesReturn.SalesReturnDelStatus = true;

                salesReturnDetails = salesReturnDetails.Select((k) =>
                {
                    k.SalesReturnDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_salesReturnDetailsRepository.UpdateList(salesReturnDetails);

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




                // salesReturn.SalesReturnDetails = salesReturnDetails;

                _salesReturnRepository.Update(salesReturn);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == salesReturn.SalesReturnNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _salesReturnRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _salesReturnRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
        public SalesReturnModel InsertSalesReturn(SalesReturn salesReturn, List<AccountsTransactions> accountsTransactions,
            List<SalesReturnDetails> salesReturnDetails, List<StockRegister> stockRegister)
        {
            try
            {

                _accountTransactionRepository.BeginTransaction();

                string openingstockVoucherNumber = this.GenerateVoucherNo(salesReturn.SalesReturnDate.Value).VouchersNumbersVNo;
                salesReturn.SalesReturnNo = openingstockVoucherNumber;

                salesReturn.SalesReturnNo = salesReturn.SalesReturnNo;
                _Context.SalesReturn.Add(salesReturn);
                _Context.SaveChanges();
                salesReturn.SalesReturnId = salesReturn.SalesReturnId;
                Console.WriteLine("STEPTHREE = " + salesReturn.SalesReturnId);

                salesReturnDetails = salesReturnDetails.Select((x) =>
                {
                    x.SalesReturnId = salesReturn.SalesReturnId;
                    x.SalesReturnDetailsNo = salesReturn.SalesReturnNo;
                    return x;
                }).ToList();
                _Context.SalesReturnDetails.AddRange(salesReturnDetails);
                _Context.SaveChanges();
                Console.WriteLine("FOUR = " + salesReturn.SalesReturnId);

                //var lSv = _Context.AccountsTransactions.FirstOrDefault(x => x.AccountsTransactionsVoucherNo == salesReturn.SalesReturnSVNo);
                //lSv.AccountsTransactionsAllocCredit = 0;
                //lSv.AccountsTransactionsAllocDebit = salesReturn.SalesReturnNetAmount == null ? 0 : salesReturn.SalesReturnNetAmount.Value;
                //lSv.AccountsTransactionsAllocBalance = lSv.AccountsTransactionsDebit - lSv.AccountsTransactionsAllocDebit;

                //_Context.AccountsTransactions.Update(lSv);
                //_Context.SaveChanges(true);
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsAccNo = k.AccountsTransactionsAccNo ?? "12345";
                    k.AccountsTransactionsUserId = salesReturn.SalesReturnUserID == null ? -1 : salesReturn.SalesReturnUserID;
                    k.AccountsTransactionsVoucherNo = salesReturn.SalesReturnNo;
                    k.AccountsTransactionsVoucherType = VoucherType.SalesReturn_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    k.AccountsTransactionsAllocCredit = k.AccountsTransactionsCredit;
                    k.AccountsTransactionsAllocDebit = k.AccountsTransactionsDebit;
                    k.AccountsTransactionsAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    k.AccountsTransactionsFcAllocDebit = 0;
                    k.AccountsTransactionsFcAllocCredit = 0;
                    k.AccountsTransactionsFcAllocBalance = k.AccountsTransactionsDebit == 0 ? k.AccountsTransactionsCredit : k.AccountsTransactionsDebit;
                    return k;
                }).ToList();

                _Context.AccountsTransactions.AddRange(accountsTransactions);
                _Context.SaveChanges();

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(salesReturn.SalesReturnCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;


                List<StockRegister> sr = new List<StockRegister>();

                foreach (var item in salesReturnDetails)
                {
                    var converontype = this.getConverionTypebyUnitId(item.SalesReturnDetailsMatId, item.SalesReturnDetailsUnitId);
                    sr.Add(new StockRegister()
                    {
                        StockRegisterRefVoucherNo = salesReturn.SalesReturnSVNo,
                        StockRegisterPurchaseID = salesReturn.SalesReturnNo,
                        StockRegisterAssignedDate = DateTime.Now,
                        StockRegisterTransType = VoucherType.SalesReturn_TYPE,
                        StockRegisterFCAmount = item.SalesReturnDetailsRate * item.SalesReturnDetailsQuantity * rate,
                        StockRegisterFcRate = salesReturn.SalesReturnFc_Rate,
                        StockRegisterStatus = "A",
                        StockRegisterJobID = salesReturn.SalesReturnJobId,
                        StockRegisterLocationID = salesReturn.SalesReturnLocationID,
                        StockRegisterFSNO = null,
                        StockRegisterVoucherDate = salesReturn.SalesReturnDate,
                        StockRegisterSIN = item.SalesReturnDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterRate = item.SalesReturnDetailsRate,
                        StockRegisterAmount = item.SalesReturnDetailsRate * item.SalesReturnDetailsQuantity,
                        StockRegisterMaterialID = item.SalesReturnDetailsMatId,
                        StockRegisterQuantity = item.SalesReturnDetailsQuantity * (decimal)(converontype) ?? 0,

                    });
                }

                //sr.StockRegisterUsedQTY= salesReturn.
                // return sr;
                // }).ToList();
                _Context.StockRegister.AddRange(sr);
                _Context.SaveChanges();
                _accountTransactionRepository.TransactionCommit();
                return this.GetSavedSalesReturnDetails(salesReturn.SalesReturnNo);
            }
            catch (Exception ex)
            {
                _salesReturnRepository.TransactionRollback();
                throw ex;
            }
        }

        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public List<SalesReturn> GetSalesReturn()
        {
            List<SalesReturn> salesReturn_ALL = _salesReturnRepository.GetAll().Where(k => k.SalesReturnDelStatus == false || k.SalesReturnDelStatus == null).ToList();
            return salesReturn_ALL;
        }
        public SalesReturnModel GetSavedSalesReturnDetails(string pvno)
        {
            SalesReturnModel salesReturnModel = new SalesReturnModel();
            salesReturnModel.salesReturn = _salesReturnRepository.GetAsQueryable().Where(k => k.SalesReturnNo == pvno && k.SalesReturnDelStatus != true).FirstOrDefault();
            if (salesReturnModel.salesReturn != null)
            {
                salesReturnModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.SalesReturn_TYPE && (c.AccountstransactionsDelStatus != true)).ToList();
                salesReturnModel.salesReturnDetails = _salesReturnDetailsRepository.GetAsQueryable().Where(x => x.SalesReturnDetailsNo == pvno && (x.SalesReturnDetailsDelStatus != true)).ToList();

                foreach (var item in salesReturnModel.salesReturnDetails)
                {
                    decimal sumReturnedQty = 0;
                    var stockQuery = _stockRegisterRepository
                       .GetAsQueryable()
                       .Where(a => a.StockRegisterMaterialID == item.SalesReturnDetailsMatId &&
                                   a.StockRegisterTransType == "SalesReturn" &&
                                    a.StockRegisterRefVoucherNo == salesReturnModel.salesReturn.SalesReturnSVNo
                                     && a.StockRegisterPurchaseID != item.SalesReturnDetailsNo);

                    sumReturnedQty = stockQuery.Any()
                                       ? stockQuery.Sum(s => (decimal?)s.StockRegisterSIN) ?? 0
                                       : 0;

                    if (sumReturnedQty != null)
                    {
                        item.SalesReturnQtyTill = sumReturnedQty;
                    }
                }
            }
            return salesReturnModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.SalesReturn_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.SalesReturn_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";fv
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.SalesReturn_TYPE,
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



        //using Microsoft.Extensions.Logging;
        //private readonly ILogger<PaymentVoucherService> _logger;
        //IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
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
