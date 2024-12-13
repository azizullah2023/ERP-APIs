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
using Inspire.Erp.Domain.Modals;
using System.Threading.Tasks;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Application.MODULE;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private InspireErpDBContext _context;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PurchaseOrder> _purchaseOrderRepository;
        private IRepository<PurchaseReturn> _purchaseReturnRepository;
        private IRepository<PurchaseReturnDetails> _purchaseReturnDetailsRepository;
        private IRepository<PurchaseOrderDetails> _purchaseOrderDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger;
        private readonly IRepository<TrackingRegister> _trackingRegisterRepository;
        private readonly IRepository<PurOrderRegister> _purOrderRegisterRepository;
        private readonly IRepository<PermissionApproval> _permissionApprovalRepo;
        private readonly IRepository<Approval> _approvalRepo;
        private readonly IRepository<ApprovalDetail> _approvalDetailRepo;
        private readonly IRepository<PurchaseVoucherDetails> _purchaseVoucherDetailsRepo;
        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<SuppliersMaster> _suppliersMasterRepository;
        private IRepository<LocationMaster> _locationMasterRepository;
        private IRepository<UserFile> _userFileRepository;
        private IRepository<UnitDetails> _UnitDetailsRepository;
        private IRepository<PermissionApprovalDetail> _permissionApprovalDetailRepo;


        // private readonly InspireErpDBContext _dbContext;
        //private IRepository<ReportPurchaseOrder> _reportPurchaseOrderRepository;
        public PurchaseOrderService(
            IRepository<UserFile> userFileRepository, IRepository<PermissionApproval> permissionApprovalRepo,
            IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo, InspireErpDBContext context,
            IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> locationMasterRepository,
            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
             IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger,
            IRepository<PurchaseOrder> purchaseOrderRepository, IRepository<PurchaseOrderDetails> purchaseOrderDetailsRepository,
            IRepository<PurchaseReturn> purchaseReturnRepository, IRepository<PurchaseReturnDetails> purchaseReturnDetailsRepository,
            IRepository<TrackingRegister> trackingRegisterRepository, IRepository<PurOrderRegister> purOrderRegisterRepository,
            IRepository<UnitDetails> unitDetailsRepository,
            IRepository<PurchaseVoucherDetails> purchaseVoucherDetailsRepo,
            IRepository<PermissionApprovalDetail> permissionApprovalDetailRepo

            )
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository;
            this._purchaseOrderRepository = purchaseOrderRepository;
            this._purchaseOrderDetailsRepository = purchaseOrderDetailsRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumbersRepository = voucherNumbers;
            _logger = logger; _context = context;
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
            _UnitDetailsRepository = unitDetailsRepository;
            _purchaseVoucherDetailsRepo = purchaseVoucherDetailsRepo;
            _permissionApprovalDetailRepo = permissionApprovalDetailRepo;

        }
        public IEnumerable<UserFile> GetAllUserFile()
        {
            return _userFileRepository.GetAll();
        }
        public IEnumerable<LocationMaster> PurchaseOrder_GetAllLocationMaster()
        {
            return _locationMasterRepository.GetAll();
        }
        public IEnumerable<SuppliersMaster> PurchaseOrder_GetAllSuppliersMaster()
        {
            return _suppliersMasterRepository.GetAll();
        }
        public IEnumerable<UnitMaster> PurchaseOrder_GetAllUnitMaster()
        {
            return _unitMasterRepository.GetAll();
        }
        public IEnumerable<ItemMaster> PurchaseOrder_GetAllItemMaster()
        {
            return _itemMasterRepository.GetAll();
        }

        public PurchaseOrderModel UpdatePurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<PurchaseOrderDetails> purchaseOrderDetails
            )
        {

            try
            {
                _purchaseOrderRepository.BeginTransaction();

                var deleteTrackingRegister = _trackingRegisterRepository.GetAsQueryable().Where(x => x.TrackingRegisterVoucherType == purchaseOrder.PurchaseOrderType && x.TrackingRegisterVoucherNo == purchaseOrder.PurchaseOrderNo).FirstOrDefault();
                if (deleteTrackingRegister != null)
                {
                    _trackingRegisterRepository.Delete(deleteTrackingRegister);
                    _trackingRegisterRepository.SaveChangesAsync();
                }

                var deletePurOrderRegister = _purOrderRegisterRepository.GetAsQueryable().Where(x => x.PurOrderRegisterTransType == purchaseOrder.PurchaseOrderType && x.PurOrderRegisterRefVoucherNo == purchaseOrder.PurchaseOrderNo).FirstOrDefault();
                if (deletePurOrderRegister != null)
                {
                    _purOrderRegisterRepository.Delete(deletePurOrderRegister);
                    _purOrderRegisterRepository.SaveChangesAsync();
                }

                purchaseOrder.PurchaseOrderDetails = purchaseOrder.PurchaseOrderDetails.Select((k) =>
                {
                    k.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
                    k.PurchaseOrderDetailsNo = purchaseOrder.PurchaseOrderNo;
                    return k;
                }).ToList();
                _purchaseOrderRepository.Update(purchaseOrder);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = purchaseOrder.PurchaseOrderDate;
                        k.AccountsTransactionsVoucherNo = purchaseOrder.PurchaseOrderNo;
                        k.AccountsTransactionsVoucherType = VoucherType.PurchaseOrder_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                //insert tracking register
                var listTrackRegs = new List<TrackingRegister>();
                var listTrackPurchOrderReg = new List<PurOrderRegister>();
                foreach (var purOrderDet in purchaseOrder.PurchaseOrderDetails)
                {
                    var tr = new TrackingRegister
                    {
                        TrackingRegisterDetailsId = (int)purOrderDet.PurchaseOrderDetailsId,
                        TrackingRegisterVoucherNo = purOrderDet.PurchaseOrderDetailsNo,
                        TrackingRegisterMaterialId = (int)purOrderDet.PurchaseOrderDetailsMatId,
                        TrackingRegisterVoucherType = purchaseOrder.PurchaseOrderType,
                        TrackingRegisterQty = (int)purOrderDet.PurchaseOrderDetailsQuantity,
                        TrackingRegisterQtyin = (int)purOrderDet.PurchaseOrderDetailsQuantity,
                        TrackingRegisterQtyout = 0,
                        TrackingRegisterRate = purOrderDet.PurchaseOrderDetailsRate,
                        TrackingRegisterTrackDate = purchaseOrder.PurchaseOrderDate,
                        TrackingRegisterFsno = null,

                    };
                    listTrackRegs.Add(tr);

                    var poReg = new PurOrderRegister
                    {
                        PurOrderRegisterOrderNo = purOrderDet.PurchaseOrderDetailsId.ToString(),
                        PurOrderRegisterRefVoucherNo = purOrderDet.PurchaseOrderDetailsNo,
                        PurOrderRegisterMaterialId = (int)purOrderDet.PurchaseOrderDetailsMatId,
                        PurOrderRegisterTransType = purchaseOrder.PurchaseOrderType,
                        PurOrderRegisterQtyOrder = (int)purOrderDet.PurchaseOrderDetailsQuantity,
                        PurOrderRegisterQtyIssued = 0,
                        PurOrderRegisterRate = purOrderDet.PurchaseOrderDetailsRate,
                        PurOrderRegisterAssignedDate = DateTime.Now,
                        PurOrderRegisterFsno = null,
                        PurOrderRegisterSupplierId = (int)purchaseOrder.PurchaseOrderPartyId,
                        PurOrderRegisterStatus = "A"

                    };
                    listTrackPurchOrderReg.Add(poReg);

                }
                _trackingRegisterRepository.InsertList(listTrackRegs);
                _purOrderRegisterRepository.InsertList(listTrackPurchOrderReg);

                //PermissionApprovalDetail by zaid               

                //  var permsionApproval = _context.Permissionapproval.AsNoTracking().Select(x => x.LevelId).Distinct().ToList();
                var rights = _context.tbl_approvalforms.Where(a => a.Voucher_Type.Contains("Purchase Order") && a.is_active == true).AsNoTracking().ToList();
                if (rights.Count > 0)
                {
                    var existdata = _permissionApprovalDetailRepo.GetAsQueryable().Where(a => a.VoucherId == purchaseOrder.PurchaseOrderNo && a.VoucherType == "Purchase Order").ToList();

                    if (existdata.Count > 0)
                    {
                        _permissionApprovalDetailRepo.DeleteList(existdata);
                    }
                    foreach (var item in rights)
                    {
                        for (int level = 1; level <= item.NoOfLevel; level++)
                        {
                            PermissionApprovalDetail POList = new PermissionApprovalDetail();
                            POList.VoucherType = "Purchase Order";
                            POList.VoucherId = purchaseOrder.PurchaseOrderNo;
                            POList.VoucherDate = purchaseOrder.PurchaseOrderDate;
                            POList.Amount = (double)purchaseOrder.PurchaseOrderNetAmount;
                            POList.CreatedBy = (int?)purchaseOrder.PurchaseOrderUserId ?? 1;
                            POList.LevelId = level;
                            POList.Status = "Pending";
                            POList.Remarks = purchaseOrder.PurchaseOrderDescription;
                            _permissionApprovalDetailRepo.Insert(POList);
                        }
                    }

                }
                _purchaseOrderRepository.TransactionCommit();
            }
            catch (Exception ex)
            {
                _purchaseOrderRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedPurchaseOrderDetails(purchaseOrder.PurchaseOrderNo);
        }
        private decimal getConverionTypebyUnitId(int? unitDetailsid)
        {
            try
            {
                return (decimal)this._UnitDetailsRepository.GetAsQueryable().FirstOrDefault(x => x.UnitDetailsUnitId == unitDetailsid).UnitDetailsConversionType;
            }
            catch
            {
                return 1;
            }
        }
        public int DeletePurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<PurchaseOrderDetails> purchaseOrderDetails
            )
        {
            int i = 0;
            try
            {
                _purchaseOrderRepository.BeginTransaction();
                purchaseOrder.PurchaseOrderDelStatus = true;

                purchaseOrder.PurchaseOrderDetails = purchaseOrder.PurchaseOrderDetails.Select((k) =>
                {
                    k.PurchaseOrderDetailsDelStatus = true;
                    return k;
                }).ToList();

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                _purchaseOrderRepository.Update(purchaseOrder);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == purchaseOrder.PurchaseOrderNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _purchaseOrderRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _purchaseOrderRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;
        }
        public PurchaseOrderModel InsertPurchaseOrder(PurchaseOrder purchaseOrder, List<AccountsTransactions> accountsTransactions,
            List<PurchaseOrderDetails> purchaseOrderDetails
            )
        {
            try
            {
                _purchaseOrderRepository.BeginTransaction();

                string purchaseOrderNumber = this.GenerateVoucherNo(purchaseOrder.PurchaseOrderDate.Date).VouchersNumbersVNo;
                purchaseOrder.PurchaseOrderNo = purchaseOrderNumber;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _purchaseOrderRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.PurchaseOrderId) + 1);

                purchaseOrder.PurchaseOrderId = maxcount;

                purchaseOrder.PurchaseOrderDetails = purchaseOrder.PurchaseOrderDetails.Select((x) =>
                {
                    x.PurchaseOrderId = maxcount;
                    x.PurchaseOrderDetailsNo = purchaseOrderNumber;
                    return x;
                }).ToList();

                purchaseOrder.PurchaseOrderDetails.Clear();
                foreach (var item in purchaseOrderDetails)
                {
                    item.PurchaseOrderId = maxcount;
                    item.PurchaseOrderDetailsNo = purchaseOrderNumber;
                    purchaseOrder.PurchaseOrderDetails.Add(item);
                }


                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = purchaseOrder.PurchaseOrderNo;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseOrder_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);
                _purchaseOrderRepository.Insert(purchaseOrder);

                //insert tracking register
                var listTrackRegs = new List<TrackingRegister>();
                var listTrackPurchOrderReg = new List<PurOrderRegister>();
                foreach (var purOrderDet in purchaseOrder.PurchaseOrderDetails)
                {

                    // var converontype = this.getConverionTypebyUnitId(Convert.ToInt32(purOrderDet.PurchaseOrderDetailsUnitId));


                    var tr = new TrackingRegister
                    {
                        TrackingRegisterDetailsId = (int)purOrderDet.PurchaseOrderDetailsId,
                        TrackingRegisterVoucherNo = purOrderDet.PurchaseOrderDetailsNo,
                        TrackingRegisterMaterialId = (int)purOrderDet.PurchaseOrderDetailsMatId,
                        TrackingRegisterVoucherType = purchaseOrder.PurchaseOrderType,
                        TrackingRegisterQty = (int)purOrderDet.PurchaseOrderDetailsQuantity,
                        TrackingRegisterQtyin = (int)purOrderDet.PurchaseOrderDetailsQuantity,
                        TrackingRegisterQtyout = 0,
                        TrackingRegisterRate = purOrderDet.PurchaseOrderDetailsRate,
                        TrackingRegisterTrackDate = purchaseOrder.PurchaseOrderDate,
                        TrackingRegisterFsno = null,//to be alloted

                    };
                    listTrackRegs.Add(tr);

                    var poReg = new PurOrderRegister
                    {
                        PurOrderRegisterOrderNo = purOrderDet.PurchaseOrderDetailsId.ToString(),
                        PurOrderRegisterRefVoucherNo = purOrderDet.PurchaseOrderDetailsNo,
                        PurOrderRegisterMaterialId = (int)purOrderDet.PurchaseOrderDetailsMatId,
                        PurOrderRegisterTransType = purchaseOrder.PurchaseOrderType,
                        PurOrderRegisterQtyOrder = purOrderDet.PurchaseOrderDetailsQuantity,
                        PurOrderRegisterQtyIssued = 0,
                        PurOrderRegisterRate = purOrderDet.PurchaseOrderDetailsRate,
                        PurOrderRegisterAssignedDate = DateTime.Now,
                        PurOrderRegisterFsno = null,//to be alloted,
                        PurOrderRegisterSupplierId = (int)purchaseOrder.PurchaseOrderPartyId,
                        PurOrderRegisterStatus = "A"

                    };
                    listTrackPurchOrderReg.Add(poReg);

                }

                _trackingRegisterRepository.InsertList(listTrackRegs);
                _purOrderRegisterRepository.InsertList(listTrackPurchOrderReg);
                //var permsionApproval = _permissionApprovalRepo.GetAsQueryable().Where(x => x.VoucherType == "Purchase Order").OrderBy(x => x.LevelOrder).ToList();
                //var approvals = new Approval()
                //{
                //    CreatedBy = 0,
                //    LocationId = null,
                //    Status = false,
                //    VoucherType = "Purchase Order"
                //};
                //_approvalRepo.Insert(approvals);
                //_approvalDetailRepo.InsertList(permsionApproval.Select(x => new ApprovalDetail()
                //{
                //    ApprovalId = approvals.Id,
                //    Status = false,
                //    UserId = x.UserId,
                //    LevelOrder = x.LevelOrder,
                //    ApprovedAt = DateTime.Now,
                //}).ToList());

                // PermissionApprovalDetail by zaid

                //var permsionApproval = _context.Permissionapproval.AsNoTracking().Select(x => x.LevelId).Distinct().ToList();
                var rights = _context.tbl_approvalforms.Where(a => a.Voucher_Type.Contains("Purchase Order") && a.is_active == true).AsNoTracking().ToList();
                if (rights.Count > 0)
                {
                    foreach (var item in rights)
                    {
                        for (int level = 1; level <= item.NoOfLevel; level++)
                        {
                            PermissionApprovalDetail POList = new PermissionApprovalDetail();
                            POList.VoucherType = "Purchase Order";
                            POList.VoucherId = purchaseOrder.PurchaseOrderNo;
                            POList.VoucherDate = purchaseOrder.PurchaseOrderDate;
                            POList.Amount = (double)purchaseOrder.PurchaseOrderNetAmount;
                            POList.CreatedBy = (int?)purchaseOrder.PurchaseOrderUserId ?? 1;
                            POList.LevelId = level;
                            POList.Status = "Pending";
                            POList.Remarks = purchaseOrder.PurchaseOrderDescription;
                            _permissionApprovalDetailRepo.Insert(POList);
                        }
                    }
                }
                _purchaseOrderRepository.TransactionCommit();
                return this.GetSavedPurchaseOrderDetails(purchaseOrder.PurchaseOrderNo);

            }
            catch (Exception ex)
            {
                _purchaseOrderRepository.TransactionRollback();
                throw ex;
            }

        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<PurchaseOrder> GetPurchaseOrder()
        {
            IEnumerable<PurchaseOrder> purchaseOrder_ALL = _purchaseOrderRepository.GetAll().Where(k => k.PurchaseOrderDelStatus == false || k.PurchaseOrderDelStatus == null);
            return purchaseOrder_ALL;
        }
        public PurchaseOrderModel GetSavedPurchaseOrderDetails(string pvno)
        {
            PurchaseOrderModel purchaseOrderModel = new PurchaseOrderModel();
            purchaseOrderModel.purchaseOrder = _purchaseOrderRepository.GetAsQueryable().Where(k => k.PurchaseOrderNo == pvno).SingleOrDefault();
            try
            {
                purchaseOrderModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.PurchaseOrder_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            }
            catch
            {
            }
            purchaseOrderModel.purchaseOrderDetails = _purchaseOrderDetailsRepository.GetAsQueryable().Where(x => x.PurchaseOrderDetailsNo == pvno && (x.PurchaseOrderDetailsDelStatus == false || x.PurchaseOrderDetailsDelStatus == null)).ToList();

            foreach (var purchaseOrderDetail in purchaseOrderModel.purchaseOrderDetails)
            {
                if (_stockRegisterRepository.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == purchaseOrderDetail.PurchaseOrderDetailsNo).FirstOrDefault() != null)
                {
                    purchaseOrderDetail.PurchaseOrderNetStock = _stockRegisterRepository.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == purchaseOrderDetail.PurchaseOrderDetailsNo).FirstOrDefault().StockRegisterSIN;

                }
                purchaseOrderDetail.PurchaseOrderNetStock = 0;
                purchaseOrderDetail.PurchaseOrderTillNowQTY = GetTillNowQTY(pvno);
            }

            return purchaseOrderModel;

        }
        private decimal? GetTillNowQTY(string pvno)
        {
            try
            {
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
                    }
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

                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseOrder_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseOrder_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseOrder_TYPE,
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
                VouchersNumbers vouchersNumbers = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
        public IQueryable GetPODetailsForGRN()
        {
            try
            {
                var materialList = _itemMasterRepository.GetAsQueryable().Where(a => a.ItemMasterDelStatus != true).ToList();
                var datalist = _purchaseOrderDetailsRepository.GetAsQueryable().Where(a => a.PurchaseOrderDetailsDelStatus != true).ToList();
                var data = datalist.Select(d => new
                {
                    purchaseOrderDetailsNo = d.PurchaseOrderDetailsNo,
                    itemId = d.PurchaseOrderDetailsMatId,
                    itemObj = materialList.FirstOrDefault(m => m.ItemMasterItemId == d.PurchaseOrderDetailsMatId),
                    unitId = d.PurchaseOrderDetailsUnitId,
                    poQty = d.PurchaseOrderDetailsQuantity,
                }).AsQueryable();
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public List<PurchaseOrderDTO> GetAllPOForGRN(decimal id)
        {
            try
            {
                var materialList = _itemMasterRepository.GetAsQueryable().Where(a => a.ItemMasterDelStatus != true).Select(c => new
                {
                    c.ItemMasterItemName,
                    c.ItemMasterItemId,
                    c.ItemMasterLastPurchasePrice
                });
                var unitList = _unitMasterRepository.GetAsQueryable().Where(a => a.UnitMasterUnitDelStatus != true).Select(c => new
                {
                    c.UnitMasterUnitId,
                    c.UnitMasterUnitShortName,
                    c.UnitMasterUnitFullName
                }).ToList();
                var detailList = _purchaseOrderDetailsRepository.GetAsQueryable().ToList();
                var details = _purchaseOrderRepository.GetAsQueryable().Where(a => a.PurchaseOrderDelStatus != true && a.PurchaseOrderPartyId == id).ToList();
                var data = details.Select(o => new PurchaseOrderDTO
                {
                    PurchaseOrder = o,
                    PurchaseOrderDetails = detailList.Where(a => a.PurchaseOrderDetailsNo == o.PurchaseOrderNo).Select(d => new PurchaseOrderDetailsDTO
                    {
                        itemId = d.PurchaseOrderDetailsMatId,
                        itemname = materialList.Where(m => m.ItemMasterItemId == (long?)d.PurchaseOrderDetailsMatId).Select(c => c.ItemMasterItemName ?? "").FirstOrDefault() ?? "",
                        unitId = d.PurchaseOrderDetailsUnitId ?? 0,
                        unit = unitList.Where(u => u.UnitMasterUnitId == (int?)d.PurchaseOrderDetailsUnitId).Select(c => c.UnitMasterUnitFullName ?? "").FirstOrDefault() ?? "",
                        price = materialList.Where(m => m.ItemMasterItemId == (long?)d.PurchaseOrderDetailsMatId).Select(c => (decimal?)c.ItemMasterLastPurchasePrice).FirstOrDefault() ?? 0,
                        poQty = d.PurchaseOrderDetailsQuantity ?? 0,
                    }).ToList()
                }).ToList();
                return data;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        //saleem
        public async Task<Response<List<PODetailsViewModel>>> GetGRMPOs(int? supplierId)
        {
            try
            {
                var result = await (from po in _purchaseOrderRepository.GetAsQueryable()
                                    join pod in _purchaseOrderDetailsRepository.GetAsQueryable() on po.PurchaseOrderId equals pod.PurchaseOrderId
                                    join sm in _suppliersMasterRepository.GetAsQueryable() on po.PurchaseOrderPartyId equals sm.SuppliersMasterSupplierId
                                    join im in _itemMasterRepository.GetAsQueryable() on (long)pod.PurchaseOrderDetailsMatId equals im.ItemMasterItemId
                                    //where sm.SuppliersMasterSupplierId == supplierId
                                    where (supplierId == null || supplierId == 0 || sm.SuppliersMasterSupplierId == supplierId)
                                   && po.PurchaseOrderStatus.Trim().ToUpper() == "OPEN"
                                    select new PODetailsViewModel
                                    {
                                        ItemId = (int)pod.PurchaseOrderDetailsMatId,
                                        ItemName = im.ItemMasterItemName,
                                        POId = (int)po.PurchaseOrderId,
                                        PODId = (int)pod.PurchaseOrderDetailsId,
                                        PONo = po.PurchaseOrderNo,
                                        PODate = po.PurchaseOrderDate,
                                        SupplierName = sm.SuppliersMasterSupplierName,
                                        Rate = pod.PurchaseOrderDetailsRate,
                                        Quantity = (int)pod.PurchaseOrderDetailsQuantity,
                                        UnitId = pod.PurchaseOrderDetailsUnitId,
                                        UnitName = _unitMasterRepository.GetAsQueryable().Where(x => x.UnitMasterUnitId == pod.PurchaseOrderDetailsUnitId).FirstOrDefault().UnitMasterUnitFullName.Trim(),
                                        BalanceQuantity = (int)pod.PurchaseOrderDetailsQuantity - (_purchaseVoucherDetailsRepo.GetAsQueryable().Where(pr =>
                                        (pr.PurchaseVoucherDetailsPodId == pod.PurchaseOrderDetailsId
                                           && pr.PurchaseVoucherDetailsMaterialId == pod.PurchaseOrderDetailsMatId))
                                        .Sum(prdq => (int)((prdq.PurchaseVoucherDetailsQuantity) ?? 0))),

                                        DeliveredQuantity = (_purchaseVoucherDetailsRepo.GetAsQueryable().Where(pr =>
                                                (pr.PurchaseVoucherDetailsPodId == pod.PurchaseOrderDetailsId
                                                 && pr.PurchaseVoucherDetailsMaterialId == pod.PurchaseOrderDetailsMatId))
                                        .Sum(prdq => (int)((prdq.PurchaseVoucherDetailsQuantity) ?? 0))),

                                    })
                                    .OrderBy(x => x.PODate).ThenBy(x => x.PONo)
                                    .ToListAsync();
                return Response<List<PODetailsViewModel>>.Success(result, "Data found");
            }
            catch (System.Exception ex)
            {
                return Response<List<PODetailsViewModel>>.Fail(new List<PODetailsViewModel>(), ex.Message);
            }
        }
    }
}
