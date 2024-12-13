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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Policy;

namespace Inspire.Erp.Application.Sales.Implementations
{
    public class PosReportService : IPosReportService
    {
        private readonly InspireErpDBContext _dbContext;

        public PosReportService(InspireErpDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCash(int wpId, int stationId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                fromDate = fromDate.Date; toDate = toDate.Date;

                var ab = await (from c in _dbContext.POS_SalesVoucher
                                join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                                where d.Amount != 0 && d.PaymentMode == "CAS" && c.WorkPeriodID == wpId
                                && c.StationID == stationId && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                orderby d.Id
                                select new POS_SummaryDateWiseReport
                                {
                                    VoucherNo = c.VoucherNo,
                                    PaymentMode = c.Voucher_Type,
                                    Discount = c.Discount ?? 0m,
                                    Amount = d.Amount ?? 0m,
                                    GrossAmount = c.GrossAmount ?? 0m,
                                    VatAmount = c.VatAmount ?? 0m
                                }).ToListAsync();

                var ac = await (from c in _dbContext.POS_SalesVoucher
                                join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                                where d.PaymentMode == "CAS" && d.Amount == 0
                                && c.WorkPeriodID == wpId && c.StationID == stationId
                                  && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                orderby d.Id
                                select new POS_SummaryDateWiseReport
                                {
                                    VoucherNo = c.VoucherNo,
                                    PaymentMode = c.Voucher_Type,
                                    Discount = c.Discount ?? 0m,
                                    Amount = d.Amount ?? 0m,
                                    GrossAmount = c.GrossAmount ?? 0m,
                                    VatAmount = c.VatAmount ?? 0m
                                }).ToListAsync();

                var result = ab.Union(ac).OrderBy(a => a.VoucherNo).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCard(int wpId, int stationId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                fromDate = fromDate.Date; toDate = toDate.Date;
                var ab = await (from c in _dbContext.POS_SalesVoucher
                                join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                                where d.Amount != 0 && d.PaymentMode == "CRD" && c.WorkPeriodID == wpId
                                && c.StationID == stationId && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                select new POS_SummaryDateWiseReport
                                {
                                    VoucherNo = c.VoucherNo,
                                    PaymentMode = c.Voucher_Type,
                                    Discount = c.Discount ?? 0m,
                                    Amount = d.Amount ?? 0m,
                                    GrossAmount = c.GrossAmount ?? 0m,
                                    VatAmount = c.VatAmount ?? 0m
                                }).ToListAsync();

                var ac = await (from c in _dbContext.POS_SalesVoucher
                                join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                                where d.PaymentMode == "CRD" && d.Amount == 0 && c.WorkPeriodID == wpId
                                && c.StationID == stationId && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                orderby d.Id
                                select new POS_SummaryDateWiseReport
                                {
                                    VoucherNo = c.VoucherNo,
                                    PaymentMode = c.Voucher_Type,
                                    Discount = c.Discount ?? 0m,
                                    Amount = d.Amount ?? 0m,
                                    GrossAmount = c.GrossAmount ?? 0m,
                                    VatAmount = c.VatAmount ?? 0m
                                }).ToListAsync();

                var result = ab.Union(ac).OrderBy(a => a.VoucherNo).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<POS_MaintodaySales> GetTodaySalesByDate(int wpId, int stationId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                fromDate = fromDate.Date; toDate = toDate.Date;

                //var result = await (from c in _dbContext.POS_SalesVoucher
                //                    join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                //                    where c.WorkPeriodID == wpId && c.StationID == stationId
                //                    && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                //                    select new POS_todaySalesByDate
                //                    {
                //                        VoucherNo = c.VoucherNo,
                //                        CustomerName = c.CustomerName,
                //                        PaymentMode = c.Voucher_Type,
                //                        Discount = (d.PaymentMode.Equals("DISNT") ? (decimal)d.Amount : 0m),
                //                        NetAmount = (d.PaymentMode.Equals("NET") ? (decimal)d.Amount : 0m),
                //                        GrossAmount = (d.PaymentMode.Equals("CASH") ? (decimal)d.Amount : d.PaymentMode.Equals("CARD") ? (decimal)d.Amount : 0m),
                //                        VatAmount = (d.PaymentMode.Equals("VAT") ? (decimal)d.Amount : 0m),
                //                        VoucherDate = c.VoucherDate
                //                    }).Distinct().ToListAsync();
                
                var rawData = await (from c in _dbContext.POS_SalesVoucher
                                     join d in _dbContext.POS_SalesTransactionDetails on c.VoucherNo equals d.InvoiceNo
                                     where c.WorkPeriodID == wpId && c.StationID == stationId
                                     && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                     select new
                                     {
                                         c.VoucherNo,
                                         c.CustomerName,
                                         c.Voucher_Type,
                                         d.PaymentMode,
                                         d.Amount,
                                         c.VoucherDate
                                     }).ToListAsync();

               
                var result = rawData.GroupBy(
                    x => new { x.VoucherNo, x.CustomerName, x.Voucher_Type, x.VoucherDate })
                    .Select(g => new POS_todaySalesByDate
                    {
                        VoucherNo = g.Key.VoucherNo,
                        CustomerName = g.Key.CustomerName,
                        PaymentMode = g.Key.Voucher_Type,
                        Discount = g.Where(x => x.PaymentMode == "DISNT").Sum(x => (decimal?)x.Amount) ?? 0m,
                        NetAmount = g.Where(x => x.PaymentMode == "NET").Sum(x => (decimal?)x.Amount) ?? 0m,
                        GrossAmount = g.Where(x => x.PaymentMode == "CASH" || x.PaymentMode == "CARD").Sum(x => (decimal?)x.Amount) ?? 0m,
                        VatAmount = g.Where(x => x.PaymentMode == "VAT").Sum(x => (decimal?)x.Amount) ?? 0m,
                        VoucherDate = g.Key.VoucherDate
                    }).ToList();


                var result1 = await (from c in _dbContext.POS_SalesVoucher
                                     join d in _dbContext.POS_SalesVoucherDetails on c.VoucherNo equals d.VoucherNo
                                     join z in _dbContext.ItemMaster on d.ItemId equals z.ItemMasterItemId
                                     join u in _dbContext.UnitMaster on d.UnitId equals u.UnitMasterUnitId
                                     where c.WorkPeriodID == wpId && c.StationID == stationId
                                     && c.VoucherDate.Value.Date >= fromDate && c.VoucherDate.Value.Date <= toDate
                                     group new { d, z, u } by new { z.ItemMasterItemName, u.UnitMasterUnitShortName } into g
                                     select new POS_todaySalesDetailByDate
                                     {
                                         ItemMasterItemName = g.Key.ItemMasterItemName,
                                         Unit = g.Key.UnitMasterUnitShortName,
                                         TotalSoldQty = g.Sum(x => x.d.Sold_Qty ?? 0m),
                                         TotalAmount = g.Sum(x => x.d.NetAmount ?? 0m)
                                     }).ToListAsync();

                var response = new POS_MaintodaySales
                {
                    POS_todaySalesByDate = result,
                    POS_todaySalesDetailByDate = result1,
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TodaySalesByCounterWise>> GetTodaySalesByCounterWise(DateTime fromDate, DateTime toDate)
        {
            try
            {
                fromDate = fromDate.Date; toDate = toDate.Date;


                var Cashtotal = await _dbContext.POS_SalesTransactionDetails
                    .Where(a => a.PaymentMode == "CAS" && a.Amount > 0 && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate.Value.Date <= toDate)
                    .GroupBy(c => c.CounterName).Select(x => new TodaySalesByCounterWise
                    {
                        CounterName = x.Key,
                        Cash = x.Sum(c => c.Amount ?? 0m)
                    }).ToListAsync();


                var Cardtotal = await (from d in _dbContext.POS_SalesTransactionDetails
                                       join c in _dbContext.POS_TransactionCodes on d.PaymentMode equals c.TransCode
                                       join m in _dbContext.POS_Trans_Group_Master on c.TransGroup equals m.Id
                                       where d.PaymentMode == "CRD" && d.Amount > 0 && d.InvoiceDate.Value.Date >= fromDate && d.InvoiceDate.Value.Date <= toDate && c.TransGroup == 3
                                       group d by d.CounterName into g
                                       select new TodaySalesByCounterWise
                                       {
                                           CounterName = g.Key,
                                           Card = g.Sum(d => d.Amount ?? 0m)
                                       }).ToListAsync();

                var Credittotal = await _dbContext.POS_SalesTransactionDetails
                                         .Where(a => a.PaymentMode == "CREDIT" && a.Amount > 0 && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate <= toDate)
                                         .GroupBy(c => c.CounterName).Select(x => new TodaySalesByCounterWise
                                         {
                                             CounterName = x.Key,
                                             Credit = x.Sum(c => c.Amount ?? 0m)
                                         }).ToListAsync();

                var Returntotal = await (from c in _dbContext.POS_SalesTransactionDetails
                                         join r in _dbContext.POS_SalesRet_Master on c.InvoiceNo equals r.Narration
                                         where c.Amount < 0 && c.InvoiceDate.Value.Date >= fromDate && c.InvoiceDate.Value.Date <= toDate
                                         group c by c.CounterName into g
                                         select new TodaySalesByCounterWise
                                         {
                                             CounterName = g.Key,
                                             Return = g.Sum(c => c.Amount ?? 0m)
                                         }).ToListAsync();

                var CreditReturntotal = await _dbContext.POS_SalesTransactionDetails
                                         .Where(a => a.PaymentMode == "CREDIT" && a.Amount < 0 && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate.Value.Date <= toDate)
                                         .GroupBy(c => c.CounterName).Select(x => new TodaySalesByCounterWise
                                         {
                                             CounterName = x.Key,
                                             Credit = x.Sum(c => c.Amount ?? 0m)
                                         }).ToListAsync();


                var salesTotal = await _dbContext.POS_SalesTransactionDetails
                  .Where(a => a.PaymentMode == "BIL" && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate.Value.Date <= toDate)
                  .GroupBy(c => c.CounterName).Select(g => new TodaySalesByCounterWise
                  {
                      CounterName = g.Key,
                      Sales = g.Sum(x => x.Amount ?? 0m)
                  }).ToListAsync();


                var DiscountTotal = await _dbContext.POS_SalesTransactionDetails
                  .Where(a => a.PaymentMode == "DISNT" && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate.Value.Date <= toDate)
                  .GroupBy(c => c.CounterName).Select(x => new TodaySalesByCounterWise
                  {
                      CounterName = x.Key,
                      Vat = x.Sum(c => c.Amount ?? 0m)
                  }).ToListAsync();


                var VatTotal = await _dbContext.POS_SalesTransactionDetails
                   .Where(a => a.PaymentMode == "VAT" && a.InvoiceDate.Value.Date >= fromDate && a.InvoiceDate.Value.Date <= toDate)
                   .GroupBy(c => c.CounterName).Select(x => new TodaySalesByCounterWise
                   {
                       CounterName = x.Key,
                       Vat = x.Sum(c => c.Amount ?? 0m)
                   }).ToListAsync();

                var result = Cashtotal.Union(Cardtotal)
                    .Union(Credittotal).Union(Returntotal)
                    .Union(CreditReturntotal).Union(salesTotal)
                    .Union(DiscountTotal).Union(VatTotal)
                    .GroupBy(x => x.CounterName)
                    .Select(g => new TodaySalesByCounterWise
                    {
                        CounterName = g.Key,
                        Cash = g.Sum(x => x.Cash),
                        Card = g.Sum(x => x.Card),
                        Credit = g.Sum(x => x.Credit),
                        Return = g.Sum(x => x.Return),
                        ReturnCredit = g.Sum(x => x.ReturnCredit),
                        Discount = g.Sum(x => x.Discount),
                        Sales = g.Sum(x => x.Sales),
                        Vat = g.Sum(x => x.Vat)

                    }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}