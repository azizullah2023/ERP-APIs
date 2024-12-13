using AutoMapper.Configuration.Annotations;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.StoreWareHouse.Implementation
{
    public class GRNService : IGRNService
    {
        private IRepository<GRNResponse> _grnRepository;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PurchaseVoucher> _purchaseVoucherRepository;
        private IRepository<PurchaseVoucherDetails> _purchaseVoucherDetailsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository; private IRepository<CurrencyMaster> _currencyMasterRepository;
        IRepository<ProgramSettings> _programsettingsRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly InspireErpDBContext _context;
        public static string prefix;
        public GRNService(
            ILogger<PaymentVoucherService> logger,
            IRepository<GRNResponse> grnRepository,
            IRepository<AccountsTransactions> accountTransactionRepository,
            IRepository<StockRegister> stockRegisterRepository,
            IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, IRepository<CurrencyMaster> currencyMasterRepository,
            IRepository<PurchaseVoucher> purchaseVoucherRepository,
            IRepository<PurchaseVoucherDetails> purchaseVoucherDetailsRepository,
            InspireErpDBContext context)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _stockRegisterRepository = stockRegisterRepository;
            _purchaseVoucherRepository = purchaseVoucherRepository;
            _purchaseVoucherDetailsRepository = purchaseVoucherDetailsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _programsettingsRepository = programsettingsRepository;
            _grnRepository = grnRepository; _currencyMasterRepository = currencyMasterRepository;
            _logger = logger;
            _context = context;
            prefix = programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
        }


        public IQueryable GetAllGRN()
        {
            try
            {
                var grnList = _context.PurchaseVoucher.Where(o => o.PurchaseVoucherVoucherNo.Contains("GRN") && o.PurchaseVoucherDelStatus != true).AsQueryable();
                return grnList;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public PurchaseVoucher GetByID(string voucherNo)
        {
            try
            {
                PurchaseVoucher pv = new PurchaseVoucher();
                var userTrackngData = (from hdr in _context.UserTracking.Where(o => o.UserTrackingUserVpType == "GRN" && o.UserTrackingUserVpNo == voucherNo)
                                       join dtl in _context.UserFile on (long)hdr.UserTrackingUserUserId equals dtl.UserId
                                       select new UserTrackingDisplay()
                                       {
                                           Action = hdr.UserTrackingUserVpAction,
                                           Id = dtl.UserId.ToString(),
                                           Name = dtl.UserName,
                                           VType = hdr.UserTrackingUserVpType,
                                           Date = Convert.ToDateTime(hdr.UserTrackingUserChangeDt).ToString("dd/MMM/yyyy"),
                                           Time = Convert.ToDateTime(hdr.UserTrackingUserChangeTime).ToShortTimeString(),
                                           VNo = hdr.UserTrackingUserVpNo,
                                       }).ToList();
                pv = _purchaseVoucherRepository.GetAll().FirstOrDefault(o => o.PurchaseVoucherVoucherNo == voucherNo);
                pv.PurchaseVoucherDetails = _purchaseVoucherDetailsRepository.GetAll().Where(o => o.PurchaseVoucherDetailsVoucherNo == voucherNo).ToList();
                pv.AccountsTransactions = _accountTransactionRepository.GetAll().Where(o => o.AccountsTransactionsVoucherNo == voucherNo && o.AccountstransactionsDelStatus != true).ToList();
                pv.UserTrackingData = userTrackngData;
                return pv;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public PurchaseVoucher updateGRN(PurchaseVoucher grn, List<AccountsTransactions> accountsTransactions)
        {
            try
            {
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(grn.PurchaseVoucherVoucherNo, VoucherType.PurchaseVoucher_TYPE, _accountTransactionRepository);
                clsCommonFunctions.Delete_OldEntryOf_StockRegister(grn.PurchaseVoucherVoucherNo, VoucherType.PurchaseVoucher_TYPE, _stockRegisterRepository);

                foreach (var k in accountsTransactions)
                {
                    k.AccountsTransactionsVoucherNo = grn.PurchaseVoucherVoucherNo;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseVoucher_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    k.AccountsTransactionsUserId = 1;
                }
                _context.AccountsTransactions.AddRange(accountsTransactions);
                _context.SaveChanges();

                _purchaseVoucherRepository.Update(grn);
                var existingGrnList = _purchaseVoucherDetailsRepository.GetAll().Where(o => o.PurchaseVoucherDetailsVoucherNo == grn.PurchaseVoucherVoucherNo).ToList();
                _purchaseVoucherDetailsRepository.DeleteList(existingGrnList);

                List<StockRegister> stockRegisters = new List<StockRegister>();

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(grn.PurchaseVoucherCurrencyID, _currencyMasterRepository);
                var rate = (decimal)currencyRate;

                foreach (var item in grn.PurchaseVoucherDetails)
                {
                    item.PurchaseVoucherDetailsVoucherNo = grn.PurchaseVoucherVoucherNo;
                    if (item.PurchaseVoucherDetailsPurcahseDetailsId != 0)
                    {
                        _purchaseVoucherDetailsRepository.Update(item);

                    }
                    else
                    {
                        _purchaseVoucherDetailsRepository.Insert(item);
                    }

                    var converontype = this.getConverionTypebyUnitId(item.PurchaseVoucherDetailsMaterialId, item.PurchaseVoucherDetailsUnitId);
                    stockRegisters.Add(new StockRegister
                    {
                        StockRegisterMaterialID = item.PurchaseVoucherDetailsMaterialId,
                        StockRegisterRefVoucherNo = grn.PurchaseVoucherVoucherNo,
                        StockRegisterPurchaseID = grn.PurchaseVoucherVoucherNo,
                        StockRegisterVoucherDate = grn.PurchaseVoucherPurchaseDate,
                        StockRegisterQuantity = item.PurchaseVoucherDetailsQuantity * (converontype) ?? 0,
                        StockRegisterSIN = item.PurchaseVoucherDetailsQuantity * (converontype) ?? 0,
                        StockRegisterRate = item.PurchaseVoucherDetailsRate,
                        StockRegisterAmount = item.PurchaseVoucherDetailsQuantity * item.PurchaseVoucherDetailsRate,
                        StockRegisterFCAmount = item.PurchaseVoucherDetailsQuantity * item.PurchaseVoucherDetailsRate * rate,
                        StockRegisterTransType = VoucherType.PurchaseVoucher_TYPE,
                        StockRegisterStatus = AccountStatus.Approved,

                    });

                }
                _stockRegisterRepository.InsertList(stockRegisters);
                //_purchaseVoucherDetailsRepository.UpdateList(grn.PurchaseVoucherDetails.ToList());

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Update";
                trackingData.UserTrackingUserVpNo = grn.PurchaseVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "GRN";
                _context.UserTracking.Add(trackingData);
                _context.SaveChanges();
                return grn;
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
                return (decimal)_context.UnitDetails.FirstOrDefault(x => x.UnitDetailsItemId == itemid && x.UnitDetailsUnitId == unitDetailsid).UnitDetailsConversionType;
            }
            catch
            {
                return 1;
            }
        }
        public PurchaseVoucher saveGRN(PurchaseVoucher grn)
        {
            try
            {
                grn.PurchaseVoucherDelStatus = false;

                string contraVoucherNumber = this.GenerateVoucherNo(grn.PurchaseVoucherGRDate).VouchersNumbersVNo;
                grn.PurchaseVoucherVoucherNo = contraVoucherNumber;

                foreach (var k in grn.AccountsTransactions)
                {
                    k.AccountsTransactionsVoucherNo = grn.PurchaseVoucherVoucherNo;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseVoucher_TYPE;
                    k.AccountsTransactionsUserId = 1;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                }
                _context.AccountsTransactions.AddRange(grn.AccountsTransactions);
                _context.SaveChanges();

                _purchaseVoucherRepository.Insert(grn);


                List<StockRegister> stockRegisters = new List<StockRegister>();

                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(grn.PurchaseVoucherCurrencyID, _currencyMasterRepository);
                var rate = (decimal)currencyRate;

                foreach (var item in grn.PurchaseVoucherDetails)
                {
                    item.PurchaseVoucherDetailsVoucherNo = grn.PurchaseVoucherVoucherNo;

                    var converontype = this.getConverionTypebyUnitId(item.PurchaseVoucherDetailsMaterialId, item.PurchaseVoucherDetailsUnitId);
                    stockRegisters.Add(new StockRegister
                    {
                        StockRegisterMaterialID = item.PurchaseVoucherDetailsMaterialId,
                        StockRegisterRefVoucherNo = grn.PurchaseVoucherVoucherNo,
                        StockRegisterPurchaseID = grn.PurchaseVoucherVoucherNo,
                        StockRegisterVoucherDate = grn.PurchaseVoucherPurchaseDate,
                        StockRegisterQuantity = item.PurchaseVoucherDetailsQuantity * (converontype) ?? 0,
                        StockRegisterSIN = item.PurchaseVoucherDetailsQuantity * (converontype) ?? 0,
                        StockRegisterRate = item.PurchaseVoucherDetailsRate,
                        StockRegisterAmount = item.PurchaseVoucherDetailsQuantity * item.PurchaseVoucherDetailsRate,
                        StockRegisterFCAmount = item.PurchaseVoucherDetailsQuantity * item.PurchaseVoucherDetailsRate * rate,
                        StockRegisterTransType = VoucherType.PurchaseVoucher_TYPE,
                        StockRegisterStatus = AccountStatus.Approved,
                    });
                }
                _stockRegisterRepository.InsertList(stockRegisters);


                _purchaseVoucherDetailsRepository.InsertList(grn.PurchaseVoucherDetails.ToList());

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Insert";
                trackingData.UserTrackingUserVpNo = grn.PurchaseVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "GRN";
                _context.UserTracking.Add(trackingData);
                _context.SaveChanges();

                // PurOrderRegister

                //PurOrderRegister poReg = new PurOrderRegister();
                //poReg.PurOrderRegisterOrderNo = purOrderDet.CustomerPurchaseOrderDetailsId.ToString(),
                //poReg.PurOrderRegisterRefVoucherNo = purOrderDet.CustomerPurchaseOrderDetailsVoucherNo,
                //poReg.PurOrderRegisterMaterialId = (int)purOrderDet.CustomerPurchaseOrderDetailsItemId,
                //poReg.PurOrderRegisterTransType = VoucherType.CustomerPurchaseOrder_TYPE,
                //poReg.PurOrderRegisterQtyOrder = purOrderDet.CustomerPurchaseOrderDetailsQty,
                //poReg.PurOrderRegisterQtyIssued = 0,
                //poReg.PurOrderRegisterRate = purOrderDet.CustomerPurchaseOrderDetailsAmount,
                //poReg.PurOrderRegisterAmount = purOrderDet.CustomerPurchaseOrderDetailsAmount,
                //poReg.PurOrderRegisterFcAmount = purOrderDet.CustomerPurchaseOrderDetailsFcAmount,
                //poReg.PurOrderRegisterAssignedDate = DateTime.Now,
                //poReg.PurOrderRegisterFsno = purOrderDet.CustomerPurchaseOrderDetailsFsno,//to be alloted,
                //poReg.PurOrderRegisterSupplierId = (int)0,
                //poReg.PurOrderRegisterStatus = "A"
                // _context.PurOrderRegister.Add(poReg);
                // _context.SaveChanges();

                return grn;
            }
            catch (Exception ex)
            {
                var recd = _voucherNumbersRepository.GetAsQueryable().Where(a => a.VouchersNumbersVNo == grn.PurchaseVoucherVoucherNo).FirstOrDefault();
                if (recd != null)
                {
                    recd.VouhersNumbersDelStatus = true;
                    _voucherNumbersRepository.Update(recd);
                }
                throw ex;
            }
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseVoucher_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseVoucher_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseVoucher_TYPE,
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

        //public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        //{
        //    try
        //    {
        //        int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
        //                                                .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType ==VoucherType.GRN_TYPE)
        //                                                .DefaultIfEmpty()
        //                                                .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;
        //        var voucherNo = "GRN" + vnoMaxVal;
        //        var voucherNumber = _context.VouchersNumbers.AsNoTracking().Where(o => o.VouchersNumbersVType == "PurchaseVoucher").ToList().OrderByDescending(o => o.VouchersNumbersVNoNu).FirstOrDefault();
        //        if (voucherNumber != null)
        //        {
        //            vnoMaxVal = (int)voucherNumber.VouchersNumbersVNoNu + 1;
        //            Console.WriteLine(vnoMaxVal);
        //            VouchersNumbers vouchersNumbers = new VouchersNumbers
        //            {
        //                VouchersNumbersVNo = prefix + vnoMaxVal,
        //                VouchersNumbersVNoNu = vnoMaxVal,
        //                VouchersNumbersVType = VoucherType.g,
        //                VouchersNumbersFsno = 1,
        //                VouchersNumbersStatus = AccountStatus.Pending,
        //                VouchersNumbersVoucherDate = DateTime.Now
        //            };
        //            _voucherNumbersRepository.Insert(vouchersNumbers);
        //            return vouchersNumbers;
        //        }
        //        else
        //        {
        //            VouchersNumbers vouchersNumbers = new VouchersNumbers
        //            {
        //                VouchersNumbersVNo = prefix + vnoMaxVal,
        //                VouchersNumbersVNoNu = vnoMaxVal,
        //                VouchersNumbersVType = "PurchaseVoucher",
        //                VouchersNumbersFsno = 1,
        //                VouchersNumbersStatus = AccountStatus.Pending,
        //                VouchersNumbersVoucherDate = DateTime.Now

        //            };
        //            _voucherNumbersRepository.Insert(vouchersNumbers);
        //            return vouchersNumbers;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public IQueryable DeleteGRN(PurchaseVoucher grn)
        {
            try
            {
                VouchersNumbers vs = _voucherNumbersRepository.GetAll().FirstOrDefault(k => k.VouchersNumbersVNo == grn.PurchaseVoucherVoucherNo);
                if (vs != null)
                {
                    vs.VouhersNumbersDelStatus = true;
                    _voucherNumbersRepository.Update(vs);
                }

                PurchaseVoucher pv = _purchaseVoucherRepository.GetAll().FirstOrDefault(k => k.PurchaseVoucherVoucherNo == grn.PurchaseVoucherVoucherNo);
                if (pv != null)
                {
                    pv.PurchaseVoucherDelStatus = true;
                    _purchaseVoucherRepository.Update(pv);
                }

                List<PurchaseVoucherDetails> pvd = _purchaseVoucherDetailsRepository.GetAll().Where(k => k.PurchaseVoucherDetailsVoucherNo == grn.PurchaseVoucherVoucherNo).ToList();
                foreach (var item in pvd)
                {
                    item.PurchaseVoucherDetailsDelStatus = true;
                }
                _purchaseVoucherDetailsRepository.UpdateList(pvd);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(grn.PurchaseVoucherVoucherNo, grn.PurchaseVoucherPurchaseType, _accountTransactionRepository);
                clsCommonFunctions.Delete_OldEntryOf_StockRegister(grn.PurchaseVoucherVoucherNo, VoucherType.PurchaseVoucher_TYPE, _stockRegisterRepository);
                //List<StockRegister> sr = _stockRegisterRepository.GetAll().Where(k => k.StockRegisterRefVoucherNo == grn.PurchaseVoucherVoucherNo).ToList();
                //foreach (var item in sr)
                //{
                //    item.StockRegisterDelStatus = true;
                //}
                //_stockRegisterRepository.UpdateList(sr);
                //List<AccountsTransactions> at = _accountTransactionRepository.GetAll().Where(k => k.AccountsTransactionsVoucherNo == grn.PurchaseVoucherVoucherNo).ToList();
                //foreach (var item in at)
                //{
                //    item.AccountstransactionsDelStatus = true;
                //}
                //_accountTransactionRepository.UpdateList(at);

                UserTracking trackingData = new UserTracking();
                trackingData.UserTrackingUserUserId = 1;
                trackingData.UserTrackingUserVpAction = "Delete";
                // trackingData.UserTrackingUserVpNo = grn.PurchaseVoucherVoucherNo;
                trackingData.UserTrackingUserChangeDt = DateTime.Now;
                trackingData.UserTrackingUserChangeTime = DateTime.Now;
                trackingData.UserTrackingUserVpType = "GRN";
                _context.UserTracking.Add(trackingData);
                _context.SaveChanges();
                return this.GetAllGRN();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<GRNResponse> loadPo(GRNRequest obj)
        {
            try
            {
                var x = _grnRepository.GetBySP($"EXEC LoadPoList").Where(x => x.Suppliers_INS_Name.Equals(obj.PurchaseID)).ToList();
                return x;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }
}
