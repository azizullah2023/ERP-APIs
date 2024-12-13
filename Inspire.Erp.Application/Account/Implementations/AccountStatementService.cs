using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.AccountStatement;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Account.Implementations
{
    public class AccountStatementService : IAccountStatementService
    {
        private readonly IRepository<MasterAccountsTable> _masterAccountsTable;
        private readonly IRepository<AccountsTransactions> _accountTransaction;
        private readonly IRepository<JobMaster> _jobMaster;
        private readonly IRepository<StationMaster> _locationMaster;
        private readonly IFileService _fileService;
        public AccountStatementService(IRepository<MasterAccountsTable> masterAccountsTable, IRepository<JobMaster> jobMaster,
            IRepository<AccountsTransactions> accountTransaction,IFileService fileService, IRepository<StationMaster> locationMaster)
        {
            _masterAccountsTable = masterAccountsTable;
            _accountTransaction = accountTransaction;
            _jobMaster = jobMaster;
            _locationMaster = locationMaster;
            _fileService = fileService;
        }
        public async Task<Response<List<DropdownResponse>>> GetAccountMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Value = "",
                //    Name = "All"
                //});
                 response.AddRange( await _masterAccountsTable.ListSelectAsync(x => x.MaAccType=="A", x => new DropdownResponse
                {
                    Value = x.MaAccNo,
                    Name = x.MaAccName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<List<DropdownResponse>>> GetJobMasterDropdown()
        {
            try
            {
                var response = new List<DropdownResponse>();
                //response.Add(new DropdownResponse()
                //{
                //    Id = 0,
                //    Name = "All"
                //});
                response.AddRange(await _jobMaster.ListSelectAsync(x => 1 == 1, x => new DropdownResponse
                {
                    Id =x.JobMasterJobId != null? Convert.ToInt32(x.JobMasterJobId):0,
                    Name = x.JobMasterJobName
                }));
                return Response<List<DropdownResponse>>.Success(response, "Data found");
            }
            catch (Exception ex)
            {
                return Response<List<DropdownResponse>>.Fail(new List<DropdownResponse>(), ex.Message);
            }
        }
        public async Task<Response<GetAccountStatementResponse>> GetAccountTransactions(GenericGridViewModel model)
        {
            try
            {
                GetAccountStatementResponse gridModel = new GetAccountStatementResponse();
                var location = await _locationMaster.GetAsQueryable().FirstOrDefaultAsync();
                gridModel.Details = new List<GetAccountTransactionResponse>();
                var resultData =  _accountTransaction.GetBySP(@$" exec GetAccountTransactions {model.Skip},{model.Take},{model.Search},{model.Field},{model.Dir},{model.Filter},{model.Total}");
                var result = resultData.Select(x => new GetAccountTransactionResponse
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
                    RefNo = x.RefNo,
                    AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                    AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                    AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                    // Rest of the properties...
                }).ToList();

                string accName = string.Empty;
                string accno = string.Empty;
                decimal totalCredit = 0;
                decimal total = 0;
                foreach (var x in result)
                {
                    accno = x.AccountsTransactionsAccNo;
                    totalCredit += x.AccountsTransactionsCredit;
                    total += Convert.ToDecimal(x.AccountsTransactionsAllocBalance);
                    var accounts = _masterAccountsTable.GetAsQueryable().FirstOrDefault(c => c.MaAccNo == x.AccountsTransactionsAccNo);
                    accName = accounts != null?accounts.MaAccName:"";
                    gridModel.Details.Add(new GetAccountTransactionResponse()
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
                        RefNo = x.RefNo,
                        AccountsTransactionsTransSno = Convert.ToInt32(x.AccountsTransactionsTransSno),
                        AccountsTransactionsAllocDebit = x.AccountsTransactionsAllocDebit,
                        AccountsTransactionsAllocCredit = x.AccountsTransactionsAllocCredit
                    });
                }
                gridModel.AccName = accName;
                gridModel.AccNo = accno;
                gridModel.Total = total.ToString();
                gridModel.CreditBalance = totalCredit.ToString();
                gridModel.StationMasterStationName = location != null?location.StationMasterStationName:"";
                gridModel.StationMasterEmail = location != null ? location.StationMasterEmail : "";
                gridModel.StationMasterFax = location != null ? location.StationMasterFax : "";
                gridModel.StationMasterTele1 = location != null ? location.StationMasterTele1 : "";
                gridModel.StationMasterAddress = location != null ? location.StationMasterAddress : "";
                gridModel.StationMasterCity = location != null ? location.StationMasterCity : "";
                gridModel.StationMasterCode = location != null ? location.StationMasterCode : 0;
                gridModel.StationMasterCountry = location != null ? location.StationMasterCountry : "";
                gridModel.StationMasterLogoPath = location != null ? location.StationMasterLogoPath : "";
                gridModel.StationMasterPostOffice = location != null ? location.StationMasterPostOffice : "";
                gridModel.StationMasterSignPath = location != null ? location.StationMasterSignPath : "";
                gridModel.StationMasterVatNo = location != null ? location.StationMasterVatNo : "";
                gridModel.StationMasterWebSite = location != null ? location.StationMasterWebSite : "";
                return Response<GetAccountStatementResponse>.Success(gridModel, "Data found");
            }
            catch (Exception ex)
            {
                return Response<GetAccountStatementResponse>.Fail(new GetAccountStatementResponse(), ex.Message);
            }
        }
        public async Task<Response<ReturnPrintResponse>> AccountSTatementPrint(GenericGridViewModel model)
        {
            try
            {
                string fileName = "AccountStatement";
                ReturnPrintResponse response = new ReturnPrintResponse();
                var dbResult = await GetAccountTransactions(model);
                if (dbResult.Result.Details.Count < 1)
                {
                    return Response<ReturnPrintResponse>.Fail(new ReturnPrintResponse(), "No records");
                }
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Styles", "AccountStatement.css");
                string outPutPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", @$"{fileName}.pdf");
                await _fileService.CheckFileExist(outPutPath);
                AddPDFResponse pDFResponse = new AddPDFResponse()
                {
                    Html = GetHTMLString(dbResult.Result.Details),
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
        private string GetHTMLString(List<GetAccountTransactionResponse> records)
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
            foreach (var item in results)
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
