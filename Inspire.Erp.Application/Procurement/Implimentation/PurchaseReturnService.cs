using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.MODULE;
using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Procurement;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Procurement.Implementation
{
    public class PurchaseReturnService : IPurchaseReturnService
    {

        private readonly IRepository<VouchersNumbers> _voucherNumber;
        private IRepository<CurrencyMaster> _currencyMasterRepository;
        private readonly IRepository<PurchaseReturnVoucher> _purchaseReturn;
        private readonly IRepository<PurchaseVoucher> _purchaseVoucher;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<StockRegister> _stockRegister;
        private readonly IRepository<SuppliersMaster> _supplierMaster;
        private readonly IRepository<PurchaseReturnVoucherDetails> _purchaseReturnDetail;
        private readonly IRepository<PurchaseVoucherDetails> _purchasevoucherDetail;
        private readonly IRepository<LocationMaster> _location;
        private readonly IRepository<JobMaster> _jobMaster;
        private readonly IRepository<PurchaseReturn> _purchaseReturnRepo;
        private readonly IRepository<PurchaseReturnDetails> _purchaseReturnDetailsRepo;

        private readonly IRepository<PurchaseOrder> _purchaseOrderRepo;
        private readonly IRepository<PurchaseOrderDetails> _purchaseOrderDetailsRepo;

        private readonly IUtilityService _utilityService;
        private IRepository<UnitDetails> _UnitDetailsRepository;


        private IRepository<ProgramSettings> _programsettingsRepository;
        public PurchaseReturnService(IRepository<PurchaseReturnVoucher> purchaseReturn, IUtilityService utilityService,
            IRepository<PurchaseVoucherDetails> purchasevoucherDetail, IRepository<PurchaseVoucher> purchaseVoucher,
            IRepository<LocationMaster> location, IRepository<StockRegister> stockRegister, IRepository<JobMaster> jobMasters,
            IRepository<PurchaseReturnVoucherDetails> purchaseReturnDetail, IRepository<SuppliersMaster> supplierMaster,
            IRepository<PurchaseReturn> purchaseReturnRepo,
            IRepository<PurchaseReturnDetails> purchaseReturnDetailsRepo, IRepository<AccountsTransactions> accountTransaction,
            IRepository<CurrencyMaster> currencyMasterRepository, IRepository<ProgramSettings> programsettingsRepository,
            IRepository<VouchersNumbers> voucherNumber,
            IRepository<PurchaseOrder> purchaseOrderRepo,
            IRepository<PurchaseOrderDetails> purchaseOrderDetailsRepo, IRepository<UnitDetails> unitDetailsRepository)
        {
            _purchaseReturn = purchaseReturn;
            _purchaseReturnDetail = purchaseReturnDetail;
            _utilityService = utilityService;
            _supplierMaster = supplierMaster;
            _purchasevoucherDetail = purchasevoucherDetail;
            _purchaseVoucher = purchaseVoucher;
            _location = location;
            _jobMaster = jobMasters;
            _stockRegister = stockRegister;
            _purchaseReturnRepo = purchaseReturnRepo;
            _purchaseReturnDetailsRepo = purchaseReturnDetailsRepo;
            _accountTransaction = accountTransaction;
            _currencyMasterRepository = currencyMasterRepository;
            _programsettingsRepository = programsettingsRepository;
            _voucherNumber = voucherNumber;
            _purchaseOrderRepo = purchaseOrderRepo;
            _purchaseOrderDetailsRepo = purchaseOrderDetailsRepo;
            _UnitDetailsRepository = unitDetailsRepository;
        }
        public async Task<Response<List<DropdownResponse>>> GetLocationMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.AddRange(await _location.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.LocationMasterLocationId != null ? Convert.ToInt32(x.LocationMasterLocationId) : 0,
                    Name = x.LocationMasterLocationName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetVoucherTypeDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = "CASH",
                    Name = "CASH"
                });
                response.Add(new DropdownResponse()
                {
                    Value = "CREDIT",
                    Name = "CREDIT"
                });
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetJObMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.AddRange(await _jobMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.JobMasterJobId != null ? Convert.ToInt32(x.JobMasterJobId) : 0,
                    Name = x.JobMasterJobName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetSupplierMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = "All"
                //});
                response.AddRange(await _supplierMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id = x.SuppliersMasterSupplierId != null ? Convert.ToInt32(x.SuppliersMasterSupplierId) : 0,
                    Name = x.SuppliersMasterSupplierName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<PurchaseReturnVoucherResponse>>>> GetPurchaseReturnList(GenericGridViewModel model)
        {
            try
            {
                List<PurchaseReturnVoucherResponse> response = new List<PurchaseReturnVoucherResponse>();
                var orders = await _purchaseReturn.GetAsQueryable().ToListAsync();
                response.AddRange(orders.Select(x => new PurchaseReturnVoucherResponse
                {
                    PurchaseReturnVoucherId = x.PurchaseReturnVoucherId,
                    PurchaseReturnVoucherGrno = x.PurchaseReturnVoucherGrno,
                    PurchaseReturnVoucherRetDate = x.PurchaseReturnVoucherRetDate,
                    PurchaseReturnVoucherRetId = x.PurchaseReturnVoucherRetId,
                    PurchaseReturnVoucherRetDateString = (x.PurchaseReturnVoucherRetDate != null ? x.PurchaseReturnVoucherRetDate : DateTime.MinValue).Value.ToString("dd-MM-yyyy"),
                    PurchaseReturnVoucherVoucherDateString = (x.PurchaseReturnVoucherVoucherDate != null ? x.PurchaseReturnVoucherVoucherDate : DateTime.MinValue).Value.ToString("dd-MM-yyyy"),
                    PurchaseReturnVoucherVoucherType = x.PurchaseReturnVoucherVoucherType,
                    PurchaseReturnVoucherNetAmount = x.PurchaseReturnVoucherNetAmount,
                    PurchaseReturnVoucherNarration = x.PurchaseReturnVoucherNarration,
                    PurchaseReturnVoucherUserId = x.PurchaseReturnVoucherUserId,
                }).ToList());
                var gridResponse = new GridWrapperResponse<List<PurchaseReturnVoucherResponse>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<PurchaseReturnVoucherResponse>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<PurchaseReturnVoucherResponse>>>.Fail(new GridWrapperResponse<List<PurchaseReturnVoucherResponse>>(), ex.Message);
            }
        }
        public async Task<Response<PurchaseReturnVoucherResponse>> GetSpecificPurchaseReturn(string id = null)
        {
            try
            {
                PurchaseReturnVoucherResponse model = new PurchaseReturnVoucherResponse();
                await Task.Run(() =>
                {
                    var orders = _purchaseReturn.GetAsQueryable().FirstOrDefault(x => x.PurchaseReturnVoucherRetId == id);
                    model.PurchaseReturnVoucherId = orders.PurchaseReturnVoucherId;
                    model.PurchaseReturnVoucherGrno = orders.PurchaseReturnVoucherGrno;
                    model.PurchaseReturnVoucherRetDate = orders.PurchaseReturnVoucherRetDate;
                    model.PurchaseReturnVoucherRetId = orders.PurchaseReturnVoucherRetId;
                    model.PurchaseReturnVoucherVoucherType = orders.PurchaseReturnVoucherVoucherType;
                    model.PurchaseReturnVoucherNetAmount = orders.PurchaseReturnVoucherNetAmount;
                    model.PurchaseReturnVoucherNarration = orders.PurchaseReturnVoucherNarration;
                    model.PurchaseReturnVoucherUserId = orders.PurchaseReturnVoucherUserId;
                    model.PurchaseReturnVoucherDetails = _purchaseReturnDetail.GetAsQueryable()
                        .Where(x => x.PurchaseReturnVoucherDetailsRetId == id).Select(x => new PurchaseReturnVoucherDetailResponse
                        {
                            PurchaseReturnVoucherDetailsBatchCode = x.PurchaseReturnVoucherDetailsBatchCode,
                            PurchaseReturnVoucherDetailsRetId = x.PurchaseReturnVoucherDetailsRetId,
                            PurchaseReturnVoucherDetailsAmount = x.PurchaseReturnVoucherDetailsAmount,
                            PurchaseReturnVoucherDetailsBatch = x.PurchaseReturnVoucherDetailsBatch,
                            PurchaseReturnVoucherDetailsMaterialId = x.PurchaseReturnVoucherDetailsMaterialId,
                            PurchaseReturnVoucherDetailsQty = x.PurchaseReturnVoucherDetailsQty,
                            PurchaseReturnVoucherDetailsRemarks = x.PurchaseReturnVoucherDetailsRemarks,
                            PurchaseReturnVoucherDetailsSno = x.PurchaseReturnVoucherDetailsSno,
                            PurchaseReturnVoucherDetailsRate = x.PurchaseReturnVoucherDetailsRate,
                            PurchaseReturnVoucherDetailsVoucherNo = x.PurchaseReturnVoucherDetailsVoucherNo
                        }).ToList();

                });
                return Response<PurchaseReturnVoucherResponse>.Success(model, "Records FOund.");
            }
            catch (Exception ex)
            {
                return Response<PurchaseReturnVoucherResponse>.Fail(new PurchaseReturnVoucherResponse(), ex.Message);
            }
        }
        public async Task<Response<GridWrapperResponse<List<GetPurchaseVoucherResponse>>>> GetPurchaseVoucherList(GenericGridViewModel model)
        {
            try
            {
                string query = $@" 1 == 1 {model.Filter}";
                List<GetPurchaseVoucherResponse> response = new List<GetPurchaseVoucherResponse>();
                var orders = await _purchaseVoucher.GetAsQueryable()
                    .Where(x => model.Search != null ? x.PurchaseVoucherGRNo.Contains(model.Search) || x.PurchaseVoucherDescription.Contains(model.Search)
                    || x.PurchaseVoucherVoucherNo.Contains(model.Search) : 1 == 1).Where(query).ToListAsync();
                response.AddRange(orders.Select(x => new GetPurchaseVoucherResponse
                {
                    PurchaseVoucherActualAmount = x.PurchaseVoucherActualAmount,
                    PurchaseVoucherLpoNo = x.PurchaseVoucherLPONo,
                    PurchaseVoucherGrDate = x.PurchaseVoucherGRDate,
                    PurchaseVoucherGrDateString = x.PurchaseVoucherGRDate != null ? x.PurchaseVoucherGRDate.Value.ToString("dd-MM-yyyy") : "",
                    PurchaseVoucherVoucherNo = x.PurchaseVoucherVoucherNo,
                    PurchaseVoucherDescription = x.PurchaseVoucherDescription,
                    PurchaseVoucherCashSupplierName = x.PurchaseVoucherCashSupplierName,
                    PurchaseVoucherNetAmount = x.PurchaseVoucherNetAmount,
                    PurchaseVoucherPurchaseType = x.PurchaseVoucherPurchaseType,
                    PurchaseVoucherGrNo = x.PurchaseVoucherGRNo,
                    PurchaseVoucherPurId = (int)x.PurchaseVoucherPurID,

                }).ToList());
                var gridResponse = new GridWrapperResponse<List<GetPurchaseVoucherResponse>>();
                gridResponse.Data = response;
                gridResponse.Total = 0;
                return Response<GridWrapperResponse<List<GetPurchaseVoucherResponse>>>.Success(gridResponse, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GridWrapperResponse<List<GetPurchaseVoucherResponse>>>.Fail(new GridWrapperResponse<List<GetPurchaseVoucherResponse>>(), ex.Message);
            }
        }
        public async Task<Response<PurchaseReturnVoucherResponse>> GetSpecificPurchaseVoucher(string id)
        {
            try
            {
                PurchaseReturnVoucherResponse model = new PurchaseReturnVoucherResponse();
                var itemmaster = await _utilityService.GetItemMaster();
                var orders = await _purchaseVoucher.GetAsQueryable().FirstOrDefaultAsync(x => x.PurchaseVoucherVoucherNo == id);
                model.PurchaseReturnVoucherGrno = orders.PurchaseVoucherVoucherNo;
                model.PurchaseReturnVoucherVoucherDate = orders.PurchaseVoucherGRDate;
                model.PurchaseReturnVoucherVoucherType = orders.PurchaseVoucherPurchaseType;
                model.PurchaseReturnVoucherLocationId = orders.PurchaseVoucherLocationID;
                model.PurchaseReturnVoucherSpId = orders.PurchaseVoucherSPID;
                model.PurchaseReturnVoucherNarration = orders.PurchaseVoucherDescription;
                model.PurchaseReturnVoucherVatNo = orders.PurchaseVoucherVATNo;
                model.PurchaseReturnVoucherSupInvNo = orders.PurchaseVoucherSupplyInvoiceNo;
                model.PurchaseReturnVoucherDetails = new List<PurchaseReturnVoucherDetailResponse>();
                var purchaseReturnVoucherDetails = await _purchasevoucherDetail.GetAsQueryable()
                 .Where(x => x.PurchaseVoucherDetailsVoucherNo == id)
                 .Select(x => new PurchaseReturnVoucherDetailResponse
                 {
                     PurchaseReturnVoucherDetailsSno = 0,
                     PurchaseReturnVoucherDetailsBatchCode = x.PurchaseVoucherDetailsBatchCode,
                     PurchaseReturnVoucherDetailsAmount = Convert.ToDouble(x.PurchaseVoucherDetailsAmount),
                     PurchaseReturnVoucherDetailsFocQty = x.PurchaseVoucherDetailsFoc,
                     PurchaseReturnVoucherDetailsMaterialId = x.PurchaseVoucherDetailsMaterialId,
                     PurchaseReturnVoucherDetailsPurQty = Convert.ToDouble(x.PurchaseVoucherDetailsQuantity),
                     PurchaseReturnVoucherDetailsRemarks = x.PurchaseVoucherDetailsRemarks,
                     PurchaseReturnVoucherDetailsRate = Convert.ToDouble(x.PurchaseVoucherDetailsRate),
                     PurchaseReturnVoucherDetailsUnitId = x.PurchaseVoucherDetailsUnitId,
                     PurchaseReturnVoucherDetailsVoucherNo = x.PurchaseVoucherDetailsGrnNo,
                     PurchaseReturnVoucherDetailsVatAmount = Convert.ToDouble(x.PurchaseVoucherDetailsVatAmount),
                     PurchaseReturnVoucherDetailsQty = 0
                 })
                 .ToListAsync();

                foreach (var purchaseReturnVoucherDetail in purchaseReturnVoucherDetails)
                {
                    var material = itemmaster.Result.FirstOrDefault(c => c.ItemMasterItemId == purchaseReturnVoucherDetail.PurchaseReturnVoucherDetailsMaterialId);
                    purchaseReturnVoucherDetail.MaterialName = material != null ? material.ItemMasterItemName : "";
                }
                model.PurchaseReturnVoucherDetails.AddRange(purchaseReturnVoucherDetails);
                return Response<PurchaseReturnVoucherResponse>.Success(model, "Records FOund.");
            }
            catch (Exception ex)
            {
                return Response<PurchaseReturnVoucherResponse>.Fail(new PurchaseReturnVoucherResponse(), ex.Message);
            }
        }
        public async Task<Response<bool>> AddEditPurchaseReturn(AddEditPurchaseReturnResponse model)
        {
            try
            {
                string message = null;
                if (model.PurchaseReturnVoucherId == 0)
                {
                    message = await AddPurchaseReturn(model);
                }
                else
                {
                    message = await EditPurchaseReturn(model);
                }
                if (message == null)
                {
                    return Response<bool>.Fail(false, "Something went wrong. Please try again later.");
                }
                return Response<bool>.Success(true, "Changes Saved Successfully.");
            }
            catch (Exception ex)
            {

                return Response<bool>.Fail(false, ex.Message);
            }
        }
        private async Task<string> AddPurchaseReturn(AddEditPurchaseReturnResponse model)
        {
            try
            {
                var financial = await _utilityService.GetFinancialPeriods();
                Random random = new Random();
                int vouhcerDetailid = random.Next();

                var supplier = _supplierMaster.GetAsQueryable().Where(x => x.SuppliersMasterSupplierId == model.PurchaseReturnVoucherSpId).FirstOrDefault();
                string supAccount = supplier != null ? supplier.SuppliersMasterSupplierReffAcNo : "";
                var vouchers = await _utilityService.AddVoucherNumber("Purchase Return", "PRV");
                PurchaseReturnVoucher order = new PurchaseReturnVoucher()
                {
                    PurchaseReturnVoucherVoucherId = vouchers.Result.Value,
                    PurchaseReturnVoucherRetId = vouchers.Result.Value,
                    PurchaseReturnVoucherDiscount = model.PurchaseReturnVoucherDiscount,
                    PurchaseReturnVoucherExcludeVat = model.PurchaseReturnVoucherExcludeVat,
                    PurchaseReturnVoucherFcDiscAmount = model.PurchaseReturnVoucherFcDiscAmount,
                    PurchaseReturnVoucherFsno = financial != null ? Convert.ToInt32(financial.Result.FinancialPeriodsFsno) : 0,
                    PurchaseReturnVoucherGrno = model.PurchaseReturnVoucherGrno,
                    PurchaseReturnVoucherRetDate = model.PurchaseReturnVoucherRetDate,
                    PurchaseReturnVoucherVoucherDate = model.PurchaseReturnVoucherVoucherDate,
                    PurchaseReturnVoucherNarration = model.PurchaseReturnVoucherNarration,
                    PurchaseReturnVoucherSupInvNo = model.PurchaseReturnVoucherSupInvNo,
                    PurchaseReturnVoucherUserId = 0,

                    PurchaseReturnVoucherLocationId = model.PurchaseReturnVoucherLocationId,
                    PurchaseReturnVoucherVatNo = model.PurchaseReturnVoucherVatNo,
                    PurchaseReturnVoucherVoucherType = model.PurchaseReturnVoucherVoucherType,
                    PurchaseReturnVoucherTransportCost = model.PurchaseReturnVoucherTransportCost,
                    PurchaseReturnVoucherVatAmount = model.PurchaseReturnVoucherVatAmount,
                    PurchaseReturnVoucherSpId = model.PurchaseReturnVoucherSpId,
                    PurchaseReturnVoucherVatPercentage = model.PurchaseReturnVoucherVatPercentage,
                    PurchaseReturnVoucherVatRoundAmt = model.PurchaseReturnVoucherVatRoundAmt,
                    PurchaseReturnVoucherStatus = model.PurchaseReturnVoucherStatus,
                    PurchaseReturnVoucherNetAmount = model.PurchaseReturnVoucherNetAmount,
                    PurchaseReturnVoucherGrossAmount = model.PurchaseReturnVoucherGrossAmount,
                    PurchaseReturnVoucherVatRoundSign = model.PurchaseReturnVoucherVatRoundSign

                };
                _purchaseReturn.Insert(order);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Puchase Return",
                    Section = "Add Puchase Return",
                    Entity = "Puchase Return",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion

                _purchaseReturn.SaveChangesAsync();
                List<PurchaseReturnVoucherDetails> orderDetails = new List<PurchaseReturnVoucherDetails>();
                orderDetails.AddRange(model.PurchaseReturnVoucherDetails.Select(x => new PurchaseReturnVoucherDetails
                {
                    PurchaseReturnVoucherDetailsBatchCode = x.PurchaseReturnVoucherDetailsBatchCode,
                    PurchaseReturnVoucherDetailsAmount = x.PurchaseReturnVoucherDetailsAmountNew,
                    PurchaseReturnVoucherDetailsBatch = x.PurchaseReturnVoucherDetailsBatch,
                    PurchaseReturnVoucherDetailsJobId = model.PurchaseReturnVoucherDetailsJobId,
                    PurchaseReturnVoucherDetailsFocQty = x.PurchaseReturnVoucherDetailsFocQty,
                    PurchaseReturnVoucherDetailsMaterialId = x.PurchaseReturnVoucherDetailsMaterialId,

                    PurchaseReturnVoucherDetailsQty = x.PurchaseReturnVoucherDetailsQty + x.PurchaseReturnVoucherDetailsFocQty,
                    PurchaseReturnVoucherDetailsRemarks = x.PurchaseReturnVoucherDetailsRemarks,
                    PurchaseReturnVoucherDetailsRetId = vouchers.Result.Value,
                    PurchaseReturnVoucherDetailsRate = x.PurchaseReturnVoucherDetailsRate,
                    PurchaseReturnVoucherDetailsUnitId = x.PurchaseReturnVoucherDetailsUnitId,
                    PurchaseReturnVoucherDetailsVoucherNo = x.PurchaseReturnVoucherDetailsVoucherNo,
                    PurchaseReturnVoucherDetailsVatAmount = x.PurchaseReturnVoucherDetailsVatAmount,
                    PurchaseReturnVoucherDetailsVatPercetage = x.PurchaseReturnVoucherDetailsVatPercetage,
                    PurchaseReturnVoucherDetailsPurQty = x.PurchaseReturnVoucherDetailsPurQty,
                    PurchaseReturnVoucherDetailsFcAmount = x.PurchaseReturnVoucherDetailsAmountNew,
                }));
                _purchaseReturnDetail.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Detaillog = new AddActivityLogViewModel()
                {
                    Page = "Puchase Return",
                    Section = "Add Puchase Return Detail",
                    Entity = "Puchase Return Detail",

                };
                await _utilityService.AddUserTrackingLog(Detaillog);
                #endregion

                _purchaseReturnDetail.SaveChangesAsync();

                var stockRegister = model.PurchaseReturnVoucherDetails.Select(x => new StockRegister
                {
                    StockRegisterAmount = x.PurchaseReturnVoucherDetailsAmountNew.HasValue
                    ? Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmount)
                    : 0,
                    StockRegisterAssignedDate = DateTime.Now,
                    StockRegisterFSNO = financial.Result.FinancialPeriodsFsno,
                    StockRegisterJobID = model.PurchaseReturnVoucherDetailsJobId,
                    StockRegisterBatchCode = x.PurchaseReturnVoucherDetailsBatchCode,
                    StockRegisterMaterialID = x.PurchaseReturnVoucherDetailsMaterialId,
                    StockRegisterQuantity = x.PurchaseReturnVoucherDetailsQty.HasValue
                    ? Convert.ToDecimal(x.PurchaseReturnVoucherDetailsQty)
                    : 0,
                    StockRegisterFCAmount = Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmountNew),
                    StockRegisterRefVoucherNo = vouchers.Result.Value,
                    StockRegisterExpDate = DateTime.Now,
                    StockRegisterLocationID = model.PurchaseReturnVoucherLocationId,
                    StockRegisterRate = Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmountNew),
                    StockRegisterTransType = "Purchase Return",
                    StockRegisterRemarks = x.PurchaseReturnVoucherDetailsRemarks,

                }).ToList();
                _stockRegister.InsertList(stockRegister);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Detaillogs = new AddActivityLogViewModel()
                {
                    Page = "Puchase Return",
                    Section = "Add Puchase Return Stock",
                    Entity = "Puchase Return Stock",

                };
                await _utilityService.AddUserTrackingLog(Detaillogs);
                #endregion
                _stockRegister.SaveChangesAsync();
                foreach (var item in model.PurchaseReturnVoucherDetails)
                {
                    var debit = new AccountsTransactions
                    {

                        AccountsTransactionsFsno = Convert.ToDecimal(financial.Result.FinancialPeriodsFsno),
                        AccountsTransactionsAccNo = supAccount,
                        AccountsTransactionsDebit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcDebit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsTransDate = DateTime.Now,
                        AccountsTransactionsVoucherType = "Purchase Return",
                        AccountsTransactionsParticulars = $@"Purchase Return as {supAccount} ",
                        AccountsTransactionsVoucherNo = vouchers.Result.Value,
                        AccountsTransactionsApprovalDt = DateTime.Now,
                        AccountsTransactionsDescription = model.PurchaseReturnVoucherNarration,
                        AccountsTransactionsUserId = 0,
                        RefNo = "",
                        AccountsTransactionsJobNo = model.PurchaseReturnVoucherDetailsJobId,

                    };
                    await _utilityService.SaveAccountTransaction(debit);
                    var credit = new AccountsTransactions
                    {
                        AccountsTransactionsFsno = Convert.ToDecimal(financial.Result.FinancialPeriodsFsno),
                        AccountsTransactionsAccNo = supAccount,
                        AccountsTransactionsCredit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcCredit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsTransDate = DateTime.Now,
                        AccountsTransactionsVoucherType = "Purchase Return",
                        AccountsTransactionsParticulars = $@"Purchase Return as {supAccount} ",
                        AccountsTransactionsVoucherNo = vouchers.Result.Value,
                        AccountsTransactionsApprovalDt = DateTime.Now,
                        AccountsTransactionsDescription = model.PurchaseReturnVoucherNarration,
                        AccountsTransactionsUserId = 0,
                        RefNo = "",
                        AccountsTransactionsJobNo = model.PurchaseReturnVoucherDetailsJobId,

                    };
                    await _utilityService.SaveAccountTransaction(credit);

                }
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel logs = new AddActivityLogViewModel()
                {
                    Page = "Purchase Return",
                    Section = "Add Purchase Return",
                    Entity = "Purchase Return",

                };
                await _utilityService.AddUserTrackingLog(logs);
                #endregion

                _stockRegister.SaveChangesAsync();
                return "Record Added successfully.";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private async Task<string> EditPurchaseReturn(AddEditPurchaseReturnResponse model)
        {
            try
            {
                Random random = new Random();
                var financial = await _utilityService.GetFinancialPeriods();
                int vouhcerDetailid = random.Next();
                var supplier = _supplierMaster.GetAsQueryable().Where(x => x.SuppliersMasterSupplierId == model.PurchaseReturnVoucherSpId).FirstOrDefault();
                string supAccount = supplier != null ? supplier.SuppliersMasterSupplierReffAcNo : "";
                var vouchers = await _utilityService.AddVoucherNumber("Purchase Return", "PR");
                var orders = _purchaseReturn.GetAsQueryable().FirstOrDefault(x => x.PurchaseReturnVoucherId == model.PurchaseReturnVoucherId);
                orders.PurchaseReturnVoucherDiscount = model.PurchaseReturnVoucherDiscount;
                orders.PurchaseReturnVoucherExcludeVat = model.PurchaseReturnVoucherExcludeVat;
                orders.PurchaseReturnVoucherFcDiscAmount = model.PurchaseReturnVoucherFcDiscAmount;
                orders.PurchaseReturnVoucherGrno = model.PurchaseReturnVoucherGrno;
                orders.PurchaseReturnVoucherRetDate = model.PurchaseReturnVoucherRetDate;
                orders.PurchaseReturnVoucherVoucherDate = model.PurchaseReturnVoucherVoucherDate;
                orders.PurchaseReturnVoucherNarration = model.PurchaseReturnVoucherNarration;
                orders.PurchaseReturnVoucherSupInvNo = model.PurchaseReturnVoucherSupInvNo;
                orders.PurchaseReturnVoucherUserId = 0;
                orders.PurchaseReturnVoucherLocationId = model.PurchaseReturnVoucherLocationId;
                orders.PurchaseReturnVoucherVatNo = model.PurchaseReturnVoucherVatNo;
                orders.PurchaseReturnVoucherVoucherType = model.PurchaseReturnVoucherVoucherType;
                orders.PurchaseReturnVoucherTransportCost = model.PurchaseReturnVoucherTransportCost;
                orders.PurchaseReturnVoucherVatAmount = model.PurchaseReturnVoucherVatAmount;
                orders.PurchaseReturnVoucherSpId = model.PurchaseReturnVoucherSpId;
                orders.PurchaseReturnVoucherVatPercentage = model.PurchaseReturnVoucherVatPercentage;
                orders.PurchaseReturnVoucherVatRoundAmt = model.PurchaseReturnVoucherVatRoundAmt;
                orders.PurchaseReturnVoucherStatus = model.PurchaseReturnVoucherStatus;
                orders.PurchaseReturnVoucherNetAmount = model.PurchaseReturnVoucherNetAmount;
                orders.PurchaseReturnVoucherGrossAmount = model.PurchaseReturnVoucherGrossAmount;
                _purchaseReturn.Update(orders);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel log = new AddActivityLogViewModel()
                {
                    Page = "Purchase Return",
                    Section = "Update Purchase Return",
                    Entity = "Purchase Return",

                };
                await _utilityService.AddUserTrackingLog(log);
                #endregion

                _purchaseReturn.SaveChangesAsync();

                var listOrdersDetails = _purchaseReturnDetail.GetAsQueryable().Where(x => x.PurchaseReturnVoucherDetailsRetId == model.PurchaseReturnVoucherRetId).ToList();
                _purchaseReturnDetail.DeleteList(listOrdersDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Deletelog = new AddActivityLogViewModel()
                {
                    Page = "Purchase Return",
                    Section = "Delete Purchase Return",
                    Entity = "Purchase Return",

                };
                await _utilityService.AddUserTrackingLog(Deletelog);
                #endregion

                _purchaseReturnDetail.SaveChangesAsync();
                List<PurchaseReturnVoucherDetails> orderDetails = new List<PurchaseReturnVoucherDetails>();
                orderDetails.AddRange(model.PurchaseReturnVoucherDetails.Select(x => new PurchaseReturnVoucherDetails
                {
                    PurchaseReturnVoucherDetailsBatchCode = x.PurchaseReturnVoucherDetailsBatchCode,
                    PurchaseReturnVoucherDetailsAmount = x.PurchaseReturnVoucherDetailsAmountNew,
                    PurchaseReturnVoucherDetailsBatch = x.PurchaseReturnVoucherDetailsBatch,
                    PurchaseReturnVoucherDetailsJobId = model.PurchaseReturnVoucherDetailsJobId,
                    PurchaseReturnVoucherDetailsFocQty = x.PurchaseReturnVoucherDetailsFocQty,
                    PurchaseReturnVoucherDetailsMaterialId = x.PurchaseReturnVoucherDetailsMaterialId,

                    PurchaseReturnVoucherDetailsQty = x.PurchaseReturnVoucherDetailsQty + x.PurchaseReturnVoucherDetailsFocQty,
                    PurchaseReturnVoucherDetailsRemarks = x.PurchaseReturnVoucherDetailsRemarks,
                    PurchaseReturnVoucherDetailsRetId = vouchers.Result.Value,
                    PurchaseReturnVoucherDetailsRate = x.PurchaseReturnVoucherDetailsRate,
                    PurchaseReturnVoucherDetailsUnitId = x.PurchaseReturnVoucherDetailsUnitId,
                    PurchaseReturnVoucherDetailsVoucherNo = x.PurchaseReturnVoucherDetailsVoucherNo,
                    PurchaseReturnVoucherDetailsVatAmount = x.PurchaseReturnVoucherDetailsVatAmount,
                    PurchaseReturnVoucherDetailsVatPercetage = x.PurchaseReturnVoucherDetailsVatPercetage,
                    PurchaseReturnVoucherDetailsPurQty = x.PurchaseReturnVoucherDetailsPurQty,
                    PurchaseReturnVoucherDetailsFcAmount = x.PurchaseReturnVoucherDetailsAmountNew,
                }));
                _purchaseReturnDetail.InsertList(orderDetails);
                #region ADD ACTIVITY LOGS
                AddActivityLogViewModel Addlog = new AddActivityLogViewModel()
                {
                    Page = "Purchase Return",
                    Section = "Add Purchase Return Detail",
                    Entity = "Purchase Return Detail",

                };
                await _utilityService.AddUserTrackingLog(Addlog);
                #endregion

                _purchaseReturnDetail.SaveChangesAsync();

                var stockRegister = model.PurchaseReturnVoucherDetails.Select(x => new StockRegister
                {
                    StockRegisterAmount = x.PurchaseReturnVoucherDetailsAmountNew.HasValue
        ? Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmount)
        : 0,
                    StockRegisterAssignedDate = DateTime.Now,
                    StockRegisterFSNO = financial.Result.FinancialPeriodsFsno,
                    StockRegisterJobID = model.PurchaseReturnVoucherDetailsJobId,
                    StockRegisterBatchCode = x.PurchaseReturnVoucherDetailsBatchCode,
                    StockRegisterMaterialID = x.PurchaseReturnVoucherDetailsMaterialId,
                    StockRegisterQuantity = x.PurchaseReturnVoucherDetailsQty.HasValue
        ? Convert.ToDecimal(x.PurchaseReturnVoucherDetailsQty)
        : 0,
                    StockRegisterFCAmount = Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmountNew),
                    StockRegisterRefVoucherNo = vouchers.Result.Value,
                    StockRegisterExpDate = DateTime.Now,
                    StockRegisterLocationID = model.PurchaseReturnVoucherLocationId,
                    StockRegisterRate = Convert.ToDecimal(x.PurchaseReturnVoucherDetailsAmountNew),
                    StockRegisterTransType = "Purchase Return",
                    StockRegisterRemarks = x.PurchaseReturnVoucherDetailsRemarks,

                }).ToList();
                _stockRegister.InsertList(stockRegister);

                _stockRegister.SaveChangesAsync();
                foreach (var item in model.PurchaseReturnVoucherDetails)
                {
                    var debit = new AccountsTransactions
                    {

                        AccountsTransactionsFsno = Convert.ToDecimal(financial.Result.FinancialPeriodsFsno),
                        AccountsTransactionsAccNo = supAccount,
                        AccountsTransactionsDebit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcDebit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsTransDate = DateTime.Now,
                        AccountsTransactionsVoucherType = "Purchase Return",
                        AccountsTransactionsParticulars = $@"Purchase Return as {supAccount} ",
                        AccountsTransactionsVoucherNo = vouchers.Result.Value,
                        AccountsTransactionsApprovalDt = DateTime.Now,
                        AccountsTransactionsDescription = model.PurchaseReturnVoucherNarration,
                        AccountsTransactionsUserId = 0,
                        RefNo = "",
                        AccountsTransactionsJobNo = model.PurchaseReturnVoucherDetailsJobId,

                    };
                    await _utilityService.SaveAccountTransaction(debit);
                    var credit = new AccountsTransactions
                    {
                        AccountsTransactionsFsno = Convert.ToDecimal(financial.Result.FinancialPeriodsFsno),
                        AccountsTransactionsAccNo = supAccount,
                        AccountsTransactionsCredit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcCredit = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsFcAllocBalance = Convert.ToDecimal(item.PurchaseReturnVoucherDetailsAmountNew),
                        AccountsTransactionsTransDate = DateTime.Now,
                        AccountsTransactionsVoucherType = "Purchase Return",
                        AccountsTransactionsParticulars = $@"Purchase Return as {supAccount} ",
                        AccountsTransactionsVoucherNo = vouchers.Result.Value,
                        AccountsTransactionsApprovalDt = DateTime.Now,
                        AccountsTransactionsDescription = model.PurchaseReturnVoucherNarration,
                        AccountsTransactionsUserId = 0,
                        RefNo = "",
                        AccountsTransactionsJobNo = model.PurchaseReturnVoucherDetailsJobId,

                    };
                    await _utilityService.SaveAccountTransaction(credit);

                }
                _stockRegister.SaveChangesAsync();
                return "Record Updated successfully.";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<PurchaseReturn> GetPurchaseReturn()
        {
            var AllPurchaseReturn = _purchaseReturnRepo.GetAll().Where(c => c.PurchaseReturnDelStatus != true).ToList();
            //IEnumerable<CurrencyMaster> _currencyMaster = _currencyMasterRepository.GetAll().ToList();
            //foreach (var purchaseReturn in AllPurchaseReturn)
            //{
            //    try
            //    {
            //        if (purchaseReturn.PurchaseReturnCurrencyId == null) continue;
            //        var currency = _currencyMaster
            //           .FirstOrDefault(c => c.CurrencyMasterCurrencyId == purchaseReturn.PurchaseReturnCurrencyId);
            //        purchaseReturn.CurrencyMasterCurrencyName = currency?.CurrencyMasterCurrencyName ?? "";
            //    }
            //    catch { }

            //}
            return AllPurchaseReturn;
        }
        public PurchaseReturnModel GetSavedPurchaseReturnDetails(string pvno)
        {
            PurchaseReturnModel purchaseReturnModel = new PurchaseReturnModel();

            purchaseReturnModel.purchaseReturn = _purchaseReturnRepo.GetAsQueryable().Where(k => k.PurchaseReturnNo == pvno && k.PurchaseReturnDelStatus != true).FirstOrDefault();
            if (purchaseReturnModel.purchaseReturn != null)
            {
                purchaseReturnModel.accountsTransactions = _accountTransaction.GetAsQueryable().Where(c => c.AccountsTransactionsVoucherNo == pvno && c.AccountsTransactionsVoucherType == VoucherType.PurchaseReturn_TYPE && (c.AccountstransactionsDelStatus != true)).ToList();
                purchaseReturnModel.purchaseReturnDetails = _purchaseReturnDetailsRepo.GetAsQueryable().Where(x => x.PurchaseReturnDetailsNo == pvno && (x.PurchaseReturnDetailsDelStatus == false || x.PurchaseReturnDetailsDelStatus == null)).ToList();

                List<PurchaseVoucherDetails> details = new List<PurchaseVoucherDetails>();
                //purchase voucher quantity calculation
                var pr = _purchaseVoucher.GetAsQueryable().FirstOrDefault(c => c.PurchaseVoucherVoucherNo == purchaseReturnModel.purchaseReturn.PurchaseReturnGrno);
                if (pr != null)
                {
                    details = _purchasevoucherDetail.GetAll().Where(c => c.PurchaseVoucherDetailsVoucherNo == pr.PurchaseVoucherVoucherNo).ToList();
                }
                //purchase return  till quantity 
                foreach (var item in purchaseReturnModel.purchaseReturnDetails)
                {
                    decimal sumReturnedQty = 0;
                    var stockQuery = _stockRegister
                    .GetAsQueryable()
                    .Where(a => a.StockRegisterMaterialID == item.PurchaseReturnDetailsMatId &&
                                a.StockRegisterTransType == "PurchaseReturn" &&
                                a.StockRegisterRefVoucherNo == purchaseReturnModel.purchaseReturn.PurchaseReturnGrno
                                && a.StockRegisterPurchaseID != purchaseReturnModel.purchaseReturn.PurchaseReturnNo);

                    sumReturnedQty = stockQuery.Any()
                                       ? stockQuery.Sum(s => (decimal?)s.StockRegisterSout) ?? 0
                                       : 0;

                    if (sumReturnedQty != null)
                    {
                        item.PurchaseReturnQtyTill = sumReturnedQty;
                    }

                    if (details != null)
                    {
                        var total = details.Where(a => a.PurchaseVoucherDetailsMaterialId == item.PurchaseReturnDetailsMatId).Select(c => c.PurchaseVoucherDetailsQuantity).FirstOrDefault();
                        item.PurchaseOrderPurchaseQTY = total;
                    }
                    // item.PurchaseOrderPurchaseQTY = GetPurchaseQTY(purchaseReturnModel.purchaseReturn.PurchaseReturnGrno);


                }
                //foreach (var purchaseReturnDetail in purchaseReturnModel.purchaseReturnDetails)
                //{
                //    if (_stockRegister.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == purchaseReturnDetail.PurchaseReturnDetailsNo).FirstOrDefault() != null)
                //    {
                //        purchaseReturnDetail.PurchaseReturnNetStock = _stockRegister.GetAsQueryable().Where(c => c.StockRegisterRefVoucherNo == purchaseReturnDetail.PurchaseReturnDetailsNo).FirstOrDefault().StockRegisterSIN;

                //    }                
                //}
                //var purchaseReturnList = _purchaseReturnRepo.GetAll().Where(c => c.PurchaseReturnGrno == purchaseReturnModel.purchaseReturn.PurchaseReturnGrno);
                //if (purchaseReturnList != null)
                //{
                //    decimal? qty = 0.0M;
                //    foreach (var prdetail in purchaseReturnList)
                //    {
                //        var rslt = _purchaseReturnDetailsRepo.GetAll().Where(c => c.PurchaseReturnDetailsNo == prdetail.PurchaseReturnNo).FirstOrDefault();
                //        if (rslt != null)
                //        {
                //            qty += rslt.PurchaseReturnDetailsQuantity ?? 0;
                //        }
                //    }
                //    purchaseReturnModel.purchaseReturnDetails = purchaseReturnModel.purchaseReturnDetails.Select((c) =>
                //      {
                //          c.PurchaseReturnQtyTill = qty;
                //          return c;
                //      }).ToList();
                //}
            }
            return purchaseReturnModel;

        }
        private decimal? GetPurchaseQTY(string pvno)
        {
            try
            {
                var pr = _purchaseVoucher.GetAsQueryable().FirstOrDefault(c => c.PurchaseVoucherVoucherNo == pvno);

                if (pr != null)
                {
                    var total = 0;
                    var qty = _purchasevoucherDetail.GetAll().Where(c => c.PurchaseVoucherDetailsVoucherNo == pr.PurchaseVoucherVoucherNo).ToList();

                    return total;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        public int DeletePurchaseReturn(PurchaseReturn purchaseReturn, List<AccountsTransactions> accountsTransactions, List<PurchaseReturnDetails> purchaseReturnDetails, List<StockRegister> stockRegister)
        {
            int i = 0;
            try
            {
                _purchaseReturnRepo.BeginTransaction();
                clsCommonFunctions.Delete_OldEntry_StockRegister(purchaseReturn.PurchaseReturnNo, VoucherType.PurchaseReturn_TYPE, _stockRegister);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(purchaseReturn.PurchaseReturnNo, VoucherType.PurchaseReturn_TYPE, _accountTransaction);

                purchaseReturn.PurchaseReturnDelStatus = true;
                purchaseReturnDetails = purchaseReturnDetails.Select((k) =>
                {
                    k.PurchaseReturnDetailsDelStatus = true;
                    return k;
                }).ToList();
                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountstransactionsDelStatus = true;
                    return k;
                }).ToList();
                _accountTransaction.UpdateList(accountsTransactions);

                stockRegister = stockRegister.Select((k) =>
                {
                    k.StockRegisterDelStatus = true;
                    return k;
                }).ToList();
                _stockRegister.UpdateList(stockRegister);
                purchaseReturn.PurchaseReturnDetails = purchaseReturnDetails;
                _purchaseReturnRepo.Update(purchaseReturn);
                var vchrnumer = _voucherNumber.GetAsQueryable().Where(k => k.VouchersNumbersVNo == purchaseReturn.PurchaseReturnNo).FirstOrDefault();
                _voucherNumber.Update(vchrnumer);
                _purchaseReturnRepo.TransactionCommit();
                i = 1;
            }
            catch (Exception ex)
            {
                _purchaseReturnRepo.TransactionRollback();
                i = 0;
                throw ex;
            }

            return i;
        }
        public PurchaseReturnModel UpdatePurchaseReturn(PurchaseReturn purchaseReturn, List<AccountsTransactions> accountsTransactions, List<PurchaseReturnDetails> purchaseReturnDetails, List<StockRegister> stockRegister)
        {
            try
            {
                _purchaseReturnRepo.BeginTransaction();

                //=================================
                // clsCommonFunctions.Delete_OldEntry_StockRegister(purchaseReturn.PurchaseReturnGrno, VoucherType.PurchaseReturn_TYPE, _stockRegister);
                clsCommonFunctions.Delete_OldEntry_AccountsTransactions(purchaseReturn.PurchaseReturnNo, VoucherType.PurchaseReturn_TYPE, _accountTransaction);

                clsCommonFunctions.Delete_OldEntryOf_StockRegister(purchaseReturn.PurchaseReturnNo, VoucherType.PurchaseReturn_TYPE, _stockRegister);

                //=================================
                purchaseReturn.PurchaseReturnDetails = purchaseReturnDetails.Select((k) =>
                {

                    k.PurchaseReturnId = purchaseReturn.PurchaseReturnId;
                    k.PurchaseReturnDetailsNo = purchaseReturn.PurchaseReturnNo;
                    return k;
                }).ToList();

                _purchaseReturnRepo.Update(purchaseReturn);

                accountsTransactions = accountsTransactions.Select((k) =>
                {

                    k.AccountsTransactionsTransDate = purchaseReturn.PurchaseReturnDate;
                    k.AccountsTransactionsVoucherNo = purchaseReturn.PurchaseReturnNo;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseReturn_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransaction.InsertList(accountsTransactions);

                //stockRegister = stockRegister.Select((k) =>
                //{
                //    if (k.StockRegisterStoreID == 0)
                //    {
                //        k.StockRegisterVoucherDate = purchaseReturn.PurchaseReturnDate;
                //        k.StockRegisterRefVoucherNo = purchaseReturn.PurchaseReturnGrno;
                //        k.StockRegisterTransType = VoucherType.PurchaseReturn_TYPE;
                //        k.StockRegisterStatus = AccountStatus.Approved;
                //    }
                //    return k;
                //}).ToList();
                //_stockRegister.UpdateList(stockRegister);

                List<StockRegister> sr = new List<StockRegister>();
                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(purchaseReturn.PurchaseReturnCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;


                foreach (var item in purchaseReturnDetails)
                {
                    var converontype = this.getConverionTypebyUnitId(item.PurchaseReturnDetailsMatId, item.PurchaseReturnDetailsUnitId);

                    sr.Add(new StockRegister()
                    {
                        StockRegisterRefVoucherNo = purchaseReturn.PurchaseReturnGrno,
                        StockRegisterPurchaseID = purchaseReturn.PurchaseReturnNo,
                        StockRegisterQuantity = item.PurchaseReturnDetailsQuantity * (converontype) ?? 0,
                        StockRegisterAssignedDate = DateTime.Now,
                        StockRegisterTransType = VoucherType.PurchaseReturn_TYPE,
                        StockRegisterFCAmount = item.PurchaseReturnDetailsQuantity * item.PurchaseReturnDetailsRate * rate,
                        StockRegisterFcRate = purchaseReturn.PurchaseReturnFcRate,
                        StockRegisterStatus = "A",
                        StockRegisterJobID = purchaseReturn.PurchaseReturnJobId,
                        StockRegisterLocationID = purchaseReturn.PurchaseReturnLocationId,
                        StockRegisterFSNO = null,
                        StockRegisterVoucherDate = purchaseReturn.PurchaseReturnDate,
                        StockRegisterSout = item.PurchaseReturnDetailsQuantity * (converontype) ?? 0,
                        StockRegisterRate = item.PurchaseReturnDetailsRate,
                        StockRegisterAmount = item.PurchaseReturnDetailsQuantity * item.PurchaseReturnDetailsRate,
                        StockRegisterMaterialID = item.PurchaseReturnDetailsMatId,

                    });
                }
                _stockRegister.InsertList(sr);


                _purchaseReturnRepo.TransactionCommit();

            }
            catch (Exception ex)
            {
                _purchaseReturnRepo.TransactionRollback();
                throw ex;
            }

            return this.GetSavedPurchaseReturnDetails(purchaseReturn.PurchaseReturnNo);
        }
        public VouchersNumbers GetVouchersNumbers(string pvno)
        {
            try
            {
                VouchersNumbers vouchersNumbers = _voucherNumber.GetAsQueryable().Where(k => k.VouchersNumbersVNo == pvno).SingleOrDefault();
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
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
        public PurchaseReturnModel InsertPurchaseReturn(PurchaseReturn purchaseReturn,
                                                        List<AccountsTransactions> accountsTransactions,
                                                        List<PurchaseReturnDetails> purchaseReturnDetails,
                                                        List<StockRegister> stockRegister)
        {
            try
            {
                _purchaseReturnRepo.BeginTransaction();
                string purchaseReturnNumber = this.GenerateVoucherNo(purchaseReturn.PurchaseReturnDate.Date).VouchersNumbersVNo;
                purchaseReturn.PurchaseReturnNo = purchaseReturnNumber;


                int maxcount = 0;
                maxcount = Convert.ToInt32(
                    _purchaseReturnRepo.GetAsQueryable()
                    .DefaultIfEmpty().Max(o => o == null ? 0 : o.PurchaseReturnId) + 1);

                purchaseReturn.PurchaseReturnId = maxcount;

                purchaseReturnDetails = purchaseReturnDetails.Select((x) =>
                {
                    x.PurchaseReturnId = maxcount;
                    x.PurchaseReturnDetailsNo = purchaseReturnNumber;
                    x.PurchaseReturnDetailsId = 0;
                    return x;
                }).ToList();
                //_purchaseReturnDetailsRepo.InsertList(purchaseReturnDetails);

                accountsTransactions = accountsTransactions.Select((k) =>
                {
                    k.AccountsTransactionsVoucherNo = purchaseReturnNumber;
                    k.AccountsTransactionsVoucherType = VoucherType.PurchaseReturn_TYPE;
                    k.AccountsTransactionsStatus = AccountStatus.Approved;
                    return k;
                }).ToList();
                _accountTransaction.InsertList(accountsTransactions);
                var currencyRate = clsCommonFunctions.getConverionCurrencyRate(purchaseReturn.PurchaseReturnCurrencyId, _currencyMasterRepository);
                var rate = (decimal)currencyRate;

                List<StockRegister> sr = new List<StockRegister>();

                foreach (var item in purchaseReturnDetails)
                {
                    var converontype = this.getConverionTypebyUnitId(item.PurchaseReturnDetailsMatId, item.PurchaseReturnDetailsUnitId);

                    sr.Add(new StockRegister()
                    {
                        StockRegisterRefVoucherNo = purchaseReturn.PurchaseReturnGrno,
                        StockRegisterPurchaseID = purchaseReturn.PurchaseReturnNo,
                        StockRegisterQuantity = item.PurchaseReturnDetailsQuantity * (converontype) ?? 0,
                        StockRegisterAssignedDate = DateTime.Now,
                        StockRegisterTransType = VoucherType.PurchaseReturn_TYPE,
                        StockRegisterFcRate = purchaseReturn.PurchaseReturnFcRate,
                        StockRegisterStatus = "A",
                        StockRegisterJobID = purchaseReturn.PurchaseReturnJobId,
                        StockRegisterLocationID = purchaseReturn.PurchaseReturnLocationId,
                        StockRegisterFSNO = null,
                        StockRegisterVoucherDate = purchaseReturn.PurchaseReturnDate,
                        StockRegisterSout = item.PurchaseReturnDetailsQuantity * (converontype) ?? 0,
                        StockRegisterRate = item.PurchaseReturnDetailsRate,
                        StockRegisterAmount = item.PurchaseReturnDetailsQuantity * item.PurchaseReturnDetailsRate,
                        StockRegisterFCAmount = item.PurchaseReturnDetailsQuantity * item.PurchaseReturnDetailsRate * rate,
                        StockRegisterMaterialID = item.PurchaseReturnDetailsMatId,

                    });
                }
                _stockRegister.InsertList(sr);


                purchaseReturn.PurchaseReturnDetails = purchaseReturnDetails;


                _purchaseReturnRepo.Insert(purchaseReturn);
                _purchaseReturnRepo.TransactionCommit();
                return this.GetSavedPurchaseReturnDetails(purchaseReturn.PurchaseReturnNo);

            }
            catch (Exception ex)
            {
                _purchaseReturnRepo.TransactionRollback();
                throw ex;
            }
        }

        public VouchersNumbers GenerateVoucherNo(DateTime? VoucherGenDate)
        {
            try
            {
                var prefix = this._programsettingsRepository.GetAsQueryable().Where(k => k.ProgramSettingsKeyValue == Prefix.PurchaseReturn_Prefix).FirstOrDefault().ProgramSettingsTextValue;
                int vnoMaxVal = Convert.ToInt32(_voucherNumber.GetAsQueryable()
                                                        .Where(x => x.VouchersNumbersVNoNu > 0 && x.VouchersNumbersVType == VoucherType.PurchaseReturn_TYPE)
                                                        .DefaultIfEmpty()
                                                        .Max(o => o == null ? 0 : o.VouchersNumbersVNoNu)) + 1;


                //var prefix = "CN";
                //int vnoMaxVal = 1;


                VouchersNumbers vouchersNumbers = new VouchersNumbers
                {
                    VouchersNumbersVNo = prefix + vnoMaxVal,
                    VouchersNumbersVNoNu = vnoMaxVal,
                    VouchersNumbersVType = VoucherType.PurchaseReturn_TYPE,
                    VouchersNumbersFsno = 1,
                    VouchersNumbersStatus = AccountStatus.Pending,
                    VouchersNumbersVoucherDate = VoucherGenDate

                };
                _voucherNumber.Insert(vouchersNumbers);
                return vouchersNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public decimal? CurrencyConversion(PurchaseReturn pr)
        {
            decimal? convertedValiue = 0.0M;
            var currencyMaster = _currencyMasterRepository.GetAsQueryable().Where(c => c.CurrencyMasterCurrencyId == pr.PurchaseReturnCurrencyId).FirstOrDefault();
            if (currencyMaster == null)
            {
                return null;
            }
            foreach (var prDetails in pr.PurchaseReturnDetails)
            {
                convertedValiue = prDetails.PurchaseReturnDetailsQuantity * Convert.ToDecimal(currencyMaster.CurrencyMasterCurrencyRate);
            }
            return convertedValiue;
        }
        public decimal? GetCurrencyRate(PurchaseReturn pr)
        {
            decimal? currencyRate = 0.0M;
            var currencyMaster = _currencyMasterRepository.GetAsQueryable().Where(c => c.CurrencyMasterCurrencyId == pr.PurchaseReturnCurrencyId).FirstOrDefault();
            if (currencyMaster == null)
            {
                return null;
            }

            return Convert.ToDecimal(currencyMaster.CurrencyMasterCurrencyRate);
        }
    }
}
