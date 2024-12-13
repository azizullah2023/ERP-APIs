using Inspire.Erp.Application.Account.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Procurement.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Procurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NPOI.SS.UserModel;

namespace Inspire.Erp.Web.Controllers.Procurement
{
    [Route("api/PurchaseOrder")]
    [Produces("application/json")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private InspireErpDBContext _context;
        private IPurchaseOrderService _purchaseOrderService; private IRepository<PermissionApprovalDetail> _permissionApprovalDetailRepo;
        private IRepository<ItemMaster> itemrepository; private IRepository<UnitMaster> unitrepository; private IRepository<PurOrderRegister> _purOrderRegisterRepository;
        private IRepository<PurchaseOrder> _purchaseOrderRepository; private IRepository<PurchaseOrderDetails> _purchaseOrderDetailsRepository;
        private IRepository<JobMaster> jobrepository; private readonly IRepository<SuppliersMaster> supplierrepository;
        private IRepository<LocationMaster> locationrepository; private IRepository<CurrencyMaster> currencyrepository;
        private IRepository<ViewAccountsTransactionsVoucherType> _viewAccountsTransactionsVoucherTypeRepository;

        private readonly IMapper _mapper;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IMapper mapper, IRepository<ItemMaster> _itemrepository, IRepository<UnitMaster> _unitrepository,
            IRepository<PurchaseOrder> purchaseOrderRepository, IRepository<PurOrderRegister> purOrderRegisterRepository, IRepository<PurchaseOrderDetails> purchaseOrderDetailsRepository,
            IRepository<JobMaster> _jobrepository, IRepository<LocationMaster> _locationrepository, InspireErpDBContext context
            , IRepository<CurrencyMaster> _currencyrepository, IRepository<SuppliersMaster> _supplierrepository, IRepository<PermissionApprovalDetail> permissionApprovalDetailRepo,
            IRepository<ViewAccountsTransactionsVoucherType> viewAccountsTransactionsVoucherTypeRepository)
        {
            _purchaseOrderService = purchaseOrderService; _context = context; _permissionApprovalDetailRepo = permissionApprovalDetailRepo;
            _mapper = mapper; supplierrepository = _supplierrepository; _viewAccountsTransactionsVoucherTypeRepository = viewAccountsTransactionsVoucherTypeRepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; _purchaseOrderRepository = purchaseOrderRepository; _purOrderRegisterRepository = purOrderRegisterRepository;
            _purchaseOrderDetailsRepository = purchaseOrderDetailsRepository; jobrepository = _jobrepository; locationrepository = _locationrepository; currencyrepository = _currencyrepository;
        }

        //[HttpGet]
        //[Route("PurchaseOrder_GetReportPurchaseOrder")]
        //public ApiResponse<List<ReportPurchaseOrder>> PurchaseOrder_GetReportPurchaseOrder()
        //{
        //    ApiResponse<List<ReportPurchaseOrder>> apiResponse = new ApiResponse<List<ReportPurchaseOrder>>
        //    {
        //        Valid = true,
        //        Result = _mapper.Map<List<ReportPurchaseOrder>>(_purchaseOrderService.PurchaseOrder_GetReportPurchaseOrder()),
        //        Message = ""
        //    };
        //    return apiResponse;
        //}
        [HttpGet]
        [Route("GetAllUserFile")]

        public List<UserFile> GetAllUserFile()
        {
            return _mapper.Map<List<UserFile>>(_purchaseOrderService.GetAllUserFile());
        }

        [HttpPost]
        [Route("InsertPurchaseOrder")]
        public ApiResponse<PurchaseOrderViewModel> InsertPurchaseOrder([FromBody] PurchaseOrderViewModel voucherCompositeView)
        {

            ApiResponse<PurchaseOrderViewModel> apiResponse = new ApiResponse<PurchaseOrderViewModel>();
            var param1 = _mapper.Map<PurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseOrderDetails>>(voucherCompositeView.PurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _purchaseOrderService.InsertPurchaseOrder(param1, param3, param2
                );

            PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel();
            purchaseOrderViewModel = _mapper.Map<PurchaseOrderViewModel>(xs.purchaseOrder);

            purchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
            apiResponse = new ApiResponse<PurchaseOrderViewModel>
            {
                Valid = true,
                Result = purchaseOrderViewModel,
                Message = PurchaseOrderMessage.SaveVoucher
            };

            return apiResponse;
        }

        [HttpPost]
        [Route("UpdatePurchaseOrder")]
        public ApiResponse<PurchaseOrderViewModel> UpdatePurchaseOrder([FromBody] PurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseOrderDetails>>(voucherCompositeView.PurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _purchaseOrderService.UpdatePurchaseOrder(param1, param3, param2
                );

            PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel();

            purchaseOrderViewModel = _mapper.Map<PurchaseOrderViewModel>(xs.purchaseOrder);
            purchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);

            ApiResponse<PurchaseOrderViewModel> apiResponse = new ApiResponse<PurchaseOrderViewModel>
            {
                Valid = true,
                Result = purchaseOrderViewModel,
                Message = PurchaseOrderMessage.UpdateVoucher
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("DeletePurchaseOrder")]
        public ApiResponse<int> DeletePurchaseOrder([FromBody] PurchaseOrderViewModel voucherCompositeView)
        {
            var param1 = _mapper.Map<PurchaseOrder>(voucherCompositeView);
            var param2 = _mapper.Map<List<PurchaseOrderDetails>>(voucherCompositeView.PurchaseOrderDetails);
            var param3 = _mapper.Map<List<AccountsTransactions>>(voucherCompositeView.AccountsTransactions);
            var xs = _purchaseOrderService.DeletePurchaseOrder(param1, param3, param2
                );
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = 0,
                Message = PurchaseOrderMessage.DeleteVoucher
            };

            return apiResponse;
        }


        [HttpGet]
        [Route("GetAllAccountTransaction")]
        public ApiResponse<List<AccountsTransactions>> GetAllAccountTransaction()
        {
            ApiResponse<List<AccountsTransactions>> apiResponse = new ApiResponse<List<AccountsTransactions>>
            {
                Valid = true,
                Result = _mapper.Map<List<AccountsTransactions>>(_purchaseOrderService.GetAllTransaction()),
                Message = ""
            };
            return apiResponse;

        }

        [HttpGet]
        [Route("GetPurchaseOrder")]
        public ApiResponse<List<PurchaseOrder>> GetAllPurchaseOrder()
        {
            ApiResponse<List<PurchaseOrder>> apiResponse = new ApiResponse<List<PurchaseOrder>>
            {
                Valid = true,
                Result = _purchaseOrderService.GetPurchaseOrder().ToList(),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetSavedPurchaseOrderDetails/{id}")]
        public ApiResponse<PurchaseOrderViewModel> GetSavedPurchaseOrderDetails(string id)
        {
            PurchaseOrderModel purchaseOrder = _purchaseOrderService.GetSavedPurchaseOrderDetails(id);
            if (purchaseOrder != null)
            {
                PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel
                {
                    PurchaseOrderId = purchaseOrder.purchaseOrder.PurchaseOrderId,
                    PurchaseOrderNo = purchaseOrder.purchaseOrder.PurchaseOrderNo,
                    PurchaseOrderDate = purchaseOrder.purchaseOrder.PurchaseOrderDate,
                    PurchaseOrderType = purchaseOrder.purchaseOrder.PurchaseOrderType,
                    PurchaseOrderPartyId = purchaseOrder.purchaseOrder.PurchaseOrderPartyId,
                    PurchaseOrderPartyName = purchaseOrder.purchaseOrder.PurchaseOrderPartyName,
                    PurchaseOrderPartyAddress = purchaseOrder.purchaseOrder.PurchaseOrderPartyAddress,
                    PurchaseOrderPartyVatNo = purchaseOrder.purchaseOrder.PurchaseOrderPartyVatNo,
                    PurchaseOrderSupInvNo = purchaseOrder.purchaseOrder.PurchaseOrderSupInvNo,
                    PurchaseOrderRefNo = purchaseOrder.purchaseOrder.PurchaseOrderRefNo,
                    PurchaseOrderDescription = purchaseOrder.purchaseOrder.PurchaseOrderDescription,
                    PurchaseOrderGrno = purchaseOrder.purchaseOrder.PurchaseOrderGrno,
                    PurchaseOrderGrdate = purchaseOrder.purchaseOrder.PurchaseOrderGrdate,
                    PurchaseOrderLpono = purchaseOrder.purchaseOrder.PurchaseOrderLpono,
                    PurchaseOrderLpodate = purchaseOrder.purchaseOrder.PurchaseOrderLpodate,
                    PurchaseOrderQtnNo = purchaseOrder.purchaseOrder.PurchaseOrderQtnNo,
                    PurchaseOrderQtnDate = purchaseOrder.purchaseOrder.PurchaseOrderQtnDate,
                    PurchaseOrderExcludeVat = purchaseOrder.purchaseOrder.PurchaseOrderExcludeVat,
                    PurchaseOrderPono = purchaseOrder.purchaseOrder.PurchaseOrderPono,
                    PurchaseOrderBatchCode = purchaseOrder.purchaseOrder.PurchaseOrderBatchCode,
                    PurchaseOrderDayBookNo = purchaseOrder.purchaseOrder.PurchaseOrderDayBookNo,
                    PurchaseOrderLocationId = purchaseOrder.purchaseOrder.PurchaseOrderLocationId,
                    PurchaseOrderUserId = purchaseOrder.purchaseOrder.PurchaseOrderUserId,
                    PurchaseOrderCurrencyId = purchaseOrder.purchaseOrder.PurchaseOrderCurrencyId,
                    PurchaseOrderCompanyId = purchaseOrder.purchaseOrder.PurchaseOrderCompanyId,
                    PurchaseOrderJobId = purchaseOrder.purchaseOrder.PurchaseOrderJobId,
                    PurchaseOrderFsno = purchaseOrder.purchaseOrder.PurchaseOrderFsno,
                    PurchaseOrderFcRate = purchaseOrder.purchaseOrder.PurchaseOrderFcRate,
                    PurchaseOrderStatus = purchaseOrder.purchaseOrder.PurchaseOrderStatus,
                    PurchaseOrderTotalGrossAmount = purchaseOrder.purchaseOrder.PurchaseOrderTotalGrossAmount,
                    PurchaseOrderTotalItemDisAmount = purchaseOrder.purchaseOrder.PurchaseOrderTotalItemDisAmount,
                    PurchaseOrderTotalActualAmount = purchaseOrder.purchaseOrder.PurchaseOrderTotalActualAmount,
                    PurchaseOrderTotalDisPer = purchaseOrder.purchaseOrder.PurchaseOrderTotalDisPer,
                    PurchaseOrderTotalDisAmount = purchaseOrder.purchaseOrder.PurchaseOrderTotalDisAmount,
                    PurchaseOrderVatAmt = purchaseOrder.purchaseOrder.PurchaseOrderVatAmt,
                    PurchaseOrderVatPer = purchaseOrder.purchaseOrder.PurchaseOrderVatPer,
                    PurchaseOrderVatRoundSign = purchaseOrder.purchaseOrder.PurchaseOrderVatRoundSign,
                    PurchaseOrderVatRountAmt = purchaseOrder.purchaseOrder.PurchaseOrderVatRountAmt,
                    PurchaseOrderNetDisAmount = purchaseOrder.purchaseOrder.PurchaseOrderNetDisAmount,
                    PurchaseOrderNetAmount = purchaseOrder.purchaseOrder.PurchaseOrderNetAmount,
                    PurchaseOrderTransportCost = purchaseOrder.purchaseOrder.PurchaseOrderTransportCost,
                    PurchaseOrderHandlingcharges = purchaseOrder.purchaseOrder.PurchaseOrderHandlingcharges,
                    PurchaseOrderIssueId = purchaseOrder.purchaseOrder.PurchaseOrderIssueId,
                    PurchaseOrderJobDirectPur = purchaseOrder.purchaseOrder.PurchaseOrderJobDirectPur,
                    PurchaseOrderDelStatus = purchaseOrder.purchaseOrder.PurchaseOrderDelStatus,
                    PurchaseOrderIndentNo = purchaseOrder.purchaseOrder.PurchaseOrderIndentNo,
                    PurchaseOrderContPerson = purchaseOrder.purchaseOrder.PurchaseOrderContPerson,
                    PurchaseOrderPreparedBy = purchaseOrder.purchaseOrder.PurchaseOrderPreparedBy,
                    PurchaseOrderRecommendedBy = purchaseOrder.purchaseOrder.PurchaseOrderRecommendedBy,
                    PurchaseOrderApproveEnable = purchaseOrder.purchaseOrder.PurchaseOrderApproveEnable,
                    PurchaseOrderApproveDate = purchaseOrder.purchaseOrder.PurchaseOrderApproveDate,
                    PurchaseOrderApproveStatus = purchaseOrder.purchaseOrder.PurchaseOrderApproveStatus,
                    PurchaseOrderApproveUserId = purchaseOrder.purchaseOrder.PurchaseOrderApproveUserId,
                    PurchaseOrderHeader = purchaseOrder.purchaseOrder.PurchaseOrderHeader,
                    PurchaseOrderDelivery = purchaseOrder.purchaseOrder.PurchaseOrderDelivery,
                    PurchaseOrderNotes = purchaseOrder.purchaseOrder.PurchaseOrderNotes,
                    PurchaseOrderFooter = purchaseOrder.purchaseOrder.PurchaseOrderFooter,
                    PurchaseOrderPaymentTerms = purchaseOrder.purchaseOrder.PurchaseOrderPaymentTerms,
                    PurchaseOrderSubject = purchaseOrder.purchaseOrder.PurchaseOrderSubject,
                    PurchaseOrderContent = purchaseOrder.purchaseOrder.PurchaseOrderContent,
                    PurchaseOrderRemarks1 = purchaseOrder.purchaseOrder.PurchaseOrderRemarks1,
                    PurchaseOrderRemarks2 = purchaseOrder.purchaseOrder.PurchaseOrderRemarks2,
                    PurchaseOrderRemarks3 = purchaseOrder.purchaseOrder.PurchaseOrderRemarks3,
                    PurchaseOrderDelivFromDate = purchaseOrder.purchaseOrder.PurchaseOrderDelivFromDate,
                    PurchaseOrderDelivToDate = purchaseOrder.purchaseOrder.PurchaseOrderDelivToDate,
                    PurchaseOrderDelivStatus = purchaseOrder.purchaseOrder.PurchaseOrderDelivStatus,
                    PurchaseOrderDelivPlace = purchaseOrder.purchaseOrder.PurchaseOrderDelivPlace,
                };
                purchaseOrderViewModel.PurchaseOrderDetails = _mapper.Map<List<PurchaseOrderDetailsViewModel>>(purchaseOrder.purchaseOrderDetails);
                purchaseOrderViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(purchaseOrder.accountsTransactions);
                ApiResponse<PurchaseOrderViewModel> apiResponse = new ApiResponse<PurchaseOrderViewModel>
                {
                    Valid = true,
                    Result = purchaseOrderViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;

        }

        [HttpGet]
        [Route("CheckVnoExist/{id}")]
        public ApiResponse<bool> CheckVnoExist(string id)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>
            {
                Valid = true,
                Result = true,
                Message = PurchaseOrderMessage.VoucherNumberExist
            };
            var x = _purchaseOrderService.GetVouchersNumbers(id);
            if (x == null)
            {
                apiResponse.Result = false;
                apiResponse.Message = "";
            }

            return apiResponse;
        }

        [HttpGet]
        [Route("GetPODetailsForGRN")]
        public IActionResult GetPODetailsForGRN()
        {
            try
            {
                var item = _purchaseOrderService.GetPODetailsForGRN();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetGRMPOs/{supplierId}")]
        public async Task<IActionResult> GetGRMPOs(int? supplierId)
        {
            try
            {
                var item = await _purchaseOrderService.GetGRMPOs(supplierId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpGet]
        [Route("GetAllPOForGRN/{id}")]
        public ResponseInfo GetAllPOForGRN(decimal id)
        {

            var objResponse = new ResponseInfo();

            var materialList = itemrepository.GetAsQueryable().Where(a => a.ItemMasterDelStatus != true).Select(c => new
            {
                c.ItemMasterItemName,
                c.ItemMasterItemId,
                c.ItemMasterLastPurchasePrice
            });
            var unitList = unitrepository.GetAsQueryable().Where(a => a.UnitMasterUnitDelStatus != true).Select(c => new
            {
                c.UnitMasterUnitId,
                UnitMasterUnitShortName = c.UnitMasterUnitShortName.Trim(),
                c.UnitMasterUnitFullName
            }).ToList();
            var tracking = _purOrderRegisterRepository.GetAsQueryable().Where(a => a.PurOrderRegisterDelStatus != true).ToList();
            var detailList = _purchaseOrderDetailsRepository.GetAsQueryable().ToList();
            var details = _purchaseOrderRepository.GetAsQueryable().Where(a => a.PurchaseOrderDelStatus != true && a.PurchaseOrderPartyId == id).Select(c => new
            {
                c.PurchaseOrderId,
                c.PurchaseOrderNo,
                c.PurchaseOrderDate
            }).ToList();
            var purchaseOrderNos = details.Select(d => d.PurchaseOrderNo).ToList();

            // Filtering detailList based on PurchaseOrderNos
            var data = (from d in detailList
                        join c in details on d.PurchaseOrderId equals c.PurchaseOrderId
                        where purchaseOrderNos.Contains(d.PurchaseOrderDetailsNo)
                        select new
                        {
                            PODID = d.PurchaseOrderDetailsId,
                            POID = d.PurchaseOrderId,
                            date = c.PurchaseOrderDate.Date,
                            PONO = d.PurchaseOrderDetailsNo,
                            itemId = d.PurchaseOrderDetailsMatId,
                            itemname = materialList
                        .Where(m => m.ItemMasterItemId == (long?)d.PurchaseOrderDetailsMatId)
                        .Select(c => c.ItemMasterItemName ?? "")
                        .FirstOrDefault() ?? "",
                            unitId = d.PurchaseOrderDetailsUnitId ?? 0,
                            unit = unitList
                        .Where(u => u.UnitMasterUnitId == (int?)d.PurchaseOrderDetailsUnitId)
                        .Select(c => c.UnitMasterUnitFullName.Trim() ?? "")
                        .FirstOrDefault() ?? "",
                            price = d.PurchaseOrderDetailsRate ?? 0,
                            poQty = d.PurchaseOrderDetailsQuantity ?? 0,
                            QtyIUssued = tracking.Where(a => a.PurOrderRegisterOrderNo == d.PurchaseOrderDetailsNo)
                    .Sum(c => c.PurOrderRegisterQtyIssued ?? 0),
                        })
                .ToList();

            objResponse.ResultSet = new
            {
                result = data
            };
            return objResponse;

        }

        [HttpGet]
        [Route("PurchaseLoadDropdown")]
        public ResponseInfo PurchaseLoadDropdown()
        {
            var objresponse = new ResponseInfo();

            var jobMasters = jobrepository.GetAsQueryable().AsNoTracking().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                JobMasterJobName = c.JobMasterJobName.Trim(),
            }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().AsNoTracking().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                LocationMasterLocationName = c.LocationMasterLocationName.Trim(),
            }).ToList();

            var currencyMasters = currencyrepository.GetAsQueryable().AsNoTracking().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                CurrencyMasterCurrencyName = c.CurrencyMasterCurrencyName.Trim(),
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var SuppliersMaster = supplierrepository.GetAsQueryable().AsNoTracking().Where(a => a.SuppliersMasterSupplierDelStatus != true).Select(c => new
            {
                c.SuppliersMasterSupplierId,
                c.SuppliersMasterSupplierName,
                c.SuppliersMasterSupplierVatNo
            }).ToList();
            var itemMaster = itemrepository.GetAsQueryable().AsNoTracking().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                    && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
                    {
                        k.ItemMasterItemId,
                        k.ItemMasterPartNo,
                        k.ItemMasterAccountNo,
                        ItemMasterItemName = k.ItemMasterItemName.Trim()
                    }).ToList();
            var unitMasters = unitrepository.GetAsQueryable().AsNoTracking().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                UnitMasterUnitFullName = x.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim()
            }).ToList();
            var itemMasterGroup = itemrepository.GetAsQueryable().AsNoTracking().Where(k => k.ItemMasterAccountNo == 0 && (k.ItemMasterDelStatus != true)
                && k.ItemMasterItemType == ItemMasterStatus.Group).Select(k => new
                {
                    k.ItemMasterItemId,
                    k.ItemMasterItemName,
                }).ToList();

            var vouchers = _viewAccountsTransactionsVoucherTypeRepository.GetAsQueryable().AsNoTracking().ToList();
            objresponse.ResultSet = new
            {
                jobMasters = jobMasters,
                LocationMaster = LocationMaster,
                currencyMasters = currencyMasters,
                itemMaster = itemMaster,
                unitMasters = unitMasters,
                itemMasterGroup = itemMasterGroup,
                SuppliersMaster = SuppliersMaster,
                vouchers = vouchers
            };
            return objresponse;
        }

        [HttpGet]
        [Route("POApprovalDropdown")]
        public ResponseInfo POApprovalDropdown()
        {
            var objresponse = new ResponseInfo();

            var data = _context.tbl_approvalforms.AsNoTracking().ToList();

            var userList = _context.UserFile.AsNoTracking().Where(a => a.Deleted != true).Select(x => new
            {
                x.UserId,
                x.UserName,
            }).ToList();
            objresponse.ResultSet = new
            {
                result = data,
                userList = userList
            };
            return objresponse;
        }

        [HttpPost]
        [Route("POApprovalList")]
        public ResponseInfo POApprovalList(approlvalmodel model)
        {
            var objresponse = new ResponseInfo();

            var voucherTypes = model.type ?? new List<string>();
            IEnumerable<PermissionApprovalDetail> data;

            var rightsQuery = from approval in _context.tbl_approvalforms.AsNoTracking()
                              join permission in _context.Permissionapproval.AsNoTracking()
                                  on approval.id equals permission.ApprovalformId
                              where permission.UserId == model.userId
                              && (voucherTypes == null || !voucherTypes.Any() || voucherTypes.Contains(approval.Voucher_Type))
                              select new
                              {
                                  approval.Voucher_Type,
                                  permission.LevelId,
                                  permission.ApprovalformId,
                                  permission.UserId
                              };

            var rights = rightsQuery.ToList();


            if (model.isDate)
            {
                data = _context.PermissionApprovalDetail.AsNoTracking().AsEnumerable()
               .Where(a =>
               a.VoucherDate >= model.fromDate &&
               a.VoucherDate <= model.toDate &&
              (!rights.Any() || rights.Any(r => r.LevelId == a.LevelId && r.Voucher_Type == a.VoucherType)));
            }
            else
            {
                data = _context.PermissionApprovalDetail.AsNoTracking().AsEnumerable()
                   .Where(a =>
                   (!rights.Any() || rights.Any(r => r.LevelId == a.LevelId && r.Voucher_Type == a.VoucherType)));
            }

            var lowestPendingLevels = (from detail in _context.PermissionApprovalDetail.AsNoTracking()
                                       where detail.Status == "Pending"
                                       group detail by new { detail.VoucherId, detail.VoucherType } into grouped
                                       select new
                                       {
                                           grouped.Key.VoucherId,
                                           grouped.Key.VoucherType,
                                           LowestPendingLevel = grouped.Min(d => d.LevelId)
                                       }).ToList();

            var pendingRecords = data
                .Where(a => a.Status == "Pending" &&
                 lowestPendingLevels.Any(l => l.VoucherId == a.VoucherId && l.VoucherType == a.VoucherType && l.LowestPendingLevel == a.LevelId))
                 .ToList();

            var approvedRecords = data
                .Where(a => a.Status == "Approved")
                .ToList();

            var combinedData = pendingRecords.Concat(approvedRecords).ToList();

            if (!string.IsNullOrEmpty(model.status) && model.status != "All")
            {
                combinedData = (List<PermissionApprovalDetail>)combinedData.Where(a => a.Status == model.status);
            }

            objresponse.ResultSet = new
            {
                result = combinedData,
            };
            return objresponse;
        }

        [HttpPost]
        [Route("POApprovedList")]
        public ResponseInfo POApprovedList(List<PermissionApprovalDetail>? model)
        {
            var objresponse = new ResponseInfo();

            if (model == null || model.Count == 0)
            {
                objresponse.ErrorMessage = "No data provided for approval.";
                return objresponse;
            }
            try
            {
                _permissionApprovalDetailRepo.BeginTransaction();
                var currentDateTime = DateTime.Now;
                foreach (var voucherType in model)
                {
                    voucherType.ModifiedDate = currentDateTime;
                    voucherType.Status = "Approved";
                    _permissionApprovalDetailRepo.Update(voucherType);
                }
                _permissionApprovalDetailRepo.TransactionCommit();
                objresponse.Message = "Approved Successfully";
            }
            catch (Exception ex)
            {
                _permissionApprovalDetailRepo.TransactionRollback();
                objresponse.ErrorMessage = $"An error occurred while approving: {ex.Message}";
            }
            return objresponse;
        }
    }
}
