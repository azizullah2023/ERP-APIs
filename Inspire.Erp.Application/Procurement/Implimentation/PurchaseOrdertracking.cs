using Inspire.Erp.Application.Procurement.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inspire.Erp.Application.Procurement.Implimentation
{
    public class PurchaseOrdertracking : IPurchaseOrdertracking
    {
        private IRepository<PurchaseOrder> _purchaseOrder;
        private IRepository<PurchaseOrderDetails> _poDetails;
        private IRepository<Suppliers> _supliers;
        private IRepository<JobMaster> _jobMaster;
        private IRepository<ItemMaster> _itemMaster;
        private IRepository<CurrencyMaster> _curreny;
        private IRepository<StockRegister> _stock;
        private IRepository<PurOrderRegister> _poRegister;
        private IRepository<UserTracking> _UserTracking;
        private IRepository<UserFile> _UserFile;
        private IRepository<SuppliersMaster> _supliersMaster;
        public PurchaseOrdertracking(IRepository<UserFile> UserFile, IRepository<UserTracking> UserTracking, IRepository<PurchaseOrder> purchaseOrder, IRepository<PurchaseOrderDetails> poDetails, IRepository<Suppliers> supliers, IRepository<JobMaster> jobMaster, IRepository<ItemMaster> itemMaster, IRepository<CurrencyMaster> curreny, IRepository<StockRegister> stock, IRepository<PurOrderRegister> poRegister, IRepository<SuppliersMaster> supliersMaster)
        {
            _purchaseOrder = purchaseOrder;
            _poDetails = poDetails;
            _supliers = supliers;
            _jobMaster = jobMaster;
            _itemMaster = itemMaster;
            _curreny = curreny;
            _stock = stock;
            _poRegister = poRegister;
            _UserFile = UserFile;
            _UserTracking = UserTracking;
            _supliersMaster = supliersMaster;
        }

        public async Task<Response<List<POTrackingDetails>>> GetPurchaseOrderTrackingDetails(ReportFilter model)
        {
            List<POTrackingDetails> data = new List<POTrackingDetails>();            
            try
            {
                // Suleman 8/3/2024 changes filter 
                string filteredValue = string.Empty;

                if (model.IsDateCheck)
                {
                    var result = await (from po in _purchaseOrder.GetAsQueryable()
                                        join sp in _supliersMaster.GetAsQueryable() on (int?)po.PurchaseOrderPartyId equals sp.SuppliersMasterSupplierId into spGroup
                                        from sp in spGroup.DefaultIfEmpty()
                                        join pod in _poDetails.GetAsQueryable() on po.PurchaseOrderId equals pod.PurchaseOrderId into podGroup
                                        from pod in podGroup.DefaultIfEmpty()
                                        join i in _itemMaster.GetAsQueryable() on (long)pod.PurchaseOrderDetailsMatId equals i.ItemMasterItemId into iGroup
                                        from i in iGroup.DefaultIfEmpty()
                                        join cm in _curreny.GetAsQueryable() on po.PurchaseOrderCurrencyId equals cm.CurrencyMasterCurrencyId into cmGroup
                                        from cm in cmGroup.DefaultIfEmpty()
                                        join jm in _jobMaster.GetAsQueryable() on po.PurchaseOrderJobId equals jm.JobMasterJobId into jmGroup
                                        from jm in jmGroup.DefaultIfEmpty()

                                        where (model.SupplierId == 0 || po.PurchaseOrderPartyId == model.SupplierId) &&
                                        (model.JobId == 0 || po.PurchaseOrderJobId == model.JobId) &&
                                        (model.LocationId == 0 || po.PurchaseOrderLocationId == model.LocationId) &&
                                        (model.PONO == "" || po.PurchaseOrderNo == model.PONO) &&
                                        (model.Status == "" || po.PurchaseOrderStatus == model.Status) &&
                                        (model.ItemId == 0 || pod.PurchaseOrderDetailsMatId == model.ItemId) &&
                                        (po.PurchaseOrderDate >= model.fromDate && po.PurchaseOrderDate <= model.toDate)

                                        select new POTrackingDetails()
                                        {
                                            PONO = po.PurchaseOrderNo,
                                            PODate = po.PurchaseOrderDate,
                                            Supplier = sp.SuppliersMasterSupplierName ?? "",
                                            POQuantity = pod.PurchaseOrderDetailsQuantity,
                                            PartNO = (i != null) ? i.ItemMasterPartNo : "",
                                            ItemName = pod.PurchaseOrderDetailsItemName,
                                            ItemRate = pod.PurchaseOrderDetailsRate,
                                            PODetailsId = pod.PurchaseOrderDetailsPodId,
                                            ItemId = pod.PurchaseOrderDetailsMatId.ToString(),
                                            POStatus = po.PurchaseOrderDelivStatus,
                                            CurrencyRate = (cm != null) ? (decimal?)cm.CurrencyMasterCurrencyRate : 1,
                                            DiscountPerc = po.PurchaseOrderTotalDisPer,
                                            VATPerc = po.PurchaseOrderVatPer,
                                            Amount = po.PurchaseOrderNetAmount,
                                            DiscountAmount = po.PurchaseOrderTotalDisAmount,
                                            IssuedQty = _poRegister.GetAsQueryable()
                                                          .Where(pr => pr.PurOrderRegisterOrderNo == po.PurchaseOrderPono && pr.PurOrderRegisterDetailId == pod.PurchaseOrderDetailsPodId && pr.PurOrderRegisterMaterialId == pod.PurchaseOrderDetailsMatId)
                                                          .Sum(pr => (int?)pr.PurOrderRegisterQtyIssued) ?? 0,
                                            BalanceQty = pod.PurchaseOrderDetailsQuantity - (_poRegister.GetAsQueryable().Where(pr => pr.PurOrderRegisterOrderNo == po.PurchaseOrderPono && pr.PurOrderRegisterDetailId == pod.PurchaseOrderDetailsPodId && pr.PurOrderRegisterMaterialId == pod.PurchaseOrderDetailsMatId)
                                                                           .Sum(pr => (int?)pr.PurOrderRegisterQtyIssued) ?? 0),
                                            Currency = (cm != null) ? cm.CurrencyMasterCurrencyName : "",
                                            JobName = (jm != null) ? jm.JobMasterJobName : "",
                                            JobNo = (jm != null) ? jm.JobMasterJobNo : ""
                                        }).OrderBy(x => x.PODate).ThenBy(x => x.PONO).ToDynamicListAsync();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            data.Add(item);
                        }
                    }
                    return new Response<List<POTrackingDetails>>()
                    {
                        Valid = true,
                        Message = "Data Found",
                        Result = data
                    };
                }
                else
                {
                    var result = await (from po in _purchaseOrder.GetAsQueryable()
                                        join sp in _supliersMaster.GetAsQueryable() on (int?)po.PurchaseOrderPartyId equals sp.SuppliersMasterSupplierId into spGroup
                                        from sp in spGroup.DefaultIfEmpty()
                                        join pod in _poDetails.GetAsQueryable() on po.PurchaseOrderId equals pod.PurchaseOrderId into podGroup
                                        from pod in podGroup.DefaultIfEmpty()
                                        join i in _itemMaster.GetAsQueryable() on (long)pod.PurchaseOrderDetailsMatId equals i.ItemMasterItemId into iGroup
                                        from i in iGroup.DefaultIfEmpty()
                                        join cm in _curreny.GetAsQueryable() on po.PurchaseOrderCurrencyId equals cm.CurrencyMasterCurrencyId into cmGroup
                                        from cm in cmGroup.DefaultIfEmpty()
                                        join jm in _jobMaster.GetAsQueryable() on po.PurchaseOrderJobId equals jm.JobMasterJobId into jmGroup
                                        from jm in jmGroup.DefaultIfEmpty()

                                        where (model.SupplierId == 0 || po.PurchaseOrderPartyId == model.SupplierId) &&
                                        (model.JobId == 0 || po.PurchaseOrderJobId == model.JobId) &&
                                        (model.LocationId == 0 || po.PurchaseOrderLocationId == model.LocationId) &&
                                        (model.PONO == "" || po.PurchaseOrderNo == model.PONO) &&
                                        (model.Status == "" || po.PurchaseOrderStatus == model.Status) &&
                                        (model.ItemId == 0 || pod.PurchaseOrderDetailsMatId == model.ItemId)
                                        select new POTrackingDetails()
                                        {
                                            PONO = po.PurchaseOrderNo,
                                            PODate = po.PurchaseOrderDate,
                                            Supplier = sp.SuppliersMasterSupplierName ?? "",
                                            POQuantity = pod.PurchaseOrderDetailsQuantity,
                                            PartNO = (i != null) ? i.ItemMasterPartNo : "",
                                            ItemName = pod.PurchaseOrderDetailsItemName,
                                            ItemRate = pod.PurchaseOrderDetailsRate,
                                            PODetailsId = pod.PurchaseOrderDetailsPodId,
                                            ItemId = pod.PurchaseOrderDetailsMatId.ToString(),
                                            POStatus = po.PurchaseOrderDelivStatus,
                                            CurrencyRate = (cm != null) ? (decimal?)cm.CurrencyMasterCurrencyRate : 1,
                                            DiscountPerc = po.PurchaseOrderTotalDisPer,
                                            VATPerc = po.PurchaseOrderVatPer,
                                            Amount = po.PurchaseOrderNetAmount,
                                            DiscountAmount = po.PurchaseOrderTotalDisAmount,
                                            IssuedQty = _poRegister.GetAsQueryable()
                                                          .Where(pr => pr.PurOrderRegisterOrderNo == po.PurchaseOrderPono && pr.PurOrderRegisterDetailId == pod.PurchaseOrderDetailsPodId && pr.PurOrderRegisterMaterialId == pod.PurchaseOrderDetailsMatId)
                                                          .Sum(pr => (int?)pr.PurOrderRegisterQtyIssued) ?? 0,
                                            BalanceQty = pod.PurchaseOrderDetailsQuantity - (_poRegister.GetAsQueryable().Where(pr => pr.PurOrderRegisterOrderNo == po.PurchaseOrderPono && pr.PurOrderRegisterDetailId == pod.PurchaseOrderDetailsPodId && pr.PurOrderRegisterMaterialId == pod.PurchaseOrderDetailsMatId)
                                                                           .Sum(pr => (int?)pr.PurOrderRegisterQtyIssued) ?? 0),
                                            Currency = (cm != null) ? cm.CurrencyMasterCurrencyName : "",
                                            JobName = (jm != null) ? jm.JobMasterJobName : "",
                                            JobNo = (jm != null) ? jm.JobMasterJobNo : ""
                                        }).OrderBy(x => x.PODate).ThenBy(x => x.PONO).ToDynamicListAsync();

                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            data.Add(item);
                        }
                    }
                    return new Response<List<POTrackingDetails>>()
                    {
                        Valid = true,
                        Message = "Data Found",
                        Result = data
                    };

                }
                
            }
            catch (Exception ex)
            {
                return Response<List<POTrackingDetails>>.Fail(new List<POTrackingDetails>(), ex.Message);
            }
        }


        public async Task<Response<List<POTrackingSummary>>> GetPurchaseOrderTrackingSummary(ReportFilter model)
        {
            try
            {
                List<POTrackingSummary> data = new List<POTrackingSummary>();

                #region Update Existing Record for Delivered Qty If Any
                //updating existing record
                var query = from a in _poRegister.GetAsQueryable()
                            group a by new { a.PurOrderRegisterOrderNo, a.PurOrderRegisterMaterialId, a.PurOrderRegisterDetailId } into g
                            select new
                            {
                                OrderNo = g.Key.PurOrderRegisterOrderNo,
                                Mat_id = g.Key.PurOrderRegisterMaterialId,
                                DetailId = g.Key.PurOrderRegisterDetailId,
                                DelQty = g.Sum(x => x.PurOrderRegisterQtyIssued ?? 0)
                            };

                foreach (var item in query.ToList())
                {
                    var purchaseOrderDetail = _poDetails
                        .GetAsQueryable()
                        .FirstOrDefault(pod => pod.PurchaseOrderDetailsId == item.DetailId && pod.PurchaseOrderDetailsMatId == item.Mat_id);

                    if (purchaseOrderDetail != null)
                    {
                        purchaseOrderDetail.PurchaseOrderDetailsDelQty = item.DelQty;
                        _poDetails.Update(purchaseOrderDetail);
                    }
                }
                #endregion

                #region Update Existing Record for Delivered Qty If NULL
                //UPDATE PurchaseOrderDetails SET DelQty = 0 WHERE DelQty IS NULL
                var nullQty = from a in _poDetails.GetAsQueryable().Where(x => x.PurchaseOrderDetailsDelQty == null) select a;
                foreach (var item in nullQty.ToList())
                {
                    item.PurchaseOrderDetailsDelQty = 0;
                    _poDetails.Update(item);
                }
                #endregion

                #region UPDATE PurchaseOrder SET POStatus=NULL
                var poStatusNull = from a in _purchaseOrder.GetAsQueryable() select a;
                foreach (var item in poStatusNull.ToList())
                {
                    item.PurchaseOrderStatus = null;
                    _purchaseOrder.Update(item);
                }
                #endregion

                #region update PurchaseOrder SET POStatus='P' WHERE ID IN (SELECT poid from PurchaseOrderDetails WHERE DelQty=0 )
                var poIds = from a in _poDetails.GetAsQueryable().Where(x => x.PurchaseOrderDetailsDelQty == 0) select a;
                foreach (var item in poIds.ToList())
                {
                    //var po = from p in _purchaseOrder.GetAsQueryable().Where(x => x.PurchaseOrderId == item.PurchaseOrderId) select p;
                    var po = _purchaseOrder.GetAsQueryable().FirstOrDefault(x => x.PurchaseOrderId == item.PurchaseOrderId);
                    // po.PurchaseOrderStatus = "P";
                    // _purchaseOrder.Update(po);
                }
                #endregion

                #region UPDATE PurchaseOrder SET POStatus='R' FROM PurchaseOrder INNER JOIN (SELECT SUM(DelQty) AS DelQty,SUM(Quantity) AS Quantity,POID FROM PurchaseOrderDetails GROUP BY POID) AS T ON PurchaseOrder.ID=T.POID WHERE t.DelQty>0 AND t.DelQty<t.Quantity
                var qtySummaries = from po in _purchaseOrder.GetAsQueryable() // Assuming GetAll() returns IQueryable<PurchaseOrder>
                                   join pod in _poDetails.GetAll() on (int)po.PurchaseOrderId equals pod.PurchaseOrderDetailsPoId
                                   group pod by pod.PurchaseOrderDetailsPoId into grouped
                                   where grouped.Sum(g => g.PurchaseOrderDetailsDelQty) > 0 && grouped.Sum(g => g.PurchaseOrderDetailsDelQty) < grouped.Sum(g => g.PurchaseOrderDetailsQuantity)
                                   select new { POID = grouped.Key };

                foreach (var item in qtySummaries.ToList())
                {
                    var po = _purchaseOrder.GetAsQueryable().FirstOrDefault(x => x.PurchaseOrderId == item.POID);
                    if (po != null)
                    {
                        po.PurchaseOrderDelivStatus = "R";
                        _purchaseOrder.Update(po);
                    }
                }


                #endregion

                #region UPDATE PurchaseOrder SET POStatus='L' WHERE Status='CLOSE'
                var closePOs = _purchaseOrder.GetAsQueryable().Where(x => x.PurchaseOrderStatus.ToUpper().Equals("Close".ToUpper()));
                foreach (var item in closePOs.ToList())
                {
                    if (item != null)
                    {
                        item.PurchaseOrderStatus = "L";
                        _purchaseOrder.Update(item);
                    }
                }
                #endregion

                #region UPDATE PurchaseOrder SET POStatus='C' WHERE POStatus IS NULL
                var nullPOs = _purchaseOrder.GetAsQueryable().Where(x => x.PurchaseOrderStatus == null);
                foreach (var item in nullPOs.ToList())
                {
                    if (item == null) continue;
                    item.PurchaseOrderStatus = "C";
                    _purchaseOrder.Update(item);
                }
                #endregion

                string filteredValue = string.Empty;
                // Suleman 8/3/2024 changes filter 
                if (model.IsDateCheck)
                {
                    var detailsQuery = await (from po in _purchaseOrder.GetAsQueryable()
                                                  // join sp in _supliers.GetAsQueryable() on po.PurchaseOrderSupInvNo equals sp.SuppliersInsId.ToString() into spGroup
                                              join sp in _supliersMaster.GetAsQueryable() on (int?)po.PurchaseOrderPartyId equals sp.SuppliersMasterSupplierId into spGroup
                                              from sp in spGroup.DefaultIfEmpty()
                                              join cm in _curreny.GetAsQueryable() on po.PurchaseOrderCurrencyId equals cm.CurrencyMasterCurrencyId into cmGroup
                                              from cm in cmGroup.DefaultIfEmpty()
                                              join jm in _jobMaster.GetAsQueryable() on po.PurchaseOrderJobId equals jm.JobMasterJobId into jmGroup
                                              from jm in jmGroup.DefaultIfEmpty()

                                              where (model.SupplierId == 0 || po.PurchaseOrderPartyId == model.SupplierId) &&
                                              (model.JobId == 0 || po.PurchaseOrderJobId == model.JobId) &&
                                              (model.LocationId == 0 || po.PurchaseOrderLocationId == model.LocationId) &&
                                              (model.PONO == "" || po.PurchaseOrderPono == model.PONO) &&
                                              (model.Status == "" || po.PurchaseOrderStatus == model.Status) &&
                                              (po.PurchaseOrderDate >= model.fromDate && po.PurchaseOrderDate <= model.toDate)

                                              select new POTrackingSummary()
                                              {
                                                  PONO = po.PurchaseOrderPono,
                                                  PODate = po.PurchaseOrderDate,
                                                  //Supplier = sp != null ? sp.SuppliersInsName : "",
                                                  Supplier = sp.SuppliersMasterSupplierName ?? "",
                                                  Amount = po.PurchaseOrderNetAmount,
                                                  POStatus = po.PurchaseOrderStatus,
                                                  DeliveryFrom = po.PurchaseOrderDelivFromDate,
                                                  DeliveryTo = po.PurchaseOrderDelivToDate,
                                                  Status = po.PurchaseOrderStatus,
                                                  Currency = cm != null ? cm.CurrencyMasterCurrencyName : "",
                                                  JobName = jm != null ? jm.JobMasterJobName : "",
                                                  JobNo = jm != null ? jm.JobMasterJobNo : "",
                                                  CurrencyRate = cm != null ? (decimal?)cm.CurrencyMasterCurrencyRate : 1
                                              }).OrderBy(x => x.PODate).ThenBy(x => x.PONO).ToDynamicListAsync();
                    foreach (var item in detailsQuery)
                    {
                        data.Add(item);
                    }

                    return Response<List<POTrackingSummary>>.Success(data, "Data Found");
                }
                else
                {
                    var detailsQuery = await (from po in _purchaseOrder.GetAsQueryable()
                                                  // join sp in _supliers.GetAsQueryable() on po.PurchaseOrderSupInvNo equals sp.SuppliersInsId.ToString() into spGroup
                                              join sp in _supliersMaster.GetAsQueryable() on (int?)po.PurchaseOrderPartyId equals sp.SuppliersMasterSupplierId into spGroup
                                              from sp in spGroup.DefaultIfEmpty()
                                              join cm in _curreny.GetAsQueryable() on po.PurchaseOrderCurrencyId equals cm.CurrencyMasterCurrencyId into cmGroup
                                              from cm in cmGroup.DefaultIfEmpty()
                                              join jm in _jobMaster.GetAsQueryable() on po.PurchaseOrderJobId equals jm.JobMasterJobId into jmGroup
                                              from jm in jmGroup.DefaultIfEmpty()

                                              where (model.SupplierId == 0 || po.PurchaseOrderPartyId == model.SupplierId) &&
                                              (model.JobId == 0 || po.PurchaseOrderJobId == model.JobId) &&
                                              (model.LocationId == 0 || po.PurchaseOrderLocationId == model.LocationId) &&
                                              (model.PONO == "" || po.PurchaseOrderPono == model.PONO) &&
                                              (model.Status == "" || po.PurchaseOrderStatus == model.Status)             
                                              select new POTrackingSummary()
                                              {
                                                  PONO = po.PurchaseOrderPono,
                                                  PODate = po.PurchaseOrderDate,
                                                  //Supplier = sp != null ? sp.SuppliersInsName : "",
                                                  Supplier = sp.SuppliersMasterSupplierName ?? "",
                                                  Amount = po.PurchaseOrderNetAmount,
                                                  POStatus = po.PurchaseOrderStatus,
                                                  DeliveryFrom = po.PurchaseOrderDelivFromDate,
                                                  DeliveryTo = po.PurchaseOrderDelivToDate,
                                                  Status = po.PurchaseOrderStatus,
                                                  Currency = cm != null ? cm.CurrencyMasterCurrencyName : "",
                                                  JobName = jm != null ? jm.JobMasterJobName : "",
                                                  JobNo = jm != null ? jm.JobMasterJobNo : "",
                                                  CurrencyRate = cm != null ? (decimal?)cm.CurrencyMasterCurrencyRate : 1
                                              }).OrderBy(x => x.PODate).ThenBy(x => x.PONO).ToDynamicListAsync();
                    foreach (var item in detailsQuery)
                    {
                        data.Add(item);
                    }

                    return Response<List<POTrackingSummary>>.Success(data, "Data Found");
                }
            }
            catch (Exception ex)
            {
                return Response<List<POTrackingSummary>>.Fail(new List<POTrackingSummary>(), ex.Message);
            }
        }
        public async Task<Response<List<UserTrackingDetails>>> GetUserTrackingDetails(UserTrackFilter model)
        {
            try
            {
                List<UserTrackingDetails> data = new List<UserTrackingDetails>();
                string filteredValue = string.Empty;

                var query = _UserTracking.GetAsQueryable().Where(s => s.UserTrackingUserDelStatus != true).ToList();
                if (model.Userid > 0)
                {
                    query = query.Where(a => a.UserTrackingUserUserId == model.Userid).ToList();

                }
                if (model.VPAction != null)
                {
                    query = query.Where(a => a.UserTrackingUserVpAction == model.VPAction).ToList();

                }
                if (model.VPType != null)
                {
                    query = query.Where(a => a.UserTrackingUserVpType == model.VPType).ToList();

                }
                if (model.fromDate != null && model.toDate != null)
                {

                    if (model.isCheck == true)

                        query = query.Where(x => x.UserTrackingUserChangeDt.Value.Date >= Convert.ToDateTime(model.fromDate).Date && x.UserTrackingUserChangeDt.Value.Date <= Convert.ToDateTime(model.toDate).Date).ToList();

                }


                data = (from ut in query
                        join uf in _UserFile.GetAsQueryable()
                        on (long?)ut.UserTrackingUserUserId equals uf.UserId

                        select new UserTrackingDetails
                        {
                            VP_Action = ut.UserTrackingUserVpAction,
                            UserName = uf.UserName,
                            VP_Type = ut.UserTrackingUserVpType,
                            Change_Dt = ut.UserTrackingUserChangeDt,
                            Change_Time = ut.UserTrackingUserChangeTime,
                            User_VP_NO = ut.UserTrackingUserVpNo
                        }).ToList();

                return new Response<List<UserTrackingDetails>>()
                {
                    Valid = true,
                    Message = "Data Found",
                    Result = data
                };
            }
            catch (Exception ex)
            {
                return Response<List<UserTrackingDetails>>.Fail(new List<UserTrackingDetails>(), ex.Message);
            }
        }

        public async Task<Response<List<DropdownUserVPType>>> GetTrackingUserVPType()
        {
            try
            {
                var response = new List<DropdownUserVPType>();

                //response.AddRange(await _UserTracking.ListSelectAsync(x => new DropdownUserVPType
                //{
                //    VPType = x.UserTrackingUserVpType
                //}));
                response = _UserTracking.GetAsQueryable().Select(ut => new DropdownUserVPType
                { VPType = ut.UserTrackingUserVpType ?? "" }).
                Distinct().Union(new[] { new DropdownUserVPType
                { VPType = " ALL" } }).OrderBy(ut => ut.VPType).ToList();

                return Response<List<DropdownUserVPType>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownUserVPType>>.Fail(new List<DropdownUserVPType>(), ex.Message);
            }
        }
    }
}
