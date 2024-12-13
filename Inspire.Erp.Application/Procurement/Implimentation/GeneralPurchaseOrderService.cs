using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Application.Account.Implementations;
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
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class GeneralPurchaseOrderService : IGeneralPurchaseOrderService
    {
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<GeneralPurchaseOrder> _GeneralpurchaseOrderRepository;
        private IRepository<PurchaseReturn> _purchaseReturnRepository;
        private IRepository<PurchaseReturnDetails> _purchaseReturnDetailsRepository;
        private IRepository<GeneralPurchaseOrderDetails> _GeneralpurchaseOrderDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<TrackingRegister> _trackingRegisterRepository;
        private readonly IRepository<PurOrderRegister> _purOrderRegisterRepository;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;
        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;
        private IRepository<UserFile> _userFileRepository;

        public GeneralPurchaseOrderService(
            IRepository<UserFile> userFileRepository, IRepository<PermissionApproval> permissionApprovalRepo,
            IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,

            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,

            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,

            IRepository<GeneralPurchaseOrder> GeneralpurchaseOrderRepository, IRepository<GeneralPurchaseOrderDetails> GeneralpurchaseOrderDetailsRepository, IRepository<PurchaseReturn> purchaseReturnRepository, IRepository<PurchaseReturnDetails> purchaseReturnDetailsRepository, IRepository<TrackingRegister> trackingRegisterRepository, IRepository<PurOrderRegister> purOrderRegisterRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._GeneralpurchaseOrderRepository = GeneralpurchaseOrderRepository;
            this._GeneralpurchaseOrderDetailsRepository = GeneralpurchaseOrderDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _logger = logger;
            _trackingRegisterRepository = trackingRegisterRepository;
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _suppliersMasterRepository = suppliersMasterRepository;
            _locationMasterRepository = locationMasterRepository;
            _userFileRepository = userFileRepository;
            _purchaseReturnRepository = purchaseReturnRepository;
            _purchaseReturnDetailsRepository = purchaseReturnDetailsRepository;
            _purOrderRegisterRepository = purOrderRegisterRepository;
            _permissionApprovalRepo = permissionApprovalRepo;
            _approvalRepo = approvalRepo;
            _approvalDetailRepo = approvalDetailRepo;
            //_reportPurchaseOrderRepository = reportPurchaseOrderRepository;
        }

        public IEnumerable<UserFile> GetAllUserFile()
        {
            return _userFileRepository.GetAll();
        }
        public GeneralPurchaseOrderModel UpdateGeneralPurchaseOrder(GeneralPurchaseOrder GeneralpurchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<GeneralPurchaseOrderDetails> GeneralpurchaseOrderDetails
            )
        {

            try
            {
                _GeneralpurchaseOrderRepository.BeginTransaction();

                decimal maxcount = 0;
                maxcount =
                    _GeneralpurchaseOrderDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.GeneralPurchaseOrderDetailsPoid) + 1;

                GeneralpurchaseOrder.GeneralPurchaseOrderDetails.Clear();
                _GeneralpurchaseOrderRepository.Update(GeneralpurchaseOrder);

                foreach (var item in GeneralpurchaseOrderDetails)
                {
                    item.GeneralPurchaseOrderId = GeneralpurchaseOrder.GeneralPurchaseOrderId;
                    item.GeneralPurchaseOrderPono = GeneralpurchaseOrder.GeneralPurchaseOrderPono;
                    if (item.GeneralPurchaseOrderDetailsPoid != 0)
                    {
                        _GeneralpurchaseOrderDetailsRepository.Update(item);
                    }
                    else
                    {
                        item.GeneralPurchaseOrderDetailsPoid = maxcount;
                        _GeneralpurchaseOrderDetailsRepository.Insert(item);
                        maxcount++;

                    }
                }               
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = GeneralpurchaseOrder.GeneralPurchaseOrderPoDate;
                        k.AccountsTransactionsVoucherNo = GeneralpurchaseOrder.GeneralPurchaseOrderPono;
                        k.AccountsTransactionsVoucherType = VoucherType.GeneralPurchaseOrder_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);


                _GeneralpurchaseOrderRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _GeneralpurchaseOrderRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedGeneralPurchaseOrderDetails(GeneralpurchaseOrder.GeneralPurchaseOrderPono);
        }
        public int DeleteGeneralPurchaseOrder(GeneralPurchaseOrder GeneralpurchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<GeneralPurchaseOrderDetails> GeneralpurchaseOrderDetails
            )
        {
            int i = 0;
            try
            {
                _GeneralpurchaseOrderRepository.BeginTransaction();


                GeneralpurchaseOrder.GeneralPurchaseOrderDelStatus = true;

                GeneralpurchaseOrderDetails = GeneralpurchaseOrder.GeneralPurchaseOrderDetails.Select((k) =>
                {
                    k.GeneralPurchaseOrderDetailsDelStatus = true;
                    return k;
                }).ToList();

                _GeneralpurchaseOrderDetailsRepository.UpdateList(GeneralpurchaseOrderDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                GeneralpurchaseOrder.GeneralPurchaseOrderDetails = GeneralpurchaseOrderDetails;
                _GeneralpurchaseOrderRepository.Update(GeneralpurchaseOrder);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == GeneralpurchaseOrder.GeneralPurchaseOrderPono).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _GeneralpurchaseOrderRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _GeneralpurchaseOrderRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;
        }
        public GeneralPurchaseOrderModel InsertGeneralPurchaseOrder(GeneralPurchaseOrder GeneralpurchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<GeneralPurchaseOrderDetails> GeneralpurchaseOrderDetails
            )
        {
            try
            {
                _GeneralpurchaseOrderRepository.BeginTransaction();
                string purchaseOrderNumber = this.GenerateVoucherNo(GeneralpurchaseOrder.GeneralPurchaseOrderPoDate.Date).VouchersNumbersVNo;
                GeneralpurchaseOrder.GeneralPurchaseOrderPono = purchaseOrderNumber;


                decimal maxcount = 0;
                maxcount =
                    _GeneralpurchaseOrderRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.GeneralPurchaseOrderId) + 1;

                GeneralpurchaseOrder.GeneralPurchaseOrderId = maxcount;

                GeneralpurchaseOrder.GeneralPurchaseOrderDetails = GeneralpurchaseOrder.GeneralPurchaseOrderDetails.Select((x) =>
                {
                    x.GeneralPurchaseOrderId = maxcount;
                    x.GeneralPurchaseOrderPono = purchaseOrderNumber;// purchaseOrder.PurchaseOrderNo;
                    return x;
                }).ToList();

                GeneralpurchaseOrder.GeneralPurchaseOrderDetails.Clear();

                decimal max1count = 0;
                max1count =
                    _GeneralpurchaseOrderDetailsRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : (decimal)o.GeneralPurchaseOrderDetailsPoid) + 1;
                foreach (var item in GeneralpurchaseOrderDetails)
                {
                    item.GeneralPurchaseOrderDetailsPoid = max1count;
                    item.GeneralPurchaseOrderId = maxcount;
                    item.GeneralPurchaseOrderPono = purchaseOrderNumber ?? "";
                    GeneralpurchaseOrder.GeneralPurchaseOrderDetails.Add(item);
                    max1count++;
                }


                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = GeneralpurchaseOrder.GeneralPurchaseOrderPono;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseOrder_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                _GeneralpurchaseOrderRepository.Insert(GeneralpurchaseOrder);

                var permsionApproval = _permissionApprovalRepo.GetAsQueryable().Where(x => x.VoucherType == "General Purchase Order").OrderBy(x => x.LevelOrder).ToList();
                var approvals = new Approval()
                {
                    CreatedBy = 0,
                    LocationId = null,
                    Status = false,
                    VoucherType = "General Purchase Order"
                };
                _approvalRepo.Insert(approvals);
                _approvalDetailRepo.InsertList(permsionApproval.Select(x => new ApprovalDetail()
                {
                    ApprovalId = approvals.Id,
                    Status = false,
                    UserId = x.UserId,
                    LevelOrder = x.LevelOrder,
                    ApprovedAt = DateTime.Now,
                }).ToList());
                _GeneralpurchaseOrderRepository.TransactionCommit();
                return this.GetSavedGeneralPurchaseOrderDetails(GeneralpurchaseOrder.GeneralPurchaseOrderPono);

            }
            catch (Exception ex)
            {
                _GeneralpurchaseOrderRepository.TransactionRollback();
                throw ex;
            }

        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<GeneralPurchaseOrder> GetGeneralPurchaseOrder()
        {
            IEnumerable<GeneralPurchaseOrder> GeneralpurchaseOrder_ALL = _GeneralpurchaseOrderRepository.GetAll().Where(k => k.GeneralPurchaseOrderDelStatus == false || k.GeneralPurchaseOrderDelStatus == null);
            return GeneralpurchaseOrder_ALL;
        }
        public GeneralPurchaseOrderModel GetSavedGeneralPurchaseOrderDetails(string pvno)
        {
            GeneralPurchaseOrderModel generalpurchaseOrderModel = new GeneralPurchaseOrderModel();

            generalpurchaseOrderModel.GeneralpurchaseOrder = _GeneralpurchaseOrderRepository.GetAsQueryable().Where(k => k.GeneralPurchaseOrderPono == pvno).SingleOrDefault();

            try
            {
                generalpurchaseOrderModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.GeneralPurchaseOrder_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            }
            catch
            {
            }
            generalpurchaseOrderModel.GeneralpurchaseOrderDetails = _GeneralpurchaseOrderDetailsRepository.GetAsQueryable().Where(x => x.GeneralPurchaseOrderPono == pvno && (x.GeneralPurchaseOrderDetailsDelStatus == false || x.GeneralPurchaseOrderDetailsDelStatus == null)).ToList();

            foreach (var GeneralpurchaseOrderDetail in generalpurchaseOrderModel.GeneralpurchaseOrderDetails)
            {
                if (_stockRegisterRepository.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == GeneralpurchaseOrderDetail.GeneralPurchaseOrderPono).FirstOrDefault() != null)
                {
                    GeneralpurchaseOrderDetail.GeneralPurchaseOrderNetStock = _stockRegisterRepository.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == GeneralpurchaseOrderDetail.GeneralPurchaseOrderPono).FirstOrDefault().StockRegisterSIN;

                }
                GeneralpurchaseOrderDetail.GeneralPurchaseOrderNetStock = 0;
                GeneralpurchaseOrderDetail.GeneralPurchaseOrderTillNowQTY = GetTillNowQTY(pvno);
            }

            return generalpurchaseOrderModel;

        }
        private decimal? GetTillNowQTY(string pvno)
        {
            try
            {
                //var pr = _purchaseReturnRepository.GetAll().Where(c => c.PurchaseReturnGrno == pvno).LastOrDefault();

                //if (pr != null)
                //{
                //    decimal? qty = 0.0M;

                //        var prDetails = _purchaseReturnDetailsRepository.GetAll().Where(c => c.PurchaseReturnDetailsNo == pr.PurchaseReturnNo);
                //        if (prDetails != null)
                //        {
                //        foreach (var item in prDetails)
                //        {
                //            if (item.PurchaseReturnDetailsQuantity !=null)
                //            {
                //                qty += item.PurchaseReturnDetailsQuantity;
                //            }
                //        }
                //            //qty += Convert.ToDecimal(_purchaseReturnDetailsRepository.GetAll().Where(c => c.PurchaseReturnDetailsNo == item.PurchaseReturnNo).FirstOrDefault().PurchaseReturnDetailsQuantity);

                //        }


                //    return qty;
                //}
                //return 0;


                //=====================================//
                var pr = _purchaseReturnRepository.GetAll().Where(c => c.PurchaseReturnGrno == pvno);
                if (pr != null)
                {
                    decimal? qty = 0.0M;
                    foreach (var prdetail in pr)
                    {
                        var rslt = _purchaseReturnDetailsRepository.GetAll().Where(c => c.PurchaseReturnDetailsNo == prdetail.PurchaseReturnNo).FirstOrDefault();
                        if (rslt != null)
                        {
                            qty += rslt.PurchaseReturnDetailsQuantity;
                        }
                        // qty += rslt;
                    }
                    //var prDetails = _purchaseReturnDetailsRepository.GetAll().Where(c => c.PurchaseReturnDetailsNo == pr.PurchaseReturnNo);
                    return qty;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.GeneralPurchaseOrder_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.GeneralPurchaseOrder_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;

                //var prefix = "CN";
                //int vnoMaxVal = 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.GeneralPurchaseOrder_TYPE,
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
        public VouchersNumbers GetVouchersNumbers(string pvno, string vType)
        {
            try
            {
                if (string.IsNullOrEmpty(pvno))
                {

                }
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno && k.VouchersNumbersVType == vType).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
        //public IQueryable GetPODetailsForGRN()
        //{
        //    try
        //    {
        //        var materialList = _itemMasterRepository.GetAll();
        //        //var unitList = _unitMasterRepository.GetAll().ToList();
        //        var data = _GeneralpurchaseOrderDetailsRepository.GetAll().Select(d => new
        //        {
        //            purchaseOrderDetailsNo = d.GeneralPurchaseOrderDetailsSno,
        //            itemId = d.GeneralPurchaseOrderDetailsMaterialId,
        //            itemObj = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.GeneralPurchaseOrderDetailsMaterialId),
        //            //itemname = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.PurchaseOrderDetailsMatId).ItemMasterItemName,
        //            unitId = d.GeneralPurchaseOrderDetailsUnitId,
        //            //unit = unitList.FirstOrDefault(u => u.UnitMasterUnitId == d.PurchaseOrderDetailsUnitId).UnitMasterUnitFullName,
        //            //price = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.PurchaseOrderDetailsMatId).ItemMasterLastPurchasePrice,
        //            poQty = d.GeneralPurchaseOrderDetailsQuantity,
        //        }).AsQueryable();
        //        return data;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public IEnumerable<ItemMaster> GeneralPurchaseOrder_GetAllItemMaster()
        {
            return _itemMasterRepository.GetAll();
        }
        public IEnumerable<UnitMaster> GeneralPurchaseOrder_GetAllUnitMaster()
        {
            return _unitMasterRepository.GetAll();
        }
        public IEnumerable<SuppliersMaster> GeneralPurchaseOrder_GetAllSuppliersMaster()
        {
            return _suppliersMasterRepository.GetAll();
        }
        public IEnumerable<LocationMaster> GeneralPurchaseOrder_GetAllLocationMaster()
        {
            return _locationMasterRepository.GetAll();
        }
        public IQueryable GetGPODetailsForGRN()
        {
            try
            {
                var materialList = _itemMasterRepository.GetAll();
                //var unitList = _unitMasterRepository.GetAll().ToList();
                var data = _GeneralpurchaseOrderDetailsRepository.GetAll().Select(d => new
                {
                    purchaseOrderDetailsNo = d.GeneralPurchaseOrderDetailsSno,
                    itemId = d.GeneralPurchaseOrderDetailsMaterialId,
                    itemObj = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.GeneralPurchaseOrderDetailsMaterialId),
                    //itemname = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.PurchaseOrderDetailsMatId).ItemMasterItemName,
                    unitId = d.GeneralPurchaseOrderDetailsUnitId,
                    //unit = unitList.FirstOrDefault(u => u.UnitMasterUnitId == d.PurchaseOrderDetailsUnitId).UnitMasterUnitFullName,
                    //price = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.PurchaseOrderDetailsMatId).ItemMasterLastPurchasePrice,
                    poQty = d.GeneralPurchaseOrderDetailsQuantity,
                }).AsQueryable();
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
