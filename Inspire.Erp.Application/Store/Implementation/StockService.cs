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
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Domain.Modals;
using System.Threading.Tasks;
using Spire.Pdf.Exporting.XPS.Schema;
using Inspire.Erp.Domain.Modals.Procurement;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Dynamic.Core;
using System.Collections;
using System.Runtime.CompilerServices;
using DinkToPdf;
using EllipticCurve.Utils;
using Inspire.Erp.Domain.Models;

namespace Inspire.Erp.Application.Store.Implementation
{
    public class StockService : IStockService
    {
        private InspireErpDBContext _context;
        private IRepository<StockRegister> _stockRegisterRepository;
        private IRepository<ItemMaster> _itemMasterRepository;
        private IRepository<UnitMaster> _unitMasterRepository;
        private IRepository<UnitDetails> _unitDetailsRepository;
        private IRepository<PurchaseVoucher> _purchaseVoucherRepository;
        private IRepository<PurchaseVoucherDetails> _purchaseVoucherDRepository;
        //private IRepository<UnitDetails> _unitDetailsRepository;
        private readonly ILogger<StockService> _logger;

        public StockService(
            InspireErpDBContext context,
            IRepository<ItemMaster> itemMasterRepository,
            IRepository<UnitMaster> unitMasterRepository,
            IRepository<UnitDetails> unitDetailsRepository,
             IRepository<StockRegister> stockRegisterRepository,
             IRepository<PurchaseVoucher> purchaseVoucherRepository,
             IRepository<PurchaseVoucherDetails> purchaseVoucherDRepository,
            ILogger<StockService> logger)
        {

            _logger = logger;
            _context = context;
            _itemMasterRepository = itemMasterRepository;
            _unitMasterRepository = unitMasterRepository;
            _unitDetailsRepository = unitDetailsRepository;
            _stockRegisterRepository = stockRegisterRepository;
            _purchaseVoucherDRepository = purchaseVoucherDRepository;
            _purchaseVoucherRepository = purchaseVoucherRepository;
        }

        public async Task<Response<List<StockReportBaseUnitResponse>>> GetStockReportBaseUnitList(StockFilterModel model)
        {
            try
            {

                List<StockReportBaseUnitResponse> response = new List<StockReportBaseUnitResponse>();

                var subrq = (from s in _unitDetailsRepository.GetAsQueryable()
                             group s by new { s.UnitDetailsItemId, s.UnitDetailsId } into g
                             select new
                             {
                                 ItemId = g.Key.UnitDetailsItemId,
                                 UnitId = g.Key.UnitDetailsId,
                                 ConTyp = g.Max(s => s.UnitDetailsConversionType)
                             });


                var results = await (from im in _itemMasterRepository.GetAsQueryable().Where(x => x.ItemMasterItemName.ToLower().Contains(model.ItemName.ToLower())
                                     // && x.ItemMasterDefaultLocationName.ToLower().Contains(model.Location.ToLower()) 
                                     && (model.LocationId == null || x.ItemMasterLocationId == model.LocationId)
                                     && (model.ItemId == null || x.ItemMasterItemId == model.ItemId)
                                     && (model.ItemGroupId == null || x.ItemMasterRef1 == model.ItemGroupId.ToString())
                                     )

                                     join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals sr.StockRegisterMaterialID.GetValueOrDefault() into gim
                                     from y in gim.DefaultIfEmpty()
                                     join r in subrq on y.StockRegisterMaterialID.GetValueOrDefault() equals r.ItemId into grj
                                     from q in grj.DefaultIfEmpty()
                                     join um in _unitMasterRepository.GetAsQueryable() on q.UnitId equals um.UnitMasterUnitId into gum
                                     from n in gum.DefaultIfEmpty()
                                     where im.ItemMasterItemType == "A"
                                     select new StockReportBaseUnitResponse
                                     {
                                         ItemId = y.StockRegisterMaterialID,
                                         ItemName = im.ItemMasterItemName,
                                         Stock = (int)((y.StockRegisterSIN ?? 0 - y.StockRegisterSout ?? 0) / (decimal)q.ConTyp),
                                         Unit = q.UnitId,
                                         PartNo = n.UnitMasterUnitShortName,
                                         AverageRate = y.StockRegisterRate,
                                         Amount = y.StockRegisterAmount
                                     }).ToListAsync();
                return Response<List<StockReportBaseUnitResponse>>.Success(results, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<StockReportBaseUnitResponse>>.Fail(new List<StockReportBaseUnitResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<StockReportBaseUnitResponse>>> GetStockReportBaseUnit(StockFilterModel model)
        {
            try
            {
                List<StockReportBaseUnitResponse> response = new List<StockReportBaseUnitResponse>();
                var query = _itemMasterRepository.GetAsQueryable();
                if (model.ItemId > 0)
                {
                    query = query.Where(im => im.ItemMasterItemId == model.ItemId);
                }
                else if (model.ItemGroupId > 0)
                {
                    query = query.Where(im => im.ItemMasterAccountNo == model.ItemGroupId && im.ItemMasterItemType == "A")
                                 .OrderBy(im => im.ItemMasterItemId);
                }
                else
                {
                    query = query.Where(im => im.ItemMasterItemType == "A")
                                 .OrderBy(im => im.ItemMasterItemId);
                }


                var subrq = (from s in _unitDetailsRepository.GetAsQueryable().Where(x => x.UnitDetailsConversionType == 1)
                             select new
                             {
                                 ItemId = s.UnitDetailsItemId,
                                 UnitId = s.UnitDetailsId
                             });

                var stockBaseUnits = (from im in query
                                      join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals sr.StockRegisterMaterialID.GetValueOrDefault() into gim
                                      from y in gim.DefaultIfEmpty()
                                      join r in subrq on y.StockRegisterMaterialID.GetValueOrDefault() equals r.ItemId into grj
                                      from q in grj.DefaultIfEmpty()
                                      join um in _unitMasterRepository.GetAsQueryable() on q.UnitId equals um.UnitMasterUnitId into gum
                                      from n in gum.DefaultIfEmpty()
                                      where im.ItemMasterItemType == "A"
                                      select new StockReportBaseUnitResponse
                                      {
                                          ItemId = (long)im.ItemMasterItemId,
                                          ItemName = im.ItemMasterItemName,
                                          Stock = (int)(y.StockRegisterSIN ?? 0 - y.StockRegisterSout ?? 0),
                                          Unit = q.UnitId,
                                          PartNo = n.UnitMasterUnitShortName,
                                      }).GroupBy(
        m => new { m.ItemId, m.ItemName, m.Unit, m.PartNo },
        (k, g) => new { k.ItemId, k.ItemName, k.Unit, k.PartNo, Stock = g.Sum(x => x.Stock) }
    )
    .Select(am => new
    {
        ItemId = am.ItemId,
        ItemName = am.ItemName,
        Stock = am.Stock,
        Unit = am.Unit,
        PartNo = am.PartNo,
    });

                // dbo].[GetStockValue]
                var stockValues = (from ss in query
                                   join a in _stockRegisterRepository.GetAsQueryable() on ss.ItemMasterItemId equals (long)a.StockRegisterMaterialID.GetValueOrDefault()
                                   group a by a.StockRegisterMaterialID into g
                                   select new
                                   {
                                       ItemId = (long)g.Key,
                                       TotalQty = g.Sum(x => x.StockRegisterSIN),
                                       StockRate = g.Sum(x => x.StockRegisterSIN * x.StockRegisterRate),
                                       StockValue = (decimal)g.Sum(x => x.StockRegisterSIN) * (g.Sum(x => x.StockRegisterSIN) > 0 ? g.Sum(x => x.StockRegisterSIN * x.StockRegisterRate) / g.Sum(x => x.StockRegisterSIN) : 0)
                                   });

                var results = await (from im in stockBaseUnits
                                     join sr in stockValues on im.ItemId equals sr.ItemId into gim
                                     from y in gim.DefaultIfEmpty()
                                     select new StockReportBaseUnitResponse
                                     {
                                         ItemId = im.ItemId,
                                         ItemName = im.ItemName,
                                         Stock = im.Stock,
                                         Unit = im.Unit,
                                         PartNo = im.PartNo,
                                         AverageRate = y.StockRate,
                                         Amount = im.Stock * y.StockRate,
                                         AveragePurchaseValue = y.StockValue,
                                     }).ToListAsync();
                return Response<List<StockReportBaseUnitResponse>>.Success(results, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<StockReportBaseUnitResponse>>.Fail(new List<StockReportBaseUnitResponse>(), ex.Message);
            }
        }
        public async Task<Response<decimal>> GetAveragePurchasePrice(int itemId, int? locationId)
        {
            try
            {
                var totalQuantity = (from sr in _stockRegisterRepository.GetAsQueryable()
                                     where (locationId == null || sr.StockRegisterLocationID == locationId)
                                     && sr.StockRegisterMaterialID == itemId
                                     group sr by 1 into og
                                     select new
                                     {
                                         Total = og.Sum(item => (int)(item.StockRegisterSIN ?? 0 - item.StockRegisterSout ?? 0)),

                                     }).FirstOrDefault().Total;

                var rateQuantiyFromStock = await (from sr in _stockRegisterRepository.GetAsQueryable()
                                                  where (locationId == null || sr.StockRegisterLocationID == locationId)
                                                  && sr.StockRegisterMaterialID == itemId

                                                  select new PurVoucherRateQuantityModel
                                                  {
                                                      Quantity = sr.StockRegisterQuantity,
                                                      Rate = sr.StockRegisterRate,
                                                      PurchaseDate = sr.StockRegisterVoucherDate
                                                  }).OrderByDescending(y => y.PurchaseDate).ToListAsync();

                var PurchaseVoucherIds = await _purchaseVoucherRepository.GetAsQueryable().Where(x => x.PurchaseVoucherStatus.Trim().ToUpper() == "P")
                    .Select(o => o.PurchaseVoucherPurID).ToListAsync();

                var purchseVouchers = await (from pvd in _purchaseVoucherDRepository.GetAsQueryable()
                                             join pv in _purchaseVoucherRepository.GetAsQueryable() on (long)pvd.PurchaseVoucherDetailsPrId equals pv.PurchaseVoucherPurID
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
                    var im = _itemMasterRepository.GetAsQueryable().Where(k => k.ItemMasterItemId == itemId).FirstOrDefault();
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


        public async Task<Response<decimal>> GetAveragePurchasePriceGPC(int itemId, int unitDetailId, decimal? quantity)
        {
            try
            {
                decimal? issueqty = 0;

                decimal basicConversion = 1;
                var unitD = await _unitDetailsRepository.GetAsQueryable().Where(x => x.UnitDetailsItemId == itemId && x.UnitDetailsUnitId == unitDetailId).FirstOrDefaultAsync();
                if (unitD != null)
                {
                    basicConversion = Convert.ToDecimal(unitD.UnitDetailsConversionType);
                }

                if (basicConversion == 0)
                {
                    basicConversion = 1;
                }

                issueqty = quantity * basicConversion;

                var currStock = (from sr in _stockRegisterRepository.GetAsQueryable()
                                 where sr.StockRegisterMaterialID == itemId
                                 group sr by 1 into og
                                 select new
                                 {
                                     Total = og.Sum(item => (item.StockRegisterSIN ?? 0 - item.StockRegisterSout ?? 0)),

                                 }).FirstOrDefault().Total;

                //updae stockregister "update stock_register set NETSTKBAL=0 where mat_id=" & itemId
                var listPrice = await (from sr in _stockRegisterRepository.GetAsQueryable()
                                       where sr.StockRegisterMaterialID == itemId &&
                                                  (new string[] { "ITEM_MANUFACTURE", "PurchaseVoucher", "OpeningStockVoucher", "SalesReturn", "ISSUE RETURN", "STOCK ADJUSTMENT" }).ToList().Contains(sr.StockRegisterTransType.Trim().ToUpper())
                                                 && ((int)sr.StockRegisterSIN != 0 && sr.StockRegisterSIN != null)
                                       select new StockRegisterModel
                                       {
                                           SIN = (decimal)sr.StockRegisterSIN,
                                           Rate = sr.StockRegisterRate,
                                           AssingedDate = sr.StockRegisterAssignedDate,
                                           NetStockBalance = sr.StockRegisterNetStkBal,
                                           // PurchaseId=sr.StockRegisterPurchaseID,
                                           StoreId = sr.StockRegisterStoreID

                                       }).OrderByDescending(y => y.AssingedDate).ToListAsync();



                decimal stockUpdate = 0;
                if (listPrice.Count() > 0)
                {
                    foreach (var pvd in listPrice)
                    {
                        if (pvd.SIN <= currStock)
                        {
                            stockUpdate = pvd.SIN;
                            currStock = currStock - pvd.SIN;

                            var stockReg = await _stockRegisterRepository.GetAsQueryable().Where(x => x.StockRegisterStoreID == pvd.StoreId)
                                                 .ToListAsync();

                            foreach (var sr in stockReg)
                            {
                                sr.StockRegisterNetStkBal = stockUpdate;
                            }

                        }
                        else
                        {
                            stockUpdate = currStock;

                            var stockReg = await _stockRegisterRepository.GetAsQueryable().Where(x => x.StockRegisterStoreID == pvd.StoreId)
                                              .ToListAsync();

                            foreach (var sr in stockReg)
                            {
                                sr.StockRegisterNetStkBal = stockUpdate;
                            }

                            break;
                        }

                        
                    }
                }



                decimal? issueBal = 0;
               // _stockRegisterRepository.SaveChangesAsync();

               // await Task.Run(() => _stockRegisterRepository.SaveChangesAsync());



                var avgPrice = await (from sr in _stockRegisterRepository.GetAsQueryable().AsNoTracking()
                                      where sr.StockRegisterMaterialID == itemId
                                       && (sr.StockRegisterNetStkBal != 0 && sr.StockRegisterNetStkBal != null)
                                      select new StockRegisterModel
                                      {
                                          SIN = (decimal)sr.StockRegisterSIN,
                                          Rate = sr.StockRegisterRate,
                                          AssingedDate = sr.StockRegisterAssignedDate,
                                          NetStockBalance = sr.StockRegisterNetStkBal,
                                          PurchaseId = sr.StockRegisterPurchaseID,

                                      }).OrderByDescending(y => y.AssingedDate).ToListAsync();

                decimal? totalAmount = 0;
                foreach (var pvd in avgPrice)
                {
                    if (issueBal < pvd.NetStockBalance)
                    {
                        totalAmount = totalAmount + issueBal * pvd.Rate;
                        break;
                    }
                    else
                    {
                        totalAmount = totalAmount + pvd.NetStockBalance * pvd.Rate;
                        issueBal = issueBal - pvd.NetStockBalance;
                    }
                }

                decimal? costPrice = 0;
                if (issueqty > 0)
                {
                    if (issueqty > currStock)
                    {

                        if (totalAmount <= 0)
                        {
                            costPrice = 0;
                        }
                        else
                        {
                            costPrice = (decimal)totalAmount / currStock;
                        }
                    }
                    else
                    {
                        costPrice = (decimal)totalAmount / (decimal)issueqty;
                    }

                    if (costPrice == 0)
                    {

                        var costPriceL = await (from sr in _stockRegisterRepository.GetAsQueryable()
                                                where sr.StockRegisterMaterialID == itemId &&
                                               (new string[] { "ITEM_MANUFACTURE", "PURCHASEVOUCHER", "OPENINGSTOCKVOUCHER", "SALESRETURN", "ISSUE RETURN", "STOCK ADJUSTMENT" }).ToList().Contains(sr.StockRegisterTransType.Trim().ToUpper())
                                              && ((int)sr.StockRegisterSIN != 0 && sr.StockRegisterSIN != null)
                                                select new StockRegisterModel
                                                {
                                                    SIN = (decimal)sr.StockRegisterSIN,
                                                    Rate = sr.StockRegisterRate,
                                                    AssingedDate = sr.StockRegisterAssignedDate,
                                                    NetStockBalance = sr.StockRegisterNetStkBal,
                                                    // PurchaseId=sr.StockRegisterPurchaseID,
                                                    StoreId = sr.StockRegisterStoreID

                                                }).OrderByDescending(y => y.AssingedDate).ToListAsync();



                        if (costPriceL.Count() > 0)
                            costPrice = costPriceL.FirstOrDefault().Rate;
                    }
                    else
                    {
                        var im = _itemMasterRepository.GetAsQueryable().Where(k => k.ItemMasterItemId == itemId).FirstOrDefault();
                        costPrice = im.ItemMasterLastPurchasePrice ?? 0;
                    }

                }
              
                return Response<decimal>.Success(costPrice ?? 0, "Data found");
            }
            catch (Exception ex)
            {
                return Response<decimal>.Fail(0, ex.Message);
            }
        }
        public async Task<Response<StockMovementReportResponse>> GetStockMovementReport(StockFilterModel model)
        {
            try
            {
                var stockMov = new StockMovementReportResponse();
                var query = _context.ItemMaster.AsQueryable();
                if (model.ItemId > 0)
                {
                    query = query.Where(im => im.ItemMasterItemId == model.ItemId);
                }
                else if (model.ItemGroupId > 0)
                {
                    query = query.Where(im => im.ItemMasterAccountNo == model.ItemGroupId && im.ItemMasterItemType == "A")
                                 .OrderBy(im => im.ItemMasterItemId);
                }
                else
                {
                    query = query.Where(im => im.ItemMasterItemType == "A")
                                 .OrderBy(im => im.ItemMasterItemId);
                }

                var GroupItemIds = await _context.ItemMaster.Where(o => o.ItemMasterAccountNo == (long)model.ItemGroupId).Select(o => o.ItemMasterItemId).ToListAsync();
                var MatIds = await _stockRegisterRepository.GetAsQueryable().Select(o => o.StockRegisterMaterialID).ToListAsync();
                //var subrq = (from s in _stockRegisterRepository.GetAsQueryable().Where(x => x.StockRegisterMaterialID == model.ItemId
                //             && GroupItemIds.Contains((long)x.StockRegisterMaterialID)
                //             )
                //             select new
                //             {
                //                 ItemId = s.UnitDetailsItemId,
                //                 UnitId = s.UnitDetailsId
                //             });
                //&& GroupItemIds.Contains((long)y.StockRegisterMaterialID)
                var stockInOutGW = (from im in query
                                    join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals sr.StockRegisterMaterialID.GetValueOrDefault() into gim
                                    from y in gim.DefaultIfEmpty()
                                    join ig in _context.ItemMaster on im.ItemMasterAccountNo equals ig.ItemMasterItemId
                                    where im.ItemMasterItemType == "A"
                                     && MatIds.Contains((int)im.ItemMasterItemId)
                                    select new
                                    {
                                        ItemId = (long)im.ItemMasterItemId,
                                        ItemMasterAccountNo = im.ItemMasterAccountNo,
                                        GroupName = ig.ItemMasterItemName,
                                        ItemName = im.ItemMasterItemName,
                                        OpenQuantity = y.StockRegisterSIN - y.StockRegisterSout,
                                        OpenQuantityAmount = (y.StockRegisterSIN - y.StockRegisterSout) * y.StockRegisterRate,

                                        BalanceTotal = y.StockRegisterSIN - y.StockRegisterSout,
                                        BalanceAmount = (y.StockRegisterSIN - y.StockRegisterSout) * y.StockRegisterRate,

                                        PurchaseInQty = y.StockRegisterTransType == "P" ? y.StockRegisterSIN ?? 0 : 0,
                                        PurchaseInAmount = y.StockRegisterTransType == "P" ? (y.StockRegisterSIN ?? 0 * y.StockRegisterRate) : 0,
                                        PurchaseOutQty = y.StockRegisterTransType == "P" ? y.StockRegisterSout ?? 0 : 0,
                                        PurchaseOutAmount = y.StockRegisterTransType == "P" ? (y.StockRegisterSout ?? 0 * y.StockRegisterRate) : 0,

                                        SaleInQty = y.StockRegisterTransType == "S" ? y.StockRegisterSIN ?? 0 : 0,
                                        SaleInAmount = y.StockRegisterTransType == "S" ? (y.StockRegisterSIN ?? 0 * y.StockRegisterRate) : 0,
                                        SaleOutQty = y.StockRegisterTransType == "S" ? y.StockRegisterSout ?? 0 : 0,
                                        SaleOutAmount = y.StockRegisterTransType == "S" ? (y.StockRegisterSout ?? 0 * y.StockRegisterRate) : 0,
                                    }).GroupBy(
                                    m => new { m.ItemId, m.ItemMasterAccountNo, m.GroupName, m.ItemName },
                                    (k, g) => new
                                    {
                                        k.ItemId,
                                        k.ItemMasterAccountNo,
                                        k.GroupName,
                                        k.ItemName,
                                        OpenQuantity = g.Sum(x => x.OpenQuantity),
                                        OpenQuantityAmount = g.Sum(x => x.OpenQuantityAmount),
                                        BalanceTotal = g.Sum(x => x.BalanceTotal),
                                        BalanceAmount = g.Sum(x => x.BalanceAmount),

                                        PurchaseInQty = g.Sum(x => x.PurchaseInQty),
                                        PurchaseInAmount = g.Sum(x => x.PurchaseInAmount),
                                        PurchaseOutQty = g.Sum(x => x.PurchaseOutQty),
                                        PurchaseOutAmount = g.Sum(x => x.PurchaseOutAmount),

                                        SaleInQty = g.Sum(x => x.SaleInQty),
                                        SaleInAmount = g.Sum(x => x.SaleInAmount),
                                        SaleOutQty = g.Sum(x => x.SaleOutQty),
                                        SaleOutAmount = g.Sum(x => x.SaleOutAmount),
                                    }
                                )
                                .Select(am => new StockMovementSInOutGroupWiseResponse
                                {
                                    ItemId = am.ItemId,
                                    ItemName = am.ItemName,
                                    GroupName = am.GroupName,
                                    ItemMasterAccountNo = am.ItemMasterAccountNo,
                                    OpenQuantity = am.OpenQuantity,
                                    OpenQuantityAmount = am.OpenQuantityAmount,
                                    BalanceTotal = am.BalanceTotal,
                                    BalanceAmount = am.BalanceAmount,

                                    PurchaseInQty = am.PurchaseInQty,
                                    PurchaseInAmount = am.PurchaseInAmount,
                                    PurchaseOutQty = am.PurchaseOutQty,
                                    PurchaseOutAmount = am.PurchaseOutAmount,

                                    SaleInQty = am.SaleInQty,
                                    SaleInAmount = am.SaleInAmount,
                                    SaleOutQty = am.SaleOutQty,
                                    SaleOutAmount = am.SaleOutAmount,
                                }).ToList();

                stockMov.StockMovementInOutGroupWise = stockInOutGW;



                //var PurchOrdDetails = (from pod in _context.PurchaseOrderDetails
                //                       join pvd in _context.PurchaseVoucherDetails on pod.PurchaseOrderDetailsPodId equals
                //                       pvd.PurchaseVoucherDetailsPodId
                //                       select new
                //                       {
                //                           pod.PurchaseOrderId,
                //                           pvd.PurchaseVoucherDetailsVoucherNo
                //                       }).AsQueryable();

                var queryInOut = (from sr in _stockRegisterRepository.GetAsQueryable()
                                  join pv in _context.PurchaseVoucher on sr.StockRegisterRefVoucherNo equals pv.PurchaseVoucherVoucherNo into gsr
                                  from srp in gsr.DefaultIfEmpty()

                                  join pv in _context.PurchaseVoucher on sr.StockRegisterRefVoucherNo equals pv.PurchaseVoucherVoucherNo into pvg
                                  from pvgs in pvg.DefaultIfEmpty()

                                  join im in _context.ItemMaster on (long)sr.StockRegisterMaterialID equals im.ItemMasterItemId into gim
                                  from ims in gim.DefaultIfEmpty()

                                  join gm in _context.ItemMaster on ims.ItemMasterItemId equals gm.ItemMasterAccountNo into ggm
                                  from gmi in ggm.DefaultIfEmpty()

                                  join jm in _context.JobMaster on sr.StockRegisterJobID equals jm.JobMasterJobId into gjm
                                  from sjm in gjm.DefaultIfEmpty()

                                  join lm in _context.LocationMaster on sr.StockRegisterLocationID equals lm.LocationMasterLocationId into glm
                                  from lms in glm.DefaultIfEmpty()

                                  where ims.ItemMasterItemType == "A" && sr.StockRegisterMaterialID == model.ItemId
                                 && GroupItemIds.Contains((int)sr.StockRegisterMaterialID)
                                 && sr.StockRegisterFSNO == 1
                                 && (model.fromDate == null || sr.StockRegisterAssignedDate >= model.fromDate)
                                 && (model.toDate == null || sr.StockRegisterAssignedDate <= model.toDate)
                                  // && sr.StockRegisterSout != 0
                                  select new
                                  {
                                      sr.StockRegisterMaterialID,
                                      UnitId = 0,
                                      ShortName = "",
                                      sr.StockRegisterRefVoucherNo,
                                      sr.StockRegisterAssignedDate,
                                      sr.StockRegisterTransType,
                                      DisplayTranstype = sr.StockRegisterTransType == "I" ? "ISSUE" : sr.StockRegisterTransType == "DLV" ? "DELIVERY NOTE" : sr.StockRegisterTransType == "S" ? "SALES" : sr.StockRegisterTransType == "SR" ? "SALES RETURN" : sr.StockRegisterTransType == "P" ? "PURCHASE" : sr.StockRegisterTransType == "PR" ? "PURCHASE RETURN" : sr.StockRegisterTransType == "STOCKTRANS" ? "STOCK TRANSFER" : sr.StockRegisterTransType == "OS" ? "OPENING STOCK" : sr.StockRegisterTransType,
                                      RelativeNo = ims.ItemMasterAccountNo,
                                      GroupName = gmi.ItemMasterItemName ?? "",
                                      JobId = sr.StockRegisterJobID ?? 0,
                                      JobName = sjm.JobMasterJobName ?? "",
                                      LocationId = sr.StockRegisterLocationID ?? 0,
                                      Loca_Name = lms.LocationMasterLocationName ?? "",
                                      QtyIn = sr.StockRegisterSIN ?? 0,
                                      QtyInTotAmt = (sr.StockRegisterSIN * sr.StockRegisterRate),
                                      QtyOut = sr.StockRegisterSout ?? 0,
                                      QtyOutTotAmt = (sr.StockRegisterSout * sr.StockRegisterRate),
                                      Rate = sr.StockRegisterRate,
                                      LPO = pvgs.PurchaseVoucherPONo
                                  }
                            )
                             .Select(am => new StockMovementInOutResponse
                             {
                                 Mat_ID = am.StockRegisterMaterialID,
                                 UnitId = am.UnitId,
                                 ShortName = am.ShortName,
                                 Ref_Vocher_No = am.StockRegisterRefVoucherNo,
                                 AssingedDate = Convert.ToString(am.StockRegisterAssignedDate),
                                 TransType = am.DisplayTranstype,
                                 RelativeNo = am.RelativeNo,
                                 GroupName = am.GroupName,
                                 JobId = am.JobId,
                                 jobname = am.JobName,
                                 LocationId = am.LocationId,
                                 Loca_Name = am.Loca_Name,
                                 Rate = am.Rate,
                                 QtyIn = am.QtyIn,
                                 QtyInAmount = am.QtyInTotAmt,
                                 QtyOut = am.QtyOut,
                                 QtyOutAmount = am.QtyOutTotAmt,
                                 LPO = am.LPO
                             }).ToList();

                stockMov.StockMovementInOut = queryInOut;
                return Response<StockMovementReportResponse>.Success(stockMov, "Data found");
            }
            catch (Exception ex)
            {
                return Response<StockMovementReportResponse>.Fail(new StockMovementReportResponse(), ex.Message);
            }
        }
        private decimal getQuantityValue(long itemId)
        {
            var res = (from d in _stockRegisterRepository.GetAsQueryable()
                       where (new string[] { "P", "OS", "OPENING_STOCK_VOUCHER" }).ToList().Contains(d.StockRegisterTransType)
                        && (int)d.StockRegisterMaterialID == itemId
                       select new
                       {
                           SIN = (decimal)d.StockRegisterSIN,
                           Rate = (decimal)d.StockRegisterRate
                       }).ToList().Sum(x => x.SIN * x.Rate);
            return res;
        }
        public async Task<Response<List<VWStockBaseUnitWithValue>>> GetStockReportBaseUnitWithValue(GenericGridViewModel model)
        {
            try
            {
                string query = $" ItemId > 0 {model.Filter}";
                var data = await (from vw in this._context.VWStockBaseUnitWithValue.Where(query)
                                  join i in this._context.ItemMaster.Where(a => a.ItemMasterDelStatus != true) on vw.ItemId equals i.ItemMasterItemId
                                  select new VWStockBaseUnitWithValue()
                                  {
                                      ItemId = vw.ItemId,
                                      Stock = (decimal)vw.Stock,
                                      ItemName = (string)vw.ItemName,
                                      RelativeNO = vw.RelativeNO,
                                      UnitId = vw.UnitId,
                                      UnitShortName = (string)vw.UnitShortName,
                                      AVGRATE = vw.AVGRATE,
                                      AMOUNT = vw.AMOUNT,
                                      AVGPURCHASEValue = vw.AVGPURCHASEValue,
                                      ItemNo = i.ItemMasterPartNo
                                  }).ToListAsync();
                return Response<List<VWStockBaseUnitWithValue>>.Success(data, "Data Were Found");
            }
            catch (Exception ex)
            {
                return Response<List<VWStockBaseUnitWithValue>>.Fail(new List<VWStockBaseUnitWithValue>(), "Not Data Were Found");
            }
        }      
        public async Task<Response<StockMovementResponse>> GetStockMovement(StockFilterModel model)
        {
            try
            {
                var response = new List<StockReportBaseUnitResponse>();

                var query = _context.ItemMaster.AsQueryable();

                if (model.ItemId > 0)
                {
                    query = query.Where(im => im.ItemMasterItemId == model.ItemId && im.ItemMasterDelStatus != true);
                }
                if (model.ItemGroupId > 0)
                {
                    query = query.Where(im => im.ItemMasterAccountNo == model.ItemGroupId && im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                 .OrderBy(im => im.ItemMasterItemId);
                }
                else
                {
                    query = query.Where(im => im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                 .OrderBy(im => im.ItemMasterItemId);
                }


                var subrq = (from s in _context.UnitDetails.Where(x => x.UnitDetailsConversionType == 1 && (model.ItemId == null || x.UnitDetailsItemId == model.ItemId))
                             select new
                             {
                                 ItemId = s.UnitDetailsItemId,
                                 UnitId = s.UnitDetailsUnitId
                             }).Take(1);

                var stockValues = (from ss in query
                                   join a in _stockRegisterRepository.GetAsQueryable() on ss.ItemMasterItemId equals (long)a.StockRegisterMaterialID.GetValueOrDefault()
                                   where a.StockRegisterVoucherDate >= model.fromDate && a.StockRegisterVoucherDate <= model.toDate
                                   group a by a.StockRegisterMaterialID into g
                                   select new
                                   {
                                       ItemId = (long)g.Key,
                                       TotalQty = g.Sum(x => x.StockRegisterSIN - x.StockRegisterSout),
                                       TotalAmount = g.Sum(x => (x.StockRegisterSIN - x.StockRegisterSout) * x.StockRegisterRate)
                                   }).ToList();


                var results = (from im in query
                               join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals (long)sr.StockRegisterMaterialID.GetValueOrDefault() into gimV1
                               from y in gimV1.DefaultIfEmpty()
                               join r in subrq on y.StockRegisterMaterialID.GetValueOrDefault() equals (int)r.ItemId into grj
                               from q in grj.DefaultIfEmpty()
                               join um in _unitMasterRepository.GetAsQueryable() on q.UnitId equals um.UnitMasterUnitId into gum
                               from n in gum.DefaultIfEmpty()
                               where im.ItemMasterItemType == "A" && (model.ItemId == null || im.ItemMasterItemId == model.ItemId)
                               && (model.LocationId == null || y.StockRegisterLocationID == model.LocationId)
                               select new
                               {
                                   im.ItemMasterItemId,
                                   im.ItemMasterItemName,
                                   n.UnitMasterUnitShortName,
                                   y.StockRegisterSIN,
                                   y.StockRegisterSout,
                                   y.StockRegisterVoucherDate,
                                   y.StockRegisterAmount
                               })
               .AsEnumerable()
               .Select(x => new StockRegisterResponse
               {
                   Item_Id = x.ItemMasterItemId,
                   Item_Name = x.ItemMasterItemName,
                   Stock_Register_Unit = x.UnitMasterUnitShortName ?? "",
                   StockIn = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0 && x.StockRegisterVoucherDate >= model.fromDate && x.StockRegisterVoucherDate <= model.toDate) ? x.StockRegisterSIN : 0,
                   StockInAmount = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0 && x.StockRegisterVoucherDate >= model.fromDate && x.StockRegisterVoucherDate <= model.toDate) ? x.StockRegisterAmount ?? 0 : 0,
                   StockOut = (x.StockRegisterSout != null && x.StockRegisterSout > 0 && x.StockRegisterVoucherDate >= model.fromDate && x.StockRegisterVoucherDate <= model.toDate) ? x.StockRegisterSout ?? 0 : 0,
                   StockOutAmount = (x.StockRegisterSout != null && x.StockRegisterSout > 0 && x.StockRegisterVoucherDate >= model.fromDate && x.StockRegisterVoucherDate <= model.toDate) ? x.StockRegisterAmount ?? 0 : 0,
                   OpenQty = (x.StockRegisterVoucherDate < model.fromDate) ? (x.StockRegisterSIN ?? 0) - (x.StockRegisterSout ?? 0) : 0,
                   OpenQtyAmount = (x.StockRegisterVoucherDate < model.fromDate) ? ((x.StockRegisterSIN ?? 0) > 0 ? (x.StockRegisterAmount ?? 0) : 0) - ((x.StockRegisterSout ?? 0) > 0 ? (x.StockRegisterAmount ?? 0) : 0) : 0,

                   TotalBal = 0,
                   TotalBalAmount = 0
               })
               .GroupBy(
                   m => new { m.Item_Id, m.Item_Name, m.Stock_Register_Unit },
                   (k, g) => new StockRegisterResponse
                   {
                       Item_Id = k.Item_Id,
                       Item_Name = k.Item_Name,
                       Stock_Register_Unit = k.Stock_Register_Unit ?? "",
                       StockIn = g.Sum(x => x.StockIn),
                       StockInAmount = g.Sum(a => a.StockInAmount),
                       StockOut = g.Sum(a => a.StockOut),
                       StockOutAmount = g.Sum(a => a.StockOutAmount),
                       OpenQty = g.Sum(a => a.OpenQty),
                       OpenQtyAmount = g.Sum(a => a.OpenQtyAmount),
                       TotalBal = g.Sum(a => a.StockIn) - g.Sum(a => a.StockOut),
                       TotalBalAmount = g.Sum(a => a.StockInAmount) - g.Sum(a => a.StockOutAmount)
                   })
               .ToList();

                var StockIn = (from im in query
                               join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals (long)sr.StockRegisterMaterialID.GetValueOrDefault() into gimV1
                               from y in gimV1.DefaultIfEmpty()
                               join r in subrq on y.StockRegisterMaterialID.GetValueOrDefault() equals (int)r.ItemId into grj
                               from q in grj.DefaultIfEmpty()
                               join um in _unitMasterRepository.GetAsQueryable() on q.UnitId equals um.UnitMasterUnitId into gum
                               from n in gum.DefaultIfEmpty()
                               join lm in _context.LocationMaster on y.StockRegisterLocationID equals lm.LocationMasterLocationId into glm
                               from lms in glm.DefaultIfEmpty()
                               where im.ItemMasterItemType == "A" && (model.ItemId == null || im.ItemMasterItemId == model.ItemId)
                                     && (model.LocationId == null || y.StockRegisterLocationID == model.LocationId)
                               select new StockMovementInOut
                               {
                                   Ref_Vocher_No = y.StockRegisterRefVoucherNo,
                                   Voucher_Date = Convert.ToDateTime(y.StockRegisterVoucherDate).ToString("dd/MM/yyyy"),
                                   TransType = y.StockRegisterTransType,
                                   Unit = n.UnitMasterUnitShortName ?? "",
                                   Location = lms.LocationMasterLocationName ?? "",
                                   Rate = y != null && y.StockRegisterSIN > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterRate : 0,
                                   Qty = y != null && y.StockRegisterSIN > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterSIN : 0,
                                   QtyAmount = y != null && y.StockRegisterSIN > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterAmount : 0,
                               }).ToList();

                var indexedStockMovementIn = StockIn.Where(x => x.Qty > 0)
                                     .Select((x, index) =>
                                     {
                                         x.Sr = index + 1;
                                         return x;
                                     })
                                     .ToList();

                var StockOut = (from im in query
                                join sr in _stockRegisterRepository.GetAsQueryable() on im.ItemMasterItemId equals (long)sr.StockRegisterMaterialID.GetValueOrDefault() into gimV1
                                from y in gimV1.DefaultIfEmpty()
                                join r in subrq on y.StockRegisterMaterialID.GetValueOrDefault() equals (int)r.ItemId into grj
                                from q in grj.DefaultIfEmpty()
                                join um in _unitMasterRepository.GetAsQueryable() on q.UnitId equals um.UnitMasterUnitId into gum
                                from n in gum.DefaultIfEmpty()
                                join lm in _context.LocationMaster on y.StockRegisterLocationID equals lm.LocationMasterLocationId into glm
                                from lms in glm.DefaultIfEmpty()
                                where im.ItemMasterItemType == "A" && (model.ItemId == null || im.ItemMasterItemId == model.ItemId)
                                      && (model.LocationId == null || y.StockRegisterLocationID == model.LocationId)
                                select new StockMovementInOut
                                {
                                    Ref_Vocher_No = y.StockRegisterRefVoucherNo,
                                    Voucher_Date = Convert.ToDateTime(y.StockRegisterVoucherDate).ToString("dd/MM/yyyy"),
                                    TransType = y.StockRegisterTransType,
                                    Unit = n.UnitMasterUnitShortName ?? "",
                                    Location = lms.LocationMasterLocationName ?? "",
                                    Rate = y != null && y.StockRegisterSout > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterRate : 0,
                                    Qty = y != null && y.StockRegisterSout > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterSout : 0,
                                    QtyAmount = y != null && y.StockRegisterSout > 0 && y.StockRegisterVoucherDate >= model.fromDate && y.StockRegisterVoucherDate <= model.toDate ? y.StockRegisterAmount : 0,
                                }).ToList();

                var indexedStockMovementOut = StockOut.Where(x => x.Qty > 0)
                                     .Select((x, index) =>
                                     {
                                         x.Sr = index + 1;
                                         return x;
                                     })
                                     .ToList();
                var response1 = new StockMovementResponse
                {
                    StockRegisterResponse = results,
                    StockMovementIn = indexedStockMovementIn,
                    StockMovementOut = indexedStockMovementOut,
                };
                return Response<StockMovementResponse>.Success(response1, "Data found");
            }
            catch (Exception ex)
            {
                return Response<StockMovementResponse>.Fail(new StockMovementResponse(), ex.Message);
            }

        }
    }
}
