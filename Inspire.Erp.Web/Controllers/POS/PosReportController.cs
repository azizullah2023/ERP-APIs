using AutoMapper;
using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.POS
{
    [Route("api/pos/item")]
    [Produces("application/json")]
    [ApiController]
    public class PosReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ISalesVoucherService _salesVoucherService;
        private IPosReportService _posReportService;
        private readonly IloginService _loginsvc;
        private IRepository<PosStationSettings> stationSettingsRepo;
        public PosReportController(IMapper mapper,
            ISalesVoucherService salesVoucherService, IloginService loginsvc, IPosReportService posReportService)
        {

            _mapper = mapper;
            _salesVoucherService = salesVoucherService;
            _loginsvc = loginsvc;
            _posReportService = posReportService;
        }


        [HttpPost]
        [Route("GetSummaryDateWise")]
        public async Task<PrintData> GetSummaryDateWise(PosReportFilterModel model)
        {
            var stationSett = stationSettingsRepo.GetAsQueryable().FirstOrDefault(o => o.Id == 1);

            System.Diagnostics.Process OSK;
            string eClear = Strings.Chr(27) + "@";
            string eCentre = Strings.Chr(27) + Strings.Chr(97) + "1";
            string eLeft = Strings.Chr(27) + Strings.Chr(97) + "0";
            string eRight = Strings.Chr(27) + Strings.Chr(97) + "2";
            string eDrawer = eClear + Strings.Chr(27) + "p" + Strings.Chr(0) + ".}";
            string eCut = Strings.Chr(27) + "i" + Constants.vbCrLf;
            string eSmlText = Strings.Chr(27) + "!" + Strings.Chr(1);
            string eNmlText = Strings.Chr(27) + "!" + Strings.Chr(0);
            string eInit = eNmlText + Strings.Chr(13) + Strings.Chr(27) + "c6" + Strings.Chr(1) + Strings.Chr(27) + "R3" + Constants.vbCrLf;
            string eBigCharOn = Strings.Chr(27) + "!" + Strings.Chr(56);
            string eBigCharOff = Strings.Chr(27) + "!" + Strings.Chr(0);
            string CreateNewLine;
            string eBigCharOn1 = Strings.Chr(27) + "!" + Strings.Chr(50);
            string eBigCharOn2 = Strings.Chr(27) + "!" + Strings.Chr(40);
            string eBigCharOn3 = Strings.Chr(27) + "!" + Strings.Chr(33);

            DateTime dateTime = DateTime.Now;
            DateTimeFormatInfo dateformat = new DateTimeFormatInfo { MonthDayPattern = "MMMM" };
            string strdate = dateTime.ToString("MMMM,dd,yyyy HH:mm", dateformat);

            StringBuilder strRen = new StringBuilder();
            strRen.AppendLine(eBigCharOff + "---------------------");
            strRen.AppendLine(eCentre + eBigCharOn3 + " Report Name");
            strRen.AppendLine(eCentre + eBigCharOn3 + " " + strdate);
            strRen.AppendLine(eBigCharOff + "---------------------");

            if (!string.IsNullOrEmpty(stationSett.PrintHeader1))
            {
                strRen.AppendLine(eCentre + eBigCharOn2 + stationSett.PrintHeader1);
            }
            if (!string.IsNullOrEmpty(stationSett.PrintHeader2))
            {
                strRen.AppendLine(eCentre + eBigCharOn3 + stationSett.PrintHeader2);
            }
            if (!string.IsNullOrEmpty(stationSett.PrintHeader3))
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintHeader3);
            }
            if (!string.IsNullOrEmpty(stationSett.PrintHeader4))
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintHeader4);
            }
            strRen.AppendLine(eBigCharOff + "-------------------");
            strRen.AppendLine(eCentre + eBigCharOn2 + " Cash Payment");
            strRen.AppendLine(eBigCharOff + "-------------------");


            strRen.AppendLine(eBigCharOff + "--------------------");
            strRen.AppendLine(eCentre + " BillNo    Payment   Total");
            strRen.AppendLine(eBigCharOff + "--------------------");

            var CashsalesDetails = await this.GetSalesTransactionCash(model);

            decimal CashAmount = 0;
            decimal CashDiscount = 0;
            decimal SGrossAmount = 0;
            decimal CashVatAmount = 0;
           
            foreach (var item in CashsalesDetails)
            {
                strRen.AppendLine($"       {item.VoucherNo}      {item.PaymentMode}      {item.Amount}");

                CashAmount += item.Amount;
                CashDiscount += item.Discount;
                SGrossAmount += item.GrossAmount;
                CashVatAmount += item.VatAmount;
            }

            strRen.AppendLine(eBigCharOff + "---------------");
            strRen.AppendLine(eCentre + $" Total     {CashAmount}");
            strRen.AppendLine(eBigCharOff + "---------------");

            strRen.AppendLine(eBigCharOff + "----------------");
            strRen.AppendLine(eCentre + eBigCharOn2 + " Card Payment");
            strRen.AppendLine(eBigCharOff + "-----------------");

            strRen.AppendLine(eBigCharOff + "--------------------");
            strRen.AppendLine(eCentre + " BillNo    Payment   Total");
            strRen.AppendLine(eBigCharOff + "--------------------");

            var CardsalesDetails = await this.GetSalesTransactionCard(model);
            decimal CardAmount = 0;
            decimal CardDiscount = 0;
            decimal DGrossAmount = 0;
            decimal CardVatAmount = 0;

           
            foreach (var item in CardsalesDetails)
            {
                strRen.AppendLine($"       {item.VoucherNo}     {item.PaymentMode}    {item.Amount}");
                CardAmount += item.Amount;
                CardDiscount += item.Discount;
                DGrossAmount += item.VatAmount;
                CardVatAmount += item.VatAmount;
            }

            strRen.AppendLine(eBigCharOff + "---------------");
            strRen.AppendLine(eCentre + $" Total     {CardAmount}");
            strRen.AppendLine(eBigCharOff + "---------------");


            strRen.AppendLine("------------------");
            strRen.AppendLine($" GrossAmount  : {SGrossAmount + DGrossAmount}");
            strRen.AppendLine($" VATAmount    : {CashVatAmount + CardVatAmount}");
            strRen.AppendLine($" Discount     : {CashDiscount + CardDiscount}");
            strRen.AppendLine($" Total        : {CashAmount + CardAmount}");


            strRen.AppendLine(eBigCharOff + "-----------------------------------------------");

            if (stationSett.PrintFooter1 != "")
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintFooter1);
            }
            if (stationSett.PrintFooter2 != "")
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintFooter2);
            }
            if (stationSett.PrintFooter3 != "")
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintFooter3);
            }
            if (stationSett.PrintFooter4 != "")
            {
                strRen.AppendLine(eCentre + eBigCharOff + stationSett.PrintFooter4);
            }


            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine(eCut);

            string path = "C:\\Users\\user\\Desktop\\New Text Document.txt";
            await System.IO.File.WriteAllTextAsync(path, strRen.ToString());



            // RawPrinterHelper.SendStringToPrinter(PrinterNameRen, StrRen.ToString);
            return new PrintData
            {
                PrinterName = stationSett.PrinterName,
                Data = strRen.ToString()
            };

        }

        [HttpPost]
        [Route("GetSalesTransactionCash")]
        public async Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCash(PosReportFilterModel model)
        {
            return await _posReportService.GetSalesTransactionCash(model.wpId, model.stationId, model.fromDate, model.toDate);

        }

        [HttpPost]
        [Route("GetSalesTransactionCard")]
        public async Task<List<POS_SummaryDateWiseReport>> GetSalesTransactionCard(PosReportFilterModel model)
        {
            return await _posReportService.GetSalesTransactionCard(model.wpId, model.stationId, model.fromDate, model.toDate);

        }

        [HttpPost]
        [Route("GetTodaySalesByDate")]
        public async Task<POS_MaintodaySales> GetTodaySalesByDate(PosReportFilterModel model)
        {
            return await _posReportService.GetTodaySalesByDate(model.wpId, model.stationId, model.fromDate, model.toDate);

        }
        [HttpPost]
        [Route("GetTodaySalesByCounterWise")]
        public async Task<PrintData> GetTodaySalesByCounterWise(PosReportFilterModel model)
        {

            var stationSett = stationSettingsRepo.GetAsQueryable().FirstOrDefault(o => o.Id == 1);

            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo
            {
                MonthDayPattern = "MMMM"
            };
            string strDateFullMonth = model.fromDate.ToString("MMMM,dd,yyyy, HH:mm", dateFormat);

            string strDateOnly = model.fromDate.ToString("yyyy-MM-dd");

            System.Diagnostics.Process OSK;
            string eClear = Strings.Chr(27) + "@";
            string eCentre = Strings.Chr(27) + Strings.Chr(97) + "1";
            string eLeft = Strings.Chr(27) + Strings.Chr(97) + "0";
            string eRight = Strings.Chr(27) + Strings.Chr(97) + "2";
            string eDrawer = eClear + Strings.Chr(27) + "p" + Strings.Chr(0) + ".}";
            string eCut = Strings.Chr(27) + "i" + Constants.vbCrLf;
            string eSmlText = Strings.Chr(27) + "!" + Strings.Chr(1);
            string eNmlText = Strings.Chr(27) + "!" + Strings.Chr(0);
            string eInit = eNmlText + Strings.Chr(13) + Strings.Chr(27) + "c6" + Strings.Chr(1) + Strings.Chr(27) + "R3" + Constants.vbCrLf;
            string eBigCharOn = Strings.Chr(27) + "!" + Strings.Chr(56);
            string eBigCharOff = Strings.Chr(27) + "!" + Strings.Chr(0);
            string CreateNewLine;
            string eBigCharOn1 = Strings.Chr(27) + "!" + Strings.Chr(50);
            string eBigCharOn2 = Strings.Chr(27) + "!" + Strings.Chr(40);
            string eBigCharOn3 = Strings.Chr(27) + "!" + Strings.Chr(33);
            var strRen = new StringBuilder();


            strRen.AppendLine(eBigCharOff + "------------------------------------------");
            strRen.AppendLine(eCentre + eBigCharOn3 + "All Counters Summary Report");
            strRen.AppendLine(eBigCharOff + "------------------------------------------");

            //strRen.AppendLine(eCentre + eBigCharOn3 + stationSett.StationName);
            strRen.AppendLine(eCentre + eBigCharOn3 + strDateFullMonth);
            strRen.AppendLine(eCentre + eBigCharOn3 + strDateOnly);

            strRen.AppendLine(eBigCharOff + "-----------------------------------------------");

            strRen.AppendLine(eBigCharOff + "-----------------------------------------------");
            strRen.AppendLine(eCentre + " Payment Mode    Total");
            strRen.AppendLine(eBigCharOff + "-----------------------------------------------");

            var CashsalesDetails = await _posReportService.GetTodaySalesByCounterWise(model.fromDate, model.toDate);


            decimal totalCash = 0;
            decimal totalCard = 0;
            decimal totalCredit = 0;
            decimal totalReturn = 0;
            decimal totalReturnCredit = 0;
            decimal totalSales = 0;
            decimal totalDiscount = 0;
            decimal totalVat = 0;

            foreach (var item in CashsalesDetails)
            {

                totalCash += item.Cash;
                totalCard += item.Card;
                totalCredit += item.Credit;
                totalReturn += item.Return;
                totalReturnCredit += item.ReturnCredit;
                totalSales += item.Sales;
                totalDiscount += item.Discount;
                totalVat += item.Vat;

                strRen.AppendLine($" Total Cash     : {totalCash}");
                strRen.AppendLine($" Total Card     : {totalCard}");
                strRen.AppendLine($" Total Credit   : {totalCredit}");
                strRen.AppendLine($" Sales Return   : {totalReturn}");
                strRen.AppendLine($" Return-Credit  : {totalReturnCredit}");
                strRen.AppendLine($"Total Sales     : {totalSales}");
                strRen.AppendLine($" Discount       : {totalDiscount}");
                strRen.AppendLine($"Vat Payable     : {totalVat}");

                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");
                strRen.AppendLine($"Grand Total     : {totalCash + totalCard}");
                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");


            }

            foreach (var item in CashsalesDetails)
            {
                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");
                strRen.AppendLine(eCentre + eBigCharOn3 + item.CounterName);
                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");

                strRen.AppendLine($" Total Cash     : {item.Card}");
                strRen.AppendLine($" Total Card     : {item.Cash}");
                strRen.AppendLine($" Total Credit   : {item.Credit}");
                strRen.AppendLine($" Sales Return   : {item.Return}");
                strRen.AppendLine($" Return-Credit  : {item.ReturnCredit}");
                strRen.AppendLine($"Total Sales     : {item.Sales}");
                strRen.AppendLine($" Discount       : {item.Discount}");
                strRen.AppendLine($"Vat Payable     : {item.Vat}");

                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");
                strRen.AppendLine($"Grand Total     : {item.Card + item.Card}");
                strRen.AppendLine(eBigCharOff + "-----------------------------------------------");
            }

            strRen.AppendLine("-------- END");



            string path = "C:\\Users\\user\\Desktop\\New Text Document.txt";
            await System.IO.File.WriteAllTextAsync(path, strRen.ToString());




            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine("");
            strRen.AppendLine(eCut);

            return new PrintData
            {
                PrinterName = stationSett.PrinterName,
                Data = strRen.ToString()
            };

        }

    }
}