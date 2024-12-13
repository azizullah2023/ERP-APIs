using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Account.Implementations;
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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using SendGrid.Helpers.Mail;
using Inspire.Erp.Domain.Entities.POS;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Inspire.Erp.Domain.Models;
using System.Diagnostics;
using System.IO.Pipelines;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Store.Implementation;
using Inspire.Erp.Application.Store.Interfaces;

namespace Inspire.Erp.Application.Sales.Implementations
{
    public class SalesVoucherService : ISalesVoucherService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<CurrencyMaster> _currencyMasterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<SalesVoucher> _salesVoucherRepository;
        private IRepository<SalesVoucherDetails> _salesVoucherDetailsRepository;
        private IRepository<Domain.Entities.ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;

        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;



        private IRepository<CustomerMaster> _customerMasterRepository;
        private IRepository<SalesManMaster> _salesManMasterRepository;
        private IRepository<DepartmentMaster> _departmentMasterRepository;
        private IRepository<UnitDetails> _UnitDetailsRepository;


        private IRepository<SalesReturn> _salesReturn;
        private IRepository<SalesReturnDetails> _salesReturnDtls;
        private IRepository<OptionsMaster> _optionMaster;
        private InspireErpDBContext _context;


        /// <summary>
        /// Master accounts
        /// </summary>
        private IRepository<MasterAccountsTable> accMaster;

        //private IRepository<POS_Sales_Voucher> _posSalesVoucherRepository;
        //private IRepository<POS_Sales_Voucher_Details> _posSalesVoucherDetailsRepository;



        private IRepository<SalesHold> _salesHoldRepository;
        private IRepository<SalesHoldDetails> _salesHoldDetailsRepository;

        private IRepository<POS_WorkPeriod> _POSWorkPeriodRepository;
        private IRepository<UserTracking> _UserTracking;


        private IStockService _openingStockService;


        //private IRepository<ReportSalesVoucher> _reportSalesVoucherRepository;
        public SalesVoucherService(
            IRepository<UserTracking> UserTracking,
            //IRepository<ReportSalesVoucher> reportSalesVoucherRepository,
            IRepository<CustomerMaster> customerMasterRepository,
            IRepository<SalesManMaster> salesManMasterRepository,
            IRepository<DepartmentMaster> departmentMasterRepository,
            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,

            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<Domain.Entities.ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<SalesVoucher> salesVoucherRepository, IRepository<SalesVoucherDetails> salesVoucherDetailsRepository, IRepository<CurrencyMaster> currencyMasterRepository, IRepository<UnitDetails> unitDetailsRepository, IRepository<MasterAccountsTable> accMaster, IRepository<SalesReturn> salesReturn, IRepository<SalesReturnDetails> salesReturnDtls, IRepository<OptionsMaster> optionMaster
            //IRepository<POS_Sales_Voucher> posSalesVoucherRepository, IRepository<POS_Sales_Voucher_Details> posSalesVoucherDetailsRepository
            , IRepository<SalesHold> salesHoldRepository, IRepository<SalesHoldDetails> salesHoldDetailsRepository, IRepository<POS_WorkPeriod> POSWorkPeriodRepository, InspireErpDBContext context
            , IStockService openingStockService)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._salesVoucherRepository = salesVoucherRepository;
            this._salesVoucherDetailsRepository = salesVoucherDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;
            _customerMasterRepository = customerMasterRepository;
            _salesManMasterRepository = salesManMasterRepository;
            _departmentMasterRepository = departmentMasterRepository;
            _currencyMasterRepository = currencyMasterRepository;
            _UnitDetailsRepository = unitDetailsRepository;
            this.accMaster = accMaster;
            _salesReturn = salesReturn;
            _salesReturnDtls = salesReturnDtls;
            _optionMaster = optionMaster;
            //_posSalesVoucherDetailsRepository = posSalesVoucherDetailsRepository;
            //_posSalesVoucherRepository = posSalesVoucherRepository;

            _salesHoldRepository = salesHoldRepository;
            _salesHoldDetailsRepository = salesHoldDetailsRepository;
            _POSWorkPeriodRepository = POSWorkPeriodRepository;
            _UserTracking = UserTracking;
            _context = context;
            _openingStockService = openingStockService;
            //_reportSalesVoucherRepository = reportSalesVoucherRepository;
        }

        //public IEnumerable<ReportSalesVoucher> SalesVoucher_GetReportSalesVoucher()
        //{
        //    return _reportSalesVoucherRepository.GetAll();
        //}



        public IEnumerable<LocationMaster> SalesVoucher_GetAllLocationMaster()
        {
            return _locationMasterRepository.GetAll();
        }



        public IEnumerable<SuppliersMaster> SalesVoucher_GetAllSuppliersMaster()
        {
            return _suppliersMasterRepository.GetAll();
        }


        public IEnumerable<DepartmentMaster> SalesVoucher_GetAllDepartmentMaster()
        {
            return _departmentMasterRepository.GetAll();
        }




        public IEnumerable<CustomerMaster> SalesVoucher_GetAllCustomerMaster()
        {
            return _customerMasterRepository.GetAll();
        }
        public IEnumerable<SalesManMaster> SalesVoucher_GetAllSalesManMaster()
        {
            return _salesManMasterRepository.GetAll();
        }
        public IEnumerable<UnitMaster> SalesVoucher_GetAllUnitMaster()
        {
            return _unitMasterRepository.GetAll();
        }
        public IEnumerable<ItemMaster> SalesVoucher_GetAllItemMaster()
        {
            return _itemMasterRepository.GetAll();
        }

        public async Task<SalesVoucherModel> UpdateSalesVoucher(SalesVoucher salesVoucher, List<AccountsTransactions> accountsTransactions,
            List<SalesVoucherDetails> salesVoucherDetails
           , List<StockRegister> stockRegister
            )
        {

            try
            {
                _salesVoucherRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntryOf_StockRegister(salesVoucher.SalesVoucherNo, VoucherType.SalesVoucher_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(salesVoucher.SalesVoucherNo, VoucherType.SalesVoucher_TYPE, _accountTransactionRepository);
                //=================================
                salesVoucher.SalesVoucherDetails = salesVoucherDetails.Select((k) =>
                {

                    k.SalesVoucherId = salesVoucher.SalesVoucherId;
                    k.SalesVoucherDetailsNo = salesVoucher.SalesVoucherNo;


                    return k;
                }).ToList();

                _salesVoucherRepository.Update(salesVoucher);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = salesVoucher.SalesVoucherDate;
                        k.AccountsTransactionsVoucherNo = salesVoucher.SalesVoucherNo;
                        k.AccountsTransactionsVoucherType = VoucherType.SalesVoucher_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).Where(x => x.AccountsTransactionsTransSno == 0).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                //stockRegister = stockRegister.Select((k) =>
                //{
                //    if (k.StockRegisterStoreID == 0)
                //    {
                //        k.StockRegisterVoucherDate = salesVoucher.SalesVoucherDate;
                //        k.StockRegisterRefVoucherNo = salesVoucher.SalesVoucherNo;
                //        k.StockRegisterTransType = VoucherType.SalesVoucher_TYPE;
                //        k.StockRegisterStatus = AccountStatus.Approved;
                //    }

                //    return k;
                //}).ToList();
                //_stockRegisterRepository.UpdateList(stockRegister);

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(salesVoucher.SalesVoucherCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;

                var stockReg = new List<StockRegister>();
                foreach (var item in salesVoucher.SalesVoucherDetails)
                {

                    var converontype = this.getConverionTypebyUnitId(item.SalesVoucherDetailsMatId, item.SalesVoucherDetailsUnitId);

                    stockReg.Add(new StockRegister()
                    {
                        StockRegisterPurchaseID = salesVoucher.SalesVoucherNo,
                        StockRegisterRefVoucherNo = salesVoucher.SalesVoucherNo,
                        StockRegisterVoucherDate = DateTime.Now.Date,
                        StockRegisterMaterialID = item.SalesVoucherDetailsMatId,
                        StockRegisterQuantity = item.SalesVoucherDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterSIN = 0,
                        StockRegisterSout = item.SalesVoucherDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterRate = item.SalesVoucherDetailsRate,
                        StockRegisterAmount = item.SalesVoucherDetailsQuantity * item.SalesVoucherDetailsRate ?? 0,
                        StockRegisterFCAmount = item.SalesVoucherDetailsQuantity * item.SalesVoucherDetailsRate * rate,
                        StockRegisterAssignedDate = DateTime.Now.Date,
                        StockRegisterStatus = "A",
                        StockRegisterTransType = VoucherType.SalesVoucher_TYPE,
                        StockRegisterUnitID = item.SalesVoucherDetailsUnitId,
                        StockRegisterLocationID = salesVoucher.SalesVoucherLocationID,
                        StockRegisterJobID = salesVoucher.SalesVoucherJobId,
                        StockRegisterDepID = salesVoucher.SalesVoucherDptID,
                        StockRegisterDelStatus = false
                    });
                }
                _stockRegisterRepository.UpdateList(stockReg);


                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = salesVoucher.SalesVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = VoucherType.SalesVoucher_TYPE;
                _UserTracking.Insert(trackingData);

                _salesVoucherRepository.TransactionCommit();


            }
            catch (Exception ex)
            {
                _salesVoucherRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedSalesVoucherDetails(salesVoucher.SalesVoucherNo);
        }


        public int DeleteSalesVoucher(SalesVoucher salesVoucher, List<AccountsTransactions> accountsTransactions,
            List<SalesVoucherDetails> salesVoucherDetails
            , List<StockRegister> stockRegister
            )
        {
            int i = 0;
            try
            {
                _salesVoucherRepository.BeginTransaction();

                //=================================
                clsCommonFunctions.Delete_OldEntry_StockRegister(salesVoucher.SalesVoucherNo, VoucherType.SalesVoucher_TYPE, _stockRegisterRepository);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(salesVoucher.SalesVoucherNo, VoucherType.SalesVoucher_TYPE, _accountTransactionRepository);
                //=================================


                salesVoucher.SalesVoucherDelStatus = true;

                salesVoucherDetails = salesVoucherDetails.Select((k) =>
                {
                    k.SalesVoucherDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_salesVoucherDetailsRepository.UpdateList(salesVoucherDetails);

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




                salesVoucher.SalesVoucherDetails = salesVoucherDetails;

                _salesVoucherRepository.Update(salesVoucher);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == salesVoucher.SalesVoucherNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);





                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                trackingData.UserTrackingUserVpNo = salesVoucher.SalesVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = VoucherType.SalesVoucher_TYPE;
                _UserTracking.Insert(trackingData);


                _salesVoucherRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _salesVoucherRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

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

        public async Task<SalesVoucherModel> InsertSalesVoucher(SalesVoucher salesVoucher, List<AccountsTransactions> accountsTransactions,
            List<SalesVoucherDetails> salesVoucherDetails
            , List<StockRegister> stockRegister
            )
        {
            try
            {
                _salesVoucherRepository.BeginTransaction();
                string salesVoucherNumber = this.GenerateVoucherNo(salesVoucher.SalesVoucherDate).VouchersNumbersVNo;
                salesVoucher.SalesVoucherNo = salesVoucherNumber;


                decimal? maxcount = 0;
                maxcount =
                    _salesVoucherRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.SalesVoucherId) + 1;

                salesVoucher.SalesVoucherId = maxcount;
                salesVoucher.SalesVoucherDetails.Clear();
                salesVoucherDetails = salesVoucherDetails.Select((x) =>
                {
                    x.SalesVoucherId = maxcount;
                    x.SalesVoucherDetailsNo = salesVoucherNumber;
                    salesVoucher.SalesVoucherDetails.Add(x);
                    return x;
                }).ToList();

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = salesVoucherNumber;
                    k.AccountsTransactionsAccNo = k.AccountsTransactionsAccNo ?? string.Empty;
                    k.AccountsTransactionsVoucherType = VoucherType.SalesVoucher_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(salesVoucher.SalesVoucherCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;


                var stockReg = new List<StockRegister>();
                foreach (var item in salesVoucher.SalesVoucherDetails)
                {

                    var converontype = this.getConverionTypebyUnitId(item.SalesVoucherDetailsMatId, item.SalesVoucherDetailsUnitId);
                    // get Price by Item Id.
                     //var Price = Convert.ToDecimal(GetAveragePurchasePrice(Convert.ToInt32(item.SalesVoucherDetailsMatId), null));

                    //var response = await _openingStockService.GetAveragePurchasePrice(Convert.ToInt32(item.SalesVoucherDetailsMatId), null);

                    stockReg.Add(new StockRegister()
                    {
                        StockRegisterPurchaseID = salesVoucher.SalesVoucherNo,
                        StockRegisterRefVoucherNo = salesVoucher.SalesVoucherNo,
                        StockRegisterVoucherDate = DateTime.Now.Date,
                        StockRegisterMaterialID = item.SalesVoucherDetailsMatId,
                        StockRegisterQuantity = item.SalesVoucherDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterSIN = 0,
                        StockRegisterSout = item.SalesVoucherDetailsQuantity * (decimal)(converontype) ?? 0,
                        StockRegisterRate = item.SalesVoucherDetailsRate,
                        StockRegisterAmount = item.SalesVoucherDetailsQuantity * item.SalesVoucherDetailsRate ?? 0,
                        StockRegisterFCAmount = item.SalesVoucherDetailsQuantity * item.SalesVoucherDetailsRate * rate,
                        StockRegisterAssignedDate = DateTime.Now.Date,
                        StockRegisterStatus = "A",
                        StockRegisterTransType = VoucherType.SalesVoucher_TYPE,
                        StockRegisterUnitID = item.SalesVoucherDetailsUnitId,
                        StockRegisterLocationID = salesVoucher.SalesVoucherLocationID,
                        StockRegisterJobID = salesVoucher.SalesVoucherJobId,
                        StockRegisterDepID = salesVoucher.SalesVoucherDptID,
                        StockRegisterDelStatus = false
                    });
                }
                _stockRegisterRepository.InsertList(stockReg);



                _salesVoucherRepository.Insert(salesVoucher);

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = salesVoucher.SalesVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = VoucherType.SalesVoucher_TYPE;
                _UserTracking.Insert(trackingData);

                _salesVoucherRepository.TransactionCommit();








                return this.GetSavedSalesVoucherDetails(salesVoucher.SalesVoucherNo);

            }
            catch (Exception ex)
            {
                _salesVoucherRepository.TransactionRollback();
                throw ex;
            }

        }
        public async Task<Response<decimal>> GetAveragePurchasePrice(int itemId, int? locationId)
        {
            try
            {
                var totalQuantity = (from sr in _context.StockRegister
                                     where (locationId == null || sr.StockRegisterLocationID == locationId)
                                     && sr.StockRegisterMaterialID == itemId
                                     group sr by 1 into og
                                     select new
                                     {
                                         Total = og.Sum(item => (int)(item.StockRegisterSIN ?? 0 - item.StockRegisterSout ?? 0)),

                                     }).FirstOrDefault().Total;

                var rateQuantiyFromStock = await (from sr in _context.StockRegister
                                                  where (locationId == null || sr.StockRegisterLocationID == locationId)
                                                  && sr.StockRegisterMaterialID == itemId

                                                  select new PurVoucherRateQuantityModel
                                                  {
                                                      Quantity = sr.StockRegisterQuantity,
                                                      Rate = sr.StockRegisterRate,
                                                      PurchaseDate = sr.StockRegisterVoucherDate
                                                  }).OrderByDescending(y => y.PurchaseDate).ToListAsync();

                var PurchaseVoucherIds = await _context.PurchaseVoucher.Where(x => x.PurchaseVoucherStatus == "P").Select(o => o.PurchaseVoucherPurID).ToListAsync();
                var purchseVouchers = await (from pvd in _context.PurchaseVoucherDetails
                                             join pv in _context.PurchaseVoucher on (long)pvd.PurchaseVoucherDetailsPrId equals pv.PurchaseVoucherPurID
                                             where pvd.PurchaseVoucherDetailsMaterialId == itemId
                                            && PurchaseVoucherIds.Contains((long)pvd.PurchaseVoucherDetailsPrId)
                                             select new PurVoucherRateQuantityModel
                                             {
                                                 Rate = (long)(pvd.PurchaseVoucherDetailsRate ?? 0),
                                                 Quantity = pvd.PurchaseVoucherDetailsQuantity ?? 0,
                                                 PurchaseDate = pv.PurchaseVoucherPurchaseDate

                                             }).OrderByDescending(y => y.PurchaseDate).ToListAsync();

                decimal actualQuantity = 0;
                decimal actualRate = 0;
                decimal quantity = 0;
                decimal rate = 0;
                actualQuantity = totalQuantity;

                if (rateQuantiyFromStock.Count() > 0)
                {
                    foreach (var pvd in rateQuantiyFromStock)
                    {
                        rate = pvd.Rate ?? 0;
                        quantity = pvd.Quantity ?? 0;
                        if (actualQuantity > 0)
                        {
                            if (pvd.Quantity >= actualQuantity)
                            {
                                quantity = actualQuantity;
                                actualRate = actualRate + (rate * quantity);
                                actualQuantity = actualQuantity - quantity;
                            }
                        }
                    }
                }
                else if (purchseVouchers.Count() > 0)
                {
                    foreach (var pvd in purchseVouchers)
                    {
                        rate = pvd.Rate ?? 0;
                        quantity = pvd.Quantity ?? 0;
                        if (actualQuantity > 0)
                        {
                            if (pvd.Quantity >= actualQuantity)
                            {
                                quantity = actualQuantity;
                                actualRate = actualRate + (rate * quantity);
                                actualQuantity = actualQuantity - quantity;
                            }
                        }
                    }
                }
                else
                {
                    //get from item master
                    var im = _context.ItemMaster.Where(k => k.ItemMasterItemId == itemId).FirstOrDefault();
                    actualRate = im.ItemMasterLastPurchasePrice ?? 0;
                }

                decimal returnAvgPrice = 0;
                if (totalQuantity <= 0)
                {
                    totalQuantity = 1;
                }
                returnAvgPrice = actualRate / totalQuantity;
                return Response<decimal>.Success(returnAvgPrice, "Data found");
            }
            catch (Exception ex)
            {
                return Response<decimal>.Fail(0, ex.Message);
            }
        }

        private void createAccountTransactionEntries(SalesVoucher salesVoucher, List<AccountsTransactions> accountsTransactions)
        {
            decimal currencyRate = getCurrencyRate(salesVoucher.SalesVoucherCurrencyId);

            if (salesVoucher.SalesVoucherType == "Credit")

            {

            }
            else
            {

            }
            accountsTransactions = accountsTransactions.Select((k) =>
            {
                k.AccountsTransactionsAccNo = getAccountNoByPartyName(salesVoucher.SalesVoucherPartyName); // need to modify for its original acc no, this is just to test the field
                k.AccountsTransactionsVoucherNo = salesVoucher.SalesVoucherNo;
                k.AccountsTransactionsVoucherType = VoucherType.SalesVoucher_TYPE;
                k.AccountsTransactionsStatus = AccountStatus.Approved;
                k.AccountsTransactionsTransDate = DateTime.Now.Date;
                k.AccountsTransactionsParticulars = $"{VoucherType.SalesVoucher_TYPE} {salesVoucher.CurrencyMasterCurrencyName}"; //need to replace salesVoucher.CurrencyMasterCurrencyName by customer name
                k.AccountsTransactionsDebit = (decimal)salesVoucher.SalesVoucherNetAmount * currencyRate; //need replace by its original value
                k.AccountsTransactionsCredit = 0;
                k.AccountsTransactionsDescription = string.Empty;//need to add description here if needed
                k.AccountsTransactionsUserId = salesVoucher.SalesVoucherUserID ?? k.AccountsTransactionsUserId;
                k.AccountsTransactionsStatus = "A"; //you can change it if needed
                k.AccountsTransactionsTstamp = DateTime.Now; // can be changable if needed for date and time
                k.RefNo = string.Empty; // you can add here the Ref no if needed
                k.AccountsTransactionsFsno = salesVoucher.SalesVoucherFSNO ?? 0m;
                k.AccountsTransactionsFcAllocDebit = 0;
                k.AccountsTransactionsFcAllocCredit = 0;
                k.AccountsTransactionsAllocCredit = 0;
                k.AccountsTransactionsAllocDebit = 0;
                k.AccountsTransactionsAllocBalance = (decimal)salesVoucher.SalesVoucherNetAmount * currencyRate;//need to replace by its original value
                k.AccountsTransactionsFcAllocBalance = (decimal)salesVoucher.SalesVoucherNetAmount * currencyRate;//need to replace by its original value
                k.AccountsTransactionsFcCredit = 0;
                k.AccountsTransactionsFcDebit = (decimal)salesVoucher.SalesVoucherNetAmount * currencyRate;//need to replace by its original value
                k.AccountsTransactionsLocation = salesVoucher.SalesVoucherLocationID;
                k.AccountsTransactionsJobNo = salesVoucher.SalesVoucherJobId;
                k.AccountsTransactionsCurrencyId = salesVoucher.SalesVoucherCurrencyId;
                k.AccountsTransactionsFcRate = currencyRate;
                k.AccountsTransactionsLpoNo = string.Empty; // change it to desired option if needed
                k.AccountsTransactionsDeptId = -1; // change it to desired option if needed
                return k;
            }).ToList();
            _accountTransactionRepository.InsertList(accountsTransactions);
        }

        private string getAccountNoByPartyName(string partyName)
        {
            try
            {

                string maAcc = this.accMaster.GetAsQueryable().Where(x => x.MaAccName.ToUpper().Equals(partyName.ToUpper())).Select(x => x.MaAccNo).FirstOrDefault();

                return maAcc == null ? "" : maAcc;
            }
            catch
            {
                return string.Empty;
            }
        }

        private decimal getCurrencyRate(int? salesVoucherCurrencyId)
        {
            try
            {
                return (decimal)this._currencyMasterRepository.GetAsQueryable().FirstOrDefault(x => x.CurrencyMasterCurrencyId == salesVoucherCurrencyId).CurrencyMasterCurrencyRate;
            }
            catch
            {
                return 1;
            }
        }


        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<SalesVoucher> GetSalesVoucher()
        {
            //IEnumerable<SalesVoucher> salesVoucher_ALL = _salesVoucherRepository.GetAll().Where(k => k.SalesVoucherDelStatus == false || k.SalesVoucherDelStatus == null).ToList();
            //IEnumerable<CurrencyMaster> _currencyMaster = _currencyMasterRepository.GetAll().ToList();

            //foreach (var salesVoucher in salesVoucher_ALL)
            //{
            //    salesVoucher.CurrencyMasterCurrencyName = _currencyMaster
            //                                                 .Where(c => c.CurrencyMasterCurrencyId == salesVoucher.SalesVoucherCurrencyId)
            //                                                 .FirstOrDefault() != null ? _currencyMaster
            //                                                 .Where(c => c.CurrencyMasterCurrencyId == salesVoucher.SalesVoucherCurrencyId)
            //                                                 .FirstOrDefault().CurrencyMasterCurrencyName : null;
            //}


            var salV = (from s in _salesVoucherRepository.GetAsQueryable()
                        join c in _customerMasterRepository.GetAsQueryable() on s.SalesVoucherPartyId equals c.CustomerMasterCustomerNo
                        join cr in _currencyMasterRepository.GetAsQueryable() on s.SalesVoucherCurrencyId equals cr.CurrencyMasterCurrencyId
                        where s.SalesVoucherDelStatus == null || s.SalesVoucherDelStatus == false
                        select new SalesVoucher()
                        {
                            SalesVoucherId = s.SalesVoucherId,
                            SalesVoucherNo = s.SalesVoucherNo,
                            SalesVoucherDate = s.SalesVoucherDate,
                            SalesVoucherType = s.SalesVoucherType,
                            SalesVoucherPartyId = c.CustomerMasterCustomerNo,
                            SalesVoucherPartyName = c.CustomerMasterCustomerName,
                            SalesVoucherPartyAddress = c.CustomerMasterCustomerLocation,
                            SalesVoucherPartyVatNo = c.CustomerMasterCustomerVatNo,
                            SalesVoucherRefNo = s.SalesVoucherRefNo,
                            SalesVoucherContPerson = s.SalesVoucherContPerson,
                            SalesVoucherDlvNo = s.SalesVoucherDlvNo,
                            SalesVoucherDlvDate = s.SalesVoucherDlvDate,
                            SalesVoucherSONo = s.SalesVoucherSONo,
                            SalesVoucherSODate = s.SalesVoucherSODate,
                            SalesVoucherQtnNo = s.SalesVoucherQtnNo,
                            SalesVoucherQtnDate = s.SalesVoucherQtnDate,
                            SalesVoucherSalesManID = s.SalesVoucherSalesManID,
                            SalesVoucherDptID = s.SalesVoucherDptID,
                            SalesVoucherEnqNo = s.SalesVoucherEnqNo,
                            SalesVoucherEnqDate = s.SalesVoucherEnqDate,
                            SalesVoucherDescription = s.SalesVoucherDescription,
                            SalesVoucherExcludeVAT = s.SalesVoucherExcludeVAT,
                            SalesVoucherLocationID = s.SalesVoucherLocationID,
                            SalesVoucherUserID = s.SalesVoucherUserID,
                            SalesVoucherCurrencyId = cr.CurrencyMasterCurrencyId,
                            CurrencyMasterCurrencyName = cr.CurrencyMasterCurrencyName,
                            SalesVoucherCompanyId = s.SalesVoucherCompanyId,
                            SalesVoucherJobId = s.SalesVoucherJobId,
                            SalesVoucherFSNO = s.SalesVoucherFSNO,
                            SalesVoucherFc_Rate = s.SalesVoucherFc_Rate,
                            SalesVoucherStatus = s.SalesVoucherStatus,
                            SalesVoucherTotalGrossAmount = s.SalesVoucherTotalGrossAmount,
                            SalesVoucherTotalItemDisAmount = s.SalesVoucherTotalItemDisAmount,
                            SalesVoucherTotalActualAmount = s.SalesVoucherTotalActualAmount,
                            SalesVoucherTotalDisPer = s.SalesVoucherTotalDisPer,
                            SalesVoucherTotalDisAmount = s.SalesVoucherTotalDisAmount,
                            SalesVoucherVat_AMT = s.SalesVoucherVat_AMT,
                            SalesVoucherVat_Per = s.SalesVoucherVat_Per,
                            SalesVoucherVat_RoundSign = s.SalesVoucherVat_RoundSign,
                            SalesVoucherVat_RountAmt = s.SalesVoucherVat_RountAmt,
                            SalesVoucherNetDisAmount = s.SalesVoucherNetDisAmount,
                            SalesVoucherNetAmount = s.SalesVoucherNetAmount,
                            SalesVoucherHeader = s.SalesVoucherHeader,
                            SalesVoucherDelivery = s.SalesVoucherDelivery,
                            SalesVoucherNotes = s.SalesVoucherNotes,
                            SalesVoucherFooter = s.SalesVoucherFooter,
                            SalesVoucherPaymentTerms = s.SalesVoucherPaymentTerms,
                            SalesVoucherSubject = s.SalesVoucherSubject,
                            SalesVoucherContent = s.SalesVoucherContent,
                            SalesVoucherRemarks1 = s.SalesVoucherRemarks1,
                            SalesVoucherRemarks2 = s.SalesVoucherRemarks2,
                            SalesVoucherTermsAndCondition = s.SalesVoucherTermsAndCondition,
                            SalesVoucherShippinAddress = s.SalesVoucherShippinAddress,
                            SalesVoucherDelStatus = s.SalesVoucherDelStatus,
                            SalesVoucherShortNo = s.SalesVoucherShortNo,
                        }).ToList();





            return salV;
        }
        public SalesVoucherModel GetSavedSalesVoucherDetails(string pvno)
        {
            SalesVoucherModel salesVoucherModel = new SalesVoucherModel();
            //salesVoucherModel.salesVoucher = _salesVoucherRepository.GetAsQueryable().Where(k => k.SalesVoucherNo == pvno && k.SalesVoucherDelStatus == false).SingleOrDefault();
            salesVoucherModel.salesVoucher = _salesVoucherRepository.GetAsQueryable().Where(k => k.SalesVoucherNo == pvno).SingleOrDefault();
            salesVoucherModel.salesVoucherDetails = _salesVoucherDetailsRepository.GetAsQueryable().Where(x => x.SalesVoucherDetailsNo == pvno && (x.SalesVoucherDetailsDelStatus == false || x.SalesVoucherDetailsDelStatus == null)).ToList();

            salesVoucherModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(x => x.AccountsTransactionsVoucherNo == pvno && x.AccountstransactionsDelStatus != true).ToList();

            if (salesVoucherModel.salesVoucherDetails.Count > 0)
            {
                foreach (var item in salesVoucherModel.salesVoucherDetails)
                {
                    decimal sumReturnedQty = 0;
                    var stockQuery = _stockRegisterRepository.GetAsQueryable()
                    .Where(a => a.StockRegisterMaterialID == item.SalesVoucherDetailsMatId &&
                                a.StockRegisterTransType == "SalesReturn" &&
                                a.StockRegisterRefVoucherNo == item.SalesVoucherDetailsNo);

                    sumReturnedQty = stockQuery.Any()
                                       ? stockQuery.Sum(s => (decimal?)s.StockRegisterSout) ?? 0
                                       : 0;

                    if (sumReturnedQty != null)
                    {
                        item.SalesReturnQtyTill = sumReturnedQty;
                    }

                }
            }


            return salesVoucherModel;
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {






                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.SalesVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.SalesVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.SalesVoucher_TYPE,
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

        public SalesVoucher GetSalesVoucherDetailsByMasterNo(string SalesVoucherNo)
        {
            try
            {
                SalesVoucher voucherModel = new SalesVoucher();
                voucherModel = _salesVoucherRepository.GetAsQueryable().Where(a => a.SalesVoucherNo == SalesVoucherNo).FirstOrDefault();
                voucherModel.SalesVoucherDetails = _salesVoucherDetailsRepository.GetAsQueryable().Where(o => o.SalesVoucherDetailsNo == SalesVoucherNo && o.SalesVoucherDetailsDelStatus != true).ToList();


                if (voucherModel.SalesVoucherDetails.Count > 0)
                {

                    foreach (var item in voucherModel.SalesVoucherDetails)
                    {
                        item.SalesVoucherDetailsNoNavigation = null;
                        decimal sumReturnedQty = 0;
                        var stockQuery = _stockRegisterRepository.GetAsQueryable()
                        .Where(a => a.StockRegisterMaterialID == item.SalesVoucherDetailsMatId &&
                                    a.StockRegisterTransType == "SalesReturn" &&
                                    a.StockRegisterRefVoucherNo == item.SalesVoucherDetailsNo);

                        sumReturnedQty = stockQuery.Any()
                                           ? stockQuery.Sum(s => (decimal?)s.StockRegisterSIN) ?? 0
                                           : 0;

                        if (sumReturnedQty != null)
                        {
                            item.SalesReturnQtyTill = sumReturnedQty;
                        }

                    }
                }

                return voucherModel;


            }

            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response<GridWrapperResponse<List<GetSalesVoucherResponse>>>> GetSalesVoucher(GenericGridViewModel model)
        {
            try
            {
                List<GetSalesVoucherResponse> response = new List<GetSalesVoucherResponse>();
                var orders = await _salesVoucherRepository.GetAsQueryable().Where(x => x.SalesVoucherEnqNo != null).Skip(model.Skip).Take(model.Take).ToListAsync();
                response.AddRange(orders.Select(x => new GetSalesVoucherResponse
                {
                    SalesVoucherId = (decimal)x.SalesVoucherId,
                    SalesVoucherNo = x.SalesVoucherNo
                }).ToList());
                var gridResponse = new GridWrapperResponse<List<GetSalesVoucherResponse>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<GetSalesVoucherResponse>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetSalesVoucherResponse>>>.Fail(new GridWrapperResponse<List<GetSalesVoucherResponse>>(), ex.Message);
            }
        }

        public IQueryable GetSalesReportSummaryWise()
        {
            try
            {
                var SalesVoucherList = _salesVoucherRepository.GetAll();
                var SalesManMasterList = _salesManMasterRepository.GetAll();// context.SalesManMaster.ToList();

                var SalesReturnDetails = _salesReturnDtls.GetAll().Select(o => new
                {
                    SalRetAmt = o.SalesReturnDetailsNetAmt,
                    SalesReturnDetailsNo = o.SalesReturnDetailsNo,
                }).ToList().GroupBy(g => new
                {
                    SalesReturnDetailsNo = g.SalesReturnDetailsNo
                }).Select(o => new
                {
                    SalesReturnDetailsNo = o.Key.SalesReturnDetailsNo,
                    SalRetAmt = o.Sum(s => s.SalRetAmt)
                }).ToList();

                var SalesReturnMaster = _salesReturn.GetAll().Select(o => new
                {
                    SalRetVatAmt = o.SalesReturnVat_RountAmt,
                    SalesReturnNo = o.SalesReturnNo,
                    SalRetAmt = o.SalesReturnNetAmount,
                    SalRetRountAmt = o.SalesReturnVat_RoundSign == "-" ? o.SalesReturnVat_RountAmt * -1 : o.SalesReturnVat_RountAmt,
                }).ToList().GroupBy(g => new
                {
                    SalesReturnNo = g.SalesReturnNo,
                }).Select(o => new
                {
                    SalesReturnNo = o.Key.SalesReturnNo,
                    SalRetVatAmt = o.Sum(s => s.SalRetVatAmt),
                    SalRetAmt = o.FirstOrDefault().SalRetAmt,
                    SalRetRountAmt = o.FirstOrDefault().SalRetRountAmt,
                }).ToList();
                var Include_Sales_Return_Details_In_Reports = _optionMaster.GetAsQueryable().FirstOrDefault(o => o.OptionsMasterDescription == "Include_Sales_Return_Details_In_Reports" && o.OptionsMasterType == "Yes");
                if (Include_Sales_Return_Details_In_Reports != null)
                {
                    var data1 = (from hdr in SalesVoucherList
                                 join dtl in SalesReturnDetails on hdr.SalesVoucherNo equals dtl.SalesReturnDetailsNo into ldtl
                                 from retDtl in ldtl.DefaultIfEmpty()
                                 join mstr in SalesReturnMaster on hdr.SalesVoucherNo equals mstr.SalesReturnNo into lmstr
                                 from retMstr in lmstr.DefaultIfEmpty()
                                 where hdr.SalesVoucherNo != null
                                 select new
                                 {
                                     voucherNo = hdr.SalesVoucherNo,
                                     voucherDate = hdr.SalesVoucherDate,
                                     voucherType = hdr.SalesVoucherType,
                                     CustomerName = _customerMasterRepository.GetAsQueryable().FirstOrDefault(x => x.CustomerMasterCustomerNo == hdr.SalesVoucherPartyId).CustomerMasterCustomerName,//  hdr.SalesVoucherPartyName,// hdr.SalesVoucherContPerson == null ? hdr.SalesVoucherPartyName : hdr.SalesVoucherContPerson,
                                     SalesmanName = SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID) != null ? SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID).SalesManMasterSalesManName : "",
                                     GrossAmount = hdr.SalesVoucherTotalGrossAmount,
                                     Discount = hdr.SalesVoucherNetDisAmount ?? 0 + hdr.SalesVoucherTotalDisAmount ?? 0,
                                     SalRetAmt = retMstr != null ? retMstr.SalRetAmt : 0,
                                     VATAmount = hdr.SalesVoucherVat_AMT - (retMstr != null ? (decimal)retMstr.SalRetVatAmt : 0),
                                     NetAmount = hdr.SalesVoucherNetAmount - (retMstr != null ? (decimal)retMstr.SalRetAmt : 0),
                                 }).ToList();

                    var data2 = (from hdr in SalesVoucherList
                                 join mstr in SalesReturnMaster on hdr.SalesVoucherNo equals mstr.SalesReturnNo into lmstr
                                 from retMstr in lmstr.DefaultIfEmpty()
                                 where hdr.SalesVoucherNo != null
                                 select new
                                 {
                                     voucherNo = hdr.SalesVoucherNo,
                                     voucherDate = hdr.SalesVoucherDate,
                                     voucherType = hdr.SalesVoucherType,
                                     CustomerName = _customerMasterRepository.GetAsQueryable().FirstOrDefault(x => x.CustomerMasterCustomerNo == hdr.SalesVoucherPartyId).CustomerMasterCustomerName,//  hdr.SalesVoucherPartyName,// hdr.SalesVoucherContPerson == null ? hdr.SalesVoucherPartyName : hdr.SalesVoucherContPerson,

                                     SalesmanName = SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID) != null ? SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID).SalesManMasterSalesManName : "",
                                     GrossAmount = hdr.SalesVoucherTotalGrossAmount,
                                     Discount = hdr.SalesVoucherNetDisAmount ?? 0 + hdr.SalesVoucherTotalDisAmount ?? 0,
                                     SalRetAmt = retMstr != null ? retMstr.SalRetAmt : 0,
                                     VATAmount = hdr.SalesVoucherVat_AMT - (retMstr != null ? (decimal)retMstr.SalRetVatAmt : 0),
                                     NetAmount = hdr.SalesVoucherNetAmount - (retMstr != null ? (decimal)retMstr.SalRetAmt : 0),
                                 }).ToList();


                    var finalData = data1.Union(data2).AsQueryable();
                    return finalData;
                }
                else
                {
                    var data = (from hdr in SalesVoucherList
                                join saleman in SalesManMasterList on hdr.SalesVoucherSalesManID equals saleman.SalesManMasterSalesManId into lsmm
                                from smm in lsmm.DefaultIfEmpty()
                                where hdr.SalesVoucherNo != null
                                select new
                                {
                                    SalesmanName = SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID) != null ? SalesManMasterList.FirstOrDefault(o => o.SalesManMasterSalesManId == hdr.SalesVoucherSalesManID).SalesManMasterSalesManName : "",
                                    voucherNo = hdr.SalesVoucherNo,
                                    voucherDate = hdr.SalesVoucherDate,
                                    voucherType = hdr.SalesVoucherType,
                                    CustomerName = _customerMasterRepository.GetAsQueryable().FirstOrDefault(x => x.CustomerMasterCustomerNo == hdr.SalesVoucherPartyId).CustomerMasterCustomerName,//  hdr.SalesVoucherPartyName,// hdr.SalesVoucherContPerson == null ? hdr.SalesVoucherPartyName : hdr.SalesVoucherContPerson,
                                    GrossAmount = hdr.SalesVoucherTotalGrossAmount,
                                    Discount = hdr.SalesVoucherNetDisAmount ?? 0 + hdr.SalesVoucherTotalDisAmount ?? 0,
                                    SalRetAmt = 0,
                                    VATAmount = hdr.SalesVoucherVat_AMT,
                                    NetAmount = hdr.SalesVoucherNetAmount,
                                }).AsQueryable();
                    return data;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable GetSalesReportDetailWise()
        {
            try
            {
                var data = (from svd in _salesVoucherDetailsRepository.GetAll()
                            join sv in _salesVoucherRepository.GetAll() on svd.SalesVoucherDetailsNo equals sv.SalesVoucherNo into lsv
                            from salesVou in lsv.DefaultIfEmpty()
                            join itemMas in _itemMasterRepository.GetAll() on (long)svd.SalesVoucherDetailsMatId equals itemMas.ItemMasterItemId into litemm
                            from itemmaster in litemm.DefaultIfEmpty()
                            join um in _unitMasterRepository.GetAll() on svd.SalesVoucherDetailsUnitId equals um.UnitMasterUnitId into lum
                            from unitMaster in lum.DefaultIfEmpty()
                            join unitDet in _UnitDetailsRepository.GetAll() on new { UnitId = (int)svd.SalesVoucherDetailsUnitId, ItemId = (int)svd.SalesVoucherDetailsMatId }
                            equals new { UnitId = unitDet.UnitDetailsUnitId, ItemId = (int)unitDet.UnitDetailsItemId } into lud
                            from unitDetail in lud.DefaultIfEmpty()
                            where svd.SalesVoucherDetailsNo != null
                            select new
                            {
                                NetAmount = svd.SalesVoucherDetailsNetAmt,
                                Discount = svd.SalesVoucherDetailsDiscAmount,
                                GrossAmt = svd.SalesVoucherDetailsGrossAmount,
                                //UnitPrice = svd.SalesVoucherDetailsCostPrice,
                                UnitDes = unitMaster.UnitMasterUnitShortName,
                                MatDes = itemmaster.ItemMasterItemName,
                                //PartNo = itemmaster.ItemMasterPartNo,
                                VoucherDate = salesVou.SalesVoucherDate,
                                VoucherNo = salesVou.SalesVoucherNo,
                                QuantitySold = svd.SalesVoucherDetailsQuantity ?? 0,
                                //unitPrice = itemmaster.ItemMasterUnitPrice ?? 0,
                                baseUnit = svd.SalesVoucherDetailsQuantity * (decimal)(unitDetail == null ? 1 : unitDetail.UnitDetailsConversionType ?? 1)   //SD.Sold_Qty * ISNULL(UD.ConversionType, 1)
                                // ItemGroup = 
                            }).AsQueryable();
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //public POS_Sales_Voucher InsertPosSalesVoucher(POS_Sales_Voucher salesVoucher, List<POS_Sales_Voucher_Details> posSalesVoucherDetails)
        //{
        //    try
        //    {
        //        _posSalesVoucherRepository.BeginTransaction();
        //        string salesVoucherNumber = this.GeneratePosVoucherNo(salesVoucher.VoucherDate).VouchersNumbersVNo;
        //        salesVoucher.VoucherNo = salesVoucherNumber;


        //        int maxcount = 0;
        //        maxcount = Convert.ToInt32(
        //            _posSalesVoucherRepository.GetAsQueryable()
        //            .DefaultIfEmpty().Max(o => o == null ? 0 : o.V_Id) + 1);

        //        posSalesVoucherDetails = posSalesVoucherDetails.Select((x) =>
        //        {
        //            x.SNO = maxcount;
        //            x.VoucherNo = salesVoucherNumber;
        //            return x;
        //        }).ToList();

        //        var stockReg = new List<StockRegister>();
        //        foreach (var item in salesVoucher.posSalesVoucherDetails)
        //        {
        //            stockReg.Add(new StockRegister()
        //            {
        //                StockRegisterPurchaseID = salesVoucher.VoucherNo,
        //                StockRegisterRefVoucherNo = salesVoucher.VoucherNo,
        //                StockRegisterVoucherDate = DateTime.Now.Date,
        //                StockRegisterMaterialID = Convert.ToInt32(item.ItemId),
        //                StockRegisterQuantity = item.Sold_Qty,
        //                StockRegisterSIN = 0,
        //                StockRegisterSout = item.Sold_Qty,
        //                StockRegisterRate = item.UnitPrice,
        //                StockRegisterAmount = item.GrossAmt ?? 0,
        //                StockRegisterFCAmount = item.GrossAmt,
        //                StockRegisterAssignedDate = DateTime.Now.Date,
        //                StockRegisterStatus = "A",
        //                StockRegisterTransType = salesVoucher.Voucher_Type,
        //                StockRegisterUnitID = Convert.ToInt32(item.UnitId),
        //                StockRegisterLocationID = salesVoucher.Location,
        //                StockRegisterDepID = Convert.ToInt32(salesVoucher.DeptID),
        //                StockRegisterDelStatus = false
        //            });
        //        }
        //        _stockRegisterRepository.InsertList(stockReg);


        //        _posSalesVoucherRepository.Insert(salesVoucher);
        //        _posSalesVoucherDetailsRepository.InsertList(posSalesVoucherDetails);
        //        _posSalesVoucherRepository.TransactionCommit();
        //        return this.GetSavedPosSalesVoucherDetails(salesVoucher.VoucherNo);

        //    }
        //    catch (Exception ex)
        //    {
        //        _posSalesVoucherRepository.TransactionRollback();
        //        throw ex;
        //    }

        //}

        //public POS_Sales_Voucher GetSavedPosSalesVoucherDetails(string pvno)
        //{
        //    POS_Sales_Voucher salesVoucher = new POS_Sales_Voucher();

        //    salesVoucher = _posSalesVoucherRepository.GetAsQueryable().Where(k => k.VoucherNo == pvno).SingleOrDefault();
        //    salesVoucher.posSalesVoucherDetails = _posSalesVoucherDetailsRepository.GetAsQueryable().Where(x => x.VoucherNo == pvno).ToList();


        //    return salesVoucher;
        //}
        public VouchersNumbers GeneratePosVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                // var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.SalesVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PosSalesVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;



                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = "POSSV" + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PosSalesVoucher_TYPE,
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

        //public POS_Sales_Voucher UpdatePosSalesVoucher(POS_Sales_Voucher salesVoucher, List<POS_Sales_Voucher_Details> PosSalesVoucherDetails)
        //{
        //    try
        //    {
        //        _posSalesVoucherRepository.BeginTransaction();
        //        _posSalesVoucherRepository.Update(salesVoucher);
        //        _posSalesVoucherDetailsRepository.UpdateList(PosSalesVoucherDetails);
        //        _posSalesVoucherRepository.TransactionCommit();

        //        return this.GetSavedPosSalesVoucherDetails(salesVoucher.VoucherNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _posSalesVoucherRepository.TransactionRollback();
        //        throw ex;
        //    }
        //}

        //public IEnumerable<POS_Sales_Voucher> GetPosSalesVoucher()
        //{
        //    var salV = (from s in _posSalesVoucherRepository.GetAsQueryable()
        //                join c in _customerMasterRepository.GetAsQueryable() on s.Customer_ID equals c.CustomerMasterCustomerNo
        //                select new POS_Sales_Voucher()
        //                {
        //                    V_Id = s.V_Id,
        //                    VoucherNo = s.VoucherNo,
        //                    VoucherDate = s.VoucherDate,
        //                    Voucher_Type = s.Voucher_Type,
        //                    Customer_ID = c.CustomerMasterCustomerNo,
        //                    CustomerName = c.CustomerMasterCustomerName,
        //                    refno = s.refno,
        //                    DeptID = s.DeptID,
        //                    Location = s.Location,
        //                    UserId = s.UserId,
        //                    CompanyId = s.CompanyId,
        //                    DiscountPer = s.DiscountPer,
        //                    Vat_Per = s.Vat_Per,
        //                    Vat_RoundSign = s.Vat_RoundSign,
        //                    Vat_RountAmt = s.Vat_RountAmt,
        //                    NetDiscount = s.NetDiscount,
        //                    NetAmount = s.NetAmount,
        //                    Remarks = s.Remarks,
        //                    ShortNo = s.ShortNo,
        //                    CashCustName = s.CashCustName,
        //                    Counter_ID_N = s.Counter_ID_N,
        //                    Discount = s.Discount,
        //                    GrossAmount = s.GrossAmount,
        //                    HOLD_B = s.HOLD_B,
        //                    WorkPeriodID = s.WorkPeriodID,
        //                    InvoiceType = s.InvoiceType,
        //                    PaymentMode = s.PaymentMode,
        //                    Vat_TaxableAmt = s.Vat_TaxableAmt,
        //                    Refrence = s.Refrence,
        //                    Salesman = s.Salesman,
        //                    Retail_B = s.Retail_B,
        //                    VatAmount = s.VatAmount,
        //                    SV_SM_VoucherNo = s.SV_SM_VoucherNo,
        //                    Shift_ID_N = s.Shift_ID_N,
        //                    StationID = s.StationID,
        //                    SV_NoVat_B = s.SV_NoVat_B,
        //                    SV_SM_Bal = s.SV_SM_Bal,
        //                    Vat_AMT = s.Vat_AMT,
        //                    Vatable_TotAmt = s.Vatable_TotAmt
        //                }).ToList();



        //    return salV;
        //}

        #region Pos Sales Hold
        public VouchersNumbers GeneratePosSaleHoldVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                // var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.SalesVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PosSalesVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;



                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = "POSSH" + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PosSalesVoucher_TYPE,
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

        public SalesHold InsertPosSalesHold(SalesHold salesHold, List<SalesHoldDetails> salesHoldDetails)
        {
            try
            {
                _salesHoldRepository.BeginTransaction();

                string salesVoucherNumber = this.GeneratePosSaleHoldVoucherNo(salesHold.VoucherDate).VouchersNumbersVNo;
                salesHold.VoucherNo = salesVoucherNumber;


                decimal maxcount = 0;
                maxcount = (
                    _salesHoldRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.V_ID) + 1);

                salesHold.V_ID = maxcount;
                salesHoldDetails = salesHoldDetails.Select((x) =>
                {
                    x.SNO = maxcount;
                    x.VoucherNo = salesVoucherNumber;
                    return x;
                }).ToList();

                salesHold.SalesHoldDetails.Clear();

                long max1count = 0;
                max1count =
                    _salesHoldDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (long)o.SDet_ID) + 1;
                foreach (var item in salesHoldDetails)
                {
                    item.SDet_ID = max1count;
                    item.SNO = salesHold.V_ID;
                    item.VoucherNo = salesVoucherNumber ?? "";
                    salesHold.SalesHoldDetails.Add(item);
                    max1count++;
                }

                _salesHoldRepository.Insert(salesHold);

                _salesHoldRepository.TransactionCommit();

                return salesHold;
            }
            catch (Exception ex)
            {
                _salesHoldRepository.TransactionRollback();
                throw ex;
            }
        }

        public List<SalesHold> DeletePosSalesHold(decimal id)
        {
            try
            {
                _salesHoldRepository.BeginTransaction();

                SalesHold SalesHold = new SalesHold();
                SalesHold = this.GetPosSalesHold(id);
                if (SalesHold != null)
                {
                    _salesHoldDetailsRepository.DeleteList(SalesHold.SalesHoldDetails.ToList());



                    _salesHoldRepository.Delete(SalesHold);
                    _salesHoldRepository.TransactionCommit();
                }

                return _salesHoldRepository.GetAll().Where(a => a.V_ID > 0).ToList();
            }
            catch (Exception ex)
            {
                _salesHoldRepository.TransactionRollback();
                throw ex;
            }
        }

        public SalesHold GetPosSalesHold(decimal id)
        {
            SalesHold SalesHold = new SalesHold();

            try
            {

                SalesHold = _salesHoldRepository.GetAsQueryable().Where(k => k.V_ID == id).FirstOrDefault();
                if (SalesHold != null)
                {
                    SalesHold.SalesHoldDetails = _salesHoldDetailsRepository.GetAsQueryable().Where(x => x.SNO == id).ToList();
                }
                return SalesHold;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<SalesHold> GetPosSalesHoldList()
        {

            try
            {

                IEnumerable<SalesHold> SalesHold = _salesHoldRepository.GetAll();
                return SalesHold;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion


        public POS_WorkPeriod StartPosWP(POS_WorkPeriod wp)
        {
            try
            {
                _POSWorkPeriodRepository.BeginTransaction();

                wp.StartDate = DateTime.Now;
                wp.Enddate = null;
                wp.StartTime = DateTime.Now;
                wp.EndTime = null;
                wp.LoginComputerName = Environment.MachineName;
                var hostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(hostName);
                IPAddress ipv4Address = localIPs.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                if (ipv4Address != null)
                {
                    wp.LoginSystemIP = ipv4Address.ToString();
                }
                else
                {
                    wp.LoginSystemIP = null;
                }

                wp.OpeningCash = 0;//query
                wp.OpeningCashUser = 0;
                wp.UserOpeningBal = 0; //text box



                _POSWorkPeriodRepository.Insert(wp);
                _POSWorkPeriodRepository.TransactionCommit();

                return wp;
            }
            catch (Exception ex)
            {
                _POSWorkPeriodRepository.TransactionRollback();
                throw ex;
            }
        }

        public POS_WorkPeriod EndPosWP(POS_WorkPeriod wp)
        {
            try
            {
                _POSWorkPeriodRepository.BeginTransaction();

                wp.Enddate = DateTime.Now;
                wp.StartDate = null;
                wp.EndTime = DateTime.Now;
                wp.StartTime = null;
                wp.LoginComputerName = Environment.MachineName;
                var hostName = Dns.GetHostName();
                IPAddress[] localIPs = Dns.GetHostAddresses(hostName);
                IPAddress ipv4Address = localIPs.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                if (ipv4Address != null)
                {
                    wp.LoginSystemIP = ipv4Address.ToString();
                }
                else
                {
                    wp.LoginSystemIP = null;
                }


                wp.ClosingCash = 0;  //query + POS_SalesTransaction_Details.amount
                wp.ClosingCashUser = 0;
                wp.UserClosebal = 0; //text box

                wp.DifferenceAmount = 0; //query + POS_SalesTransaction_Details.amount-text box

                _POSWorkPeriodRepository.Insert(wp);
                _POSWorkPeriodRepository.TransactionCommit();

                return wp;
            }
            catch (Exception ex)
            {
                _POSWorkPeriodRepository.TransactionRollback();
                throw ex;
            }
        }
    }
}

