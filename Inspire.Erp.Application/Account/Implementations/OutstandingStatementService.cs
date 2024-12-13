using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
   public class OutstandingStatementService: IOutstandingStatementService
    {
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IFileService _fileService;
        private readonly IRepository<CustomerMaster> _cusomerMaster;
        private readonly IRepository<SuppliersMaster> _supplierMaster;
        private readonly IRepository<StationMaster> _stationMaster;
        private readonly IRepository<JournalVoucher> _journalVoucher;
        private readonly IUtilityService _utilityService;
        public OutstandingStatementService(IRepository<AccountsTransactions> accountTransaction, IRepository<CustomerMaster> cusomerMaster,
            IFileService fileService, IRepository<SuppliersMaster> supplierMaster, IUtilityService utilityService, IRepository<StationMaster> stationMaster, IRepository<JournalVoucher> journalVoucher)
        {
            _accountTransaction = accountTransaction;
            _fileService = fileService;
            _supplierMaster = supplierMaster;
            _cusomerMaster = cusomerMaster;
            _utilityService = utilityService;
            _stationMaster = stationMaster;
            _journalVoucher = journalVoucher;
        }
        public async Task<Response<List<DropdownResponse>>> GetCustomerSupplierMasterGroup()
        {
            try
            {
                var response = new List<DropdownResponse>();
                response.Add(new DropdownResponse()
                {
                    Value = "",
                    Name = " All "
                }); 
                response.AddRange(await _cusomerMaster.ListSelectAsync(x => 1==1, x => new DropdownResponse
                {
                    Value = x.CustomerMasterCustomerReffAcNo,
                    Name = x.CustomerMasterCustomerName
                }));
                response.AddRange(await _supplierMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Value = x.SuppliersMasterSupplierReffAcNo,
                    Name = x.SuppliersMasterSupplierName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<OutStandingStatementResponse>> GetOutstandingStatementAccountTransactions(GenericGridViewModel model)
        {
            try
            {
                OutStandingStatementResponse gridmodel = new OutStandingStatementResponse();
                string query = @$" AccountsTransactionsAllocBalance >0  {model.Filter}";
                //var a = await _accountTransaction.GetAsQueryable().Where(x =>( x.AccountsTransactionsTransDate >= Convert.ToDateTime("02/21/2022")) && (x.AccountsTransactionsTransDate <= Convert.ToDateTime("02/21/2023")))
                //    .ToListAsync();
                var result = await _accountTransaction.GetAsQueryable().Where(query).OrderBy(row => row.AccountsTransactionsTransDate ?? DateTime.MaxValue)
                    .Select(x => new GetOutstandingStatementResponse
                    {
                    AccountsTransactionsAccNo = x.AccountsTransactionsAccNo,
                    AccountsTransactionsDebit = Convert.ToDecimal(x.AccountsTransactionsDebit),
                    AccountsTransactionsCredit = Convert.ToDecimal(x.AccountsTransactionsCredit),
                    AccountsTransactionsAllocBalance = x.AccountsTransactionsAllocBalance,
                    AccountsTransactionsVoucherNo = x.AccountsTransactionsVoucherNo,
                    AccountsTransactionsVoucherType = x.AccountsTransactionsVoucherType,
                    AccountsTransactionsDescription = x.AccountsTransactionsDescription,
                    AccountsTransactionsTransDate = Convert.ToDateTime(x.AccountsTransactionsTransDate),
                    AccountsTransactionsTransDateString = Convert.ToDateTime(x.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                    AccountsTransactionsParticulars = x.AccountsTransactionsParticulars,
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                }).ToListAsync();
                decimal runningBalance = 0;
                int ZeroToThirty = 0;
                int ThirtyOneToSixty = 0;
                int SixtyOneToNinety = 0;
                int NinetyOneToOneEighty = 0;
                int OneEightyToThreeSixty = 0;
                int aboveThreeSixty = 0;
                string accNo = "";
                gridmodel.Details = new List<GetOutstandingStatementResponse>();
                foreach (var accounts in result)
                {
                    decimal totalDebit = accounts.AccountsTransactionsDebit;
                    decimal totalCredit = accounts.AccountsTransactionsCredit;
                    runningBalance = (totalDebit - totalCredit) + (runningBalance);
                    gridmodel.Details.Add(new GetOutstandingStatementResponse()
                    {
                        AccountsTransactionsVoucherNo = accounts.AccountsTransactionsVoucherNo != null ? accounts.AccountsTransactionsVoucherNo:"" ,
                        AccountsTransactionsVoucherType = accounts.AccountsTransactionsVoucherType != null ? accounts.AccountsTransactionsVoucherType:"",
                        AccountsTransactionsDebit = accounts.AccountsTransactionsDebit,
                        AccountsTransactionsCredit=accounts.AccountsTransactionsCredit,
                        AccountsTransactionsTransDateString=accounts.AccountsTransactionsTransDateString,
                        AccountsTransactionsTransDate=accounts.AccountsTransactionsTransDate,
                        AccountsTransactionsDescription= accounts.AccountsTransactionsDescription != null ? accounts.AccountsTransactionsDescription:"",
                        AccountsTransactionsAllocBalance= accounts.AccountsTransactionsAllocBalance != null ? accounts.AccountsTransactionsAllocBalance:0,
                        AccountsTransactionsAllocDebit= accounts.AccountsTransactionsAllocDebit != null ? accounts.AccountsTransactionsAllocDebit:0,
                        RefNo=accounts.RefNo != null? accounts.RefNo:"",
                        AccountstransactionsTotalDays= Math.Round(((DateTime.Now- Convert.ToDateTime(accounts.AccountsTransactionsTransDate)).TotalDays)).ToString(),
                        AccountsTransactionsAccNo = accounts.AccountsTransactionsAccNo != null ? accounts.AccountsTransactionsAccNo:"",
          
                        AccountstransactionsRunningBalance = runningBalance.ToString()
                    });
                    accNo = accounts.AccountsTransactionsAccNo != null ? accounts.AccountsTransactionsAccNo : "";
                    var days= Math.Round(((DateTime.Now - Convert.ToDateTime(accounts.AccountsTransactionsTransDate)).TotalDays));
                    if (days >= 0 && days < 31)
                    {
                        ZeroToThirty += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                    else if (days >= 31 && days < 60)
                    {
                        ThirtyOneToSixty += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                    else if (days >= 61 && days < 90)
                    {
                        SixtyOneToNinety += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                    else if (days >= 91 && days < 180)
                    {
                        NinetyOneToOneEighty += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                    else if (days >= 181 && days < 360)
                    {
                        OneEightyToThreeSixty += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                    else if (days > 360)
                    {
                        aboveThreeSixty += Convert.ToInt32(accounts.AccountsTransactionsAllocBalance);
                    }
                }
                gridmodel.TotalRunningBalance = runningBalance.ToString();
                gridmodel.Currency = "AED";
                gridmodel.TotalRunningBalanceInWords = _utilityService.ConvertNumbertoWords(Convert.ToInt32(runningBalance));
                gridmodel.ZeroToThirty = ZeroToThirty.ToString();
                gridmodel.ThirtyOneToSixty = ThirtyOneToSixty.ToString();
                gridmodel.SixtyOneToNinety = SixtyOneToNinety.ToString();
                gridmodel.NinetyOneToOneEighty = NinetyOneToOneEighty.ToString();
                gridmodel.OneEightyToThreeSixty = OneEightyToThreeSixty.ToString();
                gridmodel.AboveThreeSixty = aboveThreeSixty.ToString();
                gridmodel.AccNo = accNo;
                var supplier = _supplierMaster.GetAsQueryable().FirstOrDefault(x => x.SuppliersMasterSupplierReffAcNo==accNo);
                if (supplier != null)
                {
                    gridmodel.AccountName = supplier.SuppliersMasterSupplierName;
                }
                else
                {
                    var customer = _cusomerMaster.GetAsQueryable().FirstOrDefault(x => x.CustomerMasterCustomerReffAcNo == accNo);
                    gridmodel.AccountName = customer != null?customer.CustomerMasterCustomerName:"";
                }
 

                return Response<OutStandingStatementResponse>.Success(gridmodel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<OutStandingStatementResponse>.Fail(new OutStandingStatementResponse(), ex.Message);
            }
        }


        public async Task<Response<OutStandingReportGridResponse>> GetOutstandingStatementAccountTransactions(OutStandingReprortFilter model)
        {
            try
            {
                OutStandingReportGridResponse gridmodel = new OutStandingReportGridResponse();
                var jvs = (from journalVoucher in _journalVoucher.GetAsQueryable()
                           where journalVoucher.JournalVoucherType == "I"
                           select journalVoucher.JournalVoucherVrefNo).ToList();

                var result = (from transaction in _accountTransaction.GetAsQueryable()
                              where transaction.AccountsTransactionsAccNo == model.accountNo
                                && transaction.AccountsTransactionsAllocBalance > 0
                                && !jvs.Contains(transaction.AccountsTransactionsVoucherNo)
                                && transaction.AccountsTransactionsVoucherType != "SalesReturn"
                              orderby transaction.AccountsTransactionsTransDate
                              select new
                              {
                                  AccountsTransactionsVoucherNo = transaction.AccountsTransactionsVoucherNo,
                                  AccountsTransactionsVoucherType = transaction.AccountsTransactionsVoucherType,
                                  AccountsTransactionsAccNo = transaction.AccountsTransactionsAccNo,
                                  AccountsTransactionsTransDate = transaction.AccountsTransactionsTransDate,
                                  AccountsTransactionsDescription = transaction.AccountsTransactionsDescription,
                                  RefNo = transaction.RefNo ?? "",
                                  AccountsTransactionsDebit = transaction.AccountsTransactionsDebit,
                                  AccountsTransactionsCredit = transaction.AccountsTransactionsCredit,
                                  settled = transaction.AccountsTransactionsDebit > transaction.AccountsTransactionsCredit ? transaction.AccountsTransactionsAllocDebit : transaction.AccountsTransactionsAllocDebit,
                                  Balance = transaction.AccountsTransactionsDebit > transaction.AccountsTransactionsCredit ? transaction.AccountsTransactionsAllocBalance : -1 * transaction.AccountsTransactionsAllocBalance,
                                  RunningBal = 0m,
                                  Days = (Convert.ToDateTime(model.fromDate).Date - transaction.AccountsTransactionsTransDate.Value.Date).Days
                              }).ToList();

                if (!model.isDateRange)
                    result = result.Where(x => x.AccountsTransactionsTransDate.Value.Date <= Convert.ToDateTime(model.fromDate).Date).ToList();
                else
                    result = result.Where(x => x.AccountsTransactionsTransDate.Value.Date >= Convert.ToDateTime(model.fromDate).Date && x.AccountsTransactionsTransDate.Value.Date <= Convert.ToDateTime(model.toDate).Date).ToList();
                decimal runningBalance = 0;
                int ZeroToThirty = 0;
                int ThirtyOneToSixty = 0;
                int SixtyOneToNinety = 0;
                int NinetyOneToOneEighty = 0;
                int OneEightyToThreeSixty = 0;
                int aboveThreeSixty = 0;
                string accNo = "";
                gridmodel.Details = new List<OutStandingReportGrid>();
                decimal? gTotal = 0m;
                foreach (var accounts in result)
                {
                    gTotal += accounts.Balance;
                    gridmodel.Details.Add(new OutStandingReportGrid()
                    {
                        AccountsTransactionsVoucherNo = accounts.AccountsTransactionsVoucherNo,
                        AccountsTransactionsVoucherType = accounts.AccountsTransactionsVoucherType,
                        AccountsTransactionsDebit = accounts.AccountsTransactionsDebit,
                        AccountsTransactionsCredit = accounts.AccountsTransactionsCredit,
                        AccountsTransactionsTransDate = Convert.ToDateTime(accounts.AccountsTransactionsTransDate).ToString("MM-dd-yyyy"),
                        AccountsTransactionsDescription = accounts.AccountsTransactionsDescription,
                        Balance = accounts.Balance,//AccountsTransactionsAllocBalance != null ? accounts.AccountsTransactionsAllocBalance : 0,
                        RefNo = accounts.RefNo,
                        Days = accounts.Days,
                        settled = accounts.settled,
                        AccountsTransactionsAccNo = accounts.AccountsTransactionsAccNo,
                        RunningBal = gTotal
                    });
                    accNo = accounts.AccountsTransactionsAccNo;
                    if (accounts.Days >= 0 && accounts.Days < 31)
                    {
                        ZeroToThirty = Convert.ToInt32(gTotal);
                    }
                    else if (accounts.Days >= 31 && accounts.Days < 60)
                    {
                        ThirtyOneToSixty = Convert.ToInt32(gTotal);
                    }
                    else if (accounts.Days >= 61 && accounts.Days < 90)
                    {
                        SixtyOneToNinety = Convert.ToInt32(gTotal);
                    }
                    else if (accounts.Days >= 91 && accounts.Days < 180)
                    {
                        NinetyOneToOneEighty = Convert.ToInt32(gTotal);
                    }
                    else if (accounts.Days >= 181 && accounts.Days < 360)
                    {
                        OneEightyToThreeSixty = Convert.ToInt32(gTotal);
                    }
                    else if (accounts.Days > 360)
                    {
                        aboveThreeSixty = Convert.ToInt32(gTotal);
                    }
                }
                gridmodel.TotalRunningBalance = runningBalance.ToString();
                gridmodel.Currency = "AED";
                gridmodel.TotalRunningBalanceInWords = _utilityService.ConvertNumbertoWords(Convert.ToInt32(runningBalance));
                gridmodel.ZeroToThirty = ZeroToThirty.ToString();
                gridmodel.ThirtyOneToSixty = ThirtyOneToSixty.ToString();
                gridmodel.SixtyOneToNinety = SixtyOneToNinety.ToString();
                gridmodel.NinetyOneToOneEighty = NinetyOneToOneEighty.ToString();
                gridmodel.OneEightyToThreeSixty = OneEightyToThreeSixty.ToString();
                gridmodel.AboveThreeSixty = aboveThreeSixty.ToString();
                gridmodel.AccNo = accNo;
                var supplier = _supplierMaster.GetAsQueryable().FirstOrDefault(x => x.SuppliersMasterSupplierReffAcNo == accNo);
                if (supplier != null)
                {
                    gridmodel.AccountName = supplier.SuppliersMasterSupplierName;
                }
                else
                {
                    var customer = _cusomerMaster.GetAsQueryable().FirstOrDefault(x => x.CustomerMasterCustomerReffAcNo == accNo);
                    gridmodel.AccountName = customer != null ? customer.CustomerMasterCustomerName : "";
                }


                return Response<OutStandingReportGridResponse>.Success(gridmodel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<OutStandingReportGridResponse>.Fail(new OutStandingReportGridResponse(), ex.Message);
            }
        }



        public async Task<Response<ReturnPrintResponse>> OutstandingStatementPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "OutstandingStatement";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetOutstandingStatementAccountTransactions(model);
                if (!dbResult.Valid)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string Html = string.Empty;
                string Css = string.Empty;
                switch (model.FormatType)
                {
                    case "FormatOne":
                        Html = GetHTMLStringFormatOne(dbResult.Result);
                        Css = "outstandingStatementFormatOneStyles.css";
                        break;

                    case "FormatTwo":
                        Html = GetHTMLStringFormatTwo(dbResult.Result);
                        Css = "outstandingStatementFormatOneStyles.css";
                        break;
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "Account", Css);
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = Html,
                    CssPath = cssPath,
                    OutputPath = outPutPath
                };
                var pdf = await _fileService.CreatePDFFromHtml(pDFResponse);
                if (pdf.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No PDF Generated");
                }
                if (model.Extension == "PDF")
                {
                    var downloadPDF = await _fileService.DownloadFile(pdf.Result);
                    var pdfContentType = await _fileService.GetContentType(pdf.Result);
                    response.stream = downloadPDF.Result;
                    response.ContentType = pdfContentType.Result;
                    response.Path = pdf.Result;
                    return Response<ReturnPrintResponse>.Success(response, "File Created");
                }
                var file = await _fileService.CreateFileFromExtension(new GetFileFromExtensionResponse()
                {
                    Extension = model.Extension,
                    Path = pdf.Result,
                    FileName = fileName
                });
                if (file.Result.Trim(' ') == "")
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No File Generated");
                }
                var download = await _fileService.DownloadFile(file.Result);
                var ContentType = await _fileService.GetContentType(file.Result);
                response.stream = download.Result;
                response.ContentType = ContentType.Result;
                response.Path = file.Result;
                return Response<ReturnPrintResponse>.Success(response, "File Created");
            }
            catch (Exception ex)
            {
                return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), ex.Message);
            }
        }
        private string GetHTMLStringFormatOne(OutStandingStatementResponse records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
    <div class=""card card-w-title"" >
        <div class=""col-12"">
            <h1><b>ACORTEC STRUCTURAL STEEL COATING & TREATMENT LLC</b><br>
                Office No. 288, Plot No-9, Block-1, Mussafah Ind Area (M-15) <br>
                Tel: +971-2-556-2248 , Fax: +971-2-556-2248 , Email:salesuae @acortec.net<br>
            </h1>
            <br><br><br>
            <h2> <b>Outstanding Details</b></h2>
            <h3> As On 26/03/2023</h3>
    
                <table class=""abc"">
                    <tr>
                        <th>Trans Date</th>
                        <th> Voucher No</th>
                        <th>LPONO</th>
                        <th>Amount</th>
                        <th>Settled</th>
                        <th> Balance</th>
                        <th>Running Bal </th>
                    </tr>");
            foreach (var item in results.Details)
            {
                sb.Append(@$"<tr>
                                    <td>{item.AccountsTransactionsVoucherType}</td>
                                    <td>{item.AccountsTransactionsVoucherNo}</td>
                                    <td>{item.AccountsTransactionsAccNo}</td>
                                    <td>{item.AccountsTransactionsAccName}</td>
                                    <td>{item.AccountsTransactionsParticulars}</td>
                                    <td>{item.AccountsTransactionsTransDateString}</td>
                                    <td>{item.AccountsTransactionsDebit}</td>
                                    <td>{item.AccountsTransactionsCredit}</td>
                                  </tr>");
            }
            sb.Append($@" </table>
            <p>A E D</p>
                    </div>
                    <br><br>
            <h2 class=""heading""><b>Ageing Details</b></h2>
        <div class=""row"">
           
            <div class=""column"" style=""background-color:#37993c;"" >
               0-31
            </div>
            <div class=""column"" style=""background-color:#37993c;"" >
               31-60
            </div>
            <div class=""column"" style=""background-color:#37993c;"" >
                61-90
            </div>
            <div class=""column"" style=""background-color:#37993c;"" >
                91-180
            </div>
            <div class=""column"" style=""background-color:#37993c;"" >
               181-360
            </div>
            <div class=""column"" style=""background-color:#37993c;"" >
                360
            </div>
        </div>
   
                            </div>
                            </body>
                        </html>");
            return sb.ToString();
        }
        private string GetHTMLStringFormatTwo(OutStandingStatementResponse records)
        {
            var results = records;
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Voucher Type</th>
                                        <th>Voucher Number</th>
                                        <th>Account Number</th>
                                        <th>Accout Name</th>
                                            <th>Particulars</th>
                                         <th>Date</th>
                                         <th>Debit</th>
                                         <th>Credit</th>
                                    </tr>");
            foreach (var item in results.Details)
            {
                sb.Append(@$"<tr>
                                    <td>{item.AccountsTransactionsVoucherType}</td>
                                    <td>{item.AccountsTransactionsVoucherNo}</td>
                                    <td>{item.AccountsTransactionsAccNo}</td>
                                    <td>{item.AccountsTransactionsAccName}</td>
                                    <td>{item.AccountsTransactionsParticulars}</td>
                                    <td>{item.AccountsTransactionsTransDateString}</td>
                                    <td>{item.AccountsTransactionsDebit}</td>
                                    <td>{item.AccountsTransactionsCredit}</td>
                                  </tr>");
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
