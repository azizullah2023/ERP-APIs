using Inspire.Erp.Application.Store.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Inspire.Erp.Infrastructure.Database;

namespace Inspire.Erp.Application.Store.Implementation
{
    public class StockLedger : IStockLedger
    {
        private InspireErpDBContext _context;
        private readonly IRepository<StockRegister> _sRegister;
        private readonly IRepository<ItemMaster> _iMster;
        private readonly IRepository<PurchaseVoucher> _pv;
        private readonly IRepository<PurchaseVoucherDetails> _pvDetail;
        private readonly IRepository<FinancialPeriods> _fP;

        public StockLedger(IRepository<StockRegister> sRegister, IRepository<ItemMaster> iMster, IRepository<PurchaseVoucherDetails> pvDetail, IRepository<PurchaseVoucher> pv, IRepository<FinancialPeriods> fP)
        {
            this._sRegister = sRegister;
            this._iMster = iMster;
            _pvDetail = pvDetail;
            _pv = pv;
            _fP = fP;
        }

        public async Task<Response<List<StockLedgerResponse>>> GetStockLedgerReport(StockFilterModel sFilter)
        {
            try
            {
                await Task.Delay(500);
                if (sFilter.LocationId == -1)
                {
                    sFilter.LocationId = null;
                }
                if (sFilter.IsDateChecked == true)
                {

                    var query = _iMster.GetAsQueryable();

                    if (sFilter.ItemId > 0)
                    {
                        query = query.Where(im => im.ItemMasterItemId == sFilter.ItemId && im.ItemMasterDelStatus != true);
                    }
                    if (sFilter.ItemGroupId > 0)
                    {
                        query = query.Where(im => im.ItemMasterAccountNo == sFilter.ItemGroupId && im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                     .OrderBy(im => im.ItemMasterItemId);
                    }
                    else
                    {
                        query = query.Where(im => im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                     .OrderBy(im => im.ItemMasterItemId);
                    }

                    var stock = _sRegister.GetAsQueryable();


                    var result = (from im in query
                                  join sr in stock on im.ItemMasterItemId equals (long)sr.StockRegisterMaterialID.GetValueOrDefault() into gimV1
                                  from y in gimV1.DefaultIfEmpty()
                                  where im.ItemMasterItemType == "A"
                                  && (sFilter.LocationId == null || y.StockRegisterLocationID == sFilter.LocationId)
                                  && (sFilter.PartNo == null || im.ItemMasterPartNo == sFilter.PartNo)
                                  && (y.StockRegisterVoucherDate >= sFilter.fromDate && y.StockRegisterVoucherDate <= sFilter.toDate)
                                  select new
                                  {
                                      im.ItemMasterItemId,
                                      im.ItemMasterItemName,
                                      y.StockRegisterSIN,
                                      y.StockRegisterSout,
                                      y.StockRegisterVoucherDate,
                                      y.StockRegisterAmount,
                                      im.ItemMasterPartNo,
                                      im.ItemMasterAccountNo,
                                  })
                                   .AsEnumerable().Select(x => new
                                   {
                                       Item_Id = x.ItemMasterItemId,
                                       Item_Name = x.ItemMasterItemName,
                                       StockIn = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0 && x.StockRegisterVoucherDate >= sFilter.fromDate && x.StockRegisterVoucherDate <= sFilter.toDate) ? x.StockRegisterSIN : 0,
                                       StockInAmount = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0 && x.StockRegisterVoucherDate >= sFilter.fromDate && x.StockRegisterVoucherDate <= sFilter.toDate) ? x.StockRegisterAmount ?? 0 : 0,
                                       StockOut = (x.StockRegisterSout != null && x.StockRegisterSout > 0 && x.StockRegisterVoucherDate >= sFilter.fromDate && x.StockRegisterVoucherDate <= sFilter.toDate) ? x.StockRegisterSout ?? 0 : 0,
                                       StockOutAmount = (x.StockRegisterSout != null && x.StockRegisterSout > 0 && x.StockRegisterVoucherDate >= sFilter.fromDate && x.StockRegisterVoucherDate <= sFilter.toDate) ? x.StockRegisterAmount ?? 0 : 0,
                                       OpenQty = (x.StockRegisterVoucherDate < sFilter.fromDate) ? (x.StockRegisterSIN ?? 0) - (x.StockRegisterSout ?? 0) : 0,
                                       OpenQtyAmount = (x.StockRegisterVoucherDate < sFilter.fromDate) ? (x.StockRegisterAmount ?? 0) - (x.StockRegisterAmount ?? 0) : 0,
                                       TotalBal = 0,
                                       TotalBalAmount = 0,
                                       ItemMasterPartNo = x.ItemMasterPartNo,
                                       ItemMasterAccountNo = x.ItemMasterAccountNo,
                                   }).GroupBy(
                         m => new { m.Item_Id, m.Item_Name },
                         (k, g) => new StockLedgerResponse()
                         {
                             ItemId = k.Item_Id.ToString(),
                             OpenQuantity = 0,
                             OpenValue = 0,
                             ReceivedQuantity = (decimal)g.Sum(x => x.StockIn),
                             ReceivedValue = (decimal)g.Sum(x => x.StockInAmount),
                             SaledQuantity = (decimal)g.Sum(x => x.StockOut),
                             SaledValue = (decimal)g.Sum(x => x.StockOutAmount),
                             ClosingQuantity = (decimal)g.Sum(a => a.StockIn) - g.Sum(a => a.StockOut),
                             ClosingValue = g.Sum(a => a.StockInAmount) - g.Sum(a => a.StockOutAmount),
                             RelativeNo = g.Max(x => x.ItemMasterPartNo),
                             PartNo = g.Max(x => x.ItemMasterAccountNo.ToString()),
                             ItemName = g.Max(x => x.Item_Name),
                             StockRate = (decimal)g.Max(x => x.StockInAmount),
                         }).ToList();

                    var openValues = getOpenValues(sFilter.fromDate, (int?)sFilter.ItemId);
                    if (openValues.Count > 0)
                    {
                        foreach (var opnV in openValues)
                        {
                            if (opnV.ItemId != null && opnV.ItemId != 0)
                            {
                                var matchedResult = result.FirstOrDefault(r => r.ItemId.ToString() == opnV.ItemId.ToString());

                                if (matchedResult != null)
                                {
                                    matchedResult.OpenQuantity = opnV.OpenQty.HasValue ? Convert.ToDecimal(opnV.OpenQty.Value) : 0;
                                    matchedResult.OpenValue = opnV.OpenValue.HasValue ? Convert.ToDecimal(opnV.OpenValue.Value) : 0;
                                }

                            }
                        }
                    }

                    if (sFilter.ExcludeZeroBalance == true)
                    {
                        result = result.Where(x => x.ClosingValue > 0).Select(x => x).ToList();
                    }
                    if (sFilter.ExcludeZeroOpeningBalance == true)
                    {
                        result = result.Where(x => x.OpenValue > 0).Select(x => x).ToList();
                    }
                    return Response<List<StockLedgerResponse>>.Success(result, "Data Found");

                }
                else
                {
                    var minFDate = _fP.GetAsQueryable().Select(x => x.FinancialPeriodsStartDate).SingleOrDefault();

                    var query = _iMster.GetAsQueryable();

                    if (sFilter.ItemId > 0)
                    {
                        query = query.Where(im => im.ItemMasterItemId == sFilter.ItemId && im.ItemMasterDelStatus != true);
                    }
                    if (sFilter.ItemGroupId > 0)
                    {
                        query = query.Where(im => im.ItemMasterAccountNo == sFilter.ItemGroupId && im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                     .OrderBy(im => im.ItemMasterItemId);
                    }
                    else
                    {
                        query = query.Where(im => im.ItemMasterItemType == "A" && im.ItemMasterDelStatus != true)
                                     .OrderBy(im => im.ItemMasterItemId);
                    }
                    var stock = _sRegister.GetAsQueryable();


                    var result = (from im in query
                                  join sr in stock on im.ItemMasterItemId equals (long)sr.StockRegisterMaterialID.GetValueOrDefault() into gimV1
                                  from y in gimV1.DefaultIfEmpty()
                                  where im.ItemMasterItemType == "A"
                                  && (sFilter.LocationId == null || y.StockRegisterLocationID == sFilter.LocationId)
                                  && (sFilter.PartNo == null || im.ItemMasterPartNo == sFilter.PartNo)                                  
                                  select new
                                  {
                                      im.ItemMasterItemId,
                                      im.ItemMasterItemName,
                                      y.StockRegisterSIN,
                                      y.StockRegisterSout,
                                      y.StockRegisterVoucherDate,
                                      y.StockRegisterAmount,
                                      im.ItemMasterPartNo,
                                      im.ItemMasterAccountNo,
                                  })
                                  .AsEnumerable().Select(x => new
                                  {
                                      Item_Id = x.ItemMasterItemId,
                                      Item_Name = x.ItemMasterItemName,
                                      StockIn = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0) ? x.StockRegisterSIN : 0,
                                      StockInAmount = (x.StockRegisterSIN != null && x.StockRegisterSIN > 0) ? x.StockRegisterAmount ?? 0 : 0,
                                      StockOut = (x.StockRegisterSout != null && x.StockRegisterSout > 0) ? x.StockRegisterSout ?? 0 : 0,
                                      StockOutAmount = (x.StockRegisterSout != null && x.StockRegisterSout > 0) ? x.StockRegisterAmount ?? 0 : 0,
                                      OpenQty = 0,
                                      OpenQtyAmount = 0,
                                      TotalBal = 0,
                                      TotalBalAmount = 0,
                                      ItemMasterPartNo = x.ItemMasterPartNo,
                                      ItemMasterAccountNo = x.ItemMasterAccountNo,
                                  }).GroupBy(
                        m => new { m.Item_Id, m.Item_Name },
                        (k, g) => new StockLedgerResponse()
                        {
                            ItemId = k.Item_Id.ToString(),
                            OpenQuantity = 0,
                            OpenValue = 0,
                            ReceivedQuantity = (decimal)g.Sum(x => x.StockIn),
                            ReceivedValue = (decimal)g.Sum(x => x.StockInAmount),
                            SaledQuantity = (decimal)g.Sum(x => x.StockOut),
                            SaledValue = (decimal)g.Sum(x => x.StockOutAmount),
                            ClosingQuantity = (decimal)g.Sum(a => a.StockIn) - g.Sum(a => a.StockOut),
                            ClosingValue = g.Sum(a => a.StockInAmount) - g.Sum(a => a.StockOutAmount),
                            RelativeNo = g.Max(x => x.ItemMasterPartNo),
                            PartNo = g.Max(x => x.ItemMasterAccountNo.ToString()),
                            ItemName = g.Max(x => x.Item_Name),
                            StockRate = (decimal)g.Max(x => x.StockInAmount),
                        }).ToList();


                    if (sFilter.ExcludeZeroBalance == true)
                    {
                        result = result.Where(x => x.ClosingValue > 0).Select(x => x).ToList();
                    }
                    if (sFilter.ExcludeZeroOpeningBalance == true)
                    {
                        result = result.Where(x => x.OpenValue > 0).Select(x => x).ToList();
                    }
                    return Response<List<StockLedgerResponse>>.Success(result, "Data Found");
                }
            }
            catch (Exception ex)
            {
                return Response<List<StockLedgerResponse>>.Fail(new List<StockLedgerResponse>(), ex.Message);
            }
        }

        private List<OpenValues> getOpenValues(DateTime? fromDate, int? itemId)
        {
            var query = _sRegister.GetAsQueryable()
                       .Where(a => fromDate == null || a.StockRegisterVoucherDate <= fromDate);

            if (itemId.HasValue)
            {
                query = query.Where(a => a.StockRegisterMaterialID == itemId.Value);
            }

            var result = query
                    .AsEnumerable()
                    .GroupBy(x => x.StockRegisterMaterialID)
                    .Select(g => new OpenValues
                    {
                        ItemId = g.Key,
                        OpenQty = g.Where(x => (x.StockRegisterSIN ?? 0) > 0 || (x.StockRegisterSout ?? 0) > 0)
                                   .Sum(x => (x.StockRegisterSIN ?? 0)) - g.Sum(x => (x.StockRegisterSout ?? 0)),
                        OpenValue = g.Where(x => (x.StockRegisterSIN ?? 0) > 0 || (x.StockRegisterSout ?? 0) > 0)
                                     .Sum(x => ((x.StockRegisterSIN ?? 0) * x.StockRegisterRate) - ((x.StockRegisterSout ?? 0) * x.StockRegisterRate))
                    })
                    .ToList();


            return result;
        }


        public List<StockValueResult> GetStockValue(long itemId, long itemGroupId)
        {
            var query = _iMster.GetAsQueryable();

            if (itemId > 0)
            {
                query = query.Where(im => im.ItemMasterItemId == itemId);
            }
            else if (itemGroupId > 0)
            {
                query = query.Where(im => im.ItemMasterAccountNo == itemGroupId && im.ItemMasterItemType == "A")
                             .OrderBy(im => im.ItemMasterItemId);
            }
            else
            {
                query = query.Where(im => im.ItemMasterItemType == "A")
                             .OrderBy(im => im.ItemMasterItemId);
            }


            var vDetailItems = (from d in _pvDetail.GetAsQueryable()
                                join p in _pv.GetAsQueryable() on d.PurchaseVoucherDetailsVoucherNo equals p.PurchaseVoucherVoucherNo
                                where p.PurchaseVoucherStatus == "P"
                                select d).ToList();

            var itemDetails = (from ss in query.ToList()
                               join a in vDetailItems on ss.ItemMasterItemId equals (long)a.PurchaseVoucherDetailsMaterialId
                               group a by a.PurchaseVoucherDetailsMaterialId into g
                               select new
                               {
                                   ItemId = g.Key,
                                   TotalQty = g.Sum(x => x.PurchaseVoucherDetailsQuantity)
                               }).ToList();


            var result = itemDetails.Select((x) => new StockValueResult
            {
                MatId = (long)x.ItemId,
                StockRate = (decimal)x.TotalQty > 0 ? getQuantityValue((long)x.ItemId) / (decimal)x.TotalQty : 0,
                StockValue = (decimal)x.TotalQty * ((decimal)x.TotalQty > 0 ? getQuantityValue((long)x.ItemId) / (decimal)x.TotalQty : 0)
            }).ToList();

            return result;
        }

        private decimal getQuantityValue(long itemId)
        {
            var res = (from d in _pvDetail.GetAsQueryable()
                       join p in _pv.GetAsQueryable() on d.PurchaseVoucherDetailsVoucherNo equals p.PurchaseVoucherVoucherNo
                       where p.PurchaseVoucherStatus == "P" && (int)d.PurchaseVoucherDetailsMaterialId == itemId
                       select new
                       {
                           Rete = (decimal)d.PurchaseVoucherDetailsRate,
                           Quantity = (decimal)d.PurchaseVoucherDetailsQuantity
                       }).ToList().Sum(x => x.Rete * x.Quantity);
            return res;
        }

    }

    public class StockValueResult
    {
        public long MatId { get; set; }
        public decimal StockRate { get; set; }
        public decimal StockValue { get; set; }
    }

    public class OpenValues
    {
        public int? ItemId { get; set; }
        public decimal? OpenQty { get; set; }
        public decimal? OpenValue { get; set; }
    }
}
