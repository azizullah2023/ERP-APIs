using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Account.Implementations;
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
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using System.Threading.Tasks;
using Inspire.Erp.Infrastructure.Database;
using SendGrid.Helpers.Mail;
using System.Linq.Dynamic.Core;
using Inspire.Erp.Domain.Entities.POS;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class PurchaseRequisitionService : IPurchaseRequisitionService
    {
        private InspireErpDBContext _context;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<AccountsTransactions> _accountTransactionRepository;
        private IRepository<PurchaseRequisition> _purchaseRequisitionRepository;
        private IRepository<PurchaseRequisitionDetails> _purchaseRequisitionDetailsRepository;
        private IRepository<ProgramSettings> _programsettingsRepository;
        private IRepository<VouchersNumbers> _voucherNumbersRepository;
        private readonly ILogger<PaymentVoucherService> _logger; private readonly IRepository<PermissionApproval> _permissionApprovalRepo; private readonly IRepository<Approval> _approvalRepo; private readonly IRepository<ApprovalDetail> _approvalDetailRepo; public readonly InspireErpDBContext dbContext;
        private IRepository<IPurchaseOrderService> _purchaseOrderService;
        private IRepository<AvIssueVoucherDetails> _AvIssueVoucherDetails;
        private IRepository<AvIssueVoucher> _AvIssueVoucher; private IRepository<PermissionApprovalDetail> _permissionApprovalDetailRepo;
        private IRepository<JobMaster> jobrepository;
        private IRepository<LocationMaster> locationrepository; private IRepository<BillOfQTyDetails> _BillOfQTyDetailsRepository;
        private IRepository<TrackingRegister> _trackingRegisterRepository; private IRepository<ItemMaster> _itemMasterRepository; private IRepository<UnitMaster> _unitMasterRepository; private IRepository<SuppliersMaster> _suppliersMasterRepository; private IRepository<LocationMaster> _locationMasterRepository;

        public PurchaseRequisitionService(IRepository<ItemMaster> itemMasterRepository, IRepository<UnitMaster> unitMasterRepository,
            IRepository<SuppliersMaster> suppliersMasterRepository, IRepository<LocationMaster> _locationrepository, IRepository<LocationMaster> locationMasterRepository,
            IRepository<PermissionApproval> permissionApprovalRepo, IRepository<Approval> approvalRepo, IRepository<ApprovalDetail> approvalDetailRepo,
            IRepository<AccountsTransactions> accountTransactionRepository, IRepository<StockRegister> stockRegisterRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumbers, ILogger<PaymentVoucherService> logger, IRepository<JobMaster> _jobrepository, IRepository<BillOfQTyDetails> BillOfQTyDetailsRepository,
             IRepository<TrackingRegister> trackingRegisterRepository, IRepository<PermissionApprovalDetail> permissionApprovalDetailRepo,
        IRepository<PurchaseRequisition> purchaseRequisitionRepository, InspireErpDBContext context, IRepository<PurchaseRequisitionDetails> purchaseRequisitionDetailsRepository)
        {
            this._accountTransactionRepository = accountTransactionRepository;
            this._stockRegisterRepository = stockRegisterRepository; _context = context;
            this._purchaseRequisitionRepository = purchaseRequisitionRepository; _permissionApprovalDetailRepo = permissionApprovalDetailRepo;
            this._purchaseRequisitionDetailsRepository = purchaseRequisitionDetailsRepository; _BillOfQTyDetailsRepository = BillOfQTyDetailsRepository;
            _programsettingsRepository = programsettingsRepository; _voucherNumbersRepository = voucherNumbers; _approvalDetailRepo = approvalDetailRepo; _permissionApprovalRepo = permissionApprovalRepo; _approvalRepo = approvalRepo; locationrepository = _locationrepository;
            jobrepository = _jobrepository; _itemMasterRepository = itemMasterRepository; _unitMasterRepository = unitMasterRepository; _suppliersMasterRepository = suppliersMasterRepository; _locationMasterRepository = locationMasterRepository; _trackingRegisterRepository = trackingRegisterRepository;
        }
        public PurchaseRequisitionModel UpdatePurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions,
            List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            )
        {

            try
            {
                _purchaseRequisitionRepository.BeginTransaction();

                this.deleteAccountTractionByVoucherId(purchaseRequisition.PurchaseRequisitionNo, VoucherType.PurchaseRequisition_TYPE);
                purchaseRequisition.PurchaseRequisitionDetails = purchaseRequisitionDetails.Select((k) =>
                {
                    if (k.PurchaseRequisitionDetailsId == 0)
                    {
                        k.PurchaseRequisitionId = purchaseRequisition.PurchaseRequisitionId;
                        k.PurchaseRequisitionDetailsNo = purchaseRequisition.PurchaseRequisitionNo;
                        k.PurchaseRequisitionDetailsId = null;
                    }
                    else
                    {
                        k.PurchaseRequisitionId = purchaseRequisition.PurchaseRequisitionId;
                        k.PurchaseRequisitionDetailsNo = purchaseRequisition.PurchaseRequisitionNo;

                    }
                    return k;
                }).ToList();

                _purchaseRequisitionRepository.Update(purchaseRequisition);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    if (k.AccountsTransactionsTransSno == 0)
                    {
                        k.AccountsTransactionsTransDate = purchaseRequisition.PurchaseRequisitionDate;
                        k.AccountsTransactionsVoucherNo = purchaseRequisition.PurchaseRequisitionNo;
                        k.AccountsTransactionsVoucherType = VoucherType.PurchaseRequisition_TYPE;
                        k.AccountsTransactionsStatus = AccountStatus.Approved;
                    }

                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);

                //PermissionApprovalDetail by zaid              

                var rights = _context.tbl_approvalforms.Where(a => a.Voucher_Type.Contains("Purchase Requisition") && a.is_active == true).AsNoTracking().ToList();
                if (rights.Count > 0)
                {
                    var existdata = _permissionApprovalDetailRepo.GetAsQueryable().Where(a => a.VoucherId == purchaseRequisition.PurchaseRequisitionNo && a.VoucherType == "Purchase Requisition").ToList();

                    if (existdata.Count > 0)
                    {
                        _permissionApprovalDetailRepo.DeleteList(existdata);
                    }
                    foreach (var item in rights)
                    {
                        for (int level = 1; level <= item.NoOfLevel; level++)
                        {
                            PermissionApprovalDetail POList = new PermissionApprovalDetail();
                            POList.VoucherType = "Purchase Requisition";
                            POList.VoucherId = purchaseRequisition.PurchaseRequisitionNo;
                            POList.VoucherDate = purchaseRequisition.PurchaseRequisitionDate;
                            POList.Amount = (double)purchaseRequisition.PurchaseRequisitionNetAmount;
                            POList.CreatedBy = (int?)(purchaseRequisition.PurchaseRequisitionUserId ?? 1);
                            POList.LevelId = level;
                            POList.Status = "Pending";
                            POList.Remarks = purchaseRequisition.PurchaseRequisitionDescription;
                            _permissionApprovalDetailRepo.Insert(POList);
                        }
                    }

                }

                foreach (var item in purchaseRequisitionDetails)
                {
                    if (item.boqdId > 0)
                    {
                        var data = _BillOfQTyDetailsRepository.GetAsQueryable().AsNoTracking().Where(a => a.Id == item.boqdId).FirstOrDefault();
                        if (data != null)
                        {
                            data.IsEdit = false;
                            _BillOfQTyDetailsRepository.Update(data);
                        }
                    }
                }
                _purchaseRequisitionRepository.TransactionCommit();

            }
            catch (Exception ex)
            {
                _purchaseRequisitionRepository.TransactionRollback();
                throw ex;
            }

            return this.GetSavedPurchaseRequisitionDetails(purchaseRequisition.PurchaseRequisitionNo);
        }

        public int DeletePurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions,
            List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            )
        {
            int i = 0;
            try
            {
                _purchaseRequisitionRepository.BeginTransaction();

                purchaseRequisition.PurchaseRequisitionDelStatus = true;

                purchaseRequisitionDetails = purchaseRequisitionDetails.Select((k) =>
                {
                    k.PurchaseRequisitionDetailsDelStatus = true;
                    return k;
                }).ToList();

                //_purchaseRequisitionDetailsRepository.UpdateList(purchaseRequisitionDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransactionRepository.UpdateList(accountsTransactions);




                //stockRegister = stockRegister.Select((k) =>
                //{
                //    k.StockRegisterDelStatus = true;
                //    return k;
                //}).ToList();
                //_stockRegisterRepository.UpdateList(stockRegister);




                purchaseRequisition.PurchaseRequisitionDetails = purchaseRequisitionDetails;

                _purchaseRequisitionRepository.Update(purchaseRequisition);

                var vchrnumer = _voucherNumbersRepository.GetAsQueryable().Where(k => k.VouchersNumbersVNo == purchaseRequisition.PurchaseRequisitionNo).FirstOrDefault();

                _voucherNumbersRepository.Update(vchrnumer);

                _purchaseRequisitionRepository.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _purchaseRequisitionRepository.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;

        }
        public void deleteAccountTractionByVoucherId(string AccountsTransactionsVoucherNo, string AccountsTransactionsVoucherType)
        {
            List<AccountsTransactions> accttrans = this._accountTransactionRepository.GetAsQueryable()
                 .Where(k => k.AccountsTransactionsVoucherNo == AccountsTransactionsVoucherNo && k.AccountsTransactionsVoucherType == AccountsTransactionsVoucherType && k.AccountstransactionsDelStatus != true).Select((l) => new AccountsTransactions
                 {
                     AccountsTransactionsTransSno = l.AccountsTransactionsTransSno,
                     AccountstransactionsDelStatus = true,

                     AccountsTransactionsAccNo = l.AccountsTransactionsAccNo,
                     AccountsTransactionsTransDate = l.AccountsTransactionsTransDate,
                     AccountsTransactionsParticulars = l.AccountsTransactionsParticulars,

                     AccountsTransactionsDebit = l.AccountsTransactionsDebit,


                     AccountsTransactionsCredit = l.AccountsTransactionsCredit,
                     AccountsTransactionsFcDebit = l.AccountsTransactionsFcDebit,
                     AccountsTransactionsFcCredit = l.AccountsTransactionsFcCredit,

                     AccountsTransactionsVoucherType = l.AccountsTransactionsVoucherType,







                     AccountsTransactionsVoucherNo = l.AccountsTransactionsVoucherNo,
                     AccountsTransactionsDescription = l.AccountsTransactionsDescription,
                     AccountsTransactionsUserId = l.AccountsTransactionsUserId,

                     AccountsTransactionsStatus = l.AccountsTransactionsStatus,







                     AccountsTransactionsTstamp = l.AccountsTransactionsTstamp,
                     RefNo = l.RefNo,
                     AccountsTransactionsFsno = l.AccountsTransactionsFsno,

                     AccountsTransactionsAllocDebit = l.AccountsTransactionsAllocDebit,



                     AccountsTransactionsAllocCredit = l.AccountsTransactionsAllocCredit,
                     AccountsTransactionsAllocBalance = l.AccountsTransactionsAllocBalance,
                     AccountsTransactionsFcAllocDebit = l.AccountsTransactionsFcAllocDebit,

                     AccountsTransactionsFcAllocCredit = l.AccountsTransactionsFcAllocCredit,




                     AccountsTransactionsFcAllocBalance = l.AccountsTransactionsFcAllocBalance,
                     AccountsTransactionsLocation = l.AccountsTransactionsLocation,
                     AccountsTransactionsJobNo = l.AccountsTransactionsJobNo,

                     AccountsTransactionsCostCenterId = l.AccountsTransactionsCostCenterId,






                     AccountsTransactionsApprovalDt = l.AccountsTransactionsApprovalDt,
                     AccountsTransactionsDepartment = l.AccountsTransactionsDepartment,
                     AccountsTransactionsFcRate = l.AccountsTransactionsFcRate,

                     AccountsTransactionsCompanyId = l.AccountsTransactionsCompanyId,

                     AccountsTransactionsCurrencyId = l.AccountsTransactionsCurrencyId,
                     AccountsTransactionsDrGram = l.AccountsTransactionsDrGram,
                     AccountsTransactionsCrGram = l.AccountsTransactionsCrGram,

                     AccountsTransactionsCheqNo = l.AccountsTransactionsCheqNo,


                     AccountsTransactionsLpoNo = l.AccountsTransactionsLpoNo,
                     AccountsTransactionsCheqDate = l.AccountsTransactionsCheqDate,
                     AccountsTransactionsOpposEntryDesc = l.AccountsTransactionsOpposEntryDesc,

                     AccountsTransactionsAllocUpdateBal = l.AccountsTransactionsAllocUpdateBal,






                     AccountsTransactionsDeptId = l.AccountsTransactionsDeptId,
                     AccountsTransactionsVatno = l.AccountsTransactionsVatno,
                     AccountsTransactionsVatableAmount = l.AccountsTransactionsVatableAmount,








                 }).ToList();

            this._accountTransactionRepository.UpdateList(accttrans);
        }
        public PurchaseRequisitionModel InsertPurchaseRequisition(PurchaseRequisition purchaseRequisition, List<AccountsTransactions> accountsTransactions,
            List<PurchaseRequisitionDetails> purchaseRequisitionDetails
            )
        {
            try
            {
                _purchaseRequisitionRepository.BeginTransaction();
                string purchaseRequisitionNumber = this.GenerateVoucherNo(purchaseRequisition.PurchaseRequisitionDate.Date).VouchersNumbersVNo;
                purchaseRequisition.PurchaseRequisitionNo = purchaseRequisitionNumber;

                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _purchaseRequisitionRepository.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.PurchaseRequisitionId) + 1);

                purchaseRequisition.PurchaseRequisitionId = maxcount;

                purchaseRequisition.PurchaseRequisitionDetails = purchaseRequisition.PurchaseRequisitionDetails.Select((x) =>
                {
                    x.PurchaseRequisitionDetailsId = null;
                    x.PurchaseRequisitionId = maxcount;
                    x.PurchaseRequisitionDetailsNo = purchaseRequisitionNumber;
                    return x;
                }).ToList();

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = purchaseRequisitionNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseRequisition_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransactionRepository.InsertList(accountsTransactions);

                _purchaseRequisitionRepository.Insert(purchaseRequisition);

                //var permsionApproval = _permissionApprovalRepo.GetAsQueryable().AsNoTracking().Where(x => x.VoucherType == "Purchase Requisition").OrderBy(x => x.LevelOrder).ToList();
                //var approvals = new Approval()
                //{
                //    CreatedBy = 0,
                //    LocationId = null,
                //    Status = false,
                //    VoucherType = "Purchase Requisition"
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

                var rights = _context.tbl_approvalforms.Where(a => a.Voucher_Type.Contains("Purchase Requisition") && a.is_active == true).AsNoTracking().ToList();
                if (rights.Count > 0)
                {
                    foreach (var item in rights)
                    {
                        for (int level = 1; level <= item.NoOfLevel; level++)
                        {
                            PermissionApprovalDetail POList = new PermissionApprovalDetail();
                            POList.VoucherType = "Purchase Requisition";
                            POList.VoucherId = purchaseRequisition.PurchaseRequisitionNo;
                            POList.VoucherDate = purchaseRequisition.PurchaseRequisitionDate;
                            POList.Amount = (double)purchaseRequisition.PurchaseRequisitionNetAmount;
                            POList.CreatedBy = (int?)(purchaseRequisition.PurchaseRequisitionUserId ?? 1);
                            POList.LevelId = level;
                            POList.Status = "Pending";
                            POList.Remarks = purchaseRequisition.PurchaseRequisitionDescription;
                            _permissionApprovalDetailRepo.Insert(POList);
                        }
                    }
                }

                foreach (var item in purchaseRequisitionDetails)
                {
                    if (item.boqdId > 0)
                    {
                        var data = _BillOfQTyDetailsRepository.GetAsQueryable().AsNoTracking().Where(a => a.Id == item.boqdId).FirstOrDefault();
                        if (data != null)
                        {
                            data.IsEdit = false;
                            _BillOfQTyDetailsRepository.Update(data);
                        }
                    }
                }
                _purchaseRequisitionRepository.TransactionCommit();
                return this.GetSavedPurchaseRequisitionDetails(purchaseRequisition.PurchaseRequisitionNo);

            }
            catch (Exception ex)
            {
                _purchaseRequisitionRepository.TransactionRollback();
                throw ex;
            }



        }
        public IEnumerable<AccountsTransactions> GetAllTransaction()
        {
            return _accountTransactionRepository.GetAll();
        }
        public IEnumerable<PurchaseRequisition> GetPurchaseRequisition()
        {
            IEnumerable<PurchaseRequisition> purchaseRequisition_ALL = _purchaseRequisitionRepository.GetAll().Where(k => k.PurchaseRequisitionDelStatus == false || k.PurchaseRequisitionDelStatus == null);
            return purchaseRequisition_ALL;
        }
        public PurchaseRequisitionModel GetSavedPurchaseRequisitionDetails(string pvno)
        {
            PurchaseRequisitionModel purchaseRequisitionModel = new PurchaseRequisitionModel();

            purchaseRequisitionModel.purchaseRequisition = _purchaseRequisitionRepository.GetAsQueryable().Where(k => k.PurchaseRequisitionNo == pvno).SingleOrDefault();

            purchaseRequisitionModel.accountsTransactions = _accountTransactionRepository.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.PurchaseRequisition_TYPE && (c.AccountstransactionsDelStatus == false || c.AccountstransactionsDelStatus == null)).ToList();
            purchaseRequisitionModel.purchaseRequisitionDetails = _purchaseRequisitionDetailsRepository.GetAsQueryable().Where(x => x.PurchaseRequisitionDetailsNo == pvno && (x.PurchaseRequisitionDetailsDelStatus == false || x.PurchaseRequisitionDetailsDelStatus == null)).ToList();
            return purchaseRequisitionModel;

        }
        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseRequisition_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumbersRepository.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseRequisition_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;

                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseRequisition_TYPE,
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
        public async Task<Response<List<PurChaseRequisitionStatus>>> GetPurchaseRequisitionStatus(PurChaseRequisitionStatusFilterReport model)
        {
            List<PurChaseRequisitionStatus> data = new List<PurChaseRequisitionStatus>();
            try
            {
                string filteredValue = string.Empty;
                if (model.JobId > 0)
                {
                    filteredValue += $" and pd.PurchaseRequisitionJobId = {model.JobId} ";
                }
                if (model.ItemId > 0)
                {
                    filteredValue += $" and pd.PurchaseRequisitionDetailsItemID = {model.ItemId} ";
                }
                if (!string.IsNullOrEmpty(model.PRNO) && model.PRNO.ToUpper() != "ALL".ToUpper())
                {
                    filteredValue += $" and po.PurchaseRequisitionRefNo = {model.PRNO} ";
                }
                if (!string.IsNullOrEmpty(model.POStatus))
                {
                    filteredValue += $" and po.PurchaseRequisitionStatus = {model.POStatus} ";
                }
                if (!string.IsNullOrEmpty(model.fromDate) && !string.IsNullOrEmpty(model.toDate))
                {
                    filteredValue += $" and (pm.PurchaseRequisitionDate >= {model.fromDate} and  pm.PurchaseRequisitionDate <= {model.toDate} )";
                }

                var subqueryRDI = (from pod in dbContext.PurchaseOrderDetails
                                   join po in dbContext.GeneralPurchaseOrder on pod.PurchaseOrderDetailsPoId equals (int?)po.GeneralPurchaseOrderId
                                   group new { pod, po } by new
                                   {
                                       pod.PurchaseOrderDetailsId,
                                       pod.PurchaseOrderDetailsPrdId,
                                       po.GeneralPurchaseOrderStatus
                                   } into grp
                                   select new
                                   {
                                       grp.Key.PurchaseOrderDetailsId,
                                       grp.Key.PurchaseOrderDetailsPrdId,
                                       grp.Key.GeneralPurchaseOrderStatus,
                                       Quantity = grp.Sum(x => x.pod.PurchaseOrderDetailsQuantity)
                                   });
                var subqueryIMT = (from d in dbContext.AvIssueVoucherDetails
                                   join i in dbContext.AvIssueVoucher on d.AvIssueVoucherDetailsAvSvNo equals i.AvIssueVoucherAccNo
                                   group d by d.AvIssueVoucherDetailsMaterialId into grp
                                   select new
                                   {
                                       grp.Key,
                                       issueQty = grp.Sum(x => x.AvIssueVoucherDetailsSoldQuantity),
                                       stock = grp.Sum(x => x.AvIssueVoucherDetailsStock),
                                       BalToIssue = grp.Sum(x => x.AvIssueVoucherDetailsStock) - grp.Sum(x => x.AvIssueVoucherDetailsSoldAmount)
                                   });
                var subquerySEI = (from d in dbContext.AvIssueVoucherDetails
                                   join i in dbContext.AvIssueVoucher on d.AvIssueVoucherDetailsAvSvNo equals i.AvIssueVoucherAccNo
                                   join s in dbContext.StockRegister on d.AvIssueVoucherDetailsMaterialId equals s.StockRegisterMaterialID
                                   group new { d, s } by d.AvIssueVoucherDetailsMaterialId into grp
                                   select new
                                   {
                                       grp.Key,
                                       issueQty = grp.Sum(x => x.d.AvIssueVoucherDetailsSoldQuantity),
                                       stock = grp.Sum(x => x.s.StockRegisterSIN - x.s.StockRegisterSout),
                                       BalToIssue = grp.Sum(x => (x.s.StockRegisterSIN - x.s.StockRegisterSout) - Convert.ToDecimal(x.d.AvIssueVoucherDetailsSoldQuantity))
                                   });
                var results = (from pd in _purchaseRequisitionDetailsRepository.GetAsQueryable().Where($"1 == 1  {filteredValue}")

                               join pm in dbContext.PurchaseRequisition on (decimal?)pd.PurchaseRequisitionId equals pm.PurchaseRequisitionId
                               join im in dbContext.ItemMaster on (long)pd.PurchaseRequisitionDetailsMatId equals im.ItemMasterItemId into imJoin
                               from im in imJoin.DefaultIfEmpty()

                               join um in dbContext.UnitMaster on pd.PurchaseRequisitionDetailsUnitId equals um.UnitMasterUnitId into umJoin
                               from um in umJoin.DefaultIfEmpty()

                               join pods in subqueryRDI on pd.PurchaseRequisitionDetailsMatId equals pods.PurchaseOrderDetailsPrdId into podJoin
                               from pod in podJoin.DefaultIfEmpty()

                               join vd in subqueryIMT on im.ItemMasterItemId equals Convert.ToInt64(vd.Key) into vdJoin
                               from vd in vdJoin.DefaultIfEmpty()
                               join vda in subquerySEI on im.ItemMasterItemId equals Convert.ToInt64(vda.Key) into vdaJoin
                               from vda in vdaJoin.DefaultIfEmpty()

                                   // where pd.PurchaseRequisitionDetailsReqStatus == "OPEN" && "$ {filteredValue}"

                               select new PurChaseRequisitionStatus
                               {
                                   issueQty = Convert.ToInt32(vd.issueQty),
                                   BalToIssue = Convert.ToInt32(vd.BalToIssue),
                                   stock = Convert.ToInt32(vd.stock),
                                   Unit = um.UnitMasterUnitShortName + " :: " + um.UnitMasterUnitId.ToString(),
                                   PartNo = Convert.ToString(im.ItemMasterPartNo),
                                   DelQty = Convert.ToInt32(pod.Quantity),
                                   PoStatus = pod.GeneralPurchaseOrderStatus,
                                   RequisitionDate = Convert.ToDateTime(pm.PurchaseRequisitionDate)

                               }).ToListAsync();
                return new Response<List<PurChaseRequisitionStatus>>()
                {
                    Valid = true,
                    Message = "Data Found",
                    Result = data
                };
            }
            catch (Exception ex)
            {
                return Response<List<PurChaseRequisitionStatus>>.Fail(new List<PurChaseRequisitionStatus>(), ex.Message);
            }
        }
        public async Task<Response<List<PurChaseReqFields>>> GetPurchaseReqJobId(PurChaseRequisitionFilterReport model)
        {
            List<PurChaseReqFields> data = new List<PurChaseReqFields>();
            try
            {
                string filteredValue = string.Empty;
                var query = _purchaseRequisitionRepository.GetAsQueryable().AsNoTracking().Where(s => s.PurchaseRequisitionDelStatus != true);
                if (model.JobId > 0)
                {
                    query = query.Where(a => a.PurchaseRequisitionJobId == model.JobId);

                }
                data = (from sm in query
                        join sd in _purchaseRequisitionDetailsRepository.GetAsQueryable().AsNoTracking()
                            on (decimal?)sm.PurchaseRequisitionId equals sd.PurchaseRequisitionId
                        join im in _itemMasterRepository.GetAsQueryable().AsNoTracking()
                            on (long?)sd.PurchaseRequisitionDetailsMatId equals im.ItemMasterItemId into imGroup
                        from im in imGroup.DefaultIfEmpty()
                        join um in _unitMasterRepository.GetAsQueryable().AsNoTracking()
                            on sd.PurchaseRequisitionDetailsUnitId equals um.UnitMasterUnitId into umGroup
                        from um in umGroup.DefaultIfEmpty()
                        join jm in jobrepository.GetAsQueryable().AsNoTracking()
                            on sm.PurchaseRequisitionJobId equals jm.JobMasterJobId into jmGroup
                        from jm in jmGroup.DefaultIfEmpty()
                        join lm in locationrepository.GetAsQueryable().AsNoTracking()
                            on sm.PurchaseRequisitionLocationId equals lm.LocationMasterLocationId into lmGroup
                        from lm in lmGroup.DefaultIfEmpty()
                        join iv in _context.IssueVoucherDetails.AsNoTracking()
                            on sd.PurchaseRequisitionDetailsId equals iv.IssueVoucherDetailsReqDId into ivGroup
                        from srq in ivGroup.DefaultIfEmpty()
                        select new PurChaseReqFields
                        {
                            PurchaseRequisitionPartyName = sm.PurchaseRequisitionPartyName ?? "",
                            purchaseRequisitionType = sm.PurchaseRequisitionType ?? "",
                            Item_Name = im != null ? im.ItemMasterItemName : "",
                            Unit = um != null ? um.UnitMasterUnitShortName : "",
                            DelQty = srq.IssueVoucherDetailsQuantity ?? 0m,
                            SRID = sm.PurchaseRequisitionId,
                            SRDID = sd.PurchaseRequisitionDetailsId,
                            SRNo = sm.PurchaseRequisitionNo ?? "",
                            Date = sm.PurchaseRequisitionDate,
                            Rate = sd.PurchaseRequisitionDetailsRate ?? 0,
                            SRQ = sd.PurchaseRequisitionDetailsQuantity ?? 0,
                            BalQty = (sd.PurchaseRequisitionDetailsQuantity - (srq.IssueVoucherDetailsQuantity ?? 0m)) ?? 0,
                            matId = sd.PurchaseRequisitionDetailsMatId,
                            unitid = sd.PurchaseRequisitionDetailsUnitId,
                            jobid = sm.PurchaseRequisitionJobId,
                            Stock = srq.IssueVoucherDetailsCurrentStockQty ?? 0m
                        })
                     .OrderBy(x => x.SRDID)
                     .ToList();

                data = data.Where(a => a.BalQty > 0).ToList();
                return new Response<List<PurChaseReqFields>>()
                {
                    Valid = true,
                    Message = "Data Found",
                    Result = data
                };
            }
            catch (Exception ex)
            {
                return Response<List<PurChaseReqFields>>.Fail(new List<PurChaseReqFields>(), ex.Message);
            }
        }

        //public async Task<Response<List<PurChaseReqFields>>> GetPurchaseReqJobId(PurChaseRequisitionFilterReport model)
        //{
        //    List<PurChaseReqFields> data = new List<PurChaseReqFields>();
        //    try
        //    {
        //        string filteredValue = string.Empty;

        //        var query = _purchaseRequisitionRepository.GetAsQueryable().Where(s => s.PurchaseRequisitionDelStatus != true).ToList();
        //        if (model.JobId > 0)
        //        {
        //            query = query.Where(a => a.PurchaseRequisitionJobId == model.JobId).ToList();

        //        }
        //        data = (from sm in query
        //                join sd in _purchaseRequisitionDetailsRepository.GetAsQueryable() on (decimal?)sm.PurchaseRequisitionId equals sd.PurchaseRequisitionId
        //                join im in _itemMasterRepository.GetAsQueryable() on (long?)sd.PurchaseRequisitionDetailsMatId equals im.ItemMasterItemId into imGroup
        //                from im in imGroup.DefaultIfEmpty()

        //                join um in _unitMasterRepository.GetAsQueryable() on sd.PurchaseRequisitionDetailsUnitId equals um.UnitMasterUnitId into umGroup
        //                from um in umGroup.DefaultIfEmpty()

        //                join jm in jobrepository.GetAsQueryable() on sm.PurchaseRequisitionJobId equals jm.JobMasterJobId into jmGroup
        //                from jm in jmGroup.DefaultIfEmpty()

        //                join lm in locationrepository.GetAsQueryable() on sm.PurchaseRequisitionLocationId equals lm.LocationMasterLocationId into lmGroup
        //                from lm in lmGroup.DefaultIfEmpty()

        //                join srq in (
        //                    from tr in _trackingRegisterRepository.GetAsQueryable()
        //                    where tr.TrackingRegisterVoucherType == "CustomerPurchaseOrder"
        //                    group tr by tr.TrackingRegisterDetailsId into trGroup
        //                    select new
        //                    {
        //                        Tracking_Register_Details_ID = trGroup.Key,
        //                        DelQty = trGroup.Sum(tr => tr.TrackingRegisterQtyout ?? 0),
        //                        Stock = trGroup.Sum(tr => tr.TrackingRegisterQtyin ?? 0 - tr.TrackingRegisterQtyout ?? 0)
        //                    }
        //                ) on sd.PurchaseRequisitionDetailsId equals srq.Tracking_Register_Details_ID into srqGroup
        //                from srq in srqGroup.DefaultIfEmpty()
        //                where (sd.PurchaseRequisitionDetailsQuantity - srq?.DelQty) > 0
        //                select new PurChaseReqFields
        //                {
        //                    PurchaseRequisitionPartyName = sm.PurchaseRequisitionPartyName ?? "",
        //                    purchaseRequisitionType = sm.PurchaseRequisitionType ?? "",
        //                    Item_Name = im.ItemMasterItemName.ToString() ?? "",
        //                    Unit = um == null ? null : um.UnitMasterUnitShortName ?? "" + " :: " + um.UnitMasterUnitId.ToString() ?? "",
        //                    DelQty = srq?.DelQty ?? 0,
        //                    SRID = sm.PurchaseRequisitionId,
        //                    SRDID = sd.PurchaseRequisitionDetailsId,
        //                    SRNo = sm.PurchaseRequisitionNo ?? "",
        //                    Date = sm.PurchaseRequisitionDate,
        //                    Rate = sd.PurchaseRequisitionDetailsRate ?? 0,
        //                    SRQ = sd.PurchaseRequisitionDetailsQuantity ?? 0,
        //                    BalQty = (sd.PurchaseRequisitionDetailsQuantity - srq?.DelQty) ?? 0,
        //                    matId = sd.PurchaseRequisitionDetailsMatId ?? 0,
        //                    unitid = sd.PurchaseRequisitionDetailsUnitId,
        //                    jobid = sm.PurchaseRequisitionJobId,
        //                    Stock = srq?.Stock ?? 0
        //                }).OrderBy(x => x.SRDID).ToList();


        //        return new Response<List<PurChaseReqFields>>()
        //        {
        //            Valid = true,
        //            Message = "Data Found",
        //            Result = data
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return Response<List<PurChaseReqFields>>.Fail(new List<PurChaseReqFields>(), ex.Message);
        //    }
        //}

    }
}
